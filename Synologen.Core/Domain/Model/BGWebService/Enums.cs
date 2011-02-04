namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public enum ErrorType
	{
		ConsentMissing = 01,
		AccountNotYetApproved = 02,
		ConsentStopped = 03,
		NotYetDebitable = 07
	}

	public enum PaymentType
	{
		Debit = 82,
		Credit = 32
	}

	public enum PaymentResult
	{
		Approved = 0,
		InsufficientFunds = 1,
		AGConnectionMissing = 2,
		WillTryAgain = 9
	}

	public enum ConsentInformationCode
	{
		InitiatedByPaymentRecipient,
		InitiatedByPayer,
		AnswerToNewAccountApplication,
		InitiatedByPayersBank,
		PaymentRecieversBankGiroAccountClosed
	}

	public enum ConsentCommentCode
	{
		ConsentTurnedDownByBank = 01,
		ConsentTurnedDownByPayer = 02,
		AccountTypeNotApproved = 03,
		ConsentMissingInBankgiroConsentRegister = 04,
		IncorrectAccountOrPersonalIdNumber = 05,
		ConsentCanceledByBankgiro = 06,
		ConsentCanceledByBankgiroBecauseOfMissingStatement = 07,
		ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsideration = 10,
		ConsentTemporarilyStoppedByPayer = 11,
		TemporaryConsentStopRevoked = 12,
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