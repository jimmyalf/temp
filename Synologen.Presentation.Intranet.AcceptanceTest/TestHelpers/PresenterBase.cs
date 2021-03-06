using System.Linq;
using FakeItEasy;
using NHibernate;
using Spinit.Test;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.MockHelpers;
using Spinit.Wpc.Synologen.Test.Data;
using StructureMap;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers
{
	public abstract class PresenterBase : BehaviorActionTestbase
	{
		protected ICustomerRepository customerRepository;
		protected IShopRepository shopRepository;
		protected ICountryRepository countryRepository;
		protected ISynologenMemberService synologenMemberService;
		protected HttpContextMock httpContext;
		private DataManager _dataManager;
		protected int testShopId;

		protected PresenterBase()
		{
			_dataManager = new DataManager();
		}

		protected override void SetUp()
		{
			httpContext = new HttpContextMock();
			customerRepository = ResolveEntity<ICustomerRepository>();
			shopRepository = ResolveEntity<IShopRepository>();
			countryRepository = ResolveEntity<ICountryRepository>();
			synologenMemberService = A.Fake<ISynologenMemberService>();
			var shop = _dataManager.CreateShop(_dataManager.GetSqlProvider(), "Testbutik");
			A.CallTo(() => synologenMemberService.GetCurrentShopId()).Returns(shop.ShopId);
			testShopId = shop.ShopId;
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
			expression = parameters.Aggregate(expression, (current, parameter) => current.With(parameter.Type, parameter.Value));
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
		protected override void TearDown()
		{
			_dataManager.CleanTables();
		}


	}
}