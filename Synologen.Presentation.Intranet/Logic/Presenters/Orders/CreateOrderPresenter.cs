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
        private IOrderRepository _orderRepository;
        private ISynologenMemberService _synologenMemberService;

        public CreateOrderPresenter(ICreateOrderView view, IOrderRepository orderRepository, ISynologenMemberService synologenMemberService) : base(view)
        {
            _synologenMemberService = synologenMemberService;
            _orderRepository = orderRepository;
            View.Submit += View_Submit;
        }

        public override void ReleaseView()
        {
            View.Submit -= View_Submit;
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
    }
}