using FakeItEasy;
using NHibernate;
using Spinit.Wpc.Core.Dependencies.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data;
using Spinit.Wpc.Synologen.Data.Repositories.LensSubscriptionRepositories;
using Spinit.Wpc.Synologen.Presentation.Site.Test.MockHelpers;
using Synologen.Test.Core;

namespace Synologen.Presentation.AcceptanceTest.LensSubscription.TestHelpers
{
	public abstract class PresenterBase : BehaviorActionTestbase
	{
		protected CustomerRepository customerRepository;
		protected ShopRepository shopRepository;
		protected CountryRepository countryRepository;
		protected ISynologenMemberService synologenMemberService;
		protected int testShopId;
		protected HttpContextMock httpContext;
		protected SubscriptionRepository subscriptionRepository;

		protected override void SetUp()
		{
			testShopId = 158;
			NHibernateFactory.MappingAssemblies.Add(typeof(SqlProvider).Assembly);
			var session = GetSession();
			httpContext = new HttpContextMock();
			customerRepository = new CustomerRepository(session);
			shopRepository = new ShopRepository(session);
			countryRepository = new CountryRepository(session);
			subscriptionRepository = new SubscriptionRepository(session);
			synologenMemberService = A.Fake<ISynologenMemberService>();
			A.CallTo(() => synologenMemberService.GetCurrentShopId()).Returns(testShopId);
		}

		protected ISession GetSession()
		{
			return NHibernateFactory.Instance.GetSessionFactory().OpenSession();
		}

		protected Customer CreateCustomer(Country country, Shop shop)
		{
			return new Customer
			{
				Address = new CustomerAddress
				{
					AddressLineOne = "AddressLineOne",
					City = "Göteborg",
					PostalCode = "43632",
					Country = country
				},
				Contact = new CustomerContact
				{
					Email = "abc@abc.se",
					MobilePhone = "0700-00 00 00",
					Phone = "031 - 00 00 00"
				},
				FirstName = "FirstName",
				LastName = "LastName",
				Shop = shop,
				PersonalIdNumber = "197910071111"
				
			};
		}

	}
}