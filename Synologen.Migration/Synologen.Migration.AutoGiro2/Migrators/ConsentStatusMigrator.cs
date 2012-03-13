using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Synologen.Migration.AutoGiro2.Migrators
{
	public class ConsentStatusMigrator : IMigrator<SubscriptionConsentStatus,Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus>
	{
		public Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus GetNewEntity(SubscriptionConsentStatus oldEntity)
		{
			switch (oldEntity)
			{
				case SubscriptionConsentStatus.NotSent: return Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.NotSent;
				case SubscriptionConsentStatus.Sent: return Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.Sent;
				case SubscriptionConsentStatus.Accepted: return Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.Accepted;
				case SubscriptionConsentStatus.Denied: return Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes.SubscriptionConsentStatus.Denied;
				default: throw new ArgumentOutOfRangeException("oldEntity");
			}
		}
	}
}