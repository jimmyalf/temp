using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
    public class BGReceivedFileReaderService : IFileReaderService
    {
        private readonly IFileIOService _fileIoService;
        private readonly string _downloadFolderPath;
        private readonly string _customerNumber;
        private readonly string _productCode;
        private const char DelimiterChar = '.';

        public BGReceivedFileReaderService(IFileIOService fileIOService, IBGConfigurationSettingsService configurationSettingsService, BGFtpServiceType serviceType)
        {
            _fileIoService = fileIOService;
            _downloadFolderPath = configurationSettingsService.GetReceivedFilesFolderPath();
            _customerNumber = configurationSettingsService.GetPaymentRevieverCustomerNumber();
            _productCode = GetProductCode(serviceType);
        }

        public string ReadFileFromDisk()
        {
            if (_fileIoService.GetNumberOfReceivedFiles(_downloadFolderPath) == 0)
                return string.Empty;

            IEnumerable<string> fileNames = _fileIoService.GetReceivedFileNames(_downloadFolderPath);
            var recievedFileDates = new List<ReceivedFileNameDate>();

            fileNames.Each(fileName =>
            {
                if (FileNameOk(fileName, _customerNumber, _productCode))
                {
                    DateTime date = GetDateFromName(fileName);
                    recievedFileDates.Add(new ReceivedFileNameDate { FileName = fileName, CreatedByBgcDate = date });
                }
            });

            if (recievedFileDates.Count == 0)
                return string.Empty;
                
            return recievedFileDates.OrderBy(x => x.CreatedByBgcDate).First().FileName;
        }

        private static bool FileNameOk(string name, string customerNumber, string productCode)
        {
            string[] splittedName = name.Split(DelimiterChar);

            if (splittedName.Length != 4)
                return false;

            if (splittedName[0] != productCode)
                return false;

            if (!(splittedName[1].StartsWith("K0")
                &&
                    (splittedName[1].Length == 8)
                    && 
                    (splittedName[1].Substring(2, 6) == customerNumber)
                ))
                return false;

            if (!(splittedName[2].StartsWith("D")
                &&
                    (splittedName[2].Length == 7)
                    &&
                    (IsNumeric(splittedName[2].Substring(1, 6)))
                ))
                return false;
    
            if (!(splittedName[3].StartsWith("D")
                &&
                    (splittedName[3].Length == 7)
                    &&
                    (IsNumeric(splittedName[3].Substring(1, 6)))
                ))
                return false;

            string[] dateAndTime = GetDateAndTimeStringFromName(name);
            string dateString = dateAndTime[0];
            string timeString = dateAndTime[1];

            DateTime dummy;
            if (DateTime.TryParseExact(string.Concat(dateString, timeString),
                                        "YYMMDDHHmmSS",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out dummy))
                return true;
            return false;
        }

        public static bool IsNumeric(string text)
        {
            return Regex.IsMatch(text, "^\\d+$");
        }

        private static string[] GetDateAndTimeStringFromName(string name)
        {
            string[] splittedName = name.Split(DelimiterChar);

            string dateString = splittedName[2];
            string timeString = splittedName[3];

            dateString.Substring(1, dateString.Length - 1);
            timeString.Substring(1, timeString.Length - 1);

            return new [] { dateString, timeString };
        }

        private static DateTime GetDateFromName(string name)
        {
            string[] dateAndTime = GetDateAndTimeStringFromName(name);
            string dateString = dateAndTime[0];
            string timeString = dateAndTime[1];

            return DateTime.ParseExact(string.Concat(dateString, timeString), "YYMMDDHHmmSS", CultureInfo.InvariantCulture);
        }

        private static string GetProductCode(BGFtpServiceType serviceType)
        {
            switch (serviceType)
            {
                case BGFtpServiceType.Autogiro: return "IAGAG";
                case BGFtpServiceType.Leverantörsbetalningar: return "ILBLB";
                case BGFtpServiceType.Löner_Kontoinsättningar: return "IKIKI";
                case BGFtpServiceType.Autogiro_Test: return "IAGZZ";
                case BGFtpServiceType.Leverantörsbetalningar_Test: return "ILBZZ";
                case BGFtpServiceType.Löner_Kontoinsättningar_Test: return "IKIZZ";
                default: throw new ArgumentOutOfRangeException("serviceType");
            }
        }
    }

    public class ReceivedFileNameDate
    {
        public string FileName { get; set; }
        public DateTime CreatedByBgcDate { get; set; }
    }
}
