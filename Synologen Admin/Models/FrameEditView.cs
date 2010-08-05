using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spinit.Wpc.Synologen.Core.Domain.Model;
using Spinit.Wpc.Synologen.Presentation.Helpers.Validation;

namespace Spinit.Wpc.Synologen.Presentation.Models
{
	public class FrameEditView
	{
		public int Id { get; set; }

		[DisplayName("Namn")]
		[Required(ErrorMessage="Namn saknas")]
		public string Name { get; set; }

		[DisplayName("Artikelnummer")]
		[Required(ErrorMessage="Artikelnummer saknas")]
		public string ArticleNumber { get; set; }

		[DisplayName("F�rg")]
		[Required(ErrorMessage="F�rg saknas")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		public int ColorId { get; set; }

		[DisplayName("M�rke")]
		[Required(ErrorMessage="M�rke saknas")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		public int BrandId { get; set; }

		[DisplayName("Min")]
		[Required(ErrorMessage="Index min saknas")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		public decimal IndexMinValue { get; set; }

		[DisplayName("Max")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		[Required(ErrorMessage="Index max saknas")]
		public decimal IndexMaxValue { get; set; }

		[DisplayName("Inkrement")]
		[GreaterThanZero(ErrorMessage = "V�rdet m�ste vara st�rre �n noll")]
		[Required(ErrorMessage="Index intervall inkrement saknas")]
		public decimal IndexIncrementation { get; set; }

		[DisplayName("Min")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		[Required(ErrorMessage="Sf�r min saknas")]
		public decimal SphereMinValue { get; set; }

		[DisplayName("Max")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		[Required(ErrorMessage="Sf�r max saknas")]
		public decimal SphereMaxValue { get; set; }

		[DisplayName("Inkrement")]
		[GreaterThanZero(ErrorMessage = "V�rdet m�ste vara st�rre �n noll")]
		[Required(ErrorMessage="Sf�r intervall inkrement saknas")]
		public decimal SphereIncrementation { get; set; }

		[DisplayName("Min")]
		[Required(ErrorMessage="Cylinder min saknas")]
		public decimal CylinderMinValue { get; set; }

		[DisplayName("Max")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		[Required(ErrorMessage="Cylinder max saknas")]
		public decimal CylinderMaxValue { get; set; }

		[DisplayName("Inkrement")]
		[GreaterThanZero(ErrorMessage = "V�rdet m�ste vara st�rre �n noll")]
		[Required(ErrorMessage="Cylinder intervall inkrement saknas")]
		public decimal CylinderIncrementation { get; set; }

		[DisplayName("Min")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		[Required(ErrorMessage="Pupill-distans min saknas")]
		public decimal PupillaryDistanceMinValue { get; set; }

		[DisplayName("Max")]
		[NotZero(ErrorMessage = "V�rdet f�r inte vara noll")]
		[Required(ErrorMessage="Pupill-distans max saknas")]
		public decimal PupillaryDistanceMaxValue { get; set; }

		[DisplayName("Inkrement")]
		[GreaterThanZero(ErrorMessage = "V�rdet m�ste vara st�rre �n noll")]
		[Required(ErrorMessage="Pupill-distans intervall inkrement saknas")]
		public decimal PupillaryDistanceIncrementation { get; set; }

		[DisplayName("Tillg�nglig f�r best�llning")]
		public bool AllowOrders { get; set; }

		public IEnumerable<FrameBrand> AvailableFrameBrands { get; set; }
		public IEnumerable<FrameColor> AvailableFrameColors { get; set; }

		public string FormLegend { get; set; }

		public static FrameEditView GetDefaultInstance(IEnumerable<FrameColor> availableFrameColors, IEnumerable<FrameBrand> availableFrameBrands, string formLegend)
		{
			return new FrameEditView
			{
				AllowOrders = true,
				IndexMinValue = 1.5m,
				IndexMaxValue = 1.6m,
				IndexIncrementation = 0.1m,
				SphereMinValue = -6,
				SphereMaxValue = 6,
				SphereIncrementation = 0.25m,
				PupillaryDistanceMinValue = 20,
				PupillaryDistanceMaxValue = 40,
				PupillaryDistanceIncrementation = 0.5m,
				CylinderMinValue = 0,
				CylinderMaxValue = 2,
				CylinderIncrementation = 0.25m,
				ColorId = 0,
				BrandId = 0,
                AvailableFrameBrands = availableFrameBrands,
                AvailableFrameColors = availableFrameColors,
				FormLegend = formLegend
			};
		}
	}
}