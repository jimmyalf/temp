using System;
using System.Web;
using Spinit.Wpc.Core.UI.Mvp.Site;
using Spinit.Wpc.Synologen.Core.Persistence;

namespace Spinit.Wpc.Synologen.Presentation.Site
{
	public class UnitOfWorkModule : IHttpModule, IDisposable
	{
		public void Init(HttpApplication context)
		{
			context.EndRequest += ContextEndRequest;
		}

		private void ContextEndRequest(object sender, EventArgs e)
		{
			Dispose();
		}

		public void Dispose()
		{
			var unitOfWork = Container.Resolve<IUnitOfWork>();
			try
			{
				unitOfWork.Commit();
			}
			catch
			{
				unitOfWork.Rollback();
			}
			finally
			{
				unitOfWork.Dispose();
			}
		}
	}
}