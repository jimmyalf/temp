using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGWebService.Test.TestHelpers;
using BGWebService_AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService.AutogiroServiceType;
using BGServer_AutogiroServiceType=Spinit.Wpc.Synologen.Core.Domain.Model.BGServer.AutogiroServiceType;

namespace Synologen.LensSubscription.BGWebService.Test
{
	[TestFixture, Category("BGWebServiceTests")]
	public class When_testing_BGWebService_connection : BGWebServiceTestBase
	{
		private bool returnedValue;

		public When_testing_BGWebService_connection()
		{
			Context = () => { };
			Because = service => 
			{
				returnedValue = service.TestConnection();
			};
		}

		[Test]
		public void Returned_value_is_true()
		{
			returnedValue.ShouldBe(true);
		}
	}

	[TestFixture, Category("BGWebServiceTests")]
	public class When_registering_a_new_payer : BGWebServiceTestBase
	{
		private string payerName;
		private BGWebService_AutogiroServiceType serviceType;
		private BGServer_AutogiroServiceType expectedServiceType;
		private int returnedPayerNumber;
		private int payerId;

		public When_registering_a_new_payer()
		{
			Context = () =>
			{
				payerName = "Adam Bertil";
				payerId = 55;
				serviceType = BGWebService_AutogiroServiceType.LensSubscription;
				expectedServiceType = BGServer_AutogiroServiceType.LensSubscription;
				A.CallTo(() => AutogiroPayerRepository.Save(A<AutogiroPayer>.Ignored)).Returns(payerId);
			};
			Because = service =>
			{
				returnedPayerNumber = service.RegisterPayer(payerName, serviceType);
			};
		}

		[Test]
		public void Webservice_stores_new_payer()
		{
			A.CallTo(() => AutogiroPayerRepository.Save(
				A<AutogiroPayer>.That.Matches(x => x.Name.Equals("Adam Bertil"))
				.And.Matches(x => x.ServiceType.Equals(expectedServiceType))
			)).MustHaveHappened();
		}

		[Test]
		public void Returned_number_is_new_payer_id()
		{
			returnedPayerNumber.ShouldBe(payerId);
		}
	}
}