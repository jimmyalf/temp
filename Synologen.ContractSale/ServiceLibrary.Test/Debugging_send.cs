using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;
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
            _provider = new SqlProvider(@"Initial Catalog=dbWpcSynologen;Data Source=demo01.hotel.se\SQL2008;uid=syn-demo;pwd=vt87VUGsEF;Pooling=true;Connect Timeout=15;");
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
    }
}