using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
    public class CategoryFormView
    {
        public CategoryFormView()
        {
            Active = true;
        }
        public CategoryFormView(int? id, DeviationCategory category, IEnumerable<DeviationDefect> defects)
        {
            SetDefects(defects);
            Name = category.Name;
            Active = category.Active;
        }

        public CategoryFormView SetDefects(IEnumerable<DeviationDefect> defects)
        {

            IList<CategoryDefectListItemView> defectListItems = new List<CategoryDefectListItemView>();
            foreach (var d in defects)
            {
                defectListItems.Add(new CategoryDefectListItemView { Id = d.Id, Name = d.Name });
            }

            Defects = defectListItems;
            return this;
        }

        public int Id { get; set; }

        [DisplayName("Namn"), Required]
        public string Name { get; set; }

        [DisplayName("Aktiv")]
        public bool Active { get; set; }

        public IList<CategoryDefectListItemView> Defects { get; set; }
        public string DefectName { get; set; }

        public string FormLegend { get; set; }

    }

    //public class CategoryDefectListItem
    //{
    //    public CategoryDefectListItem()
    //    {
    //        IsSelected = true;
    //    }

    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public bool IsSelected { get; set; }

    //}
}