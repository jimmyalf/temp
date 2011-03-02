using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.Autogiro.Readers
{
    public class ReceivedFileSplitter
    {
        public static IEnumerable<FileSection> GetSections(string[] file)
        {
            bool expectingOpeningPost = true;
            SectionType type = SectionType.ReceivedPayments;
            var fileSections = new List<FileSection>();

            if (file == null || file.Length == 0)
                throw new FormatException("File was empty");

            if (!file[0].StartsWith(FileConstants.OpeningPostType))
                throw new FormatException("Did not find expected opening post type TK01");
            
            if (!file[file.Length - 1].StartsWith(FileConstants.EndingSumPostType))
                throw new FormatException("File did not end with expected post type TK09");

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
                        var fileSection = new FileSection { SectionType = type, Posts = stringBuilder.ToString() };
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
            string errorOrPayment = line.Substring(23, 39);
            string consent = line.Substring(25, 37);

            if (consent.Trim() == FileConstants.ConsentOpeningText)
                return SectionType.ReceivedConsents;

            if (errorOrPayment.Trim() == FileConstants.ErrorOpeningText)
                return SectionType.ReceivedErrors;
             
            if (errorOrPayment.Trim() == String.Empty)
                return SectionType.ReceivedPayments;
            
            throw new FormatException(string.Format("Could not determine file section type: {0}", line));
        }
    }
}
