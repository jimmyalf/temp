using System;
using Spinit.Wpc.Synologen.Core.Domain.Services.BGWebService;

namespace Synologen.ServiceCoordinator.Services
{
	public class PaymentSenderService : ServiceBase
	{
		public PaymentSenderService(IBGWebService bgWebService) : base(bgWebService) {}
		public override void Start() { throw new NotImplementedException(); }
		public override void Execute() { throw new NotImplementedException(); }
		public override void Stop() { throw new NotImplementedException(); }
	}
}