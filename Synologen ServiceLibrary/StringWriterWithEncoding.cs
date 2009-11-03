using System.IO;
using System.Text;

namespace Spinit.Wpc.Synologen.ServiceLibrary{
	public class StringWriterWithEncoding : StringWriter {
		readonly Encoding encoding;

		public StringWriterWithEncoding (StringBuilder builder, Encoding encoding) : base(builder) {
			this.encoding = encoding;
		}

		public override Encoding Encoding{ get { return encoding; } }
	}
}