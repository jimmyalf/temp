using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using FakeItEasy;
using NHibernate;
using NHibernate.Criterion;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Extensions;
using Spinit.Test.Web;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Test.Data;
using StoryQ.Infrastructure;
using StructureMap;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers
{
	public abstract class SpecTestbase<TPresenter,TView>
			where TPresenter : Presenter<TView> 
			where TView : class, IView
	{
		protected Action Context;
		protected Func<Funktion> Story;
		private Funktion _story;
		protected FakeHttpContext HttpContext;
		protected TView View;
		protected int SwedenCountryId;
		protected ISynologenMemberService SynologenMemberService;
		protected FakeRoutingService RoutingService;
	    protected ISendOrderService SendOrderService;
		protected ISynologenSettingsService SynologenSettingsService;
		private readonly DataManager _dataManager;

		protected SpecTestbase()
		{
			_dataManager = new DataManager();
		}

		[SetUp]
		protected void RunBeforeEachTest()
	    {
			ResetData();
			SynologenMemberService = A.Fake<ISynologenMemberService>();
			RoutingService = new FakeRoutingService();
		    SendOrderService = GetSendOrderService();
			SynologenSettingsService = A.Fake<ISynologenSettingsService>();
			SwedenCountryId = 1;
			View = GetView();
			HttpContext = new FakeHttpContext();
			if (Context != null) Context();
			if (Story == null) throw new NotImplementedException("A story must be set for Spec. Use CreateStory function to create a story for the Spec.");
			_story = Story();
	    }

	    private ISendOrderService GetSendOrderService()
	    {
	        return A.Fake<ISendOrderService>();
	    }

	    protected DataManager DataManager
		{
			get { return _dataManager; }
		}

		[TearDown]
		protected void RunAfterEachTest()
		{
			
		}

		protected TPresenter GetPresenter()
		{
			var presenter = ObjectFactory
				.With(View)
				.With(SynologenMemberService)
				.With(typeof(IRoutingService), RoutingService)
                .With(SendOrderService)
				.With(SynologenSettingsService)
				.GetInstance<TPresenter>();
			presenter.HttpContext = HttpContext;
			return presenter;
		}

		protected virtual TView GetView()
		{
			var view = A.Fake<TView>();
			return view;
		}

		//protected virtual ISynologenMemberService GetSynologenMemberService()
		//{
		//    var service = A.Fake<ISynologenMemberService>();
		//    A.CallTo(() => service.GetPageUrl(A<int>.Ignored)).Returns(caller =>
		//    {
		//        var pageId = (int) caller.Arguments[0];
		//        return RoutingService.GetPageUrl(pageId);
		//    });
		//    return service;
		//}

		protected void ResetData()
		{
			DataManager.CleanTables();
		}

		protected void ClearRepository<TRepository,TModel>() 
			where TRepository : IRepository<TModel> 
			where TModel : class
		{
			var repository = WithRepository<TRepository>();
			repository.GetAll().Each(repository.Delete);
		}

		public TModel WithRepository<TRepository, TModel>(Func<TRepository,TModel> function)
		{
		    var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		    var repository = (TRepository) Activator.CreateInstance(typeof (TRepository), session);
		    return function.Invoke(repository);
		}

		public TModel CreateWithRepository<TRepository, TModel>(Func<TModel> factoryFunction)
			where TRepository : IRepository<TModel> 
			where TModel : class
		{
			var repo = WithRepository<TRepository>();
			var model = factoryFunction();
			repo.Save(model);
			return model;
		}

		public IEnumerable<TModel> CreateItemsWithRepository<TRepository, TModel>(Func<IEnumerable<TModel>> factoryFunction)
			where TRepository : IRepository<TModel> 
			where TModel : class
		{
			var repo = WithRepository<TRepository>();
			var items = factoryFunction().ToList();
			foreach (var item in items)
			{
				repo.Save(item);
			}
		    return items;
		}

		protected IEnumerable<TType> StoreItems<TType>(Func<IEnumerable<TType>> factoryMethod)
		{
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			var items = factoryMethod().ToList();
			foreach (var item in items)
			{
				session.Save(item);
			}
			session.Flush();
			return items;
		}

		protected TType StoreItem<TType>(Func<TType> factoryMehtod)
		{
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			var item = factoryMehtod();
			session.Save(item);
			session.Flush();
			return item;
		}

		protected void Save(object item)
		{
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			session.SaveOrUpdate(item);
			session.Flush();
		}

		protected IEnumerable<TType> GetAll<TType>() where TType : class
		{
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			return session.CreateCriteria<TType>().List<TType>();
		}

        protected IList<TType> GetAll<TType>(IEnumerable<int> ids) where TType : Entity
        {
            var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
            return session.CreateCriteria<TType>()
                .Add(Restrictions.In("Id", ids.ToList()))
                .List<TType>();
        }

		protected TType Get<TType>(int id) where TType : class
		{
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			return session.Get<TType>(id);
		}

		protected TShop CreateShop<TShop>(string shopName = "Testbutik")
		{
			var shop = _dataManager.CreateShop(shopName: shopName);
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			return session.Get<TShop>(shop.ShopId);
		}

		public TRepository WithRepository<TRepository>()
		{
		    var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			return ObjectFactory.With(typeof (ISession), session).GetInstance<TRepository>();
		}

		protected void SetupScenario(Func<Scenario, FragmentBase> scenarioAction)
		{
		    var callingMethod = new StackFrame(1).GetMethod();
		    var scenario = _story.MedScenario(Uncamel(callingMethod.Name));
		    scenarioAction(scenario).ExecuteWithReport(callingMethod);
		}

		protected void SetupScenario(string scenarioDescription, Func<Scenario, FragmentBase> scenarioAction)
		{
		    var callingMethod = new StackFrame(1).GetMethod();
		    var scenario = _story.MedScenario(scenarioDescription);
		    scenarioAction(scenario).ExecuteWithReport(callingMethod);
		}

		private static string Uncamel(string methodName)
		{
		    return Regex.Replace(methodName, "[A-Z_]", x => " " + x.Value.ToLowerInvariant()).Trim();
		}

		protected Exception CatchExceptionWhile(Action action)
		{
			Exception caughtException = null;
			try { action(); }
			catch(Exception ex) { caughtException =  ex; }
			return caughtException;
		}
	}
}