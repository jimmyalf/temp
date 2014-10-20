using Spinit.Security.Password;
using Spinit.Services.Client;
using Synologen.Service.Client.OrderEmailSender.Services;

namespace Synologen.Service.Client.OrderEmailSender.Factory
{
	public static class EmailClientFactory
	{
		 public static EmailClient2 Create(IConfigurationSettings configurationSettings)
		 {
            ClientFactory.SetConfigurtion(ClientFactory.CreateConfiguration(
                configurationSettings.SpinitServicesServerAddress,
                configurationSettings.SpinitServicesUserName,
                configurationSettings.SpinitServicesPassword,
                PasswordEncryptionType.Sha1,
                configurationSettings.SpinitServicesPasswordEncoding
            ));
            return ClientFactory.CreateEmail2Client();
		 }
	}
}