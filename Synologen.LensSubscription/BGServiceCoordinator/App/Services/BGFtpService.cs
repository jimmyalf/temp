using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGFtpService : IFtpService
	{
		private readonly IFtpIOService _ftpIoService;
		private readonly BGFtpServiceType _serviceType;
		private readonly IBGConfigurationSettingsService _configurationSettingsService;

		public BGFtpService(IFtpIOService ftpIoService, IBGConfigurationSettingsService configurationSettingsService, BGFtpServiceType serviceType)
		{
			_ftpIoService = ftpIoService;
			_serviceType = serviceType;
			_configurationSettingsService = configurationSettingsService;
		}

		public FtpSendResult SendFile(string fileData)
		{
			var fileName = GetFileName();
			var ftpUploadRoot = _configurationSettingsService.GetFtpUploadFolderUrl();
			var ftpUri = ftpUploadRoot.AppendUrl(fileName);
			_ftpIoService.SendFile(ftpUri, fileData);
			return new FtpSendResult(fileName);
		}

		private string GetFileName()
		{
			return String.Format("BFEP.{0}.{1}", 
				GetProductCode(_serviceType),
				_configurationSettingsService.GetPaymentRevieverCustomerNumber());
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