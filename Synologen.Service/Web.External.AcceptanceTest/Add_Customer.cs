using System.Dynamic;
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
		private string _shopUsername;
		private string _shopPassword;
		private Shop _shop;
		private AddEntityResponse _response;

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
				.Then(ACustomerWasPersisted)
					.And(ResponseShowsCustomerWasAdded));
		}

		[Test]
		public void AddExistingCustomer()
		{
			SetupScenario(scenario => scenario
				.Given(ValidAuthentication)
					.And(ValidExistingCustomer)
				.When(ACustomerIsAdded)
				.Then(ResponseShowsCustomerAlreadyExists));
		}

		[Test]
		public void AddCustomerWithInvalidAuthentication()
		{
			SetupScenario(scenario => scenario
				.Given(InvalidAuthentication)
					.And(ValidCustomer)
				.When(ACustomerIsAdded)
				.Then(ResponseShowsAuthenticationFailed));
		}

		[Test]
		public void AddCustomerWithCustomerDataMissing()
		{
			SetupScenario(scenario => scenario
				.Given(ValidAuthentication)
					.And(CustomerWithFieldsMissing)
				.When(ACustomerIsAdded)
				.Then(ResponseShowsValidationFailed)
					.And(ResponseContainsValidationErrorsForMissingFields));
		}

		[Test]
		public void AddCustomerWithInvalidCustomerData()
		{
			SetupScenario(scenario => scenario
				.Given(ValidAuthentication)
					.And(CustomerWithInvalidPersonalNumber)
				.When(ACustomerIsAdded)
				.Then(ResponseShowsValidationFailed)
					.And(ResponseContainsValidationErrorsForInvalidPersonalNumber));
		}

		#region Arrange
		private void ValidCustomer()
		{
			_customer = GetCustomer();
		}
		private void ValidExistingCustomer()
		{
			ValidCustomer();
			var customer = GetCustomerToPersist(_shop.ShopId, _customer.PersonalNumber);
			DB.SynologenOrderCustomer.Insert(customer);
		}

		private void ValidAuthentication()
		{
			_authenticationContext = new AuthenticationContext {UserName = _shopUsername, Password = _shopPassword};
		}
		private void InvalidAuthentication()
		{
			_authenticationContext = new AuthenticationContext {UserName = _shopUsername, Password = "InvalidPassword"};
		}
		private void CustomerWithFieldsMissing()
		{
			_customer = new Customer();
		}
		private void CustomerWithInvalidPersonalNumber()
		{
			_customer = new Customer
			{
				FirstName = "Adam", 
				LastName = "Bertil",
				Address1 = "Storgatan 2",
				City = "Storstad",
				PostalCode = "1234",
				PersonalNumber = "7001011234"
			};
		}
		#endregion

		#region Act
		private void ACustomerIsAdded()
		{
			_response = _client.AddCustomer(_authenticationContext, _customer);
		}
		#endregion

		#region Assert
		private void ACustomerWasPersisted()
		{
			var customer = DB.SynologenOrderCustomer.FindByShopId(_shop.ShopId);
			Assert.NotNull(customer);
			Assert.AreEqual(_customer.Address1, customer.AddressLineOne);
			Assert.AreEqual(_customer.Address2, customer.AddressLineTwo);
			Assert.AreEqual(_customer.City, customer.City);
			Assert.AreEqual(_customer.PostalCode, customer.PostalCode);
			Assert.AreEqual(_customer.Email, customer.Email);
			Assert.AreEqual(_customer.MobilePhone, customer.MobilePhone);
			Assert.AreEqual(_customer.Phone, customer.Phone);
			Assert.AreEqual(_customer.FirstName, customer.FirstName);
			Assert.AreEqual(_customer.LastName, customer.LastName);
			Assert.AreEqual(null, customer.Notes);
			Assert.AreEqual(_customer.PersonalNumber, customer.PersonalIdNumber);
			Assert.AreEqual( _customer.Email, customer.Email);
		}

		private void ResponseShowsAuthenticationFailed()
		{
			_response.Type.ShouldBe(AddEntityResponseType.AuthenticationFailed);
		}
		private void ResponseShowsValidationFailed()
		{
			_response.Type.ShouldBe(AddEntityResponseType.ValidationFailed);
		}

		private void ResponseShowsCustomerWasAdded()
		{
			_response.Type.ShouldBe(AddEntityResponseType.EntityWasAdded);
		}

		private void ResponseContainsValidationErrorsForMissingFields()
		{
			_response.ValidationErrors.ShouldContain(x => x.ErrorMessage.Contains("FirstName"));
			_response.ValidationErrors.ShouldContain(x => x.ErrorMessage.Contains("LastName"));
			_response.ValidationErrors.ShouldContain(x => x.ErrorMessage.Contains("PersonalNumber"));
			_response.ValidationErrors.ShouldContain(x => x.ErrorMessage.Contains("Address1"));
			_response.ValidationErrors.ShouldContain(x => x.ErrorMessage.Contains("PostalCode"));
			_response.ValidationErrors.ShouldContain(x => x.ErrorMessage.Contains("City"));
		}

		private void ResponseContainsValidationErrorsForInvalidPersonalNumber()
		{
			_response.ValidationErrors.ShouldContain(x => x.ErrorMessage.Contains("PersonalNumber") && x.ErrorMessage.Contains("format"));
		}

		private void ResponseShowsCustomerAlreadyExists()
		{
			_response.Type.ShouldBe(AddEntityResponseType.EntityAlreadyExists);
		}

		#endregion

		private dynamic GetCustomerToPersist(int shopId, string personalIdNumber = "197001011234")
		{
			dynamic dynamicCustomer = new ExpandoObject();
			dynamicCustomer.ShopId = shopId;
			dynamicCustomer.PersonalIdNumber = personalIdNumber;
			dynamicCustomer.FirstName = "Adam";
			dynamicCustomer.LastName = "Bertil";
			dynamicCustomer.AddressLineOne = "Storgatan 2";
			dynamicCustomer.AddressLineTwo = "Box 123";
			dynamicCustomer.City = "Storstad";
			dynamicCustomer.PostalCode = "1234";
			dynamicCustomer.Email = "a.b@test.se";
			dynamicCustomer.MobilePhone = "+46 708-12 34 56";
			dynamicCustomer.Phone = "031-123456";
			return dynamicCustomer;
		}

		private Customer GetCustomer()
		{
			return new Customer
			{
				FirstName = "Adam", 
				LastName = "Bertil",
				Address1 = "Storgatan 2",
				City = "Storstad",
				PostalCode = "1234",
				PersonalNumber = "197001011234",
				Address2 = "Box 123",
				Email = "a.b@test.se",
				MobilePhone = "+46 708-12 34 56",
				Phone = "031-123456"
			};
		}

	}
}