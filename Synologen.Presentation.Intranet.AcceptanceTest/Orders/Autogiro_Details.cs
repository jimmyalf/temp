using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Autogiro_Details")]
    public class When_entering_autogiro_details : OrderSpecTestbase<AutogiroDetailsPresenter, IAutogiroDetailsView>
    {
        private AutogiroDetailsPresenter _presenter;
    	private Func<string,int,string> _redirectUrl;
    	private string _previousUrl, _submitUrl, _abortUrl;
    	private Order _order;

    	public When_entering_autogiro_details()
        {
            Context = () =>
            {
            	/*_previousUrl = "/previous/page";
				_submitUrl = "/next/page";
				_abortUrl = "/abort/page";
                */
            	_redirectUrl = (url, orderId) => "{url}?order={orderId}".ReplaceWith(new {url, orderId});

                _submitUrl = "/test/page";
                _abortUrl = "/test/page/abort";
                _previousUrl = "/test/page/previous";
                View.NextPageId = 56;
                View.AbortPageId = 78;
                View.PreviousPageId = 77;
                A.CallTo(() => SynologenMemberService.GetPageUrl(View.NextPageId)).Returns(_submitUrl);
                A.CallTo(() => SynologenMemberService.GetPageUrl(View.AbortPageId)).Returns(_abortUrl);
                A.CallTo(() => SynologenMemberService.GetPageUrl(View.PreviousPageId)).Returns(_previousUrl);


                _presenter = GetPresenter();
            };

            Story = () => new Berättelse("Ange autogirodetaljer")
                          	.FörAtt("Ange detaljer för autogiro")
                          	.Som("inloggad användare på intranätet")
                          	.VillJag("kunna ange detaljer för autogiro");
        }

		[Test]
		public void VisaKundNamn()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
				.När(SidanVisas)
				.Så(SkallKundNamnVisas)
			);
		}

    	[Test]
        public void SparaDelAbonnemangPåNyttKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
                .Så(FörflyttasAnvändarenTillNästaSteg)
            );
        }

        [Test]
        public void SparaDelAbonnemangPåBefintligtKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBeställningHarSkapatsIFöregåendeSteg)
					.Och(AttFormuläretÄrKorrektIfyllt)
                .När(AnvändarenFörsökerFortsättaTillNästaSteg)
                .Så(FörflyttasAnvändarenTillNästaSteg)
            );
        }

        [Test]
        public void AvbrytBeställning()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningHarSkapatsIFöregåendeSteg)
                .När(AnvändarenAvbryterBeställningen)
                .Så(TasBeställningenBort)
                    .Och(AnvändarenFlyttasTillAvbrytSidan)
			);
        }

        [Test]
        public void Tillbaka()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBeställningHarSkapatsIFöregåendeSteg)
                .När(AnvändarenFörsökerGåTillFöregåendeSteg)
                .Så(AnvändarenFlyttasTillFöregåendeSteg)
			);
        }

    	#region Arrange
        private void EnBeställningHarSkapatsIFöregåendeSteg()
        {
            _order = CreateOrder();
        }

        private void AttFormuläretÄrKorrektIfyllt()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Act
        private void AnvändarenAvbryterBeställningen()
        {
            _presenter.View_Abort(null, new EventArgs());
        }
        private void AnvändarenFörsökerFortsättaTillNästaSteg()
        {
            throw new NotImplementedException();
        }
    	private void AnvändarenFörsökerGåTillFöregåendeSteg()
    	{
    		_presenter.View_Previous(null, new EventArgs());
    	}
    	private void SidanVisas()
    	{
    		_presenter.View_Load(null, new EventArgs());
    	}
        #endregion

        #region Assert
        private void TasBeställningenBort()
        {
            throw new NotImplementedException();
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

        private void AnvändarenFlyttasTillAvbrytSidan()
        {
            throw new NotImplementedException();
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortUrl);
        }

    	private void AnvändarenFlyttasTillFöregåendeSteg()
    	{
            throw new NotImplementedException();
        	var expectedUrl = _redirectUrl(_previousUrl, _order.Id);
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}

        private void FörflyttasAnvändarenTillNästaSteg()
        {
            throw new NotImplementedException();
        	var expectedUrl = _redirectUrl(_submitUrl, _order.Id);
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

    	private void SkallKundNamnVisas()
    	{
            throw new NotImplementedException();
    		View.Model.CustomerName.ShouldBe("Adam Bertil");
    	}
        #endregion
    }
}