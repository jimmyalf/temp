using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class SelectedArticleEventArgs : EventArgs
    {
        public int SelectedArticleId { get; set; }

        public SelectedArticleEventArgs(int selectedArticleId)
        {
            SelectedArticleId = selectedArticleId;
        }
    }
}