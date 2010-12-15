using System;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.Factories
{
	public static class ServiceFactory {
		public static IFrameOrderService GetFrameOrderSettingsService() 
		{
			return new MockFrameOrderSettingsService();
		}

		public static ISynologenMemberService GetSynologenMemberService()
		{
			return new MockedSessionProviderService();
		}

		public static ISynologenSettingsService GetSynologenSettingsService() { 
			return new MockedSynologenSettingsService();
		}

		internal class MockedSynologenSettingsService : ISynologenSettingsService
		{
			public MockedSynologenSettingsService()
			{
				Sphere = new Interval {Increment = 0.25M, Max = 6, Min = -6};
				Cylinder = new Interval {Increment = 0.25M, Max = 2, Min = 0};
				Addition = new Interval {Increment = 0.25M, Max = 3, Min = 1};
				Height = new Interval {Increment = 1, Max = 28, Min = 18};
				EmailOrderFrom = "frameorders@synologen.se";
				EmailOrderSupplierEmail = "carl.berg@spinit.se";
				EmailOrderSubject = "Bågbeställning från synologen";

			}
			public Interval Sphere { get; private set; }
			public Interval Cylinder { get; private set; }
			public Interval Addition { get; private set; }
			public Interval Height { get; private set; }
			public string EmailOrderSupplierEmail { get; private set; }
			public string EmailOrderFrom { get; private set; }
			public string EmailOrderSubject { get; private set; }
			public string GetFrameOrderEmailBodyTemplate() { 
			var builder = new StringBuilder()
			    .AppendLine("Beställnings-id: {OrderId}")
			    .AppendLine("Butik: {ShopName}")
			    .AppendLine("Butiksort: {ShopCity}")
			    .AppendLine("Båge: {FrameName}")
			    .AppendLine("Båge Artnr: {ArticleNumber}")
			    .AppendLine("PD Vänster: {PDLeft}")
			    .AppendLine("PD Höger: {PDRight}")
			    .AppendLine("Glastyp: {GlassTypeName}")
			    .AppendLine("Sfär Vänster: {SphereLeft}")
			    .AppendLine("Sfär Höger: {SphereRight}")
			    .AppendLine("Cylinder Vänster: {CylinderLeft}")
			    .AppendLine("Cylinder Höger: {CylinderRight}")
			    .AppendLine("Axel Vänster: {AxisLeft}")
			    .AppendLine("Axel Höger: {AxisRight}")
			    .AppendLine("Addition Vänster: {AdditionLeft}")
			    .AppendLine("Addition Höger: {AdditionRight}")
			    .AppendLine("Höjd Vänster: {HeightLeft}")
			    .AppendLine("Höjd Höger: {HeightRight}")
			    .AppendLine("Anteckningar: \r\n{Reference}");
			return builder.ToString();
			}
		}

		internal class MockFrameOrderSettingsService : IFrameOrderService{
			public FrameOrder SentFrameOrder { get; private set; }
			public void SendOrder(FrameOrder order) {
				SentFrameOrder = order;
			}
		}


		internal class MockedSessionProviderService : ISynologenMemberService {
			private int _shopId;
			private string _pageUrl;
			private bool _shopHasAccess;

			public void SetMockedShopId(int id){ _shopId = id;}
			public void SetMockedPageUrl(string url){ _pageUrl = url;}
			public void SetShopHasAccess(bool value){ _shopHasAccess = value;}
			public int GetCurrentShopId() { return _shopId; }
			public int GetCurrentMemberId() { throw new NotImplementedException(); }
			public string GetPageUrl(int pageId) { return _pageUrl; }
			public bool ShopHasAccessTo(ShopAccess accessOption) { return _shopHasAccess; }
			public bool ValidateUserPassword(string password) { throw new NotImplementedException(); }
			public string GetUserName() { throw new NotImplementedException(); }
		}

	}

	
}