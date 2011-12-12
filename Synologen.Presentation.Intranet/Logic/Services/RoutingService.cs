using Spinit.Wpc.Content.Data;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services
{
	public class RoutingService : IRoutingService
	{
		private readonly Tree _treeRepository;

		public RoutingService()
		{
			var connectionString = Utility.Business.Globals.ConnectionString("WpcServer");
			_treeRepository = new Tree(connectionString);	
		}

		public string GetPageUrl(int pageId)
		{
			return _treeRepository.GetFileUrlDownString(pageId);
		}
	}
}