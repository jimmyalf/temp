using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve
{
	public class ConsentsFile : FileBase<Consent>
	{
		public string PaymentRecieverBankgiroNumber { get; set; }
		public int NumberOfItemsInFile { get; set; }
	}
}