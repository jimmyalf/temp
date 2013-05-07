using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Presentation.Models.Deviation;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
    public class CategoryListView : CommonListView<CategoryListItem, Core.Domain.Model.Deviations.DeviationCategory>
    {
        public CategoryListView() { }
        public CategoryListView(string search, IEnumerable<Core.Domain.Model.Deviations.DeviationCategory> deviationCategories) : base(deviationCategories, search) { }

        public override CategoryListItem Convert(Core.Domain.Model.Deviations.DeviationCategory item)
        {

            return new CategoryListItem
            {
                Id = item.Id,
                Name = item.Name,
                Active = item.Active
            };
        }
    }

    public class CategoryListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}