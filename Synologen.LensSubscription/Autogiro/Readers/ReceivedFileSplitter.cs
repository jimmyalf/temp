﻿using System;
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
        private const char DelimiterChar = '.';

        public DateTime GetDateFromName(string name)
        {
            var dateAndTime = GetDateAndTimeStringFromName(name);
            var dateString = dateAndTime[0];
            var timeString = dateAndTime[1];

            var s = string.Concat(dateString, timeString);

            return DateTime.ParseExact(s, "yyMMddHHmmss", CultureInfo.InvariantCulture);
        }

        private static string[] GetDateAndTimeStringFromName(string name)
        {
            var splittedName = name.Split(DelimiterChar);

            var dateString = splittedName[2];
            var timeString = splittedName[3];

            dateString = dateString.Substring(1, dateString.Length - 1);
            timeString = timeString.Substring(1, timeString.Length - 1);

            return new [] { dateString, timeString };
        }

        public bool FileNameOk(string name, string customerNumber, string productCode)
        {
            var splittedName = name.Split(DelimiterChar);

            if (splittedName.Length != 4)
                return false;

            if (splittedName[0] != productCode)
                return false;

            if (!(splittedName[1].StartsWith("K0") &&
                    (splittedName[1].Length == 8) &&
                    (splittedName[1].Substring(2, 6) == customerNumber)
                ))
                return false;

            if (!(splittedName[2].StartsWith("D") &&
                    (splittedName[2].Length == 7) &&
                    (IsNumeric(splittedName[2].Substring(1, 6)))
                ))
                return false;

            if (!(splittedName[3].StartsWith("T") &&
                    (splittedName[3].Length == 7) &&
                    (IsNumeric(splittedName[3].Substring(1, 6)))
                ))
                return false;

            var dateAndTime = GetDateAndTimeStringFromName(name);
            var dateString = dateAndTime[0];
            var timeString = dateAndTime[1];

            DateTime dummy;
            return DateTime.TryParseExact(string.Concat(dateString, timeString), "yyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dummy);
        }

        private static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, "^\\d+$");
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
    }
}
