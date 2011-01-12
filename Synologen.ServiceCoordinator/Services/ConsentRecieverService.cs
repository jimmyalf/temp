using System;
using Spinit.Wpc.Synologen.Core.Domain.Services.BGWebService;

namespace Synologen.ServiceCoordinator.Services
{
	public class ConsentRecieverService : ServiceBase
	{
		public ConsentRecieverService(IBGWebService bgWebService) : base(bgWebService){}
		public override void Start() { throw new NotImplementedException(); }
		public override void Execute() { throw new NotImplementedException(); }
		public override void Stop() { throw new NotImplementedException(); }
	}
}