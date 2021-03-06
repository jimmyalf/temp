using System;
using System.Text.RegularExpressions;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Synologen.LensSubscription.Autogiro.Helpers;

namespace Synologen.LensSubscription.Autogiro.Readers
{
	public class ConsentItemReader : IItemReader<Consent> 
	{

		public const string DateRegexPattern = "^(19|20)[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])$";
		public Consent Read(string line)
		{
			return new Consent
			{
				RecieverBankgiroNumber = line.ReadFrom(3).To(12).TrimStart('0'),
				ActionDate = line.ReadFrom(66).To(73).ParseDate(),
				InformationCode = ParseConsentInformationCode(line.ReadFrom(62).To(63).ToInt()),
				Transmitter = new Payer{ CustomerNumber = line.ReadFrom(13).To(28).TrimStart('0') },
				Account = new Account
				{
					ClearingNumber = (UseAccountNumberData(line)) ? line.ReadFrom(29).To(32) : null,
					AccountNumber = (UseAccountNumberData(line)) ? line.ReadFrom(33).To(44).TrimStart('0') : null,
				},
				PersonalIdNumber = (UsePersonalData(line)) ? GetAsPersonalIdNumber(line.ReadFrom(45).To(56)) : null,
				OrgNumber = (UsePersonalData(line)) ? GetAsOrgNumber(line.ReadFrom(45).To(56)) : null,
				CommentCode = line.ReadFrom(64).To(65).ToInt().ToEnum<ConsentCommentCode>(),
				ConsentValidForDate = ReadConsentValidationDate(line)
			};
		}

		private static bool UseAccountNumberData(string line) 
		{
			var flag = line.ReadFrom(62).To(63);
			return HasMatchInList(flag, "04", "42", "43", "46");
		}

		private static bool UsePersonalData(string line) 
		{
			var flag = line.ReadFrom(62).To(63);
			return HasMatchInList(flag, "04", "42", "43", "46", "93");
		}

		private static bool HasMatchInList(string flagToMatch, params string[] listOfFlags)
		{
			foreach (var flagInList in listOfFlags)
			{
				if(flagInList.Equals(flagToMatch)) return true;
			}
			return false;
		}

		private static ConsentInformationCode ParseConsentInformationCode(int code)
		{
			switch (code)
			{
				case 03: return ConsentInformationCode.InitiatedByPaymentRecipient;
				case 04: return ConsentInformationCode.InitiatedByPaymentRecipient;

				case 46: return ConsentInformationCode.InitiatedByPayer;
				case 93: return ConsentInformationCode.InitiatedByPayer;

				case 42: return ConsentInformationCode.AnswerToNewAccountApplication;

				case 43: return ConsentInformationCode.InitiatedByPayersBank;

				case 10: return ConsentInformationCode.PaymentRecieversBankGiroAccountClosed;

				default: throw new ArgumentOutOfRangeException("code", "Could not parse value \"{0}\" as ConsentInformationCode", code.ToString());
			}
		}

		private static string GetAsOrgNumber(string value)
		{
			return IsOrgNumber(value) ? value.ReadFrom(3).ToEnd() : null;
		}

		private static string GetAsPersonalIdNumber(string value)
		{
			return IsOrgNumber(value) ? null : value;
		}

		private static bool IsOrgNumber(string value)
		{
			return value.ReadFrom(1).To(2).Equals("00");
		}

		private static DateTime? ReadConsentValidationDate(string line)
		{
			var date = String.Concat(line.ReadFrom(66).To(67), line.ReadFrom(74).To(79));
			if(!Regex.IsMatch(date, DateRegexPattern)) return null;
			//if(line.ReadFrom(74).To(79).Equals("000000")) return null;
			//if(line.ReadFrom(74).To(79).Equals("      ")) return null;
			return String.Concat(line.ReadFrom(66).To(67), line.ReadFrom(74).To(79)).ParseDate();

		}
	}
}