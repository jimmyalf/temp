namespace Synologen.Service.Client.OrderEmailSender.Application.Extensions
{
	public static class DecimalExtensions
	{
		 public static string GetStringValue(this decimal? value)
		 {
			return value == null ? null : value.ToString();
		 }
	}
}