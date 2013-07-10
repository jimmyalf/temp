using System;
using System.IO;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Invoicing
{
	public class General 
	{
		public static void WriteInvoiceEDIToFile(string filePath, Invoice invoice) 
		{
			var streamWriter = new StreamWriter(filePath);
			streamWriter.Write(invoice.Parse());
			streamWriter.Close();
		}

		public static Invoice CreateInvoiceEDI(IOrder order, EDIConversionSettings ediSettings) 
		{
			return Convert.ToEDIInvoice(ediSettings, order);
		}

        //TODO: Remove this static method (contains no real logic)
		public static SFTIInvoiceType CreateInvoiceSvefaktura(Order order, SvefakturaConversionSettings settings) 
		{
            var builder = new EBrevSvefakturaBuilder(new SvefakturaFormatter(), settings, new Svefaktura.SvefakturaBuilderBuilderValidator());
            return builder.Build(order);
		}
	}
}