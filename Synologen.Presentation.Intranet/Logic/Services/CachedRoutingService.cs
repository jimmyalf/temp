using System.Collections.Generic;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public class CachedRoutingService : IRoutingService
	{
		private readonly IRoutingService _routingService;
		private IDictionary<int, string> _cache;

		public CachedRoutingService(IRoutingService routingService)
		{
			_routingService = routingService;
			_cache = new Dictionary<int, string>();
		}

		public virtual string GetPageUrl(int pageId)
		{
			if (_cache.ContainsKey(pageId)) return _cache[pageId];
			var url = _routingService.GetPageUrl(pageId);
			_cache.Add(pageId, url);
			return url;
		}

		public virtual void Clear()
		{
			_cache.Clear();
		}
	}
}