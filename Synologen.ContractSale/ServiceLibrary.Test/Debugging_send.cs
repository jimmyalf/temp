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
        private readonly SqlProvider _provider;
        private readonly ISynologenService _service;
        private readonly string _reportEmail;

        public Debugging_send()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["WpcServer"].ConnectionString;
            _provider = new SqlProvider(connectionString);
            _service = new SynologenService(_provider);
            _reportEmail = "carl@carl-berg.se";
        }

        [Test, Explicit]
        public void Send_SR_Invoice()
        {
            _service.SendInvoices(new List<int> { 3886 }, _reportEmail);
        }

        [Test, Explicit]
        public void Send_SAAB_Invoice()
        {
            _service.SendInvoices(new List<int> { 3885 }, _reportEmail);
        }

        [Test, Explicit]
        public void Send_Praktikertjänst_Invoice()
        {
            _service.SendInvoices(new List<int> { 3884 }, _reportEmail);
        }

        [Test, Explicit]
        public void Send_EDI_Invoice()
        {
            _service.SendInvoices(new List<int> { 3830 }, _reportEmail);
        }

        [Test, Explicit]
        public void Send_Letter_Invoice()
        {
            _service.SendInvoices(new List<int> { 3850 }, _reportEmail);
        }

        [Test, Explicit]
        public void Send_by_wcf_service()
        {
            var client = GetClient();
            //var listOfIds = new List<int> { 9089, 9090, 9091, 9092, 9093, 9094, 9095, 9096, 9097, 9098, 9099, 9100, 9101, 9102, 9103, 9108, 9109, 9110, 9111, 9112, 9113, 9114, 9115, 9117 };
            var listOfIds = new List<int> { 9108 };

            client.SendInvoices(listOfIds, "carl@carl-berg.se");
        }

        protected ClientContract GetClient()
        {
            var endpoint = ConfigurationManager.AppSettings["SelectedServiceEndPointName"];
            var userName = ConfigurationManager.AppSettings["ClientCredentialUserName"];
            var password = ConfigurationManager.AppSettings["ClientCredentialPassword"];
            return new ClientContract(endpoint, userName, password);
        }
    }
}