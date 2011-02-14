using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Synologen.LensSubscription.Autogiro.Readers
{
	public class PaymentsFileReader : AutogiroFileReader<PaymentsFile,Payment>
	{
		public PaymentsFileReader() : base(new PaymentsFileContentReader(), new PaymentItemReader()) {}
	}
}