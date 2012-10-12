using System;
using System.Collections.Generic;
using NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Test.Data;

namespace Spinit.Wpc.Synologen.Data.Test.Orders
{
	public abstract class GenericDataBase
	{
		private readonly ISessionFactory _sessionFactory;

		protected GenericDataBase()
		{
			_sessionFactory =  NHibernateFactory.Instance.GetSessionFactory();
			DataManager = new DataManager();
		}

		protected DataManager DataManager { get; private set; }

		public T Persist<T>(T item)
		{
			var session = _sessionFactory.OpenSession();
			session.Save(typeof (T).FullName, item);
			session.Flush();
			return item;
		}

		public T Fetch<T>(object id)
		{
			var session = _sessionFactory.OpenSession();
			var item = session.Get<T>(id);
			return item;
		}

		public IEnumerable<T> FetchAll<T>() where T : class
		{
			var session = _sessionFactory.OpenSession();
			return session.CreateCriteria<T>().List<T>();
		}

		protected TShop CreateShop<TShop>(string shopName = "Testbutik")
		{
			var shop = DataManager.CreateShop(shopName: shopName);
			var session = _sessionFactory.OpenSession();
			return session.Get<TShop>(shop.ShopId);
		}
	}
}