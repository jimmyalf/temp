using System;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
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
            	_previousUrl = "/previous/page";
				_submitUrl = "/next/page";
				_abortUrl = "/abort/page";
            	_redirectUrl = (url, orderId) => "{url}?order={orderId}".ReplaceWith(new {url, orderId});
                _presenter = GetPresenter();
            };

            Story = () => new Ber�ttelse("Ange autogirodetaljer")
                          	.F�rAtt("Ange detaljer f�r autogiro")
                          	.Som("inloggad anv�ndare p� intran�tet")
                          	.VillJag("kunna ange detaljer f�r autogiro");
        }

		[Test]
		public void VisaKundNamn()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
				.N�r(SidanVisas)
				.S�(SkallKundNamnVisas)
			);
		}

    	[Test]
        public void SparaDelAbonnemangP�NyttKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
					.Och(AttFormul�ret�rKorrektIfyllt)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
                .S�(F�rflyttasAnv�ndarenTillN�staSteg)
            );
        }

        [Test]
        public void SparaDelAbonnemangP�BefintligtKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
					.Och(AttFormul�ret�rKorrektIfyllt)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
                .S�(F�rflyttasAnv�ndarenTillN�staSteg)
            );
        }

        [Test]
        public void AvbrytBest�llning()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
                .N�r(Anv�ndarenAvbryterBest�llningen)
                .S�(TasBest�llningenBort)
                    .Och(Anv�ndarenFlyttasTillAvbrytSidan)
			);
        }

        [Test]
        public void Tillbaka()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
                .N�r(Anv�ndarenF�rs�kerG�TillF�reg�endeSteg)
                .S�(Anv�ndarenFlyttasTillF�reg�endeSteg)
			);
        }

    	#region Arrange
        private void EnBest�llningHarSkapatsIF�reg�endeSteg()
        {
            _order = CreateOrder();
        }

        private void EnBest�llningHarSkapats()
        {
            throw new NotImplementedException();
        }
        private void AttFormul�ret�rKorrektIfyllt()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Act
        private void Anv�ndarenAvbryterBest�llningen()
        {
            _presenter.View_Abort(null, new EventArgs());
        }
        private void Anv�ndarenF�rs�kerForts�ttaTillN�staSteg()
        {
            throw new NotImplementedException();
        }
    	private void Anv�ndarenF�rs�kerG�TillF�reg�endeSteg()
    	{
    		_presenter.View_Previous(null, new EventArgs());
    	}
    	private void SidanVisas()
    	{
    		_presenter.View_Load(null, new EventArgs());
    	}
        #endregion

        #region Assert
        private void TasBest�llningenBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

        private void Anv�ndarenFlyttasTillAvbrytSidan()
        {
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(_abortUrl);
        }

    	private void Anv�ndarenFlyttasTillF�reg�endeSteg()
    	{
        	var expectedUrl = _redirectUrl(_previousUrl, _order.Id);
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}

        private void F�rflyttasAnv�ndarenTillN�staSteg()
        {
        	var expectedUrl = _redirectUrl(_submitUrl, _order.Id);
			HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

    	private void SkallKundNamnVisas()
    	{
    		View.Model.CustomerName.ShouldBe("Adam Bertil");
    	}
        #endregion
    }
}