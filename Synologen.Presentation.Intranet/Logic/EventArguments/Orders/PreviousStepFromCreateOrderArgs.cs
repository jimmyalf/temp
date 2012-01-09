using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class PreviousStepFromCreateOrderArgs : EventArgs
    {
        public bool OrderExists { get; set; }
    }
}