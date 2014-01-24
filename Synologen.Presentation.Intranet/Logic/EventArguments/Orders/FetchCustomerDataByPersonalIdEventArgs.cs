using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class FetchCustomerDataByPersonalIdEventArgs : EventArgs
    {
        public string PersonalIdNumber { get; set; }
    }
}