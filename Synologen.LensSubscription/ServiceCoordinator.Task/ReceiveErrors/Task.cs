using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveErrors
{
	public class Task : TaskBaseWithWebService
	{
		public Task(ILoggingService loggingService, IBGWebServiceClient bgWebServiceClient) 
			: base("RecieveErrorsTask", loggingService, bgWebServiceClient) { }

		public override void Execute(ExecutingTaskContext context)
		{
			RunLoggedTask(() =>
			{
				var subscriptionErrorRepository = context.Resolve<ISubscriptionErrorRepository>();
				var subscriptionRepository = context.Resolve<ISubscriptionRepository>();
				var errors = BGWebServiceClient.GetErrors(AutogiroServiceType.LensSubscription) ?? Enumerable.Empty<RecievedError>();
				LogDebug("Fetched {0} errors from BG Webservice", errors.Count());
				errors.Each(error =>
				{
					var subscription = subscriptionRepository.GetByBankgiroPayerId(error.PayerNumber);
					var subscriptionError = ConvertError(error, subscription);
					subscriptionErrorRepository.Save(subscriptionError);
					BGWebServiceClient.SetErrorHandled(error);
					LogDebug("Saved subscription error \"{0}\" for subscription \"{1}\"", subscriptionError.Id, subscriptionError.Subscription.Id);
				});
			});
		}
		protected virtual SubscriptionError ConvertError(RecievedError error, Subscription subscription)
		{
			return new SubscriptionError
			{
				CreatedDate = DateTime.Now,
				Subscription = subscription,
				Type = ConvertErrorType(error.CommentCode),
                BGErrorId = error.ErrorId,
			};
		}

		protected virtual SubscriptionErrorType ConvertErrorType(ErrorCommentCode errorType)
		{
			switch (errorType)
			{
				case ErrorCommentCode.ConsentMissing: return SubscriptionErrorType.ConsentMissing;
				case ErrorCommentCode.AccountNotYetApproved: return SubscriptionErrorType.NotApproved;
				case ErrorCommentCode.ConsentStopped: return SubscriptionErrorType.CosentStopped;
				case ErrorCommentCode.NotYetDebitable: return SubscriptionErrorType.NotDebitable;
				default: throw new ArgumentOutOfRangeException("errorType");
			}
		}
	}
}