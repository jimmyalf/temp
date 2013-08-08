using System;
using System.Collections;
using System.Web;

namespace Spinit.Wpc.Forum.Presentation.Site {
	public partial class CacheClearer : System.Web.UI.UserControl {
		protected void Page_Load(object sender, EventArgs e) {
			IDictionaryEnumerator CacheEnum = HttpRuntime.Cache.GetEnumerator();
			while (CacheEnum.MoveNext()){
				HttpRuntime.Cache.Remove(CacheEnum.Key.ToString());
			}
		}
	}
}