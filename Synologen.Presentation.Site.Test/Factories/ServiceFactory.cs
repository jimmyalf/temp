using System;
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

		internal class MockFrameOrderSettingsService : IFrameOrderService{
			private string _bodytext;

			public MockFrameOrderSettingsService()
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

			public string SentEmailBody { get; private set; }
			public void SetupEmailBody(string bodyText){ _bodytext = bodyText;}

			public string CreateOrderEmailBody(FrameOrder order) { return _bodytext; }
			public void SendEmail(string body)
			{
				SentEmailBody = _bodytext;
			}
		}

		internal class MockedSessionProviderService : ISynologenMemberService {
			private int _shopId;
			private string _pageUrl;

			public void SetMockedShopId(int id){ _shopId = id;}
			public void SetMockedPageUrl(string url){ _pageUrl = url;}
			public int GetCurrentShopId() { return _shopId; }
			public int GetCurrentMemberId() { throw new NotImplementedException(); }
			public string GetPageUrl(int pageId) { return _pageUrl; }
		}
	}

	
}