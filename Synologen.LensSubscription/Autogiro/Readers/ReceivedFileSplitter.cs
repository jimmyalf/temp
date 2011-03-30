using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.Autogiro.Readers
{
    public class ReceivedFileSplitter : IFileSplitter
    {
        public DateTime GetDateFromName(string name)
        {
            var dateAndTime = GetDateAndTimeStringFromName(name);
            var dateString = dateAndTime[0];
            var timeString = dateAndTime[1];

            var s = string.Concat(dateString, timeString);

            return DateTime.ParseExact(s, "yyMMddHHmmss", CultureInfo.InvariantCulture);
        }

		private static string[] GetDateAndTimeStringFromName(string fileName)
		{
        	var regexPattern = String.Concat(@"BFEP\..+\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$");
        	var match = Regex.Match(fileName, regexPattern);
			if(!match.Success)
			{
				throw new ArgumentException("Could not recognize file name", "fileName");
			}
        	var dateString = match.Groups["datePart"].Captures[0].Value;
			var timeString = match.Groups["timePart"].Captures[0].Value;
			return new[] { dateString, timeString };
		}

        public bool FileNameOk(string name, string customerNumber, string productCode)
        {
        	var regexPattern = String.Concat(@"BFEP\.", productCode, @"\.K0", customerNumber, @"\.D(?<datePart>\d{6})\.T(?<timePart>\d{6})$");
        	var match = Regex.Match(name, regexPattern);
			if(!match.Success) return false;

        	var dateString = match.Groups["datePart"].Captures[0].Value;
			var timeString = match.Groups["timePart"].Captures[0].Value;

        	return ValidateDateTimeFormat(dateString, timeString);
        }

        public IEnumerable<FileSection> GetSections(string[] file)
        {
            var expectingOpeningPost = true;
            var type = SectionType.ReceivedPayments;
            var fileSections = new List<FileSection>();

            if (file == null || file.Length == 0)
                throw new AutogiroFileSplitException("File was empty");

            if (!file[0].StartsWith(FileConstants.OpeningPostType))
                throw new AutogiroFileSplitException("Did not find expected opening post type TK01");
            
            if (!file[file.Length - 1].StartsWith(FileConstants.EndingSumPostType))
                throw new AutogiroFileSplitException("File did not end with expected post type TK09");

            var stringBuilder = new StringBuilder();

            foreach (var line in file)
            {
                stringBuilder.AppendLine(line);

                if (expectingOpeningPost)
                {
                    type = GetSectionType(line);
                    expectingOpeningPost = false;
                }
                else
                {
                    if (line.StartsWith(FileConstants.EndingSumPostType))
                    {
                        var fileSection = new FileSection
                        {
                        	SectionType = type, 
							Posts = stringBuilder.ToString().TrimEnd("\r\n".ToCharArray())
                        };
                        fileSections.Add(fileSection);
                        expectingOpeningPost = true;
                        stringBuilder = new StringBuilder();
                    }
                }
            }
            return fileSections;
        }
        
        private static SectionType GetSectionType(string line)
        {
            var errorOrPayment = line.Substring(22, 39);
            var consent = line.Substring(24, 37);

            if (consent.Trim() == FileConstants.ConsentOpeningText) return SectionType.ReceivedConsents;
            if (errorOrPayment.Trim() == FileConstants.ErrorOpeningText) return SectionType.ReceivedErrors;
            if (errorOrPayment.Trim() == String.Empty) return SectionType.ReceivedPayments;

            throw new AutogiroFileSplitException(string.Format("Could not determine file section type: {0}", line));
        }

		private static bool ValidateDateTimeFormat(string dateFormat, string timeFormat)
		{
			DateTime dummy;
			return DateTime.TryParseExact(string.Concat(dateFormat, timeFormat), "yyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dummy);
		}
    }
}
