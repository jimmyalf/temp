using System;
using NHibernate;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate
{
	public class NHibernateUnitOfWork : IUnitOfWork<ISession>
	{
		private readonly ISessionFactory _sessionFactory;
		private ISession _session;
		private bool _isDisposed;

		public NHibernateUnitOfWork(ISessionFactory sessionFactory)
		{
			_sessionFactory = sessionFactory;
		}

		public ISession Session
		{
			get
			{
				if (_session == null)
				{
					_session = _sessionFactory.OpenSession();
					_session.FlushMode = FlushMode.Never;
					_session.BeginTransaction();
				}
				return _session;
			}
		}

		public void Commit()
		{
			VerifyNotDisposed();

			if (HasSession() && _session.Transaction.IsActive)
				_session.Transaction.Commit();

			Dispose();
		}

		public void Rollback()
		{
			VerifyNotDisposed();

			if (HasSession())
				_session.Transaction.Rollback();
			
			Dispose();
		}

		public void Dispose()
		{
			if (_isDisposed) return;

			if (HasSession())
			{
				_session.Close();
				_session = null;	
			}

			_isDisposed = true;
		}

		private void VerifyNotDisposed()
		{
			if (_isDisposed) throw new InvalidOperationException("The Unit of Work has been disposed and can no longer be used");
		}

		private bool HasSession()
		{
			return _session != null;
		}
	}
}