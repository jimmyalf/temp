using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send;

namespace Synologen.LensSubscription.Autogiro.Writers
{
	public class ConsentsFileWriter : AutogiroFileWriter<ConsentsFile,  Consent>
	{
		public ConsentsFileWriter() : base(new ConsentHeaderWriter(), new ConsentItemWriter()) {  }
	}
}