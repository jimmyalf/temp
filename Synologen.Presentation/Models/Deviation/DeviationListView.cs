using System;
using System.Collections.Generic;
using System.ComponentModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Models.Deviation
{
    public class DeviationListView
    {
        public IEnumerable<DeviationListItemView> List { get; set; }
        [DisplayName("Filtrera")]
        public string SearchTerm { get; set; }
    }

    
}