using System;
using System.ServiceModel;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using StoryQ;
using Synologen.Service.Web.External.AcceptanceTest.Domain;
using AuthenticationContext = Synologen.Service.Web.External.AcceptanceTest.Domain.AuthenticationContext;
using Customer = Synologen.Service.Web.External.AcceptanceTest.Domain.Customer;

namespace Synologen.Service.Web.External.AcceptanceTest
{
	[TestFixture]
	public class Add_Customer : SpecTestbase
	{
		private AddCustomerClient _client;
		private Customer _customer;
		private AuthenticationContext _authenticationContext;
		private Exception _caughtException;
		private string _shopUsername;
		private string _shopPassword;
		private Shop _shop;

		public Add_Customer()
		{
			Context = () =>
			{
				_client = new AddCustomerClient();
				_shopUsername = "test-butik";
				_shopPassword = "test-butik-lösenord";
				_shop = CreateShopWithExternalAccess(_shopUsername, _shopPassword);
			};
			Story = () => new Story("Add Customer")
				.InOrderTo("decrease work for shops with adding customers in two systems")
			    .AsA("external client")
			    .IWant("add a customer");
		}

		[Test]
		public void AddCustomer()
		{
			SetupScenario(scenario => scenario
				.Given(ValidAuthentication)
					.And(ValidCustomer)
				.When(ACustomerIsAdded)
				.Then(ACustomerShouldBeAdded)
					.And(NoExceptionShouldBeThrown));
		}

		[Test]
		public void AddCustomerWithInvalidAuthentication()
		{
			SetupScenario(scenario => scenario
				.Given(InvalidAuthentication)
					.And(ValidCustomer)
				.When(ACustomerIsAdded)
				.Then(AnAuthenticationExceptionShouldBeThrown));
		}


		[Test]
		public void AddCustomerWithInvalidCustomerData()
		{
			SetupScenario(scenario => scenario
				.Given(ValidAuthentication)
					.And(InvalidCustomer)
				.When(ACustomerIsAdded)
				.Then(AValidationExceptionShouldBeThrown));
		}

		#region Arrange
		private void ValidCustomer()
		{
			_customer = new Customer
			{
				FirstName = "Adam", 
				LastName = "Bertil",
				Address1 = "Storgatan 2",
				City = "Storstad",
				PostalCode = "1234",
				PersonalNumber = "197001011234"
			};
		}


		private void ValidAuthentication()
		{
			_authenticationContext = new AuthenticationContext {UserName = _shopUsername, Password = _shopPassword};
		}

		private void InvalidAuthentication()
		{
			_authenticationContext = new AuthenticationContext {UserName = _shopUsername, Password = "InvalidPassword"};
		}
		private void InvalidCustomer()
		{
			_customer = new Customer();
		}
		#endregion

		#region Act
		private void ACustomerIsAdded()
		{
			WithClient(client =>
			{
				_caughtException = TryCatchException(() => client.AddCustomer(_authenticationContext, _customer));
			});
		}
		#endregion

		#region Assert
		private void ACustomerShouldBeAdded()
		{
			var customer = DB.SynologenOrderCustomer.FindByShopId(_shop.ShopId);
			Assert.NotNull(customer);
			Assert.AreEqual(customer.AddressLineOne, _customer.Address1);
			Assert.AreEqual(customer.AddressLineTwo, _customer.Address2);
			Assert.AreEqual(customer.Email, _customer.Email);
			Assert.AreEqual(customer.MobilePhone, _customer.MobilePhone);
			Assert.AreEqual(customer.Phone, _customer.Phone);
			Assert.AreEqual(customer.FirstName, _customer.FirstName);
			Assert.AreEqual(customer.LastName, _customer.LastName);
			Assert.AreEqual(customer.Notes, null);
			Assert.AreEqual(customer.PersonalIdNumber, _customer.PersonalNumber);
		}


		private void AnAuthenticationExceptionShouldBeThrown()
		{
			_caughtException.ShouldBeTypeOf<FaultException>();
			_caughtException.Message.ShouldContain("authenticated");
		}
		private void AValidationExceptionShouldBeThrown()
		{
			_caughtException.ShouldBeTypeOf<FaultException>();
			_caughtException.Message.ShouldContain("Firstname");
			_caughtException.Message.ShouldContain("LastName");
			_caughtException.Message.ShouldContain("PersonalNumber");
			_caughtException.Message.ShouldContain("Address1");
			_caughtException.Message.ShouldContain("PostalCode");
			_caughtException.Message.ShouldContain("City");
		}

		private void NoExceptionShouldBeThrown()
		{
			_caughtException.ShouldBe(null);
		}
		#endregion

		private Exception TryCatchException(Action action)
		{
			try { action(); }
			catch(Exception ex) { return ex; }
			return null;
		}

		private void WithClient(Action<AddCustomerClient> action)
		{
			if(_client.State == CommunicationState.Created) _client.Open();
			action(_client);
			if(_client.State == CommunicationState.Opened || _client.State == CommunicationState.Created)
			{
				_client.Close();
			}
		}
	}

}