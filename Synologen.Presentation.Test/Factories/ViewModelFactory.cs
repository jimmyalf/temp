using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Models.LensSubscription;

namespace Spinit.Wpc.Synologen.Presentation.Test.Factories
{
	public static class ViewModelFactory
	{
		public static FrameEditView GetFrameEditView(int id)
		{
			return new FrameEditView {
				AllowOrders = true,
				ArticleNumber = "123",
				AvailableFrameBrands = null,
				AvailableFrameColors = null,
				BrandId = 5,
				ColorId = 3,
				FormLegend = null,
				Id = id,
				Name = "Nytt bågnamn",
				PupillaryDistanceIncrementation = 0.22m,
				PupillaryDistanceMaxValue = 11,
				PupillaryDistanceMinValue = 12,
				CurrentStock = 196,
				StockAtStockDate = 200, 
				StockDate = "2010-08-01"
			};
		}

		public static FrameColorEditView GetFrameColorEditView(int id) 
		{
			return new FrameColorEditView
			{
				FormLegend = null,
				Id = id,
				Name = "Frän testfärg"
			};
		}

		public static FrameBrandEditView GetFrameBrandEditView(int id) 
		{
			return new FrameBrandEditView
			{
				FormLegend = null,
				Id = id,
				Name = "Fränt testärke"
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
				Name = "Frän glastyp"
			};
		}

		public static SubscriptionView GetSubscriptionView(int subscriptionId, int customerId)
		{
			return new SubscriptionView
			       	{
			       		AccountNumber = "9876543321",
			       		ConsentDate = "Ja",
			       		AddressLineOne = "Kullerstensgatan 14",
			       		AddressLineTwo = "Ingång F",
			       		City = "Storstad",
			       		ClearingNumber = "1133",
			       		Country = "Sverige",
			       		Created = "2010-11-25",
			       		CustomerId = customerId,
			       		CustomerNotes = "Anteckningar",
			       		PostalCode = "122 55",
			       		Email = "test@test.nu",
			       		MobilePhone = "07026677883",
			       		Phone = "08-44556677",
			       		CustomerName = "Adam Bertil",
			       		PersonalIdNumber = "781121-0496",
			       		ShopName = "Testbutik",
			       		TransactionList = null,
			       		MonthlyAmount = "666,66",
			       		Status = SubscriptionStatus.Started.GetEnumDisplayName(),
			       		ErrorList = null,
			       		SubscriptionNotes = "Abonnemangsanteckningar",
			       		FirstName = "Adam",
			       		LastName = "Bertil"
			       	};
			
		}
	}
}