using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
    public class CategoryDefectListItemView
    {
        public CategoryDefectListItemView()
        {
            IsSelected = true;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}