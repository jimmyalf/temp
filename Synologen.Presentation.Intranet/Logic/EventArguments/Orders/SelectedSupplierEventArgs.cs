using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders
{
	public class SelectedSupplierEventArgs : EventArgs
	{
		public int SupplierId { get; set; }

		public SelectedSupplierEventArgs(int supplierId)
		{
			SupplierId = supplierId;
		}
	}
}