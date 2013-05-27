using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Deviations
{
    [TestFixture, Category("Deviations_InternalList")]
    public class InternalDeviationListSpec : DeviationSpecTestbase<InternalDeviationListPresenter, IInternalDeviationListView>
    {
        private InternalDeviationListPresenter _presenter;
        private List<Deviation> _deviations;
        private List<DeviationCategory> _categories;
        private Shop _shop;

        public InternalDeviationListSpec()
        {
            Context = () =>
            {
                _presenter = GetPresenter();
                _shop = CreateShop<Shop>();
                A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
            };

            Story = () => new Berättelse("Visa interna avvikelser")
                            .FörAtt("Visa en lista med avvikelser")
                            .Som("inloggad användare på intranätet")
                            .VillJag("kunna ange avvikelse-detaljer");
        }

        [Test]
        public void VisaInternaAvvikelseListSidan()
        {
            SetupScenario(scenario => scenario
                .Givet(InternaAvvikelser)
                .När(SidanVisas)
                .Så(SkallListanPopuleras));
        }

        #region Arrange

        private void InternaAvvikelser()
        {
            _categories = SkapaKategorier();

            var firstDeviation = new Deviation
            {
                Category = _categories.First(),
                CreatedDate = DateTime.Now,
                DefectDescription = "Defekt 1",
                ShopId = _shop.Id,
                Type = DeviationType.Internal
            };

            var otherDeviation = new Deviation
            {
                Category = _categories.First(),
                CreatedDate = DateTime.Now,
                DefectDescription = "Defekt 2",
                ShopId = _shop.Id,
                Type = DeviationType.Internal
            };

            Save(firstDeviation);
            Save(otherDeviation);
            _deviations = new List<Deviation>(new[] { firstDeviation, otherDeviation });
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

        #endregion

        #region Assert

        private void SkallListanPopuleras()
        {
            foreach (var deviation in _deviations)
            {
                View.Model.Deviations.ShouldContain(viewDeviation => viewDeviation.Id == deviation.Id);
            }
        }

        #endregion
    }
}