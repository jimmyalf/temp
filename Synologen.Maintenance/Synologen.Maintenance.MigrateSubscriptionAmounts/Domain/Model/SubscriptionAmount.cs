namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
	public class SubscriptionAmount
	{
		public decimal? Taxed { get; set; }
		public decimal? TaxFree { get; set; }
		public decimal? Total
		{
			get
			{
				if (Taxed.HasValue && TaxFree.HasValue) return Taxed + TaxFree;
				if (Taxed.HasValue) return Taxed;
				if (TaxFree.HasValue) return TaxFree;
				return null;
			}
		}

		public bool Equals(SubscriptionAmount other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Taxed.Equals(Taxed) && other.TaxFree.Equals(TaxFree);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (SubscriptionAmount)) return false;
			return Equals((SubscriptionAmount) obj);
		}

		public static bool operator == (SubscriptionAmount first, SubscriptionAmount second)
		{
			if(Equals(first,null)) return false;
			if(Equals(second,null)) return false;
			return first.Equals(second);
		}

		public static bool operator != (SubscriptionAmount first, SubscriptionAmount second)
		{
			return !(first == second);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Taxed.HasValue ? Taxed.Value.GetHashCode() : 0) * 397) ^ (TaxFree.HasValue ? TaxFree.Value.GetHashCode() : 0);
			}
		}

		public override string ToString()
		{
			return "{Taxed = " + Taxed + ", TaxFree = " + TaxFree + "}";
		}
	}
}