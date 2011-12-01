using System;
using NUnit.Framework;
using Spinit.Test;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.Orders
{
	[TestFixture, Category("Create Order Tests")]
	public class When_loading_create_order_view : CreateOrderTestbase
	{
		private EventArgs _args;

		public When_loading_create_order_view()
		{
			Context = () =>
			{
				_args = new EventArgs();
			};

			Because = presenter => presenter.View_Load(null, _args);
		}
	}

	public class CreateOrderTestbase: PresenterTestbase<CreateOrderPresenter,ICreateOrderView,CreateOrderModel>
	{

		protected IOrderRepository OrderRepository;
	    protected IOrderCustomerRepository OrderCustomerRepository;
		protected ISynologenMemberService SynologenMemberService;
		protected CreateOrderTestbase()
		{
			SetUp = () =>
			{
				//MockedCustomerRepository = new Mock<ICustomerRepository>();
				//MockedCountryRepository = new Mock<ICountryRepository>();
				//MockedSynologenMemberService = new Mock<ISynologenMemberService>();
				//MockedShopRepository = new Mock<IShopRepository>();
			};

			GetPresenter = () => 
			{
				return new CreateOrderPresenter(View, OrderRepository, OrderCustomerRepository, SynologenMemberService);
			};
		}
	}
}
