using System;
using System.Collections.Generic;
using System.IO;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Utility.Types;

namespace Spinit.Wpc.Synologen.Utility {
	public class General {

		public static void WriteInvoiceEDIToFile(string filePath, Invoice invoice) {
			var streamWriter = new StreamWriter(filePath);
			streamWriter.Write(invoice.Parse());
			streamWriter.Close();
		}

		public static Invoice CreateInvoiceEDI(OrderRow order, List<OrderItemRow> orderItemList, CompanyRow company, IShop shop, EDIConversionSettings ediSettings) {
			var iorderList = new List<IOrderItem>();
			foreach(var row in orderItemList) { iorderList.Add(row); }
			var EDIInvoice = Convert.ToEDIInvoice(ediSettings, order, iorderList, company, shop);
			return EDIInvoice;
		}

		public static Invoice CreateInvoiceEDI(OrderRow order, List<IOrderItem> iorderList, CompanyRow company,IShop shop, EDIConversionSettings ediSettings) {
			var EDIInvoice = Convert.ToEDIInvoice(ediSettings, order, iorderList, company, shop);
			return EDIInvoice;
		}

		public static SFTIInvoiceType CreateInvoiceSvefaktura(OrderRow order, List<IOrderItem> iorderList, CompanyRow company, IShop shop, SvefakturaConversionSettings settings) {
			if (order == null) throw new ArgumentNullException("order");
			if (iorderList == null) throw new ArgumentNullException("iorderList");
			if (company == null) throw new ArgumentNullException("company");
			if (shop == null) throw new ArgumentNullException("shop");
			if (settings == null) throw new ArgumentNullException("settings");
			return Convert.ToSvefakturaInvoice(settings, order, iorderList, company, shop);
		}
	}
}