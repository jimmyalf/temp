using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers
{
	public class FakeRoutingService : RoutingServiceBase, IRoutingService
	{
		private readonly Dictionary<int, string> _urlInterceptors;
		public FakeRoutingService() 
		{
		    _urlInterceptors = new Dictionary<int, string>();
		}

		public void AddRoute(int pageId, string url)
		{
		    _urlInterceptors.Add(pageId, url);
		}

		public override string GetPageUrl(int pageId)
		{
			return !_urlInterceptors.ContainsKey(pageId) ? null : _urlInterceptors[pageId];
		}

		public void Clear()
		{
		    _urlInterceptors.Clear();
		}

	}
}