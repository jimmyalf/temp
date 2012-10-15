using System;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Enumerations
{
	public class SubscriptionType : Enumeration<SubscriptionType>
	{
		public static SubscriptionType Ongoing = new SubscriptionType(-2, "Löpande");
		public static SubscriptionType ThreeMonths = new SubscriptionType(3, "3 Månader");
		public static SubscriptionType SixMonths = new SubscriptionType(6, "6 Månader");
		public static SubscriptionType TwelveMonths = new SubscriptionType(12, "12 månader");
		public static SubscriptionType CustomNumberOfWithdrawals = new SubscriptionType(-1, "Valfritt");

		public SubscriptionType(int value, string displayName) :base(value,displayName)
		{
		}

		public SubscriptionType SetCustomNumberOfWithdrawals(int numberOfWithdrawals)
		{
			SelectedCustomNumberOfWithdrawals = numberOfWithdrawals;
			return this;
		}

		public static SubscriptionType GetFromWithdrawalsLimit(int? numberOfWithdrawals)
		{
			if(numberOfWithdrawals == null) return Ongoing;
			foreach (var type in GetAll())
			{
				if (type.Value == numberOfWithdrawals) return type;
			}
			return CustomNumberOfWithdrawals.SetCustomNumberOfWithdrawals(numberOfWithdrawals.Value);
		}

		public int GetNumberOfWithdrawals()
		{
			if(SelectedCustomNumberOfWithdrawals.HasValue) return SelectedCustomNumberOfWithdrawals.Value;
			if(Value <= 0) throw new ApplicationException("Cannot retrieve a valid number of withdrawals");
			return Value;
		}

		public int? SelectedCustomNumberOfWithdrawals { get; private set; }
		public bool HasCustomNumberOfWithdrawals { get { return SelectedCustomNumberOfWithdrawals.HasValue; } }
	}
}