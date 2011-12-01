using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class CreateOrderPresenter : Presenter<ICreateOrderView>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ISynologenMemberService _synologenMemberService;
        private readonly IOrderCustomerRepository _orderCustomerRepository;

        public CreateOrderPresenter(ICreateOrderView view, IOrderRepository orderRepository, IOrderCustomerRepository orderCustomerRepository, ISynologenMemberService synologenMemberService) : base(view)
        {
            _orderCustomerRepository = orderCustomerRepository;
            _synologenMemberService = synologenMemberService;
            _orderRepository = orderRepository;
        	View.Load += View_Load;
            View.Submit += View_Submit;
        }

    	public override void ReleaseView()
        {
            View.Submit -= View_Submit;
            View.Load -= View_Load;
        }

        public void View_Submit(object o, CreateOrderEventArgs form)
        {
            var order = new Order
            {
                ArticleId = form.ArticleId,
                CategoryId = form.CategoryId,
                LeftBaseCurve = form.LeftBaseCurve,
                LeftDiameter = form.LeftDiameter,
                LeftPower = form.LeftPower,
                RightBaseCurve = form.RightBaseCurve,
                RightDiameter = form.RightDiameter,
                RightPower = form.RightPower,
                ShipmentOption = form.ShipmentOption,
                SupplierId = form.SupplierId,
                TypeId = form.TypeId
            };
            _orderRepository.Save(order);

            Redirect();
        }

        private void Redirect()
        {
            var url = _synologenMemberService.GetPageUrl(View.NextPageId);
            HttpContext.Response.Redirect(url);
        }

        public void View_Load(object o, EventArgs eventArgs)
        {
            var customerId = Convert.ToInt32(HttpContext.Request.Params["customer"]);
            if(customerId <= 0)
            {
                //TODO direct back to previous step...
                return;
            }

            var customer = _orderCustomerRepository.Get(customerId);

            View.Model.CustomerId = customerId;
            View.Model.CustomerName = String.Format("{0} {1}", customer.FirstName, customer.LastName);
        }
    }
}