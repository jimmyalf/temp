using System;
using System.Collections.Generic;
using System.IO;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
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

		//public static Invoice CreateInvoiceEDI(IOrder order, List<OrderItem> orderItemList, ICompany company, IShop shop, EDIConversionSettings ediSettings) {
		//    var iorderList = new List<IOrderItem>();
		//    foreach(var row in orderItemList) { iorderList.Add(row); }
		//    var EDIInvoice = Convert.ToEDIInvoice(ediSettings, order, iorderList, company, shop);
		//    return EDIInvoice;
		//}

		public static Invoice CreateInvoiceEDI(IOrder order, IList<IOrderItem> iorderList, ICompany company, IShop shop, EDIConversionSettings ediSettings) {
			var EDIInvoice = Convert.ToEDIInvoice(ediSettings, order, iorderList, company, shop);
			return EDIInvoice;
		}

		public static SFTIInvoiceType CreateInvoiceSvefaktura(Order order, List<IOrderItem> iorderList, Company company, Shop shop, SvefakturaConversionSettings settings) {
			if (order == null) throw new ArgumentNullException("order");
			if (iorderList == null) throw new ArgumentNullException("iorderList");
			if (company == null) throw new ArgumentNullException("company");
			if (shop == null) throw new ArgumentNullException("shop");
			if (settings == null) throw new ArgumentNullException("settings");
			return Convert.ToSvefakturaInvoice(settings, order, iorderList, company, shop);
		}
	}
}