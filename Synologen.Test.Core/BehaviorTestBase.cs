using System;
using NUnit.Framework;

namespace Synologen.Test.Core
{
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
			SetUp();
			ExecuteContext();
			ExecuteBecause();
		}

		[TearDown]
		public void TearDownTest()
		{
			TearDown();
		}

		protected virtual void SetUp(){ }
		protected virtual void TearDown(){ }

		protected virtual void ExecuteContext()
		{
			Context();
		}
		protected virtual void ExecuteBecause()
		{
			Because();
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
			SetUp();
			ExecuteContext();
			TestModel = GetTestModel();
			ExecuteBecause();
		}

		protected override void ExecuteBecause()
		{
			Because(TestModel);
		}

		protected abstract TTestModel GetTestModel();

		protected TTestModel TestModel { get; private set;}

		protected new Action<TTestModel> Because { get; set;}
	}
}