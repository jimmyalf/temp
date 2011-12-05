using System;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Create_Order")]
    public class When_creating_an_order : SpecTestbase<CreateOrderPresenter, ICreateOrderView>
    {
        private CreateOrderPresenter _createOrderPresenter;
        private CreateOrderEventArgs _form;
        private string _testRedirectUrl;
        private OrderCustomer _customer;

        public When_creating_an_order()
        {
            Context = () =>
            {
                _testRedirectUrl = "/test/page";
                View.NextPageId = 56;
                A.CallTo(() => SynologenMemberService.GetPageUrl(View.NextPageId)).Returns(_testRedirectUrl);
                _createOrderPresenter = GetPresenter();
            };                  
                                
            Story = () => new Berättelse("Spara beställning")
                .FörAtt("skapa en ny beställning")
                .Som("inloggad användare på intranätet")
                .VillJag("spara innehållet i den nya beställningen");
        }                       
                                
		[Test]                  
		public void VisaBeställningsFormulär()
		{                       
		    SetupScenario(scenario => scenario
		        .Givet(AttEnKundÄrVald)
		        .När(AttAnvändarenVisarBeställningsformuläret)
		        .Så(VisasKundensNamn)
			);                  
		}                       
                                
    	[Test]                  
		public void FöregåendeSteg()
		{                       
		    SetupScenario(scenario => scenario
				.Givet(AttEnKundÄrVald)
					.Och(AttAnvändarenVisarBeställningsformuläret)
		        .När(AnvändarenKlickarPåFöregåendeSteg)
		        .Så(FörflyttasAnvändarenTillFöregåendeSteg)
			);
		}

        [Test]
        public void NästaSteg()
        {
            SetupScenario(scenario => scenario
				.Givet(AttEnKundÄrVald)
					.Och(AttAnvändarenFylltIBeställningsformuläret)
                .När(AnvändarenKlickarPåNästaSteg)
                .Så(SparasBeställningen)
                    .Och(AnvändarenFörflyttasTillVynFörNästaSteg)
			);
        }

		#region Arrange

    	private void AttEnKundÄrVald()
    	{
            _customer = OrderFactory.GetCustomer();
            WithRepository<IOrderCustomerRepository>().Save(_customer);
            HttpContext.SetupRequestParameter("customer", _customer.Id.ToString());
    	}

		private void AttAnvändarenFylltIBeställningsformuläret()
        {
            _form = OrderFactory.GetOrder();
        }

		#endregion

		#region Act

    	private void AttAnvändarenVisarBeställningsformuläret()
    	{
            _createOrderPresenter.View_Load(null, new EventArgs());
    	}

		private void AnvändarenKlickarPåFöregåendeSteg()
		{
		    throw new NotImplementedException();
		}

        private void AnvändarenKlickarPåNästaSteg()
        {
            _createOrderPresenter.View_Submit(null, _form);
        }

		#endregion

		#region Assert

		private void VisasKundensNamn()
    	{
    		View.Model.CustomerName.ShouldBe(String.Format("{0} {1}", _customer.FirstName, _customer.LastName));
    	}

		private void FörflyttasAnvändarenTillFöregåendeSteg()
		{
		    throw new NotImplementedException();
		}

        private void SparasBeställningen()
        {
            var order = WithRepository<IOrderRepository>().GetAll().First();

            order.Article.Id.ShouldBe(_form.ArticleId);
			//order.Article.Category.Id.ShouldBe(_form.CategoryId);
            order.LensRecipe.BaseCurve.Left.ShouldBe(_form.LeftBaseCurve);
            order.LensRecipe.Diameter.Left.ShouldBe(_form.LeftDiameter);
            order.LensRecipe.Power.Left.ShouldBe(_form.LeftPower);
            order.LensRecipe.BaseCurve.Right.ShouldBe(_form.RightBaseCurve);
            order.LensRecipe.Diameter.Right.ShouldBe(_form.RightDiameter);
            order.LensRecipe.Power.Right.ShouldBe(_form.RightPower);
            order.ShippingType.ToInteger().ShouldBe(_form.ShipmentOption);
            //order.Article.Supplier.Id.ShouldBe(_form.SupplierId);
        }

        private void AnvändarenFörflyttasTillVynFörNästaSteg()
        {
            HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_testRedirectUrl);
        }

		#endregion

    }
}
