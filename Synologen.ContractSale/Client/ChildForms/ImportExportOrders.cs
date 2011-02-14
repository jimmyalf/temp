using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.Visma;
using Synologen.Client.Common;

namespace Synologen.Client.ChildForms {
	internal partial class ImportExportOrders : FormBase {
		private const int invoicedStatusColumn = 7;
		private const int spcsInvoiceNumberColumn = 1;
		private bool ordersHaveBeenFetchedFromWPC;
		private bool ordersHaveBeenImportedIntoSCPS;
		private IList<Order> ordersToImport = new List<Order>();

		public ImportExportOrders() {
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);
			InitializeOrderList();
		}

		private void InitializeOrderList() {
			orderList.Items.Clear();
			orderList.Columns.Clear();
			orderList.View = View.Details;
			orderList.LabelEdit = true;
			orderList.FullRowSelect = true;
			orderList.Columns.Add("WPC-Id");
			orderList.Columns.Add("SPCS-Id");
			orderList.Columns.Add("Kundföretag");
			orderList.Columns.Add("Kundnamn");
			orderList.Columns.Add("Skapad");
			orderList.Columns.Add("Säljande butik");
			orderList.Columns.Add("Butiksort");
			orderList.Columns.Add("Exporterad till WPC");
			ResizeOrderList();
		}

		private void ResizeOrderList() {
			var counter = 0;
			const int fixedNumberColumnSize = 70;
			var fivePercentWidth = (orderList.ClientSize.Width - (fixedNumberColumnSize * 5)) / 20;
			if(orderList.Columns.Count == 0)
				return;
			orderList.Columns[counter++].Width = (fixedNumberColumnSize);
			orderList.Columns[counter++].Width = (fixedNumberColumnSize);
			orderList.Columns[counter++].Width = (fivePercentWidth * 5);
			orderList.Columns[counter++].Width = (fivePercentWidth * 5);
			orderList.Columns[counter++].Width = (fixedNumberColumnSize);
			orderList.Columns[counter++].Width = (fivePercentWidth * 6);
			orderList.Columns[counter++].Width = (fivePercentWidth * 4);
			orderList.Columns[counter].Width = (fixedNumberColumnSize*2);
		}

		private void PopulateOrders() {
			var orders = ordersToImport;
			foreach(var order in orders) { PopulateSingleOrderListItem(order); }
			if(orders.Count != 0)
				return;
			DisableButtons();
			MessageBox.Show("Inga nya ordrar hittades.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void PopulateSingleOrderListItem(IOrder order) {
			var listItem = new ListViewItem { Tag = order, Text = order.Id.ToString() };
			listItem.SubItems.Add("");
			listItem.SubItems.Add(order.ContractCompany.Name);
			listItem.SubItems.Add(order.CustomerFirstName + " " + order.CustomerLastName);
			listItem.SubItems.Add(order.CreatedDate.ToShortDateString());
			listItem.SubItems.Add(order.SellingShop.Name);
			listItem.SubItems.Add(order.SellingShop.City);
			listItem.SubItems.Add("Nej");
			listItem.SubItems.Add("Nej");
			orderList.Items.Add(listItem);
		}

		private void DisableButtons() {
			btnFetchOrders.Enabled = false;
			btnCopyToClipBoard.Enabled = false;
		}

		private void HandleInvoiceImportExport() {
			DisableButtons();
			try {
				CreateInvoicesInSPCS(orderList.Items);
				InvoiceOrders(orderList.Items);
				btnFetchOrders.Enabled = false;
			}
			catch(Exception ex) {
				SetFormStatus(Enumeration.ConnectionStatus.Disconnected, "Ett fel uppstod under fakturaimport. Var god kontakta systemadministratör.");
				ClientUtility.TryLogError("Client Got Exception while creating invoices", ex);
				ClientUtility.DisplayErrorMessage(ex);
			}
			finally {
				btnCopyToClipBoard.Enabled = true;
			}
		}

		private void CreateInvoicesInSPCS(ListView.ListViewItemCollection listItems) {
			var spcsHandler = ClientUtility.GetAdkHandler();
			var client = ClientUtility.GetWebClient();
			spcsHandler.OpenCompany();
			client.Open();
			foreach(ListViewItem listItem in listItems) {
				double invoiceSumIncludingVAT;
				double invoiceSumExcludingVAT;
				var order = (Order) listItem.Tag;
				var SPCSNumber = ImportSingleOrderToSPCS(spcsHandler, order, out invoiceSumIncludingVAT, out invoiceSumExcludingVAT);
				ExportInvoiceNumberToWPC(client, order, SPCSNumber, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
				listItem.SubItems[spcsInvoiceNumberColumn].Text = SPCSNumber.ToString();
				listItem.SubItems[invoicedStatusColumn].Text = "Ja";
				orderList.Refresh();
			}
			client.Close();
			spcsHandler.CloseCompany();
		}

		#region Actions

		private static int ImportSingleOrderToSPCS(AdkHandler spcsHandler, IOrder order, out double invoiceSumIncludingVAT, out double invoiceSumExcludingVAT) {
		    var SPCSNumber = ClientUtility.ImportOrderToSPCS(spcsHandler, order, out invoiceSumIncludingVAT, out invoiceSumExcludingVAT);
		    return SPCSNumber;
		}

		private static void ExportInvoiceNumberToWPC(ClientContract client, IOrder order, int SPCSNumber, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT) {
			//var order = (IOrder)listItem.Tag;
			ClientUtility.SetSPCSOrderInvoiceNumber(client, order.Id, SPCSNumber, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
		}

		private void InvoiceOrders(ListView.ListViewItemCollection listItems){
		    var orderItems = new List<Order>();
		    foreach (ListViewItem listItem in listItems){
		        orderItems.Add((Order) listItem.Tag);
		    }
			var backgroundWorker = new BackgroundWorker {WorkerReportsProgress = true, WorkerSupportsCancellation = true};
			backgroundWorker.DoWork += backgroundworker_DoWork;
			backgroundWorker.RunWorkerCompleted += backgroundworker_RunWorkerCompleted;
			backgroundWorker.RunWorkerAsync(orderItems);
		}

		private static void backgroundworker_DoWork (object sender, DoWorkEventArgs e) {
			var orderItems = e.Argument as List<Order>;
			if (orderItems == null) return;
			MessageBox.Show("Fakturering kan nu påbörjas. Tryck OK för att påbörja faktureringen.");
			var client = ClientUtility.GetWebClient();
			ClientUtility.SendInvoices(client, orderItems.Select(x => x.Id).ToList());
			client.Close();
			MessageBox.Show("Fakturering är klar. Ett statusmeddelande skickas till angiven epostadress.");
		}
 
		private void backgroundworker_RunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e) {
			if (e.Cancelled) MessageBox.Show("User has cancelled background thread!");
			else if (e.Error != null) throw e.Error;
			else SetFormStatus(Enumeration.ConnectionStatus.Connected, "Fakturaimport klar");
		}

		private void FetchNewOrders() {
			Cursor = Cursors.WaitCursor;
			try {
				SetFormStatus(Enumeration.ConnectionStatus.Connecting, "Ansluter...");
				ordersToImport = ClientUtility.GetOrderListFromWebService();
				SetFormStatus(Enumeration.ConnectionStatus.Connected, "Ansluten");
			}
			catch(Exception ex) {
				Cursor = Cursors.Default;
				SetFormStatus(Enumeration.ConnectionStatus.Disconnected, "Anslutning mot WPC misslyckades");
				ClientUtility.TryLogError("GotException while trying to fetch orders from Webservice", ex);
				ClientUtility.DisplayErrorMessage(ex.Message);
			}
			finally {
				Cursor = Cursors.Default;
			}
		}
		#endregion

		#region Events

		private void btnCopyToClipBoard_Click(object sender, EventArgs e) {
			CopyListViewToClipboard(orderList);
		}

		private void btnFetchOrders_Click(object sender, EventArgs e) {
			if(!ordersHaveBeenFetchedFromWPC) {
				FetchNewOrders();
				PopulateOrders();
				btnFetchOrders.Text = "Importera ordrar till SPCS och fakturera";
				ordersHaveBeenFetchedFromWPC = true;
			}
			else if (!ordersHaveBeenImportedIntoSCPS){
				if(!ClientUtility.CheckSPCSConnection()) {
					SetFormStatus(Enumeration.ConnectionStatus.Disconnected, "Anslutning mot SPCS misslyckdes");
					return;
				}
				SetFormStatus(Enumeration.ConnectionStatus.Connecting, "Importerar fakturor...");
				Cursor = Cursors.WaitCursor;
				HandleInvoiceImportExport();
				ordersHaveBeenImportedIntoSCPS = true;
				Cursor = Cursors.Default;
			}
		}

		private void ImportOrders_Resize(object sender, EventArgs e) {
			ResizeOrderList();
		}

		#endregion

	}
}