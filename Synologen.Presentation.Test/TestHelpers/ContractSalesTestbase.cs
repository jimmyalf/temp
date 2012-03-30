using System;
using System.Collections;
using System.Linq;
using FakeItEasy;
using Moq;
using NHibernate;
using NHibernate.Impl;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Business.Utility;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Data.Queries;
using Spinit.Wpc.Synologen.Presentation.Application.Services;
using Spinit.Wpc.Synologen.Presentation.Controllers;

namespace Spinit.Wpc.Synologen.Presentation.Test.TestHelpers
{
	
	public abstract class ContractSalesTestbase<TViewModel> : ControllerTestbase<ContractSalesController,TViewModel> where TViewModel : class
	{
		protected Mock<ISettlementRepository> MockedSettlementRepository;
		protected Mock<IAdminSettingsService> MockedSettingsService;
		protected Mock<IContractSaleRepository> MockedContractSaleRepository;
		protected Mock<ISqlProvider> MockedSynologenSqlProvider;
		protected Mock<ITransactionRepository> MockedTransactionRepository;
		protected IArticleRepository ArticleRepository;
		protected IContractSalesCommandService MockedContractSalesCommandService;
		protected IUserContextService UserContextService;
		protected IContractSalesViewService ViewService;
		protected ISession Session;
		protected ICriteria Criteria;
		protected Func<Query, object> QueryOverrides;

		protected ContractSalesTestbase()
		{
			SetUp = () => 
			{
				MockedSettlementRepository = new Mock<ISettlementRepository>();
				MockedSettingsService = new Mock<IAdminSettingsService>();
				MockedContractSaleRepository = new Mock<IContractSaleRepository>();
				MockedSynologenSqlProvider = new Mock<ISqlProvider>();
				MockedTransactionRepository = new Mock<ITransactionRepository>();
				UserContextService = A.Fake<IUserContextService>();
				var commandService = new ContractSalesCommandService(MockedSynologenSqlProvider.Object, UserContextService);
				MockedContractSalesCommandService = A.Fake<IContractSalesCommandService>(options => options.Wrapping(commandService));
				ArticleRepository = A.Fake<IArticleRepository>();
				Session = A.Fake<ISession>();
				Criteria = A.Fake<ICriteria>();
				A.CallTo(Session).WithReturnType<ICriteria>().Returns(Criteria);


				var viewService = new ContractSalesViewService(
					MockedSettlementRepository.Object,
					MockedContractSaleRepository.Object,
					MockedSettingsService.Object,
					MockedTransactionRepository.Object,
					MockedSynologenSqlProvider.Object,
					ArticleRepository);
				ViewService = A.Fake<IContractSalesViewService>(options => options.Wrapping(viewService));
			};
			GetController = () =>
			{
				var controller = new ContractSalesController(ViewService, MockedContractSalesCommandService, MockedSettingsService.Object, Session);
				if(QueryOverrides != null)
				{
					controller.QueryOverride = QueryOverrides;
				}
				return controller;
			};
		}

		protected object GetDefaultQueryItem(Query query)
		{
			if(query.Type.GetInterfaces().Contains(typeof(IEnumerable)))
			{
				Type type = query.Type.GetGenericArguments()[0];
				return Array.CreateInstance(type, 0);
			}
			return Activator.CreateInstance(query.Type);
		}
	}
}