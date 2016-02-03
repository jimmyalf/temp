using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Presentation.Models.Deviation;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
    public class SupplierListView
    {

        public IEnumerable<SupplierListItemView> List { get; set; }
        [DisplayName("Filtrera")]
        public string SearchTerm { get; set; }

        //public SupplierListView() { }
        //public SupplierListView(string search, IEnumerable<Core.Domain.Model.Deviations.DeviationSupplier> deviationSuppliers) : base(deviationSuppliers, search) { }

        //public override SupplierListItem Convert(Core.Domain.Model.Deviations.DeviationSupplier item)
        //{
        //    return new SupplierListItem
        //    {
        //        Id = item.Id,
        //        Name = item.Name,
        //        Active = item.Active
        //    };
        //}
    }

    //public class SupplierListItem
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public bool Active { get; set; }
    //}
}