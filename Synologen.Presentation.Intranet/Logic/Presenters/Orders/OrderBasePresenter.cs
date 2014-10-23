using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public abstract class OrderBasePresenter<TView> : Presenter<TView> where TView : class, IView
	{
		private readonly ISynologenMemberService _synologenMemberService;

		protected OrderBasePresenter(TView view, ISynologenMemberService synologenMemberService) : base(view)
		{
			_synologenMemberService = synologenMemberService;
		}

		protected virtual void CheckAccess(Shop shop)
		{
			var allowedShopId = _synologenMemberService.GetCurrentShopId();
			if(shop.Id != allowedShopId) throw new AccessDeniedException("Shop is not allowed access to customer");
		}
	}
}