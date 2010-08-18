using Spinit.Wpc.Synologen.Presentation.Models;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories
{
	public static class ViewModelFactory
	{
		public static FrameEditView GetFrameEditView(int id)
		{
			return new FrameEditView
			{
				AllowOrders = true,
				ArticleNumber = "123",
				AvailableFrameBrands = null,
				AvailableFrameColors = null,
				BrandId = 5,
				ColorId = 3,
				FormLegend = null,
				Id = id,
				Name = "Nytt b�gnamn",
				PupillaryDistanceIncrementation = 0.22m,
				PupillaryDistanceMaxValue = 11,
				PupillaryDistanceMinValue = 12,
			};
		}

		public static FrameColorEditView GetFrameColorEditView(int id) 
		{
			return new FrameColorEditView
			{
				FormLegend = null,
				Id = id,
				Name = "Fr�n testf�rg"
			};
		}

		public static FrameBrandEditView GetFrameBrandEditView(int id) 
		{
			return new FrameBrandEditView
			{
				FormLegend = null,
				Id = id,
				Name = "Fr�nt test�rke"
			};
		}

		public static FrameGlassTypeEditView GetGlassTypeEditView(int id) 
		{
			return new FrameGlassTypeEditView
			{
				FormLegend = null,
				Id = id,
				IncludeAdditionParametersInOrder = true,
				IncludeHeightParametersInOrder = false,
				Name = "Fr�n glastyp"
			};
		}
	}
}