using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
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

        [Test, Explicit]
        public void Send_by_wcf_service()
        {
		    const string userName = "synologen-client";
            const string password = "6m9M3v8";
            const string endpoint = "localEncrypted";
            var client = new ClientContract(endpoint, userName, password);
            var listOfIds = new List<int> { 832, 833, 834, 837, 842, 843, 846, 847, 848 };
            client.SendInvoices(listOfIds, "carl@carl-berg.se");
        }
    }

    //public class TestClient : ClientBase<ISynologenService>, ISynologenService
    //{
    //    public TestClient(string endpointConfigurationName, string username, string password) : base(endpointConfigurationName)
    //    {
    //        ClientCredentials.UserName.UserName = username;
    //        ClientCredentials.UserName.Password = password;
    //    }
    //    public List<Order> GetOrdersForInvoicing()
    //    {
    //        return Channel.GetOrdersForInvoicing();
    //    }

    //    public void SetOrderInvoiceNumber(int orderId, long newInvoiceNumber, double invoiceSumIncludingVAT, double invoiceSumExcludingVAT)
    //    {
    //        Channel.SetOrderInvoiceNumber(orderId, newInvoiceNumber, invoiceSumIncludingVAT, invoiceSumExcludingVAT);
    //    }

    //    public int LogMessage(LogType logType, string message)
    //    {
    //        return Channel.LogMessage(logType, message);
    //    }

    //    public List<long> GetOrdersToCheckForUpdates()
    //    {
    //        return Channel.GetOrdersToCheckForUpdates();
    //    }

    //    public void UpdateOrderStatuses(long invoiceNumber, bool invoiceIsCanceled, bool invoiceIsPayed)
    //    {
    //        Channel.UpdateOrderStatuses(invoiceNumber, invoiceIsCanceled, invoiceIsPayed);
    //    }

    //    public void SendInvoices(List<int> orderIds, string statusReportEmailAddress)
    //    {
    //        Channel.SendInvoices(orderIds, statusReportEmailAddress);
    //    }

    //    public void SendEmail(string @from, string to, string subject, string message)
    //    {
    //        Channel.SendEmail(from, to, subject, message);
    //    }
    //}
}