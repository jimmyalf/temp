using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.SendPayments
{
	public class Task : TaskBase
	{
		private readonly IBGPaymentToSendRepository _bgPaymentToSendRepository;

		public Task(
			ILoggingService loggingService, 
			IBGPaymentToSendRepository bgPaymentToSendRepository) 
			: base("SendPayments", loggingService, BGTaskSequenceOrder.SendTask)
		{
			_bgPaymentToSendRepository = bgPaymentToSendRepository;
		}

		public override void Execute()
		{
			RunLoggedTask(() =>
			{
				var payments = _bgPaymentToSendRepository.FindBy(new AllNewPaymentsToSendCriteria());
			});
		}
	}
}