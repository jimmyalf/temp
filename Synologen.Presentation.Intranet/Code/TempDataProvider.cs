using System.Collections.Generic;
using System.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Code
{
	public class TempDataProvider : ITempDataProvider
	{
		private IDictionary<string, object> _readCache;
		private readonly IDictionary<string, object> _writeCache;
		private const string CacheKey = "__spinit_webforms_tempdata";

		public TempDataProvider()
		{
			_readCache = new Dictionary<string, object>();
			_writeCache = new Dictionary<string, object>();
		}
		public void Set(string key, object value)
		{
			_writeCache[key] = value;
		}
		public object Get(string key)
		{
			if (_writeCache.ContainsKey(key)) return _writeCache[key];
			return _readCache.ContainsKey(key) ? _readCache[key] : null;
		}
		public T Get<T>(string key) where T : class
		{
			if (_writeCache.ContainsKey(key)) return _writeCache[key] as T;
			return _readCache.ContainsKey(key) ? _readCache[key] as T : null;
		}

		public void Read(HttpContext context)
		{
			_readCache = (context.Session[CacheKey] as IDictionary<string, object>) ?? new Dictionary<string, object>();
		}

		public void Write(HttpContext context)
		{
			context.Session[CacheKey] = _writeCache;
		}
	}

	public interface ITempDataProvider
	{
		void Set(string key, object value);
		object Get(string key);
		T Get<T>(string key) where T : class;
	}
}