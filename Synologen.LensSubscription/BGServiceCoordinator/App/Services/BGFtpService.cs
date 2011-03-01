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
                case BGFtpServiceType.Autogiro_From_BGC: return "UAGAG";
				case BGFtpServiceType.Leverant�rsbetalningar: return "ILBLB";
                case BGFtpServiceType.Leverant�rsbetalningar_From_BGC: return "ULBLB";
				case BGFtpServiceType.L�ner_Kontoins�ttningar: return "IKIKI";
                case BGFtpServiceType.L�ner_Kontoins�ttningar_From_BGC: return "UKIKI";
				case BGFtpServiceType.Autogiro_Test: return "IAGZZ";
                case BGFtpServiceType.Autogiro_Test_From_BGC: return "UAGZZ";
				case BGFtpServiceType.Leverant�rsbetalningar_Test: return "ILBZZ";
                case BGFtpServiceType.Leverant�rsbetalningar_Test_From_BGC: return "ULBZZ";
				case BGFtpServiceType.L�ner_Kontoins�ttningar_Test: return "IKIZZ";
                case BGFtpServiceType.L�ner_Kontoins�ttningar_Test_From_BGC: return "UKIZZ";
				default: throw new ArgumentOutOfRangeException("serviceType");
			}
		}
	}
}