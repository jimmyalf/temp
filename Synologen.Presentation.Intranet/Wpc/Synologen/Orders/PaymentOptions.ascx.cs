﻿using System;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
    [PresenterBinding(typeof(PaymentOptionsPresenter))]
    public partial class PaymentOptions : MvpUserControl<PaymentOptionsModel>, IPaymentOptionsView
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public event EventHandler<PaymentOptionsEventArgs> Submit;
    }
}