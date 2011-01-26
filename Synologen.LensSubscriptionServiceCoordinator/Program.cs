using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;
using StructureMap;

namespace Spinit.Wpc.Synologen.LensSubscriptionServiceCoordinator
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Bootstrapper.Bootstrap();
			var tasks = ObjectFactory.GetAllInstances<ITask>();

		}
	}
}