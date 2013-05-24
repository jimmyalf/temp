using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using FakeItEasy;
using NHibernate;
using NUnit.Framework;
using Spinit.Test.Web;
using Spinit.Test.Web.MVC;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Test.Data;
using StoryQ.Infrastructure;
using StoryQ.sv_SE;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public abstract class SpecTestbase
	{
		protected HttpContextBase HttpContext;
		protected Action Context;
		protected Func<Funktion> Story;
		private Funktion _story;
		private DataManager _dataManager;
		protected IAdminSettingsService AdminSettingsService;

		protected SpecTestbase()
		{
			_dataManager = new DataManager();
			AdminSettingsService = A.Fake<IAdminSettingsService>();
		}

		[SetUp]
		protected void RunBeforeEachTest()
	    {
			ResetData();
			HttpContext = new FakeHttpContext();
			if (Context != null) Context();
			if (Story == null) throw new NotImplementedException("A story must be set for Spec. Use CreateStory function to create a story for the Spec.");
			_story = Story();
	    }

		[TearDown]
		protected void RunAfterEachTest()
		{
			
		}

		protected TViewModel GetViewModel<TViewModel>(ActionResult actionResult) where TViewModel : class
		{
			if(actionResult is ViewResult)
			{
				var view = (ViewResult) actionResult;
				return (TViewModel) view.ViewData.Model;
			}
		    return  actionResult as TViewModel;
		}

		protected RedirectToRouteResult GetRedirectResult(ActionResult actionResult)
		{
		    return actionResult as RedirectToRouteResult;
		}

		protected FileContentResult GetFileContentResult(ActionResult actionResult)
		{
		    return actionResult as FileContentResult;
		}

		protected void ResetData()
		{
			var connection = ObjectFactory.GetInstance<ISession>().Connection;
			_dataManager.CleanTables(connection);
			connection.Close();
		}


		public TSqlProvider WithSqlProvider<TSqlProvider>()
		{
			return ObjectFactory.GetInstance<TSqlProvider>();
		}

		public TController GetController<TController>() where TController : Controller
		{
		    var controller = ObjectFactory
				.With(typeof(IAdminSettingsService),AdminSettingsService)
				.GetInstance<TController>();
		    controller.ControllerContext = new FakeControllerContext(controller, HttpContext)
		    {
		        HttpContext = HttpContext
		    };
		    return controller;
		}

		protected TShop CreateShop<TShop>(string shopName = "Testbutik")
		{
			var shop = _dataManager.CreateShop(shopName: shopName);
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			return session.Get<TShop>(shop.ShopId);
		}

		protected TType StoreItem<TType>(Func<TType> factoryMehtod)
		{
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			var item = factoryMehtod();
			session.Save(item);
			session.Flush();
			return item;
		}

		protected IList<TType> StoreItems<TType>(Func<IEnumerable<TType>> factoryMethod)
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
		protected TType Get<TType>(int id) where TType : class
		{
			var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			return session.Get<TType>(id);
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

		protected DataManager DataManager
		{
			get { return _dataManager; }
		}
	}


}