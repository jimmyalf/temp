using System;
using NUnit.Framework;

namespace Synologen.Test.Core
{
	public abstract class BehaviorFunctionTestbase<TEntityActionReturnValue>
	{
		protected BehaviorFunctionTestbase()
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
			ReturnValue = ExecuteBecause();
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
		protected virtual TEntityActionReturnValue ExecuteBecause()
		{
			return Because();
		}

		protected Action Context;
		protected Func<TEntityActionReturnValue> Because;
		protected TEntityActionReturnValue ReturnValue;
			
	}

	public abstract class BehaviorFunctionTestbase<TTestEntity,TEntityActionReturnValue> : BehaviorFunctionTestbase<TEntityActionReturnValue>
	{
		protected TTestEntity TestEntity;
		protected BehaviorFunctionTestbase()
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
			TestEntity = GetTestEntity();
			ReturnValue = ExecuteBecause();
		}

		protected override TEntityActionReturnValue ExecuteBecause()
		{
			return Because(TestEntity);
		}

		protected abstract TTestEntity GetTestEntity();

		protected new Func<TTestEntity,TEntityActionReturnValue> Because { get; set;}
	}

	public abstract class BehaviorFunctionTestbase<TTestEntity,TEntityActionReturnValue,TContextModel> : BehaviorFunctionTestbase<TTestEntity,TEntityActionReturnValue>
	{
		protected BehaviorFunctionTestbase()
		{
			Context = parameter => { };
			Because = parameter =>
			{
				throw new AssertionException("An action for Because has not been set!");
			};
		}

		protected override void ExecuteContext()
		{
			Context(GetContextModel());
		}

		protected abstract TContextModel GetContextModel();

		protected new Action<TContextModel> Context { get; set;}
	}
}