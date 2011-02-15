namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class ConsentToSend
	{
		public int PayerNumber { get; set; }
		public string BankAccountNumber { get; set; }
		public string PersonalIdNumber { get; set; }
		public string ClearingNumber { get; set; }
	}
}