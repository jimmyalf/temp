using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
    public class DeviationListItemView
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public string CategoryName { get; set; }
        public string Type { get; set; }
        public string SupplierName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}