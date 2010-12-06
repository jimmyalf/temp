namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve
{
	public enum ConsentCommentCode
	{
		ConsentTurnedDownByBank = 01,
		ConsentTurnedDownByPayer = 02,
		AccountTypeNotApproved = 03,
		ConsentMissingInBankgiroConsentRegister = 04,
		IncorrectAccountOrPersonalIdNumber = 05,
		ConsentCanceledByBankgiro = 06,
		ConsentCanceledByBankgiroBecauseOfMissingStatement = 07,
		ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation = 10,
		ConsentTemporarilyStoppedByPayer = 11,
		TemporaryCocentStopRevoked = 12,
		IncorrectPersonalIdNumber = 20,
		IncorrectPayerNumber = 21,
		IncorrectAccountNumber = 23,
		MaxAmountNotAllowed = 24,
		IncorrectPaymentReceiverBankgiroNumber = 29,
		PaymentReceiverBankgiroNumberMissing = 30,
		NewConsent = 32,
		Canceled = 33
	}
}