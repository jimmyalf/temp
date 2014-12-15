using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService
{
	public class ConsentToSend
	{
        public ConsentType Type{ get; set; }
		public int PayerNumber { get; set; }
		public string BankAccountNumber { get; set; }
        public string ClearingNumber { get; set; }
        public string OrgNumber { get; set; }
		public string PersonalIdNumber { get; set; }
	}
}