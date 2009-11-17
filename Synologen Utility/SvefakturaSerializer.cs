using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing{
	public static class SvefakturaSerializer{

		public const string DefaultXmlNewLine = "\r\n";
		public const Formatting DefaultXmlFormatting = Formatting.Indented;
		public static readonly Encoding DefaultXmlEncoding = Encoding.UTF8;

		public static string Serialize(SFTIInvoiceType invoice){
			return Serialize(invoice, DefaultXmlEncoding, DefaultXmlNewLine, DefaultXmlFormatting);
		}
		public static string Serialize(SFTIInvoiceType invoice, Encoding encoding)  {
			return Serialize(invoice, encoding, DefaultXmlNewLine, DefaultXmlFormatting);
		}
		public static string Serialize(SFTIInvoiceType invoice, Encoding encoding, string newLine){
			return Serialize(invoice, encoding, newLine, DefaultXmlFormatting);
		}
		public static string Serialize(SFTIInvoiceType invoice, Encoding encoding, string newLine, Formatting xmlFormatting){
			var xmlSerializer = new XmlSerializer(invoice.GetType());
			var output = new StringWriterWithEncoding(new StringBuilder(), encoding){NewLine = newLine};
			var xmlTextWriter = new XmlTextWriter( output ) {Formatting = xmlFormatting};
			xmlSerializer.Serialize(xmlTextWriter, invoice,  GetNamespaces());
			return InsertPostOfficeHeader(output.ToString(), newLine);
		}

		private static XmlSerializerNamespaces GetNamespaces(){
			var namespaces = new XmlSerializerNamespaces();
			namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
			namespaces.Add("udt", "urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0");
			namespaces.Add("sdt", "urn:oasis:names:tc:ubl:SpecializedDatatypes:1:0");
			namespaces.Add("cur", "urn:oasis:names:tc:ubl:codelist:CurrencyCode:1:0");
			namespaces.Add("ccts", "urn:oasis:names:tc:ubl:CoreComponentParameters:1:0");
			namespaces.Add("cbc", "urn:oasis:names:tc:ubl:CommonBasicComponents:1:0");
			namespaces.Add("cac", "urn:sfti:CommonAggregateComponents:1:0");
			namespaces.Add(String.Empty, "urn:sfti:documents:BasicInvoice:1:0");
			return namespaces;
		}

		private static string InsertPostOfficeHeader(string xmlContent, string xmlNewLine){
			const string header = "<?POSTNET SND=\"AVSADRESS\" REC=\"MOTADRESS\" MSGTYPE=\"MEDDELANDETYP\"?>";
			var insertPosition = xmlContent.IndexOf("?>") + 2;
			return xmlContent.Insert(insertPosition, xmlNewLine + header);
		}

		private class StringWriterWithEncoding : StringWriter {
			readonly Encoding encoding;

			public StringWriterWithEncoding (StringBuilder builder, Encoding encoding) : base(builder) {
				this.encoding = encoding;
			}

			public override Encoding Encoding{ get { return encoding; } }
		}
	}
}