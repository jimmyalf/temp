using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class SelectedSupplierEventArgs : EventArgs
	{
		public int SupplierId { get; set; }
	    public int ArticleTypeId { get; set; }

		public SelectedSupplierEventArgs(int supplierId, int articleTypeId)
		{
			SupplierId = supplierId;
		    ArticleTypeId = articleTypeId;
		}
	}
}