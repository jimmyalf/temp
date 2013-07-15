using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Presentation.Helpers.Validation;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameGlassTypeEditView
	{
		public int Id { get; set; }
		public string FormLegend { get; set; }

        [DisplayName("Leverantör")]
        [Required(ErrorMessage = "Leverantör saknas")]
        public int SupplierId { get; set; }

		[DisplayName("Namn")]
		[Required(ErrorMessage = "Namn måste anges")]
		public string Name { get; set; }

		[DisplayName("Inkludera Additionparameter i order")]
		public bool IncludeAdditionParametersInOrder { get; set; }

		[DisplayName("Inkludera Höjdparameter i order")]
		public bool IncludeHeightParametersInOrder { get; set; }

		[DisplayName("Inkrement")]
		[GreaterThanZero(ErrorMessage = "Värdet måste vara större än noll")]
		[Required(ErrorMessage="Sfär intervall inkrement saknas")]
		public decimal SphereIncrementation { get; set; }

		[DisplayName("Max")]
		[Required(ErrorMessage="Sfär max saknas")]
		public decimal SphereMaxValue { get; set; }

		[DisplayName("Min")]
		[Required(ErrorMessage="Sfär min saknas")]
		public decimal SphereMinValue { get; set; }

		[DisplayName("Inkrement")]
		[GreaterThanZero(ErrorMessage = "Värdet måste vara större än noll")]
		[Required(ErrorMessage="Cylinder intervall inkrement saknas")]
		public decimal CylinderIncrementation { get; set; }

		[DisplayName("Max")]
		[Required(ErrorMessage="Cylinder max saknas")]
		public decimal CylinderMaxValue { get; set; }

		[DisplayName("Min")]
		[Required(ErrorMessage="Cylinder min saknas")]
		public decimal CylinderMinValue { get; set; }

	    public IEnumerable<FrameSupplier> AvailableFrameSuppliers { get; set; }

	    public static FrameGlassTypeEditView GetDefaultInstance(string formLegend, IEnumerable<FrameSupplier> availableFramesuppliers)
		{
			return new FrameGlassTypeEditView
			{
				FormLegend = formLegend,
				SphereMinValue = -6,
				SphereMaxValue = 6,
				SphereIncrementation = 0.25M,
				CylinderMinValue = -2,
				CylinderMaxValue = 0,
				CylinderIncrementation = 0.25M,
                AvailableFrameSuppliers = availableFramesuppliers
			};
		}
	}



}