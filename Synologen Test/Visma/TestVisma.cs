using NUnit.Framework;
using Spinit.Wpc.Synologen.Test.Mock;
using Spinit.Wpc.Synologen.Visma;

namespace Spinit.Wpc.Synologen.Test.Visma {
	[TestFixture]
	public class TestVisma {
		private string vismaCommonFilesPath;
		private string vismaCompanyName;
		private AdkHandler adkHandler;
		private MockAdkHandler mockAdkHandler;


		[Ignore]
		public void Setup() {
			vismaCommonFilesPath = @"\\moccasin\SPCS_Administration\Gemensamma filer";
			vismaCompanyName = @"\\moccasin\SPCS_Administration\Företag\Ovnbol";
			adkHandler = new AdkHandler(vismaCommonFilesPath, vismaCompanyName);
			adkHandler.OpenCompany();
			mockAdkHandler = new MockAdkHandler();
		}

		[Ignore]
		public void TearDown() {
			adkHandler.CloseCompany();
		}
		[Ignore]
		public void TestFetchPaymentInformation() {
			var paymentInfo = adkHandler.GetInvoicePaymentInfo(200001);
			Assert.IsNotNull(paymentInfo);
		}

		[Ignore]
		public void TestMockPaymentInformation() {
			var paymentInfo = mockAdkHandler.GetInvoicePaymentInfo(0);
			Assert.IsNotNull(paymentInfo);
		}

	}
}