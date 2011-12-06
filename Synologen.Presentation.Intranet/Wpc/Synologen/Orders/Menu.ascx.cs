using System.Collections.Generic;
using System.Web.UI;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using StructureMap;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders
{
	public partial class Menu : UserControl
	{
		private ISynologenMemberService _synologenMemberService;
		private const string CacheKey = "Synologen_Menu_Cache_Key";

		protected Menu()
		{
			_synologenMemberService = ObjectFactory.GetInstance<ISynologenMemberService>();
		}
		
		public string GetPageUrl(int pageId)
		{
			return GetUrlFromCacheOrService(pageId);
		}

		private string GetUrlFromCacheOrService(int pageId)
		{
			var cachedUrl = GetRouteFromCache(pageId);
			if(cachedUrl != null) return cachedUrl;
			var url = _synologenMemberService.GetPageUrl(pageId);
			AddRouteToCache(pageId, url);
			return url;
		}


		protected virtual string GetRouteFromCache(int pageId)
		{
			var cachedRoutes = GetCachedRoutes();
			return cachedRoutes.ContainsKey(pageId) ? cachedRoutes[pageId] : null;
		}

		protected virtual void AddRouteToCache(int pageId, string url)
		{
			var cachedRoutes = GetCachedRoutes();
			cachedRoutes.Add(pageId, url);
			Page.Cache[CacheKey] = cachedRoutes;
		}

		protected virtual IDictionary<int,string> GetCachedRoutes()
		{
			return Page.Cache[CacheKey] as IDictionary<int, string> ?? new Dictionary<int, string>();
		}
	}
}