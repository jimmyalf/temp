using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using NUnit.Framework;
using Spinit.Test.Web;
using Spinit.Test.Web.MVC;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using StoryQ.Infrastructure;
using StoryQ.sv_SE;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers
{
	public abstract class SpecTestbase
	{
		protected HttpContextBase HttpContext;
		//protected User LoggedInUser;
		protected Action Context;
		protected Func<Funktion> Story;

		////Repositories, Services etc
		//protected CompanyRepository companyRepository;
		//protected RealEstateObjectRepository realEstateObjectRepository;
		//protected DistrictRepository districtRepository;
		//protected RealEstateObjectNoteRepository realEstateObjectNoteRepository;
		//protected DocumentCategoryRepository documentCategoryRepository;
		//protected RealEstateObjectDocumentRepository realEstateObjectDocumentRepository;
		//protected DocumentFileTypeRepository documentFileTypeRepository;
		//protected ContactCategoryRepository contactCategoryRepository;
		//protected ContactRepository contactRepository;
		//protected RealEstateObjectDucRepository realEstateObjectDucRepository;
		//protected CompanyDocumentRepository companyDocumentRepository;
		//protected RealEstateObjectAlarmSourceRepository realEstateObjectAlarmSourceRepository;
		//protected IRealEstateObjectManager realEstateObjectManager;
		//protected IAlarmRepository alarmRepository;
		//protected IHistoricAlarmRepository historicAlarmRepository;
		//protected IDistrictManager districtManager;
		//protected CompanyAccessService companyAccessService;
		//protected RealEstateObjectAccessService realEstateObjectAccessService;
		//protected AlarmAccessService alarmAccessService;
		//protected HistoricAlarmAccessService historicAlarmAccessService;
		//protected DistrictAccessService districtAccessService;
		//protected ExternalUserCompanyRepository externalUserCompanyRepository;
		//protected IExternalUserRepository externalUserRepository;
		//protected IUnitSystemFunctionRepository unitSystemFunctionRepository;
		//protected IActiveDirectoryService activeDirectoryService;
		private Funktion _story;
		protected int TestShopId;
		protected int TestMemberId;
		protected int TestContractCompanyId;
		//protected IAlarmAccessManager alarmAccessManager;
		//protected IAlarmPointRepository alarmPointRepository;
		

		[SetUp]
		protected void RunBeforeEachTest()
	    {
			//ResetData();
			TestShopId = 160;
			TestMemberId = 486;
			TestContractCompanyId = 57;
			HttpContext = new FakeHttpContext();
			//LoggedInUser = UserFactory.GetUser();
			//HttpContext.SetUser(LoggedInUser);
			if (Context != null) Context();
			if (Story == null) throw new NotImplementedException("A story must be set for Spec. Use CreateStory function to create a story for the Spec.");
			_story = Story();
	    }

		[TearDown]
		protected void RunAfterEachTest()
		{
			
		}

		protected TViewModel GetViewModel<TViewModel>(ActionResult actionResult)
		{
		    var view = (ViewResult) actionResult;
		    if(view == null) return default(TViewModel);
		    return (TViewModel) view.ViewData.Model;
		}

		protected RedirectToRouteResult GetRedirectResult(ActionResult actionResult)
		{
		    return actionResult as RedirectToRouteResult;
		}

		protected FileContentResult GetFileContentResult(ActionResult actionResult)
		{
		    return actionResult as FileContentResult;
		}

		//protected void ResetData()
		//{
		//    NHibernateFactory.Instance.GetConfiguration().Export();
		//    SetupRepositories();
		//    SetupAccessServices();
		//    SetupADRepositories();
		//}

		//private void SetupRepositories()
		//{
		//    var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		//    companyRepository = new CompanyRepository(session);
		//    realEstateObjectRepository = new RealEstateObjectRepository(session);
		//    districtRepository = new DistrictRepository(session);
		//    realEstateObjectNoteRepository = new RealEstateObjectNoteRepository(session);
		//    documentCategoryRepository = new DocumentCategoryRepository(session);
		//    realEstateObjectDocumentRepository = new RealEstateObjectDocumentRepository(session);
		//    documentFileTypeRepository = new DocumentFileTypeRepository(session);
		//    contactCategoryRepository = new ContactCategoryRepository(session);
		//    contactRepository = new ContactRepository(session);
		//    realEstateObjectDucRepository = new RealEstateObjectDucRepository(session);
		//    alarmRepository = new AlarmRepository(session);
		//    historicAlarmRepository = new HistoricAlarmRepository(session);
		//    companyDocumentRepository = new CompanyDocumentRepository(session);
		//    realEstateObjectAlarmSourceRepository = new RealEstateObjectAlarmSourceRepository(session);
		//    externalUserCompanyRepository = new ExternalUserCompanyRepository(session);
		//    alarmPointRepository = new AlarmPointRepository(session);
		//    unitSystemFunctionRepository = new UnitSystemFunctionRepository(session);
		//    realEstateObjectManager = ObjectFactory.GetInstance<IRealEstateObjectManager>();
		//    districtManager = ObjectFactory.GetInstance<IDistrictManager>();
		//        //new DistrictManager(districtRepository, district => new FakeActiveDirectoryService());

		//    externalUserRepository = ObjectFactory.GetInstance<IExternalUserRepository>();
		//    alarmAccessManager = ObjectFactory.GetInstance<IAlarmAccessManager>();


		//}

		//private void SetupAccessServices()
		//{
		//    realEstateObjectAccessService = new RealEstateObjectAccessService(realEstateObjectRepository);
		//    alarmAccessService = new AlarmAccessService(alarmRepository, alarmAccessManager);
		//    historicAlarmAccessService = new HistoricAlarmAccessService(historicAlarmRepository, alarmAccessManager, realEstateObjectAccessService);
		//    districtAccessService = new DistrictAccessService(districtRepository, realEstateObjectAccessService);
		//    companyAccessService = new CompanyAccessService(companyRepository,districtAccessService);    
		//}

		//private void SetupADRepositories()
		//{
		//    ((ExternalUserRepository) externalUserRepository).Purge();
		//}

		//public IList<IActionMessage> GetActionMessages(Controller controller)
		//{
		//    return (IList<IActionMessage>) controller.TempData["ActionMessages"];
		//}

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
	}


}