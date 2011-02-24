using System;
using NHibernate;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGData.Test.CommonDataTestHelpers
{
	public abstract class NHibernateRepositoryTester<TRepository> : BehaviorTestBase<TRepository,ISession>
	{
		protected override void ExecuteContext()
		{
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
		}

		protected override void ExecuteBecause()
		{
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

		protected abstract ISessionFactory GetSessionFactory();
	}
}