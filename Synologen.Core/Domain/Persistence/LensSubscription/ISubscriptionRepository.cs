using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription
{
	public interface ISubscriptionRepository : IRepository<Subscription> 
	{
		Subscription GetByBankgiroPayerId(int bankgiroPayerId);
	}
}