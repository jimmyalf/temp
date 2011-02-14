using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wp.Synologen.Autogiro.Readers
{
	public class ConsentsFileReader : AutogiroFileReader<ConsentsFile,Consent>
	{
		public ConsentsFileReader() : base(new ConsentsFileContentReader(), new ConsentItemReader()) {}
	}
}