using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class SelectedArticleTypeEventArgs : EventArgs
    {
        public int SelectedArticleTypeId { get; set; }

        public SelectedArticleTypeEventArgs(int selectedArticleTypeId)
        {
            SelectedArticleTypeId = selectedArticleTypeId;
        }
    }
}