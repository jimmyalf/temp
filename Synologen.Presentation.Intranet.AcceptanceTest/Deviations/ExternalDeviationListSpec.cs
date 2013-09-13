using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Deviations
{
    [TestFixture, Category("Deviations_ExternalList")]
    public class ExternalDeviationListSpec : DeviationSpecTestbase<ExternalDeviationListPresenter, IExternalDeviationListView>
    {
        private ExternalDeviationListPresenter _presenter;
        private List<Deviation> _deviations;
        private List<DeviationCategory> _categories;
        private List<DeviationSupplier> _suppliers;
        private Shop _shop;
        private ExternalDeviationListEventArgs _args;

        public ExternalDeviationListSpec()
        {
            Context = () =>
            {
                _presenter = GetPresenter();
                _shop = CreateShop<Shop>();
                A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
            };

            Story = () => new Berättelse("Visa externa avvikelser")
                            .FörAtt("Visa en lista med avvikelser")
                            .Som("inloggad användare på intranätet")
                            .VillJag("kunna ange avvikelse-detaljer");
        }

        [Test]
        public void VisaExternaAvvikelseListSidan()
        {
            SetupScenario(scenario => scenario
                .Givet(ExternaAvvikelser)
                    .Och(Leverantörer)
                .När(SidanVisas)
                .Så(SkallListanMedAvvikelserPopuleras)
            .Och(LeverantörerPopuleras));
        }

        [Test]
        public void VisaExternaAvvikelserBaseratPåLeverantörsFilter()
        {
            SetupScenario(scenario => scenario
                .Givet(ExternaAvvikelser)
                    .Och(Leverantörer)
                    .Och(AnvändarenVäljerLeverantör)
                .När(AnvändarenHarValtLeverantör)
                .Så(SkallDenFiltreradeListanMedAvvikelserPopuleras)
                    .Och(LeverantörerPopuleras));
        }

        #region Arrange

        private void AnvändarenVäljerLeverantör()
        {
            _args = new ExternalDeviationListEventArgs
            {
                SelectedSupplier = _suppliers.First().Id
            };
        }

        private void ExternaAvvikelser()
        {
            var firstDeviation = new Deviation
            {
                Category = SkapaKategorier().First(),
                CreatedDate = DateTime.Now,
                DefectDescription = "Defekt 1",
                ShopId = _shop.Id,
                Type = DeviationType.External,
                Supplier = SkapaLeverantörer().First()
            };

            var otherDeviation = new Deviation
            {
                Category = SkapaKategorier().First(),
                CreatedDate = DateTime.Now,
                DefectDescription = "Defekt 2",
                ShopId = _shop.Id,
                Type = DeviationType.External,
                Supplier = SkapaLeverantörer().First()
            };

            Save(firstDeviation);
            Save(otherDeviation);
            _deviations = new List<Deviation>(new[] { firstDeviation, otherDeviation });
        }

        public void Leverantörer()
        {
            _suppliers = SkapaLeverantörer();
        }

        public void Kategorier()
        {
            _categories = SkapaKategorier();
        }

        #endregion

        #region Act

        private void SidanVisas()
        {
            _presenter.View_Load(null, new EventArgs());
        }

        private void AnvändarenHarValtLeverantör()
        {
            _presenter.View_SupplierSelected(null, _args);
        }

        #endregion

        #region Assert

        private void LeverantörerPopuleras()
        {
            foreach (var supplier in _suppliers)
            {
                View.Model.Suppliers.ShouldContain(viewSupplier => viewSupplier.Id == supplier.Id);
            }
        }

        private void SkallDenFiltreradeListanMedAvvikelserPopuleras()
        {
            var expectedDeviations = _deviations.Where(x => x.Supplier.Id == _args.SelectedSupplier);
            foreach (var deviation in expectedDeviations)
            {
                View.Model.Deviations.ShouldContain(viewDeviation => viewDeviation.Id == deviation.Id);
            }
        }

        private void SkallListanMedAvvikelserPopuleras()
        {
            foreach (var deviation in _deviations)
            {
                View.Model.Deviations.ShouldContain(viewDeviation => viewDeviation.Id == deviation.Id);
            }
        }

        #endregion
    }
}