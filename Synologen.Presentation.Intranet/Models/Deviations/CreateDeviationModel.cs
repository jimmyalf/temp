using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations
{
	public class CreateDeviationModel
	{

        public CreateDeviationModel()
        {
            DisplayInternalDeviation = false;
            DisplayExternalDeviation = false;
        }

        public IEnumerable<DeviationCategoryListItem> Categories { get; set; }
        public IEnumerable<DeviationDefectListItem> Defects { get; set; }
        public IEnumerable<DeviationTypeListItem> Types { get; set; }
        public IEnumerable<DeviationSupplierListItem> Suppliers { get; set; }

        public int SelectedType { get; set; }
        public int SelectedCategoryId { get; set; }
        public int SelectedSupplierId { get; set; }
        
        public bool DisplayInternalDeviation { get; set; }
        public bool DisplayExternalDeviation { get; set; }
        
        public string DeviationCategoryName { get; set; }
        public string DefectDescription { get; set; }

        public bool Success { get; set; }
        public string Status { get; set; }
	}


}