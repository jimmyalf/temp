using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
    public class PickCustomerPresenter : Presenter<IPickCustomerView>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderCustomerRepository _orderCustomerRepository;

        public PickCustomerPresenter(IPickCustomerView view, IOrderRepository orderRepository, IOrderCustomerRepository orderCustomerRepository) : base(view)
        {
            _orderRepository = orderRepository;
            _orderCustomerRepository = orderCustomerRepository;

            View.Submit += View_Submit;
            View.FetchCustomerByPersonalIdNumber += FetchCustomerDataByPersonalIdNumber;
        }

        public void View_Submit(object o, PickCustomerEventArgs args)
        {
            throw new NotImplementedException();
        }

        public void FetchCustomerDataByPersonalIdNumber(object o, FetchCustomerDataByPersonalIdEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void ReleaseView()
        {
            View.Submit -= View_Submit;
        }
    }
}