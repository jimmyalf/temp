using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class SelectedArticleTypeEventArgs : EventArgs
    {
        public int SelectedArticleTypeId { get; set; }
        public int SelectedCategoryId { get; set; }

        public SelectedArticleTypeEventArgs(int selectedArticleTypeId, int selectedCategoryId)
        {
            SelectedArticleTypeId = selectedArticleTypeId;
            SelectedCategoryId = selectedCategoryId;
        }
    }
}