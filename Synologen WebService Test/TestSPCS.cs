using NUnit.Framework;
using Spinit.Wpc.Synologen.Test.Mock;
using Spinit.Wpc.Synologen.Visma;

namespace Spinit.Wpc.Synologen.Test {
	[TestFixture]
	public class TestSPCS {
		private string vismaCommonFilesPath;
		private string vismaCompanyName;
		private AdkHandler adkHandler;
		private MockAdkHandler mockAdkHandler;


		[TestFixtureSetUp]
		public void Setup() {
			vismaCommonFilesPath = @"\\moccasin\SPCS_Administration\Gemensamma filer";
			vismaCompanyName = @"\\moccasin\SPCS_Administration\Företag\Ovnbol";
			adkHandler = new AdkHandler(vismaCommonFilesPath, vismaCompanyName);
			adkHandler.OpenCompany();
			mockAdkHandler = new MockAdkHandler();
		}

		[TestFixtureTearDown]
		public void TearDown() {
			adkHandler.CloseCompany();
		}
		[Test]
		public void TestFetchPaymentInformation() {
			var paymentInfo = adkHandler.GetInvoicePaymentInfo(200001);
			Assert.IsNotNull(paymentInfo);
		}

		[Test]
		public void TestMockPaymentInformation() {
			var paymentInfo = mockAdkHandler.GetInvoicePaymentInfo(0);
			Assert.IsNotNull(paymentInfo);
		}

	}
}