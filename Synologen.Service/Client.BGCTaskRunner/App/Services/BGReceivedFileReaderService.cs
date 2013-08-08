using System;
using System.Collections.Generic;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Extensions;

namespace Synologen.Service.Client.BGCTaskRunner.App.Services
{
    public class BGReceivedFileReaderService : IFileReaderService
    {
        private readonly IFileIOService _fileIoService;
        private readonly IFileSplitter _fileSplitter;
        private readonly string _downloadFolderPath;
        private readonly string _backupFolderPath;
        private readonly string _customerNumber;
        private readonly string _productCode;

        public BGReceivedFileReaderService(IFileIOService fileIOService, IBGServiceCoordinatorSettingsService serviceCoordinatorSettingsService, BGFtpServiceType serviceType, IFileSplitter fileSplitter)
        {
            _fileIoService = fileIOService;
            _fileSplitter = fileSplitter;
            _downloadFolderPath = serviceCoordinatorSettingsService.GetReceivedFilesFolderPath();
            _backupFolderPath = serviceCoordinatorSettingsService.GetBackupFilesFolderPath();
            _customerNumber = serviceCoordinatorSettingsService.GetPaymentRevieverCustomerNumber();
            _productCode = GetProductCode(serviceType);
        }

        public IEnumerable<string> GetFileNames()
        {
			if(_downloadFolderPath == null) throw new ApplicationException("Download folder path has not been set");
            var fileNames = _fileIoService.GetReceivedFileNames(_downloadFolderPath);
            if (fileNames.Count() == 0) return Enumerable.Empty<string>();
            
            var recievedFileDates = new List<ReceivedFileNameDate>();

        	var fileNamesToSplit = fileNames.Where(fileName => _fileSplitter.FileNameOk(fileName, _customerNumber, _productCode));
            fileNamesToSplit.Each(fileName =>
            {
                var date = _fileSplitter.GetDateFromName(fileName);
                recievedFileDates.Add(new ReceivedFileNameDate { FileName = fileName, CreatedByBgcDate = date });
            });

            return recievedFileDates.Count == 0 
				? Enumerable.Empty<string>() 
				: recievedFileDates.OrderBy(x => x.CreatedByBgcDate).Select(x => x.FileName).ToList();
        }

        public IEnumerable<FileSection> GetSections(string[] file)
        {
            return _fileSplitter.GetSections(file);
        }

        public void MoveFile(string fileName)
        {
            _fileIoService.MoveFile(System.IO.Path.Combine(_downloadFolderPath, fileName), System.IO.Path.Combine(_backupFolderPath, fileName));
        }

        public string[] ReadFileFromDisk(string fileName)
        {
            return _fileIoService.ReadFile(System.IO.Path.Combine(_downloadFolderPath, fileName));
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
	    private class ReceivedFileNameDate
		{
			public string FileName { get; set; }
			public DateTime CreatedByBgcDate { get; set; }
		}
    }
}
