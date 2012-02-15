using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using Spinit.Test;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using Spinit.Wpc.Synologen.Test.Data;
using StructureMap;
using Synologen.LensSubscription.ServiceCoordinator.Core.TaskRunner;
using Synologen.LensSubscription.BGData;
using NHibernateFactory = Spinit.Wpc.Core.Dependencies.NHibernate.NHibernateFactory;

namespace Synologen.LensSubscription.ServiceCoordinator.AcceptanceTest.TestHelpers
{
	public abstract class TaskBase : BehaviorActionTestbase
	{
		private readonly ISqlProvider _sqlProvider;
		private readonly DataManager _dataManager;

		protected TaskBase()
		{
			_dataManager = new DataManager();
			_sqlProvider = _dataManager.GetSqlProvider();
		}

		protected override void SetUp()
		{
			base.SetUp();
			CleanDatabases();
		}

		private void CleanDatabases()
		{
			BGData.NHibernateFactory.Instance.GetConfiguration().Export();
			_dataManager.CleanTables();
		}

		protected Subscription StoreSubscription(Func<OrderCustomer,Subscription> getSubscription, int shopId)
		{
			var session = GetWPCSession();
			var shopToUse = session.Get<Shop>(shopId);
			var customer = Factory.CreateCustomer(shopToUse);
			session.Save(customer);
			var subscription = getSubscription.Invoke(customer);
			session.Save(subscription);
			return subscription;
		}

		protected TShop CreateShop<TShop>(string shopName = "Testbutik")
		{
			var session = GetWPCSession();
			var shop = _dataManager.CreateShop(_sqlProvider, shopName);
			return session.Get<TShop>(shop.ShopId);
		}

		protected int RegisterPayerWithWebService()
		{
			var payerNumber = 0;
			InvokeWebService(service =>
			{
				payerNumber = service.RegisterPayer("Test payer", AutogiroServiceType.SubscriptionVersion2);
			});
			return payerNumber;
		}

		protected TTask ResolveTask<TTask>() where TTask : ITask
		{
			return ObjectFactory.GetInstance<TTask>();
		}

		protected ITaskRunnerService GetTaskRunnerService(ITask task)
		{
			return ObjectFactory
				.With(typeof(IEnumerable<ITask>),new []{task})
				.With(typeof(ITaskRepositoryResolver), new TestTaskRepositoryResolver())
				.GetInstance<TaskRunnerService>();
		}

		private class TestTaskRepositoryResolver : ITaskRepositoryResolver
		{
			public TRepository GetRepository<TRepository>()
			{
				return ObjectFactory.GetInstance<TRepository>();
			}
		}

		protected TEntity ResolveEntity<TEntity>()
		{
			return ObjectFactory.GetInstance<TEntity>();
		}

		protected void InvokeWebService(Action<IBGWebService> function)
		{
			var client = ResolveEntity<IBGWebServiceClient>();
			client.Open();
			function.Invoke(client);
			client.Close();
		}

		protected static ISession GetBGSession()
		{
			return BGData.NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		}

		protected static ISession GetWPCSession()
		{
			return NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		}

		protected IEnumerable<TType> StoreItemsWithWpcSession<TType>(Func<IEnumerable<TType>> factoryMethod)
		{
			return StoreItems(GetWPCSession(), factoryMethod);
		}
		protected TType StoreWithWpcSession<TType>(Func<TType> factoryMehtod)
		{
			return StoreItem(GetWPCSession(), factoryMehtod);
		}

		private IEnumerable<TType> StoreItems<TType>(ISession session, Func<IEnumerable<TType>> factoryMethod)
		{
			var items = factoryMethod().ToList();
			foreach (var item in items)
			{
				session.Save(item);
			}
			session.Flush();
			return items;
		}

		private TType StoreItem<TType>(ISession session, Func<TType> factoryMehtod)
		{
			var item = factoryMehtod();
			session.Save(item);
			session.Flush();
			return item;
		}

		protected IEnumerable<TType> GetAll<TType>(Func<ISession> getSession) where TType : class
		{
			return getSession().CreateCriteria<TType>().List<TType>();
		}
		protected TType Get<TType>(Func<ISession> getSession, int id) where TType : class
		{
			return getSession().Get<TType>(id);
		}

		protected BGReceivedPayment StoreBGPayment(Func<AutogiroPayer, BGReceivedPayment> getPayment, int payerNumber)
		{
			var session = GetBGSession();
			var autogiroPayer = Get<AutogiroPayer>(() => session, payerNumber);
			var payment = getPayment.Invoke(autogiroPayer);
			session.Save(payment);
			return payment;
		}

		protected BGReceivedError StoreBGError(Func<AutogiroPayer, BGReceivedError> getError, int payerNumber) 
		{
			var session = GetBGSession();
			var autogiroPayer = Get<AutogiroPayer>(() => session, payerNumber);
			var error = getError(autogiroPayer);
			session.Save(error);
			return error;
		}

		protected BGReceivedConsent StoreBGConsent(Func<AutogiroPayer, BGReceivedConsent> getConsent, int payerNumber)
		{
			var session = GetBGSession();
			var autogiroPayer = Get<AutogiroPayer>(() => session, payerNumber);
			var consent = getConsent(autogiroPayer);
			session.Save(consent);
			return consent;
		}
	}
}