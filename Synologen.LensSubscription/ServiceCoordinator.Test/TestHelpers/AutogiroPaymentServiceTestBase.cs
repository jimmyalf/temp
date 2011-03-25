using System;
using FakeItEasy;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.ServiceCoordinator.App;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.ServiceCoordinator.Test.TestHelpers
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