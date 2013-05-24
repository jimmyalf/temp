using System;
using FakeItEasy;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.Service.Client.SubscriptionTaskRunner.App;

namespace Synologen.Service.Client.SubscriptionTaskRunner.Test.TestHelpers
{
	public abstract class AutogiroPaymentServiceTestBase : BehaviorFunctionTestbase<AutogiroPaymentService,DateTime>
	{
		protected IServiceCoordinatorSettingsService ServiceCoordinatorSettingsService;
		protected override void SetUp()
		{
			base.SetUp();
			ServiceCoordinatorSettingsService = A.Fake<IServiceCoordinatorSettingsService>();
		}

		protected override AutogiroPaymentService GetTestEntity() 
		{ 
			return new AutogiroPaymentService(ServiceCoordinatorSettingsService);
		}
	}
}