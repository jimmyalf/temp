using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve
{
	public class ErrorsFile : FileBase<Error>
	{
		public PaymentReciever Reciever { get; set; }
		public decimal TotalCreditAmountInFile { get; set; }
		public decimal TotalDebitAmountInFile { get; set; }
		public int NumberOfCreditsInFile { get; set; }
		public int NumberOfDebitsInFile { get; set; }
	}
}