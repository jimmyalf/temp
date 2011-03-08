using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;

namespace Synologen.LensSubscription.BGWebService.Test.Factories
{
	public static class ConsentFactory 
	{
		public static ConsentToSend GetConsentToSend(int payerNumber) 
		{ 
			return new ConsentToSend
			{
				BankAccountNumber = "132456",
				ClearingNumber = "9876",
				PayerNumber = payerNumber,
				PersonalIdNumber = "198512243364",
			}; 
		}
	}
}