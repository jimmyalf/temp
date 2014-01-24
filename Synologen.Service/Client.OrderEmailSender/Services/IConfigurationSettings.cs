namespace Synologen.Service.Client.OrderEmailSender.Services
{
	public interface IConfigurationSettings
	{
		string SpinitServicesUserName { get; }
		string SpinitServicesPassword { get; }
		string SpinitServicesPasswordEncoding { get; }
		string SpinitServicesServerAddress { get; }
		string OrderSenderEmailAddress { get; }
		string OrderEmailSubjectFormat { get; }
	}
}