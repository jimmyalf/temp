using System.Collections.Specialized;
using System.Configuration;

namespace Synologen.Service.Client.OrderEmailSender.Services
{
	public class ConfigurationSettings : IConfigurationSettings
	{
		private NameValueCollection _appSettings;

		public ConfigurationSettings()
		{
			_appSettings = ConfigurationManager.AppSettings;
		}

		public string SpinitServicesUserName
		{
			get { return _appSettings["SpinitServicesUserName"]; }
		}

		public string SpinitServicesPassword
		{
			get { return _appSettings["SpinitServicesPassword"]; }
		}

		public string SpinitServicesPasswordEncoding
		{
			get { return _appSettings["SpinitServicesPasswordEncoding"]; }
		}

		public string SpinitServicesServerAddress
		{
			get { return _appSettings["SpinitServicesServerAddress"]; }
		}

		public string OrderSenderEmailAddress
		{
			get { return _appSettings["OrderSenderEmailAddress"]; }
		}

		public string OrderEmailSubjectFormat
		{
			get { return _appSettings["OrderEmailSubjectFormat"]; }
		}
	}
}