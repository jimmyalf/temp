using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
    public class SupplierFormView
    {
        public SupplierFormView()
        {
            Active = true;
        }
        public SupplierFormView(int? id, DeviationSupplier supplier, IEnumerable<DeviationCategory> categories, int? supplierId)
        {
            SetCategories(categories, supplierId);
            Name = supplier.Name;
            Active = supplier.Active;
        }

        public SupplierFormView SetCategories(IEnumerable<DeviationCategory> categories, int? supplierId)
        {
            IList<SupplierCategoryListItemView> categoryListItems = new List<SupplierCategoryListItemView>();
            foreach (var c in categories)
            {
                var isSelected = c.Suppliers.Any(x => x.Id == supplierId);
                categoryListItems.Add(new SupplierCategoryListItemView { Id = c.Id, Name = c.Name, IsSelected = isSelected });
            }

            Categories = categoryListItems;
            return this;
        }

        public IList<SupplierCategoryListItemView> Categories { get; set; }

        public int Id { get; set; }

        [DisplayName("Namn"), Required]
        public string Name { get; set; }

        [DisplayName("Aktiv")]
        public bool Active { get; set; }

        [DisplayName("E-post")]
        public string Email { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        public string FormLegend { get; set; }

    }

    

}