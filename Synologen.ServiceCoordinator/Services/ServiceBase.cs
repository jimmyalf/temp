using System;
using System.Threading;
using Spinit.Wpc.Synologen.Core.Domain.Services.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.ServiceCoordinator.Services
{
	public abstract class ServiceBase : ICoordinatorService
	{
		protected ServiceBase(IBGWebService bgWebService)
		{
			BGWebService = bgWebService;
			Timer = new Timer(obj => Execute(),this, TimeSpan.Zero, TimeSpan.FromSeconds(30));
		}

		protected IBGWebService BGWebService { get; private set; }
		protected Timer Timer { get; private set; }

		public abstract void Start();
		public abstract void Execute();
		public abstract void Stop();
	}
}