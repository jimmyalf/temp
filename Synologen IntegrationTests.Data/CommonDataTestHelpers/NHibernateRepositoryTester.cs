using System;
using NHibernate;
using NUnit.Framework;

namespace Spinit.Wpc.Synologen.Integration.Test.CommonDataTestHelpers
{
	public abstract class NHibernateRepositoryTester<TRepository>
	{
		protected NHibernateRepositoryTester()
		{
			Context = (ISession session) => { };
			Because = (TRepository session) =>
			{
				throw new AssertionException("An action for Because has not been set!");
			};
		}

		[SetUp]
		protected void SetUpTest()
		{
			SetUp().Invoke();

			ISession contextSession = null;
			try
			{
				using (contextSession = GetSessionFactory().OpenSession())
				{
					Context(contextSession);
				}
			}
			catch
			{
				if (contextSession != null)
				{
					contextSession.Dispose();
				}
				throw;
			}

			ISession becauseSession = null;
			try
			{
				using (becauseSession = GetSessionFactory().OpenSession())
				{
					var repository = CreateRepository(becauseSession);
					Because(repository);
				}
			}
			catch
			{
				if (becauseSession != null)
				{
					becauseSession.Dispose();
				}
				throw;
			}
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
		
		protected Action<ISession> Context;
		protected Action<TRepository> Because;

		protected abstract ISessionFactory GetSessionFactory();
		
		protected virtual TRepository CreateRepository(ISession session)
		{
			var args = new object[] { session };
			return (TRepository)Activator.CreateInstance(typeof(TRepository), args);
		}

		protected void AssertUsing(Action<ISession> action)
		{
			ISession verificationSession = null;
			try
			{
				using (verificationSession = GetSessionFactory().OpenSession())
				{
					action(verificationSession);
				}
			}
			catch
			{
				if (verificationSession != null)
				{
					verificationSession.Dispose();
				}
				throw;
			}
		}

		protected TResult GetResult<TResult>(Func<ISession, TResult> function)
		{
			ISession verificationSession = null;
			TResult result;
			try
			{
				using (verificationSession = GetSessionFactory().OpenSession())
				{
					result = function(verificationSession);
				}
			}
			catch
			{
				if (verificationSession != null)
				{
					verificationSession.Dispose();
				}
				throw;
			}
			return result;
		}
	}
}