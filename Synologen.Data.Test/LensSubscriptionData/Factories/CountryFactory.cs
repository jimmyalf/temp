using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;

namespace Spinit.Wpc.Synologen.Data.Test.LensSubscriptionData.Factories
{
	public static class CountryFactory
	{
		public static Country Get()
		{
			return new Country {Name = "Sverige"};
		}
	}
}