using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Deviations
{
    [TestFixture, Category("Deviations_Create")]
    public class CreateDeviationSpec : DeviationSpecTestbase<CreateDeviationPresenter, ICreateDeviationView>
    {
        private CreateDeviationPresenter _presenter;
        private List<DeviationCategory> _categories;

        public CreateDeviationSpec()
        {
            Context = () =>
            {
                _presenter = GetPresenter();
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
                .När(AnvändarenSparar)
                .Så(SkallEnAvvikelseSparats));
        }

        #region Arrange

        private void Kategorier()
        {
            var firstCategory = new DeviationCategory
            {
                Active = true,
                Defects = null,
                Deviations = null,
                Name = "Kategori 1",
                Suppliers = null
            };

            var otherCategory = new DeviationCategory
            {
                Active = true,
                Defects = null,
                Deviations = null,
                Name = "Kategori 2",
                Suppliers = null
            };

            Save(firstCategory);
            Save(otherCategory);
            _categories = new List<DeviationCategory>(new[] { firstCategory, otherCategory });
        }

        private void Leverantörer()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Act

        private void SidanVisas()
        {
            _presenter.View_Load(null, new EventArgs());
        }

        private void AnvändarenSparar()
        {
            throw new NotImplementedException();
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

            // Failing test:
            // View.Model.SelectedCategoryId.ShouldBe(5);
        }

        private void LeverantörerPopuleras()
        {
            throw new NotImplementedException();
        }

        private void SkallEnAvvikelseSparats()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}