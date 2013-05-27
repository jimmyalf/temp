using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Deviations
{
    [TestFixture, Category("Deviations_Create")]
    public class CreateDeviationSpec : DeviationSpecTestbase<CreateDeviationPresenter, ICreateDeviationView>
    {
        private CreateDeviationPresenter _presenter;
        private List<DeviationCategory> _categories;
        private List<DeviationSupplier> _suppliers;
        private List<DeviationComment> _comments;
        private CreateDeviationEventArgs _args;
        private Shop _shop;
        private CreateDeviationEventArgs _categorySelectionArgs;

        public CreateDeviationSpec()
        {
            Context = () =>
            {
                _presenter = GetPresenter();
                _shop = CreateShop<Shop>();
                A.CallTo(() => SynologenMemberService.GetCurrentShopId()).Returns(_shop.Id);
            };

            Story = () => new Berättelse("Skapa avvikelse")
                            .FörAtt("Skapa en ny avvikelse")
                            .Som("inloggad användare på intranätet")
                            .VillJag("kunna ange avvikelse-detaljer");

        }

        [Test]
        public void VisaSkapaInternAvvikelseSida()
        {
            SetupScenario(scenario => scenario
                .Givet(Kategorier)
                .När(SidanVisas)
                .Så(SkallKategorierPopuleras)
                .Och(TyperPopuleras));
        }

        [Test]
        public void VisaSkapaExternAvvikelseSida()
        {
            SetupScenario(scenario => scenario
                .Givet(Kategorier)
                    .Och(Leverantörer)
                .När(SidanVisas)
                .Och(KategoriOchExternTypValts)
                .Så(SkallKategorierPopuleras)
                    .Och(TyperPopuleras)
                    .Och(LeverantörerPopuleras));
        }

        [Test]
        public void SparaExternAvvikelse()
        {
            SetupScenario(scenario => scenario
                .Givet(Kategorier)
                    .Och(Leverantörer)
                    .Och(AnvändarenFylltIFormuläretFörExternAvvikelse)
                .När(AnvändarenSparar)
                .Så(SkallEnExternAvvikelseSparats));
        }

        [Test]
        public void SparaInternAvvikelse()
        {
            SetupScenario(scenario => scenario
                .Givet(Kategorier)
                    .Och(AnvändarenFylltIFormuläretFörInternAvvikelse)
                .När(AnvändarenSparar)
                .Så(SkallEnInternAvvikelseSparats));
        }

        #region Arrange

        public void Kategorier()
        {
            _categories = SkapaKategorier();
        }

        public void Leverantörer()
        {
            _suppliers = SkapaLeverantörer();
        }

        private void AnvändarenFylltIFormuläretFörExternAvvikelse()
        {
            _args = new CreateDeviationEventArgs
                {
                    DefectDescription = "Beskrivning",
                    SelectedCategory = _categories.First().Id,
                    SelectedDefects = new List<DeviationDefectListItem>(),
                    SelectedSupplier = _suppliers.First().Id,
                    SelectedType = DeviationType.External
                };
        }

        private void AnvändarenFylltIFormuläretFörInternAvvikelse()
        {
            _args = new CreateDeviationEventArgs
            {
                DefectDescription = "Beskrivning",
                SelectedCategory = _categories.First().Id,
                SelectedDefects = new List<DeviationDefectListItem>(),
                SelectedType = DeviationType.External
            };
        }

        #endregion

        #region Act

        private void SidanVisas()
        {
            _presenter.View_Load(null, new EventArgs());
        }

        private void KategoriOchExternTypValts()
        {
            _categorySelectionArgs = new CreateDeviationEventArgs
            {
                SelectedCategory = _categories.First().Id,
                SelectedType = DeviationType.External
            };
            _presenter.View_CategorySelected(null, _categorySelectionArgs);
        }

        private void AnvändarenSparar()
        {
            _presenter.View_Submit(null, _args);
        }

        #endregion

        #region Assert

        private void SkallKategorierPopuleras()
        {
            foreach (var deviationCategory in _categories)
            {
                View.Model.Categories.ShouldContain(viewCategory => viewCategory.Id == deviationCategory.Id && viewCategory.Name == deviationCategory.Name);
            }
        }

        private void TyperPopuleras()
        {
            foreach (DeviationType type in EnumExtensions.Enumerate<DeviationType>())
            {
                var displayName = type.GetEnumDisplayName();
                View.Model.Types.ShouldContain(viewType => viewType.Id == ((int)type));
                View.Model.Types.ShouldContain(viewType => viewType.Name == displayName);
            }

        }

        private void LeverantörerPopuleras()
        {
            var expectedSuppliers = _suppliers.Where(x => x.Categories.Any(y => y.Id == _categorySelectionArgs.SelectedCategory));
            foreach (var deviationSupplier in expectedSuppliers)
            {
                View.Model.Suppliers.ShouldContain(viewSupplier => viewSupplier.Id == deviationSupplier.Id);
            }
        }

        private void SkallEnExternAvvikelseSparats()
        {
            var savedDeviation = GetAll<Deviation>().Single();
            savedDeviation.DefectDescription.ShouldBe(_args.DefectDescription);
            savedDeviation.Category.Id.ShouldBe(_args.SelectedCategory);
            savedDeviation.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
            savedDeviation.Supplier.Id.ShouldBe(_args.SelectedSupplier);
            savedDeviation.Type.ShouldBe(_args.SelectedType);
            savedDeviation.ShopId.ShouldBe(_shop.Id);
        }

        private void SkallEnInternAvvikelseSparats()
        {
            var savedDeviation = GetAll<Deviation>().Single();
            savedDeviation.DefectDescription.ShouldBe(_args.DefectDescription);
            savedDeviation.Category.Id.ShouldBe(_args.SelectedCategory);
            savedDeviation.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
            savedDeviation.Type.ShouldBe(_args.SelectedType);
            savedDeviation.ShopId.ShouldBe(_shop.Id);
        }

        #endregion
    }
}