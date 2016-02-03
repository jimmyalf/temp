using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Invoicing;
using Synologen.Service.Client.Invoicing.App;

namespace Spinit.Wpc.Synologen.Integration.Services.Test
{
    [TestFixture, Explicit]
    public class Debugging_send
    {
        private readonly string _reportEmail;

        public Debugging_send()
        {
            _reportEmail = "martin.svensson@spinit.se";
        }

        [Test, Explicit]
        public void Send_SAAB_invoice_by_wcf_service()
        {
            var client = GetClient();
            var listOfIds = new List<int> { 8432 };

            client.SendInvoices(listOfIds, _reportEmail);
        }

        [Test, Explicit]
        public void Send_FTP_Invoice()
        {
            var client = GetClient();
            var listOfIds = new List<int> { 17793 };

            client.SendInvoices(listOfIds, _reportEmail);
        }

        [Test, Explicit]
        public void Send_NoOp_service()
        {
            var client = GetClient();
            var listOfIds = new List<int> { 10033 };

            client.SendInvoices(listOfIds, _reportEmail);
        }

        [Test, Explicit]
        public void Send_PDF_Invoice_service()
        {
            var client = GetClient();
            var listOfIds = new List<int> { 13944 };

            client.SendInvoices(listOfIds, _reportEmail);
        }

        [Test, Explicit]
        public void Send_SRF_invoice_by_wcf_service()
        {
            var client = GetClient();
            var listOfIds = new List<int> { 8728 };

            client.SendInvoices(listOfIds, _reportEmail);
        }

        [Test, Explicit]
        public void Send_both_invoices_by_wcf_service()
        {
            var client = GetClient();
            var listOfIds = new List<int> { 8432, 8728 };

            client.SendInvoices(listOfIds, _reportEmail);
        }

        //[Test, Explicit]
        //public void Send_LIVE_invoices_by_wcf_service()
        //{
        //    var client = GetLiveClient();
        //    var listOfIds = new List<int> { 9210, 9215, 9217, 9231 };

        //    client.SendInvoices(listOfIds, _reportEmail);
        //}

        //[Test, Explicit]
        //public void Send_Problematic_invoices_by_wcf_service()
        //{
        //    var client = GetClient();
        //    var listOfIds = new List<int> { 9567, 9571, 9572, 9574, 9575, 9576, 9577, 9579, 9580, 9581, 9582, 9583, 9584, 9585, 9586, 9587, 9588, 9589, 9590, 9591, 9592, 9593, 9594, 9596, 9597, 9598, 9599, 9600, 9601, 9602, 9605, 9606, 9607 };

        //    client.SendInvoices(listOfIds, _reportEmail);
        //}

        protected ClientContract GetClient()
        {
            var endpoint = ConfigurationManager.AppSettings["SelectedServiceEndPointName"];
            var userName = ConfigurationManager.AppSettings["ClientCredentialUserName"];
            var password = ConfigurationManager.AppSettings["ClientCredentialPassword"];
            return new ClientContract(endpoint, userName, password);
        }

        //protected ClientContract GetLiveClient()
        //{
        //    var endpoint = "liveEncrypted";
        //    var userName = ConfigurationManager.AppSettings["ClientCredentialUserName"];
        //    var password = ConfigurationManager.AppSettings["ClientCredentialPassword"];
        //    return new ClientContract(endpoint, userName, password);
        //}
    }
}