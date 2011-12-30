using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public class CachedRoutingService : RoutingService
	{
		private IDictionary<int, string> _cache;

		public CachedRoutingService()
		{
			_cache = new Dictionary<int, string>();
		}

		public override string GetPageUrl(int pageId)
		{
			if (_cache.ContainsKey(pageId)) return _cache[pageId];
			var url = base.GetPageUrl(pageId);
			_cache.Add(pageId, url);
			return url;
		}

		public override string GetPageUrl(int pageId, object requestParameters)
		{
			return this.GetPageUrl(pageId) + base.BuildQueryString(requestParameters);
		}

		public virtual void Clear()
		{
			_cache.Clear();
		}
	}
}