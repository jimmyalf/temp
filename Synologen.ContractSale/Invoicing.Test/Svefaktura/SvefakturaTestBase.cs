using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura
{
    public abstract class SvefakturaTestBase
    {
        protected SvefakturaTestBase()
        {
            Settings = Factory.GetSettings();
            Formatter = new SvefakturaFormatter();
        }

        protected SvefakturaFormatter Formatter { get; set; }
        protected SvefakturaConversionSettings Settings { get; set; }

        protected virtual SFTIInvoiceType BuildCompleteInvoice(IOrder order)
        {
            return Convert.ToSvefakturaInvoice(Settings, order);
        }

        protected virtual TBuilder GetBuilder<TBuilder>() where TBuilder : ISvefakturaBuilder
        {
            return (TBuilder)Activator.CreateInstance(typeof(TBuilder), Settings, Formatter);
        }

        protected virtual SFTIInvoiceType BuildInvoice(IOrder order, params ISvefakturaBuilder[] builders)
        {
            var invoice = new SFTIInvoiceType();
            foreach (var builder in builders)
            {
                builder.Build(order, invoice);
            }

            return invoice;
        }

        protected virtual SFTIInvoiceType BuildInvoice<TBuilder>(IOrder order) where TBuilder : ISvefakturaBuilder
        {
            var invoice = new SFTIInvoiceType();
            var builder = GetBuilder<TBuilder>();
            builder.Build(order, invoice);
            return invoice;
        }
    }
}