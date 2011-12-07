using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.Orders
{
	[TestFixture, Category("Save_Customer")]
	public class When_loading_form_with_personal_number : SaveCustomerTestbase
	{
		private string _personalIdNumber;

		public When_loading_form_with_personal_number()
		{
			Context = () =>
			{
				_personalIdNumber = "197001011234";
				HttpContext.SetupRequestParameter("personalIdNumber", _personalIdNumber);
			};
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Personalnumber_is_loaded_in_form()
		{
			View.Model.PersonalIdNumber.ShouldBe(_personalIdNumber);
		}

		[Test]
		public void Customer_id_not_loaded_in_form()
		{
			View.Model.CustomerId.ShouldBe(null);
		}
	}

	public class SaveCustomerTestbase: PresenterTestbase<SaveCustomerPresenter,ISaveCustomerView,SaveCustomerModel>
	{
		protected IOrderCustomerRepository OrderCustomerRepository;
		protected ISynologenMemberService SynologenMemberService;
		protected IViewParser ViewParser;

		protected SaveCustomerTestbase()
		{
			SetUp = () =>
			{
				SynologenMemberService = A.Fake<ISynologenMemberService>();
				OrderCustomerRepository = A.Fake<IOrderCustomerRepository>();
				ViewParser = new ViewParser();
			};

			GetPresenter = () => new SaveCustomerPresenter(
				View, OrderCustomerRepository, ViewParser, SynologenMemberService
			);
		}
	}
}