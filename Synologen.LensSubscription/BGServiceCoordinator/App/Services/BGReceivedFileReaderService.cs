﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public void MoveFile(string fileName)
        {
            _fileIoService.MoveFile(string.Concat(_downloadFolderPath, "\\", fileName), string.Concat(_backupFolderPath, "\\", fileName));
        }

        public string[] ReadFileFromDisk(string fileName)
        {
            return _fileIoService.ReadFile(string.Concat(_downloadFolderPath, "\\", fileName));
        }

        private static string GetProductCode(BGFtpServiceType serviceType)
        {
            switch (serviceType)
            {
                case BGFtpServiceType.Autogiro: return "UAGAG";
                case BGFtpServiceType.Leverantörsbetalningar: return "ULBLB";
                case BGFtpServiceType.Löner_Kontoinsättningar: return "UKIKI";
                case BGFtpServiceType.Autogiro_Test: return "UAGZZ";
                case BGFtpServiceType.Leverantörsbetalningar_Test: return "ULBZZ";
                case BGFtpServiceType.Löner_Kontoinsättningar_Test: return "UKIZZ";
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