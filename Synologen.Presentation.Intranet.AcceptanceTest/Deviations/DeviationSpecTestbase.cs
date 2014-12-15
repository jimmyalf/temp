using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Deviations
{
    public abstract class DeviationSpecTestbase<TPresenter, TView> : SpecTestbase<TPresenter, TView>
        where TPresenter : Presenter<TView>
        where TView : class, IView
    {

        public List<DeviationCategory> SkapaKategorier()
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
            return new List<DeviationCategory>(new[] { firstCategory, otherCategory });
        }

        public List<DeviationSupplier> SkapaLeverantörer()
        {
            var firstSupplier = new DeviationSupplier
            {
                Active = true,
                Categories = SkapaKategorier(),
                Email = "myemail@myemail.ve",
                Name = "My Name",
                Phone = "0123456",
            };

            var otherSupplier = new DeviationSupplier
            {
                Active = true,
                Categories = SkapaKategorier(),
                Email = "tester@tester.ve",
                Name = "My OtherName",
                Phone = "9876543"
            };

            Save(firstSupplier);
            Save(otherSupplier);
            return new List<DeviationSupplier> { firstSupplier, otherSupplier };
        }

        
        
    }
}