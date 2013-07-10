using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Integration.Services.Test;
using Spinit.Wpc.Synologen.Invoicing;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.SvefakturaBuilders;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.ServiceLibrary;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Synologen.Service.Client.Invoicing.App;

namespace Spinit.Wpc.Synologen.Integration.Test.Webservice{
	[TestFixture, Explicit]
	public class TestWebService{
		private ClientContract client;
		private const string connectionString = "Initial Catalog=dbWpcSynologen;Data Source=BLACK;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";

		[TestFixtureSetUp]
		//TODO: Make into integrationtest
		public void Setup() {
			client =  new ClientContract();
			client.ClientCredentials.UserName.UserName = Business.Utility.Configuration.Common.ClientCredentialUserName;
			client.ClientCredentials.UserName.Password = Business.Utility.Configuration.Common.ClientCredentialPassword;
			client.Open();
		}

		[TestFixtureTearDown]
		public void TearDown() {
			client.Close();
		}

		[Test]
		public void OfflineGetOrdersForInvoicing(){
			var service = new SynologenService(new SqlProvider(connectionString));
			var orders = service.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		public void OfflineGetOrderItems(){
			var service = new SynologenService(new SqlProvider(connectionString));
			var orders = service.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
			foreach (var order in orders){
				Assert.IsNotNull(order.OrderItems);
				Assert.LessOrEqual(0, order.OrderItems.Count);
			}
		}

		[Test]
		public void WebServiceGetOrdersForInvoicing(){
			var orders = client.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		public void WebServiceGetOrdersToCheckForUpdates(){
			var orders = client.GetOrdersToCheckForUpdates();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
		}

		[Test]
		public void WebServiceGetOrderItems(){
			var orders = client.GetOrdersForInvoicing();
			Assert.IsNotNull(orders);
			Assert.LessOrEqual(0, orders.Count);
			foreach (var order in orders){
				Assert.IsNotNull(order.OrderItems);
				Assert.LessOrEqual(0, order.OrderItems.Count);
			}
		}

		[Test, Explicit("Does not need constant testing")]
		public void SendEmail() {
			client.SendEmail(
				"info@spinit.se", 
				"carl.berg@spinit.se", 
				"Automated test email",     
				"Testmail från Synologen Test-projekt.");

		}
	}

    [TestFixture, Explicit]
    public class Debugging
    {
        [Test, Explicit]
        public void Debugging_Test()
        {
            const string connectionString = @"Initial Catalog=dbWpcSynologen;Data Source=SYNOLOGEN01\SQL2008;uid=synologen_db_user;pwd=kxqjezWN7;Pooling=true;Connect Timeout=15;";
            var provider = new SqlProvider(connectionString);
            var settings = TestInvoiceParsingAndValidation.GetSettings();
            var builder = new SvefakturaBuilder(new SvefakturaFormatter(), settings, new SvefakturaBuilderBuilderValidator());

            var order = provider.GetOrder(8559);
            var invoice = builder.Build(order);
            var text = SvefakturaSerializer.Serialize(invoice, Encoding.UTF8, Environment.NewLine, Formatting.Indented, null);
            var file = new FileInfo(@"C:\Users\cber\Desktop\Generated_Test_Invoice.xml");
            if (file.Exists)
            {
                file.Delete();
            }

            using (var streamWriter = file.CreateText())
            {
                streamWriter.Write(text);
            }

            //const string connectionString = @"Initial Catalog=dbWpcSynologen;Data Source=TEAL;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";
            //var provider = new SqlProvider(connectionString);
            //var invoiceList = new List<SFTIInvoiceType>();
            //var settings = TestInvoiceParsingAndValidation.GetSettings();
            //for(var i = 4039; i <= 4074; i++)
            //{
            //    var order = provider.GetOrder(i);
            //    var invoice = General.CreateInvoiceSvefaktura(order, settings);
            //    invoiceList.Add(invoice);
            //}
            //foreach (var invoice in invoiceList)
            //{
            //    var ruleViolations = SvefakturaValidator.ValidateObject(invoice);
            //    if(ruleViolations.Any())
            //    {
            //        Console.WriteLine(ruleViolations);
            //    }
            //}

        }        
    }
}