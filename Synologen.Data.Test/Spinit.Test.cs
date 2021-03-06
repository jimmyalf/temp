﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Spinit.Test
{
	public abstract class CommonTestbase
	{
		public virtual Exception TryGetException(Action action)
		{
			try
			{
				action.Invoke();
			}
			catch(Exception ex)
			{
				return ex;
			}
			return null;
		}

		protected virtual void SetUp(){ }

		protected virtual void TearDown(){ }
	}

	public abstract class BehaviorActionTestbase : CommonTestbase
	{
		protected BehaviorActionTestbase()
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

	public abstract class BehaviorActionTestbase<TTestEntity> : BehaviorActionTestbase
	{
		protected TTestEntity TestEntity;
		protected BehaviorActionTestbase()
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
			ExecuteBecause();
		}

		protected override void ExecuteBecause()
		{
			Because(TestEntity);
		}

		protected abstract TTestEntity GetTestEntity();

		protected new Action<TTestEntity> Because { get; set;}
	}

	public abstract class BehaviorActionTestbase<TTestEntity,TContextModel> : BehaviorActionTestbase<TTestEntity>
	{
		protected BehaviorActionTestbase()
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
