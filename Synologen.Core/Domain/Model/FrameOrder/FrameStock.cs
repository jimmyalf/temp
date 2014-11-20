using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder
{
	public class FrameStock
	{
		public virtual int StockAtStockDate { get; set; }
		public virtual DateTime StockDate { get; set; }
		public virtual int CurrentStock { get; private set; }
	}
}