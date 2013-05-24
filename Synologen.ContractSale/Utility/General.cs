using System;
using System.IO;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.EDI;
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

		public static SFTIInvoiceType CreateInvoiceSvefaktura(Order order, SvefakturaConversionSettings settings) 
		{
			if (order == null) throw new ArgumentNullException("order");
			if (order.OrderItems == null) throw new ArgumentNullException("order","OrderItems missing");
			if (order.ContractCompany == null) throw new ArgumentNullException("order", "Order ContractComany missing");
			if (order.SellingShop == null) throw new ArgumentNullException("order", "Order Sellingshop missing");
			if (settings == null) throw new ArgumentNullException("settings");
			return Convert.ToSvefakturaInvoice(settings, order);
		}
	}
}