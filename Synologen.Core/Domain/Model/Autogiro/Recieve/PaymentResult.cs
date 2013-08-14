namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve
{
	public enum PaymentResult
	{
		Approved = 0,
		InsufficientFunds = 1,
		AGConnectionMissing = 2,
		WillTryAgain = 9
	}
}