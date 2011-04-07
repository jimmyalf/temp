using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
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

		[DisplayName("Färg")]
		[Required(ErrorMessage="Färg saknas")]
		public int ColorId { get; set; }

		[DisplayName("Märke")]
		[Required(ErrorMessage="Märke saknas")]
		public int BrandId { get; set; }

		[DisplayName("Aktuellt lagersaldo")]
		public int? CurrentStock { get; set; }

		[DisplayName("Lagersaldo")]
		[Range(1, int.MaxValue, ErrorMessage = "Lagersaldo måste vara större än noll")]
		public int StockAtStockDate { get; set; }

		[DisplayName("Lagersaldo uppdaterades senast")]
		public string StockDate { get; set; }

		[DisplayName("Min")]
		[GreaterThanZero(ErrorMessage = "Värdet måste vara större än noll")]
		[Required(ErrorMessage="Pupill-distans min saknas")]
		public decimal PupillaryDistanceMinValue { get; set; }

		[DisplayName("Max")]
		[GreaterThanZero(ErrorMessage = "Värdet måste vara större än noll")]
		[Required(ErrorMessage="Pupill-distans max saknas")]
		public decimal PupillaryDistanceMaxValue { get; set; }

		[DisplayName("Inkrement")]
		[GreaterThanZero(ErrorMessage = "Värdet måste vara större än noll")]
		[Required(ErrorMessage="Pupill-distans intervall inkrement saknas")]
		public decimal PupillaryDistanceIncrementation { get; set; }

		[DisplayName("Tillgänglig för beställning")]
		public bool AllowOrders { get; set; }

		public IEnumerable<FrameBrand> AvailableFrameBrands { get; set; }
		public IEnumerable<FrameColor> AvailableFrameColors { get; set; }

		public string FormLegend { get; set; }

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


		public static FrameEditView GetDefaultInstance(IEnumerable<FrameColor> availableFrameColors, IEnumerable<FrameBrand> availableFrameBrands, string formLegend)
		{
			return new FrameEditView
			{
				AllowOrders = true,
				//IndexMinValue = 1.5m,
				//IndexMaxValue = 1.6m,
				//IndexIncrementation = 0.1m,
				SphereMinValue = -6,
				SphereMaxValue = 6,
				SphereIncrementation = 0.25m,
				PupillaryDistanceMinValue = 20,
				PupillaryDistanceMaxValue = 40,
				PupillaryDistanceIncrementation = 0.5m,
				CylinderMinValue = -2,
				CylinderMaxValue = 0,
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