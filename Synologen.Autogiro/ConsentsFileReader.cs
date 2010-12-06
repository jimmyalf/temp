using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wp.Synologen.Autogiro
{
	public class ConsentsFileReader : AutogiroFileReader<ConsentsFile,Consent>
	{
		public ConsentsFileReader(string fileContent) : base(new ConsentsFileContentReader(fileContent), new ConsentItemReader()) {}
	}
}