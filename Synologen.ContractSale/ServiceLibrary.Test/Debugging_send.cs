using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;
using Synologen.Service.Client.Invoicing.App;
using Synologen.Service.Web.Invoicing;

namespace Spinit.Wpc.Synologen.Integration.Services.Test
{
    [TestFixture, Explicit]
    public class Debugging_send
    {
        private readonly string _reportEmail;

        public Debugging_send()
        {
            _reportEmail = "carl.berg@spinit.se";
        }

        [Test, Explicit]
        public void Send_SAAB_invoice_by_wcf_service()
        {
            var client = GetClient();
            var listOfIds = new List<int> { 8432 };

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