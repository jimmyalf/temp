using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class SelectedCategoryEventArgs : EventArgs
	{
		public SelectedCategoryEventArgs(int selectedCategoryId)
		{
			SelectedCategoryId = selectedCategoryId;
		}
		public int SelectedCategoryId { get; set; }
	}
}