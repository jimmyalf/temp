using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NUnit.Framework;
using Spinit.Test.Web;
using Spinit.Test.Web.MVC;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synogen.Test.Data;
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

		protected SpecTestbase()
		{
			_dataManager = new DataManager();
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
			//DataHelper.DeleteAndResetIndexForTable(connection, "tblSynologenContractArticleConnection");
			//DataHelper.DeleteAndResetIndexForTable(connection, "tblSynologenOrderItems");
			//DataHelper.DeleteAndResetIndexForTable(connection, "tblSynologenArticle");
		}

		public TModel WithRepository<TRepository, TModel>(Func<TRepository,TModel> function)
		{
		    var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		    var repository = (TRepository) Activator.CreateInstance(typeof (TRepository), session);
		    return function.Invoke(repository);
		}
		public TRepository WithRepository<TRepository>()
		{
		    var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		    return (TRepository) Activator.CreateInstance(typeof (TRepository), session);
		}

		public TSqlProvider WithSqlProvider<TSqlProvider>()
		{
			return ObjectFactory.GetInstance<TSqlProvider>();
		}

		public TController GetController<TController>() where TController : Controller
		{
		    var controller = ObjectFactory.GetInstance<TController>();
		    controller.ControllerContext = new FakeControllerContext(controller, HttpContext)
		    {
		        HttpContext = HttpContext
		    };
		    return controller;
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