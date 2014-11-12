using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Test.Factory;

namespace Spinit.Wpc.Synologen.Invoicing.Test.Svefaktura.DataParsing
{
    [TestFixture]
    public class Parse_Validation
    {
        private Order _order;
        private SvefakturaConversionSettings _settings;

        [SetUp]
        public void Setup()
        {
            _order = Factory.GetOrder();
            _settings = new SvefakturaConversionSettings();
        }

        [Test]
        public void Test_Create_Invoice_Parameter_Checks_Invoice_Missing() 
        {
            Assert.Throws<ArgumentNullException>(() => General.CreateInvoiceSvefaktura(null, _settings));
        }

        [Test]
        public void Test_Create_Invoice_Parameter_Checks_Settings_Missing() 
        {
            Assert.Throws<ArgumentNullException>(() => General.CreateInvoiceSvefaktura(_order, null));
        }
    }
}