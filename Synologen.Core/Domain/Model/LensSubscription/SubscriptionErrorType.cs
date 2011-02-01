using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public enum SubscriptionErrorType
	{

		[EnumDisplayName("Täckning saknades")]
		NoCoverage = 1,

		[EnumDisplayName("AG-koppling saknas (kontot avslutat eller medgivandet makulerat)")]
		NoAccount = 2,

		[EnumDisplayName("Utgår, medgivande saknas)")]
		ConsentMissing = 3,

		[EnumDisplayName("Utgår, konto ej godkänt")]
		NotApproved = 4,

		[EnumDisplayName("Utgår, medgivande stoppat")]
		CosentStopped = 5,

		[EnumDisplayName("Avvisad, ännu ej debiterbar")]
		NotDebitable = 6,

		[EnumDisplayName("Autogiro-medgivande avvisad, nekad av bank")]
		ConsentTurnedDownByBank = 7,

		[EnumDisplayName("Autogiro-medgivande avvisad, nekad av betalare")]
		ConsentTurnedDownByPayer = 8,

		[EnumDisplayName("Autogiro-medgivande avvisad, kontotypen är inte godkänd")]
		AccountTypeNotApproved = 9,

		[EnumDisplayName("Autogiro-medgivande avvisad, medgivande saknas i BGC medgivande-register")]
		ConsentMissingInBankgiroConsentRegister = 10,

		[EnumDisplayName("Autogiro-medgivande avvisad, ogiltigt konto- eller personnummer")]
		IncorrectAccountOrPersonalIdNumber = 11,

		[EnumDisplayName("Autogiro-medgivande avvisad, nekad av BGC")]
		ConsentCanceledByBankgiro = 12,

		[EnumDisplayName("Autogiro-medgivande avvisad, nekad av BGC pga saknad information")]
		ConsentCanceledByBankgiroBecauseOfMissingStatement = 13,

		[EnumDisplayName("Nytt autogiro-medgivande avvisad, det finns redan ett medgivande under behandling")]
		ConsentIsAlreadyInBankgiroConsentRegisterOrUnderConsederation = 14,

		[EnumDisplayName("Autogiro-medgivande avvisad, stoppad av betalare")]
		ConsentTemporarilyStoppedByPayer = 15,

		[EnumDisplayName("Autogiro-medgivande avvisad, tillfälligt stopp av medgivande anullerades")]
		TemporaryConsentStopRevoked = 16,

		[EnumDisplayName("Autogiro-medgivande avvisad, ogiltigt personnummer")]
		IncorrectPersonalIdNumber = 17,

		[EnumDisplayName("Autogiro-medgivande avvisad, ogilitigt betalarnummer")]
		IncorrectPayerNumber = 18,

		[EnumDisplayName("Autogiro-medgivande avvisad, ogiltigt kontonummer")]
		IncorrectAccountNumber = 19,

		[EnumDisplayName("Autogiro-medgivande avvisad, högsta belopp är inte tillåtet")]
		MaxAmountNotAllowed = 20,

		[EnumDisplayName("Autogiro-medgivande avvisad, ogiltigt mottagar-kontonummer")]
		IncorrectPaymentReceiverBankgiroNumber = 21,

		[EnumDisplayName("Autogiro-medgivande avvisad, mottagar-bankgironummer saknas")]
		PaymentReceiverBankgiroNumberMissing = 22,

		[EnumDisplayName("Begäran om autogiro-medgivande cancellerades")]
		Canceled = 23,

		[EnumDisplayName("Okänt fel")]
		Unknown = 24,

	}
}
