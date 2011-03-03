using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
    public class BGReceivedFileReaderService : IFileReaderService
    {
        private readonly IFileIOService _fileIoService;
        private readonly IFileSplitter _fileSplitter;
        private readonly string _downloadFolderPath;
        private readonly string _backupFolderPath;
        private readonly string _customerNumber;
        private readonly string _productCode;

        public BGReceivedFileReaderService(IFileIOService fileIOService, IBGConfigurationSettingsService configurationSettingsService, BGFtpServiceType serviceType, IFileSplitter fileSplitter)
        {
            _fileIoService = fileIOService;
            _fileSplitter = fileSplitter;
            _downloadFolderPath = configurationSettingsService.GetReceivedFilesFolderPath();
            _backupFolderPath = configurationSettingsService.GetBackupFilesFolderPath();
            _customerNumber = configurationSettingsService.GetPaymentRevieverCustomerNumber();
            _productCode = GetProductCode(serviceType);
        }

        public IEnumerable<string> GetFileNames()
        {
            IEnumerable<string> fileNames = _fileIoService.GetReceivedFileNames(_downloadFolderPath);
            if (fileNames.Count() == 0)
                return Enumerable.Empty<string>();
            
            var recievedFileDates = new List<ReceivedFileNameDate>();

            fileNames.Each(fileName =>
            {
                if (_fileSplitter.FileNameOk(fileName, _customerNumber, _productCode))
                {
                    DateTime date = _fileSplitter.GetDateFromName(fileName);
                    recievedFileDates.Add(new ReceivedFileNameDate { FileName = fileName, CreatedByBgcDate = date });
                }
            });

            if (recievedFileDates.Count == 0)
                return Enumerable.Empty<string>();

            return recievedFileDates.OrderBy(x => x.CreatedByBgcDate).Select(x => x.FileName).ToList();
        }

        public IEnumerable<FileSection> GetSections(string[] file)
        {
            return _fileSplitter.GetSections(file);
        }

        public void MoveFile(string file)
        {
            _fileIoService.MoveFile(string.Concat(_downloadFolderPath, "\\", file), string.Concat(_backupFolderPath, "\\", file));
        }

        public string[] ReadFileFromDisk(string fileName)
        {
            return _fileIoService.ReadFile(string.Concat(_downloadFolderPath, "\\", fileName));
        }

        //private static bool FileNameOk(string name, string customerNumber, string productCode)
        //{
        //    string[] splittedName = name.Split(DelimiterChar);

        //    if (splittedName.Length != 4)
        //        return false;

        //    if (splittedName[0] != productCode)
        //        return false;

        //    if (!(splittedName[1].StartsWith("K0")
        //        &&
        //            (splittedName[1].Length == 8)
        //            && 
        //            (splittedName[1].Substring(2, 6) == customerNumber)
        //        ))
        //        return false;

        //    if (!(splittedName[2].StartsWith("D")
        //        &&
        //            (splittedName[2].Length == 7)
        //            &&
        //            (IsNumeric(splittedName[2].Substring(1, 6)))
        //        ))
        //        return false;
    
        //    if (!(splittedName[3].StartsWith("D")
        //        &&
        //            (splittedName[3].Length == 7)
        //            &&
        //            (IsNumeric(splittedName[3].Substring(1, 6)))
        //        ))
        //        return false;

        //    string[] dateAndTime = GetDateAndTimeStringFromName(name);
        //    string dateString = dateAndTime[0];
        //    string timeString = dateAndTime[1];

        //    DateTime dummy;
        //    if (DateTime.TryParseExact(string.Concat(dateString, timeString),
        //                                "YYMMDDHHmmSS",
        //                                CultureInfo.InvariantCulture,
        //                                DateTimeStyles.None,
        //                                out dummy))
        //        return true;
        //    return false;
        //}

        //public static bool IsNumeric(string text)
        //{
        //    return Regex.IsMatch(text, "^\\d+$");
        //}

        //private static string[] GetDateAndTimeStringFromName(string name)
        //{
        //    string[] splittedName = name.Split(DelimiterChar);

        //    string dateString = splittedName[2];
        //    string timeString = splittedName[3];

        //    dateString.Substring(1, dateString.Length - 1);
        //    timeString.Substring(1, timeString.Length - 1);

        //    return new [] { dateString, timeString };
        //}

        //private static DateTime GetDateFromName(string name)
        //{
        //    string[] dateAndTime = GetDateAndTimeStringFromName(name);
        //    string dateString = dateAndTime[0];
        //    string timeString = dateAndTime[1];

        //    return DateTime.ParseExact(string.Concat(dateString, timeString), "YYMMDDHHmmSS", CultureInfo.InvariantCulture);
        //}

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
