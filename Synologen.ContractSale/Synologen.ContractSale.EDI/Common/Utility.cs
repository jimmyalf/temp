using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;

namespace Spinit.Wpc.Synologen.EDI.Common {
	public static class EDIUtility {
		private const string NewLine = "\r\n";
		private const string CharactersToEscape = ":+?";
		private const char EscapeCharacter = '?';

		public static float ConvertToPriceIncludingVAT(List<InvoiceRow> invoiceRows, float amountOfVAT) {
			float sum = 0;
			foreach(var article in invoiceRows) {
				if (article.NoVAT){
					sum += article.TotalRowPriceExcludingVAT;
				}
				else {
					sum += article.TotalRowPriceExcludingVAT*(1 + amountOfVAT);
				}
			}
			return sum;
		}

		//public static float ConvertToPriceExcludingVAT(float priceIncludingVAT, float amountOfVAT) {
		//    return priceIncludingVAT/(1 + amountOfVAT);
		//}

		public static float GetAmountOfVAT(List<InvoiceRow> invoiceRows, float amountOfVAT) {
			var priceIncluingVAT = ConvertToPriceIncludingVAT(invoiceRows, amountOfVAT);
			var priceExcludingVAT = GetTotalArticleSumExcludingVAT(invoiceRows);
			return priceIncluingVAT - priceExcludingVAT;
		}

		public static float GetTotalArticleSumExcludingVAT(List<InvoiceRow> articles) {
			float sumExcludingVAT = 0;
			foreach(var article in articles) {
				sumExcludingVAT += article.TotalRowPriceExcludingVAT;
			}
			return sumExcludingVAT;
		}

		public static string Escape(string value) {
			if (String.IsNullOrEmpty(value)) return value;
			foreach (var character in CharactersToEscape) {
				value = value.Replace(character.ToString(), String.Format("{0}{1}",EscapeCharacter,character));
			}
			return value;
		}

		public static string ParseRow(string format, int parseItem) {
			return parseItem <= 0 ? string.Empty : string.Format(format, parseItem);
		}
		public static string ParseRow(string format, float parseItem) {
			return parseItem <= 0 ? string.Empty : string.Format(format, parseItem);
		}
		public static string ParseRow(string format, string parseItem) {
			var escapedParseItem = Escape(parseItem);
			return String.IsNullOrEmpty(parseItem) ? string.Empty : string.Format(format, escapedParseItem);
		}
		public static string ParseRow(string format, DateTime parseItem, string EDIDateFormat) {
			if(parseItem <= DateTime.MinValue) throw new ArgumentOutOfRangeException("parseItem");
			return string.Format(format, parseItem.ToString(EDIDateFormat));
		}
		public static string ParseRow(string format, Address parseItem) {
			if(parseItem == null) return String.Empty;
			if(parseItem.IsEmpty) return String.Empty;
			return string.Format(format, 
				Escape(parseItem.Address1),
				Escape(parseItem.Address2), 
				Escape(parseItem.Zip), 
				Escape(parseItem.City)
				);
		}
		public static string ParseRow(ContactFormat format, Contact parseItem) {
			if(parseItem.IsEmpty) return String.Empty;
			var block = String.Empty;
			if (!String.IsNullOrEmpty(parseItem.ContactInfo)) {
				block = AddRowToBlock(block, ParseRow(format.ContactInfo, Escape(parseItem.ContactInfo)));
			}
			if(!String.IsNullOrEmpty(parseItem.Telephone)) {
				block = AddRowToBlock(block, ParseRow(format.Telephone, Escape(parseItem.Telephone)));
			}
			if(!String.IsNullOrEmpty(parseItem.Fax)) {
				block = AddRowToBlock(block, ParseRow(format.Fax, Escape(parseItem.Fax)));
			}
			if(!String.IsNullOrEmpty(parseItem.Email)) {
				block = AddRowToBlock(block, ParseRow(format.Email, Escape(parseItem.Email)));
			}
			return block;
		}
		public static string ParseRow(string format, InterchangeHeader parseItem, string dateTimeFormat) {
			if(parseItem == null) return String.Empty;
			if(parseItem.IsEmpty) return String.Empty;
			var dateTimeValue = parseItem.DateOfPreparation.ToString(dateTimeFormat);
			return string.Format(format, 
				Escape(parseItem.SenderId), 
				Escape(parseItem.RecipientId), 
				dateTimeValue, 
				Escape(parseItem.ControlReference)
			);
		}
		public static string ParseTrailerRow(string format, int numberOfSegmentsInMessage, string messageReference) {
			return string.Format(format,numberOfSegmentsInMessage,messageReference);
		}

		public static string AddRowToBlock(string blockOfRows, string newRow) {
			if(String.IsNullOrEmpty(newRow))return blockOfRows;
			if(String.IsNullOrEmpty(blockOfRows)) return newRow;
			return blockOfRows + NewLine + newRow;

		}

		public static int GetNewRandomInt(int minValue, int maxValue) {
			var random = GetRandomSeededRandom();
			return random.Next(minValue, maxValue);
		}

		public static int GetNewRandomInt(int maxValue) {
			return GetNewRandomInt(0, maxValue);
		}

		public static Random GetRandomSeededRandom() {
			var randomBytes = new byte[4];

			// Generate 4 random bytes.
			var rng = new RNGCryptoServiceProvider();
			rng.GetBytes(randomBytes);

			// Convert 4 bytes into a 32-bit integer value.
			var seed = (randomBytes[0] & 0x7f) << 24 |
						randomBytes[1] << 16 |
						randomBytes[2] << 8 |
						randomBytes[3];

			// Now, this is real randomization.
			return new Random(seed);
		}

		public static string GetNewRandomAlphaNumericString(int length, bool useOnlyCapitals) {
			var guidString = Guid.NewGuid().ToString("N");
			if(useOnlyCapitals) {
				guidString = guidString.ToUpper();
			}
			if(guidString.Length > length) return guidString.Substring(0, length-1);
			throw new ArgumentOutOfRangeException("length");
		}

		public static float TryRound(float value, int numberOfDecimalsInRoundedValues) {
			return (float)Math.Round(value, numberOfDecimalsInRoundedValues);
		}


	}
}
