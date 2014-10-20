using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
    public class FrameSupplierEditView
    {
        public int Id { get; set; }
        [DisplayName("Namn")]
        [Required(ErrorMessage = "Namn m�ste anges")]
        public string Name { get; set; }
        [DisplayName("E-post")]
        [Required(ErrorMessage = "E-post m�ste anges")]
        public string Email { get; set; }
        public string FormLegend { get; set; }
    }
}