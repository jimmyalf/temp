using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.LensSubscription.Autogiro.Security;

namespace Synologen.LensSubscription.Autogiro.Test.TestHelpers
{
	[TestFixture]
	public abstract class HMACMessageDigestServiceTestBase
	{
		protected Func<IHashService,string> Because;
		protected Action Context;
		protected Func<string> SetupKey;
		protected string Key { get; private set;}
		protected string MessageDigest { get; private set;}

		protected HMACMessageDigestServiceTestBase()
		{
			Context = () => { };
			Because = service => { throw new AssertionException("No Action set for Because"); };
			SetupKey = () => { throw new AssertionException("No Action setup for SetupKey"); };
		}

		[SetUp]
		public void SetUp()
		{
			Context.Invoke();
			Key = SetupKey.Invoke();
			MessageDigest = Because.Invoke(GetService(Key));
		}

		[TearDown]
		public void TearDown()
		{
			
		}

		protected IHashService GetService(string key)
		{
			return new HMACHashService(key);
		}
	}
}