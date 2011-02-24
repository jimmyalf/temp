using System;
using FakeItEasy;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.BGServiceCoordinator.App.Services;

namespace Synologen.LensSubscription.BGService.Test.BaseHelpers
{
	public abstract class BGSentFileWriterServiceTestBase : BehaviorTestBase<BGSentFileWriterService>
	{
		protected IFileIOService FileIOService;
		protected IBGConfigurationSettingsService BGConfigurationSettingsService;

		protected BGSentFileWriterServiceTestBase()
		{
			FileIOService = A.Fake<IFileIOService>();
			BGConfigurationSettingsService = A.Fake<IBGConfigurationSettingsService>();
		}
		
		protected override BGSentFileWriterService GetTestModel()
		{
			return new BGSentFileWriterService(FileIOService, BGConfigurationSettingsService);
		}
	}

	#region Generic test classes to be extracted

	public abstract class BehaviorTestBase
	{
		protected BehaviorTestBase()
		{
			Context = () => { };
			Because = () =>
			{
				throw new AssertionException("An action for Because has not been set!");
			};
		}

		[SetUp]
		protected virtual void SetUpTest()
		{
			SetUp().Invoke();
			Context();
			Because();
		}

		[TearDown]
		public void TearDownTest()
		{
			TearDown().Invoke();
		}

		protected virtual Action SetUp()
		{
			return () => { };
		}

		protected virtual Action TearDown()
		{
			return () => { };
		}

		protected Action Context;
		protected Action Because;
			
	}

	public abstract class BehaviorTestBase<TTestModel> : BehaviorTestBase
	{
		protected BehaviorTestBase()
		{
			Context = () => { };
			Because = parameter =>
			{
				throw new AssertionException("An action for Because has not been set!");
			};
		}

		[SetUp]
		protected override void SetUpTest()
		{
			SetUp().Invoke();
			Context();
			TestModel = GetTestModel();
			Because(TestModel);
		}

		protected abstract TTestModel GetTestModel();

		protected TTestModel TestModel { get; private set;}

		protected new Action<TTestModel> Because { get; set;}
	}

	#endregion
}