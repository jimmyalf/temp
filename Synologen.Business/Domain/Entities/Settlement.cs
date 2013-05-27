using System;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class Settlement : ISettlement{
		public int Id { get; set; }
		public DateTime CreatedDate { get; set; }
		public int NumberOfConnectedOrders { get; set; }
	}
}