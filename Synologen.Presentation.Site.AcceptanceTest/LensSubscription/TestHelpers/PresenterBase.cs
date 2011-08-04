using FakeItEasy;
using NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers;
using StructureMap;
using Synologen.Test.Core;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest.LensSubscription.TestHelpers
{
	public abstract class PresenterBase : BehaviorActionTestbase
	{
		protected ICustomerRepository customerRepository;
		protected IShopRepository shopRepository;
		protected ICountryRepository countryRepository;
		protected ISynologenMemberService synologenMemberService;
		protected int testShopId;
		protected HttpContextMock httpContext;

		protected override void SetUp()
		{
			testShopId = 158;
			httpContext = new HttpContextMock();
			customerRepository = ResolveEntity<ICustomerRepository>();
			shopRepository = ResolveEntity<IShopRepository>();
			countryRepository = ResolveEntity<ICountryRepository>();
			synologenMemberService = A.Fake<ISynologenMemberService>();
			A.CallTo(() => synologenMemberService.GetCurrentShopId()).Returns(testShopId);
		}

		protected ISession GetSession()
		{
			return NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		}

		protected TEntity ResolveEntity<TEntity>()
		{
		    return GetDefaultInjectedExpression().GetInstance<TEntity>();
		}

		protected TEntity ResolveEntityWith<TEntity>(params TypeValuePair[] parameters)
		{
			var expression = GetDefaultInjectedExpression();
			foreach (var parameter in parameters)
			{
				expression = expression.With(parameter.Type, parameter.Value);
			}
			return expression.GetInstance<TEntity>();
		}

		protected TPresenter ResolvePresenter<TPresenter,TView>(TView view)
			where TPresenter : Presenter<TView> 
			where TView : class, IView
		{
			var presenter = ResolveEntityWith<TPresenter>(new TypeValuePair<TView>(view));
			presenter.HttpContext = httpContext.Object;
			return presenter;
		}

		private ExplicitArgsExpression GetDefaultInjectedExpression()
		{
			return ObjectFactory
				.With(typeof(ISession), GetSession())
				.With(typeof (ISynologenMemberService), synologenMemberService);
		}

		public class TypeValuePair
		{
			public TypeValuePair(System.Type type, object value)
			{
				Type = type;
				Value = value;
			}
			public System.Type Type{ get; protected set;}
			public object Value{ get; protected set;}
		}
		public class TypeValuePair<TEntity> : TypeValuePair
		{
			public TypeValuePair(TEntity value) : base(typeof(TEntity),value) { }
		}



	}
}