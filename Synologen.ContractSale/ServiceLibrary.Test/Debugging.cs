using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Validators;

namespace Spinit.Wpc.Synologen.Integration.Services.Test
{
    [TestFixture, Explicit]
    public class Debugging
    {
        private readonly SvefakturaBuilder _invoiceBuilder;
        private readonly SqlProvider _provider;
        private readonly string _desktopPath;
        private readonly Encoding _encoding;

        public Debugging()
        {
            var settings = TestInvoiceParsingAndValidation.GetSettings();
            _invoiceBuilder = new SvefakturaBuilder(new SvefakturaFormatter(), settings, new SvefakturaBuilderValidator());
            _provider = new SqlProvider(@"Initial Catalog=dbWpcSynologen;Data Source=BLACK\SQL2008;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;");
            _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            _encoding = Encoding.GetEncoding("ISO-8859-1");
        }

        [Test, Explicit]
        public void Create_SR_Invoice()
        {
            var order = _provider.GetOrder(3886);
            CreateInvoice(order, "Test_Invoice_SR");
        }

        [Test, Explicit]
        public void Create_SAAB_Invoice()
        {
            var order = _provider.GetOrder(3885);
            CreateInvoice(order, "Test_Invoice_SAAB");
        }

        [Test, Explicit]
        public void Create_Praktikertjänst_Invoice()
        {
            var order = _provider.GetOrder(3884);
            CreateInvoice(order, "Test_Invoice_Praktikertjänst");
        }     
 
        protected void CreateInvoice(IOrder order, string fileName)
        {
            var invoice = _invoiceBuilder.Build(order);
            var text = SvefakturaSerializer.Serialize(invoice, _encoding, Environment.NewLine, Formatting.Indented, null);

            var file = GetFile(fileName);
            if (file.Exists)
            {
                file.Delete();
            }

            using (var streamWriter = new StreamWriter(file.FullName, false, _encoding))
            {
                streamWriter.Write(text);
            }            
        }

        protected FileInfo GetFile(string fileName)
        {
            const string Format = @"{0}\{1}_{2:yyyy-MM-dd_HH_mm_ss}.xml";
            var filePath = string.Format(Format, _desktopPath, fileName, DateTime.Now);
            return new FileInfo(filePath);
        }
    }
}