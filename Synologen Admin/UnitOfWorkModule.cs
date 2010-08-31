using System;
using System.Web;
using Spinit.Data;
using Spinit.Wpc.Core.UI.Mvc;

namespace Spinit.Wpc.Synologen.Presentation
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
			var unitOfWork = ServiceLocation.Resolve<IUnitOfWork>();
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