using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Business.Domain.Entities{
	public class Contract : IContract{
		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Address { get; set; }
		public string Address2 { get; set; }
		public string Zip { get; set; }
		public string City { get; set; }
		public string Phone { get; set; }
		public string Phone2 { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
        public bool Active { get; set; }

		public bool DisableInvoice { get; set; }
	}
}