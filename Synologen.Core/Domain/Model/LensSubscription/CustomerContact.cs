namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class CustomerContact
	{
		public string Email { get; set; }
		public string MobilePhone { get; set; }
		public string Phone { get; set; }

		public override bool Equals(object obj)
		{
			var entity = obj as CustomerContact;
			return entity != null
			       && Equals(Email, entity.Email)
			       && Equals(MobilePhone, entity.MobilePhone)
			       && Equals(Phone, entity.Phone);
		}
	}
}