namespace Spinit.Wpc.Synologen.EDI.Common.Types {
	public class Contact {
		public string ContactInfo { get; set; }
		public string Telephone { get; set; }
		public string Fax { get; set; }
		public string Email { get; set; }
		public bool IsEmpty {
			get {
				return (ContactInfo + Telephone + Fax + Email).Length <= 0;
			}
		}
	}
}