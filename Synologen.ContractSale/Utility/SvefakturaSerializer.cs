using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing
{
	public static class SvefakturaSerializer
    {
		public static string Serialize(SFTIInvoiceType invoice, Encoding encoding, string newLine, Formatting xmlFormatting, string postOfficeHeader)
        {
			var xmlSerializer = new XmlSerializer(invoice.GetType());
			var output = new StringWriterWithEncoding(new StringBuilder(), encoding) { NewLine = newLine };
			var xmlTextWriter = new XmlTextWriter(output) { Formatting = xmlFormatting };
			xmlSerializer.Serialize(xmlTextWriter, invoice,  GetNamespaces());
			return InsertPostOfficeHeader(output.ToString(), postOfficeHeader, newLine);
		}

		public static string Serialize(SFTIInvoiceList invoices, Encoding encoding, string newLine, Formatting xmlFormatting, string postOfficeHeader)
        {
			var xmlSerializer = new XmlSerializer(invoices.GetType());
			var output = new StringWriterWithEncoding(new StringBuilder(), encoding) { NewLine = newLine };
			var xmlTextWriter = new XmlTextWriter(output) { Formatting = xmlFormatting };
			xmlSerializer.Serialize(xmlTextWriter, invoices,  GetNamespaces());
			return InsertPostOfficeHeader(output.ToString(), postOfficeHeader, newLine);
		}

		private static XmlSerializerNamespaces GetNamespaces()
        {
			var namespaces = new XmlSerializerNamespaces();
			namespaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
			namespaces.Add("udt", "urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0");
			namespaces.Add("sdt", "urn:oasis:names:tc:ubl:SpecializedDatatypes:1:0");
			namespaces.Add("cur", "urn:oasis:names:tc:ubl:codelist:CurrencyCode:1:0");
			namespaces.Add("ccts", "urn:oasis:names:tc:ubl:CoreComponentParameters:1:0");
			namespaces.Add("cbc", "urn:oasis:names:tc:ubl:CommonBasicComponents:1:0");
			namespaces.Add("cac", "urn:sfti:CommonAggregateComponents:1:0");
			namespaces.Add(string.Empty, "urn:sfti:documents:BasicInvoice:1:0");
			return namespaces;
		}

		private static string InsertPostOfficeHeader(string xmlContent, string header,  string xmlNewLine)
		{
		    if (string.IsNullOrEmpty(header))
		    {
		        return xmlContent;
		    }

		    var insertPosition = xmlContent.IndexOf("?>", System.StringComparison.InvariantCulture) + 2;
		    return xmlContent.Insert(insertPosition, xmlNewLine + header);
		}

		private class StringWriterWithEncoding : StringWriter 
        {
			private readonly Encoding _encoding;

			public StringWriterWithEncoding(StringBuilder builder, Encoding encoding) : base(builder)
            {
				_encoding = encoding;
			}

		    public override Encoding Encoding
		    {
		        get { return _encoding; }
		    }
		}
	}
}