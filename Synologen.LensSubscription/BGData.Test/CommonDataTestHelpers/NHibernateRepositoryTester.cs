using System;
using NHibernate;
using Synologen.Test.Core;

namespace Synologen.LensSubscription.BGData.Test.CommonDataTestHelpers
{
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