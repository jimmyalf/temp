using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Visma;
using Synologen.Client.Common;

namespace Synologen.Client.ChildForms {
	internal partial class StatusSync : FormBase {
		private bool statusesHaveBeenFetched;
		private IList<long> orderIdList = new List<long>();
		private readonly IList<IInvoiceStatus> invoicePaymentInfoList = new List<IInvoiceStatus>();

		public StatusSync() {
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);
			btnFetch.Text = "Sök efter uppdateringar";
			statusesHaveBeenFetched = false;
			InitializeInvoiceList();
		}

		private void btnFetch_Click(object sender, EventArgs e) {
			if(!statusesHaveBeenFetched){
				btnFetch.Enabled = false;
				GetWpcOrderIdsFromWebService();
				if(orderIdList.Count<=0){
					DisplayInformationMessage("Inga ordrar för statusuppdatering hittades.");
					return;
				}
				PopulateOrdersIds();
				bool foundUpdates;
				PopulateOrderInformation(out foundUpdates);
				btnFetch.Text = "Uppdatera WPC";
				statusesHaveBeenFetched = true;
				btnFetch.Enabled = foundUpdates;
				if (!foundUpdates) {
					DisplayInformationMessage("Inga uppdateringar hittades.");
					return;
				}
			}
			else{
				UpdateWPCWithStatuses();
			}
		}

		private void btnCopyToClipBoard_Click(object sender, EventArgs e) {
			CopyListViewToClipboard(orderList);
		}

		private static void DisplayInformationMessage(string message) {
			MessageBox.Show(message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void UpdateWPCWithStatuses() {
			Cursor = Cursors.WaitCursor;
			var client = ClientUtility.GetWebClient();
			try {
				client.Open();
				SetFormStatus(Enumeration.ConnectionStatus.Connecting, "Skickar statusuppdateringar till WPC...");
				foreach (var paymentInfo in invoicePaymentInfoList) {
					ClientUtility.SetSPCSOrderInformation(client,paymentInfo);	
				}
				SetFormStatus(Enumeration.ConnectionStatus.Connected, "Uppdatering klar!");
				btnCopyToClipBoard.Enabled = false;
				btnFetch.Enabled = false;
			}
			catch(Exception ex) {
				btnCopyToClipBoard.Enabled = false;
				btnFetch.Enabled = false;
				Cursor = Cursors.Default;
				SetFormStatus(Enumeration.ConnectionStatus.Disconnected, "Anslutning mot WPC misslyckades");
				ClientUtility.DisplayErrorMessage(ex.Message);
			}
			finally {
				client.Close();
				Cursor = Cursors.Default;
			}
		}

		private bool SetRowOrderInformation(AdkHandler spcsHandler, ListViewItem listItem, double spcsOrderid) {
			var foundUpdates = false;
			try{
				var paymentInfo = ClientUtility.GetSPCSInvoiceStatus(spcsHandler, spcsOrderid);
				invoicePaymentInfoList.Add(new PaymentInfo(paymentInfo));
				listItem.SubItems[1].Text = "Ja";
				listItem.SubItems[2].Text = paymentInfo.InvoiceIsPayed ? "Ja" : "";
				listItem.SubItems[3].Text = paymentInfo.InvoiceCanceled ? "Ja" : "";
				if(paymentInfo.InvoiceIsPayed || paymentInfo.InvoiceCanceled) {
					foundUpdates = true;
				}
			}
			catch {
				listItem.SubItems[1].Text = "Hittas Ej";
			}
			return foundUpdates;
		}

		private void GetWpcOrderIdsFromWebService() {
			Cursor = Cursors.WaitCursor;
			try{
				SetFormStatus(Enumeration.ConnectionStatus.Connecting, "Ansluter...");
				orderIdList = ClientUtility.GetOrderIdListForUpdateCheckFromWebService();
				SetFormStatus(Enumeration.ConnectionStatus.Connected, "Ansluten");
			}
			catch(Exception ex) {
				Cursor = Cursors.Default;
				SetFormStatus(Enumeration.ConnectionStatus.Disconnected, "Anslutning mot WPC misslyckades");
				ClientUtility.DisplayErrorMessage(ex.Message);
			}
			finally {
				Cursor = Cursors.Default;
			}
		}

		private void InitializeInvoiceList(){
			orderList.Items.Clear();
			orderList.Columns.Clear();
			orderList.View = View.Details;
			orderList.LabelEdit = true;
			orderList.FullRowSelect = true;
			var fivePercentWidth = (orderList.ClientSize.Width) / 20;
			orderList.Columns.Add("SPCS-Id", fivePercentWidth * 2);
			orderList.Columns.Add("Status hämtad", fivePercentWidth * 5);
			orderList.Columns.Add("Faktura betalad", fivePercentWidth * 5);
			orderList.Columns.Add("Makulerad", fivePercentWidth * 5);
			Refresh();
		}

		private void PopulateOrdersIds() {
			foreach(var orderId in orderIdList) {
				var listItem = new ListViewItem { Tag = orderId, Text = orderId.ToString() };
				listItem.SubItems.Add("Nej");
				listItem.SubItems.Add("");
				listItem.SubItems.Add("");
				orderList.Items.Add(listItem);
				Refresh();
			}
			if (orderIdList.Count != 0) return;
			btnFetch.Enabled = false;
			btnCopyToClipBoard.Enabled = false;
		}

		private void PopulateOrderInformation(out bool foundUpdates) {
			foundUpdates = false;
			Cursor = Cursors.WaitCursor;
			var spcsHandler = ClientUtility.GetAdkHandler();
			try {
				spcsHandler.OpenCompany();
				foreach (ListViewItem listItem in orderList.Items) {
					var spcsOrderid = Convert.ToDouble(listItem.Tag);
					var rowUpdated = SetRowOrderInformation(spcsHandler, listItem, spcsOrderid);
					foundUpdates = foundUpdates | rowUpdated;
					Refresh();
				}
			}
			catch (Exception ex) {
				btnCopyToClipBoard.Enabled = false;
				btnFetch.Enabled = false;
				Cursor = Cursors.Default;
				SetFormStatus(Enumeration.ConnectionStatus.Disconnected, "Fel uppstod vid hämtning av faktura-status");
				ClientUtility.DisplayErrorMessage(ex.Message);
			}
			finally {
				Cursor = Cursors.Default;
				spcsHandler.CloseCompany();
			}
		}
	}
}
