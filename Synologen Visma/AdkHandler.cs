using System;
using AdkNetWrapper;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.Visma.Types;
using Spinit.Wpc.Synologen.Visma.Utility;

namespace Spinit.Wpc.Synologen.Visma {
	public class AdkHandler : IAdkHandler {
		public const int MaxInvoiceFreeTextLength = 60;
		public const int MaxRowAccountLength = 6;

		public AdkHandler(string vismaCommonFilesPath, string vismaCompanyName) {
			VismaCommonFilesPath = vismaCommonFilesPath;
			VismaCompanyName = vismaCompanyName;
		}

		public void OpenCompany() {
			var vismaCommonFilesPath = VismaCommonFilesPath;
			var vismaCompanyName = VismaCompanyName;
			Operations.OpenCompany(ref vismaCommonFilesPath, ref vismaCompanyName);
		}

		public void CloseCompany() {
			Operations.CloseCompany();
		}

		public double AddInvoice(OrderData order, bool markAsPrinted, bool useNoVAT, out double invoiceSumIncludingVAT, out double invoiceSumExcludingVAT) {
			double invoiceNo = 0;
			invoiceSumIncludingVAT = 0;
			invoiceSumExcludingVAT = 0;

			var pData = Api.AdkCreateData( Api.ADK_DB_INVOICE_HEAD );

			// Set CustomerNo/CompanyCode
			var contractCompany = order.ContractCompany;
			var customerNo = contractCompany.SPCSCompanyCode.Trim();


			Operations.SetString( pData, Api.ADK_OOI_HEAD_CUSTOMER_NUMBER, ref customerNo );

			var orderRSTdesc = Formatting.BuildOrderRstDescription(order, MaxInvoiceFreeTextLength);
			if (!String.IsNullOrEmpty(orderRSTdesc)){
				Operations.SetString(pData, Api.ADK_OOI_HEAD_TEXT1, ref orderRSTdesc);
			}

			var orderCustomerdesc = Formatting.BuildOrderCustomerDescription(order, MaxInvoiceFreeTextLength);
			if(!String.IsNullOrEmpty(orderCustomerdesc)){
				Operations.SetString(pData, Api.ADK_OOI_HEAD_TEXT2, ref orderCustomerdesc);
			}

			var WpcOrderNumber = Formatting.BuildOrderIdDescription(order, MaxInvoiceFreeTextLength);
			Operations.SetString(pData, Api.ADK_OOI_HEAD_TEXT3, ref WpcOrderNumber);


			// Get order items
			var orderItems = order.OrderItems;


			if(useNoVAT) { //TEST: To See if it's possible to turn off 
				Operations.SetBool(pData, Api.ADK_OOI_HEAD_INCLUDING_VAT, false);
			}

			var pRowData = Api.AdkCreateDataRow( Api.ADK_DB_INVOICE_ROW, orderItems.Count );
			var index = 0;

			// Create invoice rows
			foreach ( var orderItem in orderItems ) {
				AddInvoiceRow( pRowData, index, orderItem);
				index++;
			}

			// Declare no. of invoice item rows
			Operations.SetDouble(pData, Api.ADK_OOI_HEAD_NROWS, orderItems.Count);

			// Connect items to header
			Operations.SetData(pData, Api.ADK_OOI_HEAD_ROWS, pRowData);

			// Save to Visma
			Operations.CommitData(pData);

			// Retrieve created invoice no
			Operations.GetDouble(pData, Api.ADK_OOI_HEAD_DOCUMENT_NUMBER, ref invoiceNo);

			Operations.GetDouble(pData, Api.ADK_OOI_HEAD_TOTAL_AMOUNT, ref invoiceSumIncludingVAT);

			Operations.GetDouble(pData, Api.ADK_OOI_HEAD_EXCLUDING_OF_VAT, ref invoiceSumExcludingVAT);
			

			order.InvoiceNumber = (long) invoiceNo;

			//Set Document Printed
			if(markAsPrinted){
				Operations.SetBool(pData, Api.ADK_OOI_HEAD_DOCUMENT_PRINTED, true);
				Operations.UpdateData(pData);
			}

			// Clean-up
			Api.AdkDeleteStruct( pData );

			return invoiceNo;
		}

		public static void AddInvoiceRow(int pRowPointer, int rowIndex, IOrderItem orderItem) {
			var pTempData = Api.AdkGetDataRow(pRowPointer, rowIndex);
			var itemDescription = Formatting.BuildItemDescription(orderItem);

			// Description
			Operations.SetString(pTempData, Api.ADK_OOI_ROW_TEXT, ref itemDescription);

			// Delivered Amount
			Operations.SetDouble(pTempData, Api.ADK_OOI_ROW_QUANTITY1, orderItem.NumberOfItems);

			// Ordered Amount
			Operations.SetDouble(pTempData, Api.ADK_OOI_ROW_QUANTITY2, orderItem.NumberOfItems);

			// Price per unit
			Operations.SetDouble(pTempData, Api.ADK_OOI_ROW_PRICE_EACH_CURRENT_CURRENCY, orderItem.SinglePrice);

			if(!String.IsNullOrEmpty(orderItem.SPCSAccountNumber)) {
				var accountNumber = Formatting.TruncateString(orderItem.SPCSAccountNumber, MaxRowAccountLength);
				Operations.SetString(pTempData, Api.ADK_OOI_ROW_ACCOUNT_NUMBER, ref accountNumber);
			}

			//Set Vat Code 1 as default or 0 if flag NoVAT is Set
			var thisVatCode = "1";
			if(orderItem.NoVAT)
				thisVatCode = "0";
			Operations.SetString(pTempData, Api.ADK_OOI_ROW_VAT_CODE, ref thisVatCode);
		}

		public void CancelInvoice(double invoiceNumber) {
			var pInvoiceData = Api.AdkCreateData(Api.ADK_DB_INVOICE_HEAD);

			Operations.SetDouble(pInvoiceData, Api.ADK_OOI_HEAD_DOCUMENT_NUMBER, invoiceNumber);
			try { Operations.FetchDataIntoPointer(pInvoiceData, true); }
			catch { throw new VismaException("Invoice (" + invoiceNumber + ") does not exist in SPCS database."); }
			
			Operations.SetBool(pInvoiceData, Api.ADK_OOI_HEAD_DOCUMENT_CANCELLED, true);

			Operations.UpdateData(pInvoiceData);

			Api.AdkDeleteStruct(pInvoiceData);

		}

		public PaymentInfo GetInvoicePaymentInfo(double vismaOrderId) {
			var paymentInfo = new PaymentInfo((long)vismaOrderId);
			//Create data structure
			var pData = Api.AdkCreateData(Api.ADK_DB_CUSTOMERPAYMENT);

			
			var pInvoiceData = Api.AdkCreateData(Api.ADK_DB_INVOICE_HEAD);
			Operations.SetDouble(pInvoiceData, Api.ADK_OOI_HEAD_DOCUMENT_NUMBER, vismaOrderId);
			try { Operations.FetchDataIntoPointer(pInvoiceData, true); }
			catch { throw new VismaException("Invoice ("+vismaOrderId+") does not exist in SPCS database."); }
			var invoiceCanceled = false;
			Operations.GetBool(pInvoiceData, Api.ADK_OOI_HEAD_DOCUMENT_CANCELLED, ref invoiceCanceled);
			paymentInfo.InvoiceCanceled = invoiceCanceled;

			//Set invoice number in data structure
			Operations.SetDouble(pData, Api.ADK_CUSTOMERPAYMENT_INVOICE_NUMBER, vismaOrderId);

			//Fetch/Search data into structure 
			//(if exception it cannot be found which means invoice is not payed)
			try { Operations.FetchDataIntoPointer(pData, true); }
			catch {
				Api.AdkDeleteStruct(pInvoiceData);
				Api.AdkDeleteStruct(pData);
				return paymentInfo;
			}

			//Get Date
			paymentInfo.InvoicePaymentDate = Operations.SafeGetDateTime(pData, Api.ADK_CUSTOMERPAYMENT_DATE);
			var paymentCanceled = true;
			Operations.GetBool(pData, Api.ADK_CUSTOMERPAYMENT_CANCELLED, ref paymentCanceled);
			paymentInfo.InvoicePaymentCanceled = paymentCanceled;
			var status = string.Empty;
			Operations.GetString(pData, Api.ADK_CUSTOMERPAYMENT_STATUS_C, ref status, 1);
			paymentInfo.Status = status;
			Api.AdkDeleteStruct(pInvoiceData);
			Api.AdkDeleteStruct(pData);
			return paymentInfo;
		}

		#region Properties

		// Path to Visma "Gemensamma filer" folder
		public string VismaCommonFilesPath { get; set; }

		// Visma database "Företag"
		public string VismaCompanyName { get; set; }

		#endregion
	}

}