using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wp.Synologen.Autogiro.Readers
{
	public class ErrorFileReader : AutogiroFileReader<ErrorsFile, Error>
	{
		public ErrorFileReader() : base(new ErrorFileContentReader(), new ErrorItemReader()) { }
	}
}
