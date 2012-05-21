using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.Service.Client.BGCTaskRunner.App.Services
{
	public class BGFtpService : IFtpService
	{
		private readonly IFtpIOService _ftpIoService;
		private readonly BGFtpServiceType _serviceType;
		private readonly IBGServiceCoordinatorSettingsService _serviceCoordinatorSettingsService;

		public BGFtpService(IFtpIOService ftpIoService, IBGServiceCoordinatorSettingsService serviceCoordinatorSettingsService, BGFtpServiceType serviceType)
		{
			_ftpIoService = ftpIoService;
			_serviceType = serviceType;
			_serviceCoordinatorSettingsService = serviceCoordinatorSettingsService;
		}

		public FtpSendResult SendFile(string fileData)
		{
			var fileName = GetFileName();
			var ftpUploadRoot = _serviceCoordinatorSettingsService.GetFtpUploadFolderUrl();
			_ftpIoService.SendFile(ftpUploadRoot, fileName, fileData);
			return new FtpSendResult(fileName);
		}

		private string GetFileName()
		{
			return String.Format("BFEP.{0}.K0{1}", 
				GetProductCode(_serviceType),
				_serviceCoordinatorSettingsService.GetPaymentRevieverCustomerNumber());
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
}