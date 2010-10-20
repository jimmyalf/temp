using System;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription
{
	public class ListCustomersPresenter : Presenter<IListCustomersView>
	{
		private readonly ICustomerRepository _customerRepository;
		private readonly ISynologenMemberService _synologenMemberService;

		public ListCustomersPresenter(IListCustomersView view, ICustomerRepository customerRepository, ISynologenMemberService synologenMemberService)
			: base(view)
		{
			_customerRepository = customerRepository;
			_synologenMemberService = synologenMemberService;
			View.Load += View_Load;
		}

		public override void ReleaseView()
		{
			View.Load -= View_Load;
		}

		public void View_Load(object sender, EventArgs e)
		{
			Func<Customer, ListCustomersItemModel> converter = x => new ListCustomersItemModel
			{
				FirstName = x.FirstName,
				LastName = x.LastName,
				PersonalIdNumber = x.PersonalIdNumber
			};
			var shopId = _synologenMemberService.GetCurrentShopId();
			var criteria = new CustomersForShopMatchingCriteria { ShopId = shopId};
			View.Model.List = _customerRepository.FindBy(criteria).Select(converter);
		}
	}
}
