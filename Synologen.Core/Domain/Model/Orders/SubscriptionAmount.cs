using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders
{
	public class SubscriptionAmount
	{
		public SubscriptionAmount() { }
		public SubscriptionAmount(decimal taxedAmount, decimal taxFreeAmount)
		{
			Taxed = taxedAmount;
			TaxFree = taxFreeAmount;
		}
		public virtual decimal TaxFree { get; set; }
		public virtual decimal Taxed { get; set; }
		public virtual decimal Total
		{
			get { return TaxFree + Taxed; }
		}

		public virtual bool Equals(SubscriptionAmount other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Taxed == Taxed && other.TaxFree == TaxFree;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (SubscriptionAmount)) return false;
			return Equals((SubscriptionAmount) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Taxed.GetHashCode() * 397) ^ TaxFree.GetHashCode();
			}
		}

		public static bool operator == (SubscriptionAmount first, SubscriptionAmount second)
		{
			return Equals(first, second);
		}

		public static bool operator == (SubscriptionAmount amount, decimal value)
		{
			if(Equals(amount,null)) return false;
			return amount.Total == value;
		}

		public static bool operator != (SubscriptionAmount amount, decimal value)
		{
			return !(amount == value);
		}

		public static bool operator != (SubscriptionAmount first, SubscriptionAmount second)
		{
			return !(first == second);
		}
	}

	public static class SubscriptionAmountExtensions
	{
		public static SubscriptionAmount Sum(this IEnumerable<SubscriptionAmount> amounts)
		{
			if (amounts == null) return null;
			IList<SubscriptionAmount> list = amounts.ToList();
			var taxedAmount = list.Sum(x => x.Taxed);
			var taxFreeAmount = list.Sum(x => x.TaxFree);
			return new SubscriptionAmount(taxedAmount, taxFreeAmount);
		}
	}
}