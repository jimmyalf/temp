using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;

namespace Synologen.LensSubscription.Autogiro.Writers
{
	public class PaymentsFileWriter : AutogiroFileWriter<PaymentsFile,  Payment>
	{
		public PaymentsFileWriter() : base(new PaymentHeaderWriter(), new PaymentItemWriter()) {  }
	}
}