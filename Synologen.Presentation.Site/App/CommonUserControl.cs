using Spinit.Wpc.Synologen.Business.Domain.Interfaces;

namespace Spinit.Wpc.Synologen.Presentation.Site.App
{
	public abstract class CommonUserControl : System.Web.UI.UserControl
	{
		private ISqlProvider _sqlProvider;
		protected ISqlProvider Provider
		{
			get 
			{
				if (_sqlProvider != null) return _sqlProvider;
				_sqlProvider = GetSqlProvider();
				return _sqlProvider;
			}
		}

		protected virtual ISqlProvider GetSqlProvider()
		{
			return Core.UI.ServiceLocator.Current.GetInstance<ISqlProvider>();
		}
	}
}