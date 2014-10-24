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

        [DisplayName("Leverant�r")]
        [Required(ErrorMessage = "Leverant�r saknas")]
        public int SupplierId { get; set; }

		[DisplayName("Namn")]
		[Required(ErrorMessage="Namn saknas")]
		public string Name { get; set; }

		[DisplayName("Artikelnummer")]
		[Required(ErrorMessage="Artikelnummer saknas")]
		public string ArticleNumber { get; set; }

		[DisplayName("F�rg")]
		[Required(ErrorMessage="F�rg saknas")]
		public int ColorId { get; set; }

		[DisplayName("M�rke")]
		[Required(ErrorMessage="M�rke saknas")]
		public int BrandId { get; set; }

		[DisplayName("Aktuellt lagersaldo")]
		public int? CurrentStock { get; set; }

		[DisplayName("Lagersaldo")]
		[Range(1, int.MaxValue, ErrorMessage = "Lagersaldo m�ste vara st�rre �n noll")]
		public int StockAtStockDate { get; set; }

		[DisplayName("Lagersaldo uppdaterades senast")]
		public string StockDate { get; set; }

		[DisplayName("Min")]
		[GreaterThanZero(ErrorMessage = "V�rdet m�ste vara st�rre �n noll")]
		[Required(ErrorMessage="Pupill-distans min saknas")]
		public decimal PupillaryDistanceMinValue { get; set; }

		[DisplayName("Max")]
		[GreaterThanZero(ErrorMessage = "V�rdet m�ste vara st�rre �n noll")]
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
        public IEnumerable<FrameSupplier> AvailableFrameSuppliers { get; set; }

		public string FormLegend { get; set; }


		public static FrameEditView GetDefaultInstance(IEnumerable<FrameColor> availableFrameColors, IEnumerable<FrameBrand> availableFrameBrands, IEnumerable<FrameSupplier> availableFramesuppliers, string formLegend)
		{
			return new FrameEditView
			{
				AllowOrders = true,
				PupillaryDistanceMinValue = 20,
				PupillaryDistanceMaxValue = 40,
				PupillaryDistanceIncrementation = 0.5m,
				ColorId = 0,
				BrandId = 0,
                SupplierId = 0,
                AvailableFrameBrands = availableFrameBrands,
                AvailableFrameColors = availableFrameColors,
                AvailableFrameSuppliers = availableFramesuppliers,
				FormLegend = formLegend
			};
		}
	}
}