using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
    public class SelectedSomethingEventArgs : EventArgs
    {
        public int SelectedCategoryId { get; set; }
        public int SelectedArticleTypeId { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedArticleId { get; set; }

        public SelectedSomethingEventArgs(
            int selectedCategoryId = 0,
            int selectedArticleTypeId = 0,
            int selectedSupplierId = 0,
            int selectedArticleId = 0)
        {
            SelectedCategoryId = selectedCategoryId;
            SelectedArticleTypeId = selectedArticleTypeId;
            SelectedSupplierId = selectedSupplierId;
            SelectedArticleId = selectedArticleId;
        }
    }
}