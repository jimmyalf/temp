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
				EmailOrderSubject = "B�gbest�llning fr�n synologen";

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
			    .AppendLine("Best�llnings-id: {OrderId}")
			    .AppendLine("Butik: {ShopName}")
			    .AppendLine("Butiksort: {ShopCity}")
			    .AppendLine("B�ge: {FrameName}")
			    .AppendLine("B�ge Artnr: {ArticleNumber}")
			    .AppendLine("PD V�nster: {PDLeft}")
			    .AppendLine("PD H�ger: {PDRight}")
			    .AppendLine("Glastyp: {GlassTypeName}")
			    .AppendLine("Sf�r V�nster: {SphereLeft}")
			    .AppendLine("Sf�r H�ger: {SphereRight}")
			    .AppendLine("Cylinder V�nster: {CylinderLeft}")
			    .AppendLine("Cylinder H�ger: {CylinderRight}")
			    .AppendLine("Axel V�nster: {AxisLeft}")
			    .AppendLine("Axel H�ger: {AxisRight}")
			    .AppendLine("Addition V�nster: {AdditionLeft}")
			    .AppendLine("Addition H�ger: {AdditionRight}")
			    .AppendLine("H�jd V�nster: {HeightLeft}")
			    .AppendLine("H�jd H�ger: {HeightRight}")
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