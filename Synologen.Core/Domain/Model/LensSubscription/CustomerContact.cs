namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class CustomerContact
	{
		public virtual string Email { get; set; }
		public virtual string MobilePhone { get; set; }
		public virtual string Phone { get; set; }

		public override bool Equals(object obj)
		{
			var entity = obj as CustomerContact;
			return Equals(entity);
		}

		public virtual bool Equals(CustomerContact other)
		{
			return other != null
			       && Equals(Email, other.Email)
			       && Equals(MobilePhone, other.MobilePhone)
			       && Equals(Phone, other.Phone);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				int result = (Email != null ? Email.GetHashCode() : 0);
				result = (result * 397) ^ (MobilePhone != null ? MobilePhone.GetHashCode() : 0);
				result = (result * 397) ^ (Phone != null ? Phone.GetHashCode() : 0);
				return result;
			}
		}
	}
}