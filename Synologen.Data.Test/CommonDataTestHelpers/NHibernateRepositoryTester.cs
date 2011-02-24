using System;
using NHibernate;
using NUnit.Framework;
using Synologen.Test.Core;

namespace Spinit.Wpc.Synologen.Integration.Data.Test.CommonDataTestHelpers
{
	//public abstract class NHibernateRepositoryTester<TRepository>
	//{
	//    protected NHibernateRepositoryTester()
	//    {
	//        Context = (ISession session) => { };
	//        Because = (TRepository session) =>
	//        {
	//            throw new AssertionException("An action for Because has not been set!");
	//        };
	//    }

	//    [SetUp]
	//    protected void SetUpTest()
	//    {
	//        SetUp().Invoke();

	//        ISession contextSession = null;
	//        try
	//        {
	//            using (contextSession = GetSessionFactory().OpenSession())
	//            {
	//                Context(contextSession);
	//            }
	//        }
	//        catch
	//        {
	//            if (contextSession != null)
	//            {
	//                contextSession.Dispose();
	//            }
	//            throw;
	//        }

	//        ISession becauseSession = null;
	//        try
	//        {
	//            using (becauseSession = GetSessionFactory().OpenSession())
	//            {
	//                var repository = CreateRepository(becauseSession);
	//                Because(repository);
	//            }
	//        }
	//        catch
	//        {
	//            if (becauseSession != null)
	//            {
	//                becauseSession.Dispose();
	//            }
	//            throw;
	//        }
	//    }

	//    [TearDown]
	//    public void TearDownTest()
	//    {
	//        TearDown().Invoke();
	//    }

	//    protected virtual Action SetUp()
	//    {
	//        return () => { };
	//    }

	//    protected virtual Action TearDown()
	//    {
	//        return () => { };
	//    }
		
	//    protected Action<ISession> Context;
	//    protected Action<TRepository> Because;

	//    protected abstract ISessionFactory GetSessionFactory();
		
	//    protected virtual TRepository CreateRepository(ISession session)
	//    {
	//        var args = new object[] { session };
	//        return (TRepository)Activator.CreateInstance(typeof(TRepository), args);
	//    }

	//    protected void AssertUsing(Action<ISession> action)
	//    {
	//        ISession verificationSession = null;
	//        try
	//        {
	//            using (verificationSession = GetSessionFactory().OpenSession())
	//            {
	//                action(verificationSession);
	//            }
	//        }
	//        catch
	//        {
	//            if (verificationSession != null)
	//            {
	//                verificationSession.Dispose();
	//            }
	//            throw;
	//        }
	//    }

	//    protected TResult GetResult<TResult>(Func<ISession, TResult> function)
	//    {
	//        ISession verificationSession = null;
	//        TResult result;
	//        try
	//        {
	//            using (verificationSession = GetSessionFactory().OpenSession())
	//            {
	//                result = function(verificationSession);
	//            }
	//        }
	//        catch
	//        {
	//            if (verificationSession != null)
	//            {
	//                verificationSession.Dispose();
	//            }
	//            throw;
	//        }
	//        return result;
	//    }
	//}

	public abstract class NHibernateRepositoryTester<TRepository> : BehaviorTestBase<TRepository,ISession>
	{
		protected override void ExecuteContext()
		{
			ExecuteWithNewSession(session => Context(session));
		}

		protected override void ExecuteBecause()
		{
			ExecuteWithNewSession(session => Because(CreateRepository(session)));
		}

		protected override TRepository GetTestModel()
		{
			return CreateRepository(GetSessionFactory().OpenSession());
		}

		protected override ISession GetContextModel() { return GetSessionFactory().OpenSession(); }

		protected virtual TRepository CreateRepository(ISession session)
	    {
	        var args = new object[] { session };
	        return (TRepository)Activator.CreateInstance(typeof(TRepository), args);
	    }

	    protected TResult GetResult<TResult>(Func<ISession, TResult> function)
	    {
	    	return GetWithNewSession(function);
	    }

		protected void AssertUsing(Action<ISession> action)
	    {
			ExecuteWithNewSession(action);
	    }

		protected abstract ISessionFactory GetSessionFactory();

		private void ExecuteWithNewSession(Action<ISession> action)
		{
			ISession contextSession = null;
	        try
	        {
	            using (contextSession = GetSessionFactory().OpenSession())
	            {
	                action(contextSession);
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
		}

		private TResult GetWithNewSession<TResult>(Func<ISession, TResult> function)
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