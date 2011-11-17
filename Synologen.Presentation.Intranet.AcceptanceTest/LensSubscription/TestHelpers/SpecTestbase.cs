using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using FakeItEasy;
using NUnit.Framework;
using Spinit.Data;
using Spinit.Extensions;
using Spinit.Test.Web;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using StoryQ.Infrastructure;
using StructureMap;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.LensSubscription.TestHelpers
{
	public abstract class SpecTestbase<TPresenter,TView>
			where TPresenter : Presenter<TView> 
			where TView : class, IView
	{
		protected Action Context;
		protected Func<Funktion> Story;
		private Funktion _story;
		protected int TestShopId;
		protected int TestMemberId;
		protected int TestContractCompanyId;
		protected int TestContractId;
		protected FakeHttpContext HttpContext;
		protected TView View;
		protected int SwedenCountryId;
		protected ISynologenMemberService SynologenMemberService;
		protected int OtherShopId;

		[SetUp]
		protected void RunBeforeEachTest()
	    {
			ResetData();
			TestShopId = 159;
			OtherShopId = 160;
			//TestMemberId = 486;
			//TestContractCompanyId = 57;
			//TestContractId = 14;
			SynologenMemberService = GetSynologenMemberService();
			SwedenCountryId = 1;
			View = GetView();
			HttpContext = new FakeHttpContext();
			if (Context != null) Context();
			if (Story == null) throw new NotImplementedException("A story must be set for Spec. Use CreateStory function to create a story for the Spec.");
			_story = Story();
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
				.GetInstance<TPresenter>();
			presenter.HttpContext = HttpContext;
			return presenter;
		}

		protected virtual TView GetView()
		{
			var view = A.Fake<TView>();
			return view;
		}

		protected virtual ISynologenMemberService GetSynologenMemberService()
		{
			var service = A.Fake<ISynologenMemberService>();;
			return service;
		}

		protected void ResetData()
		{
			ClearRepository<ICustomerRepository,Customer>();
			ClearRepository<ISubscriptionRepository,Subscription>();
			ClearRepository<ITransactionArticleRepository,TransactionArticle>();
			ClearRepository<ITransactionRepository,SubscriptionTransaction>();
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

		//public TModel WithRepository<TRepository, TModel>(Func<TModel> getModel, Action<TRepository,TModel> withRepositoryAndModel)
		//{
		//    var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		//    var repository = (TRepository) Activator.CreateInstance(typeof (TRepository), session);
		//    var model = getModel();
		//    withRepositoryAndModel(repository, model);
		//    return model;
		//}
		public TRepository WithRepository<TRepository>()
		{
			return ObjectFactory.GetInstance<TRepository>();
			//var session = NHibernateFactory.Instance.GetSessionFactory().OpenSession();
			//return (TRepository) Activator.CreateInstance(typeof (TRepository), session);
		}

		//public TSqlProvider WithSqlProvider<TSqlProvider>()
		//{
		//    return ObjectFactory.GetInstance<TSqlProvider>();
		//}

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