using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Presentation.Models.Deviation;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
    public class DeviationListView : CommonListView<DeviationListItem, Core.Domain.Model.Deviations.Deviation>
    {
        public DeviationListView() { }
        public DeviationListView(string search, IEnumerable<Core.Domain.Model.Deviations.Deviation> deviations) : base(deviations, search) { }

        public IList<DeviationCategory> DeviationCategories { get; set; }
        public IList<DeviationSupplier> DeviationSuppliers { get; set; }
        [DisplayName("Kategori")]
        public int SelectedCategory { get; set; }
        [DisplayName("Leverantör")]
        public int SelectedSupplier { get; set; }
    
        public override DeviationListItem Convert(Core.Domain.Model.Deviations.Deviation item)
        {
            var supplierName = string.Empty;
            if (item.Supplier != null)
                supplierName = item.Supplier.Name;

            return new DeviationListItem
            {
                Id = item.Id,
                ShopId = item.ShopId,
                CategoryName = item.Category.Name,
                CreatedDate = item.CreatedDate,
                SupplierName = supplierName,
                Type = item.Type.ToString()
            };
        }
    }

    public class DeviationListItem
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public string CategoryName { get; set; }
        public string Type { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}