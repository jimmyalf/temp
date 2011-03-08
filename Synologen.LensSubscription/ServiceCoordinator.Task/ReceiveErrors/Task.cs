using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveErrors
{
	public class Task : TaskBase
	{
		private readonly IBGWebService _bgWebService;

		public Task(ILoggingService loggingService, IBGWebService bgWebService, ITaskRepositoryResolver taskRepositoryResolver) 
			: base("RecieveErrorsTask", loggingService, taskRepositoryResolver)
		{
			_bgWebService = bgWebService;
		}

		public override void Execute()
		{
			RunLoggedTask(repositoryResolver =>
			{
				var subscriptionErrorRepository = repositoryResolver.GetRepository<ISubscriptionErrorRepository>();
				var subscriptionRepository = repositoryResolver.GetRepository<ISubscriptionRepository>();
				var errors = _bgWebService.GetErrors(AutogiroServiceType.LensSubscription) ?? Enumerable.Empty<RecievedError>();
				LogDebug("Fetched {0} errors from BG Webservice", errors.Count());
				errors.Each(error =>
				{
					var subscription = subscriptionRepository.GetByBankgiroPayerId(error.PayerNumber);
					var subscriptionError = ConvertError(error, subscription);
					subscriptionErrorRepository.Save(subscriptionError);
					_bgWebService.SetErrorHandled(error);
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
			};
		}

		protected virtual SubscriptionErrorType ConvertErrorType(ErrorType errorType)
		{
			switch (errorType)
			{
				case ErrorType.ConsentMissing: return SubscriptionErrorType.ConsentMissing;
				case ErrorType.AccountNotYetApproved: return SubscriptionErrorType.NotApproved;
				case ErrorType.ConsentStopped: return SubscriptionErrorType.CosentStopped;
				case ErrorType.NotYetDebitable: return SubscriptionErrorType.NotDebitable;
				default: throw new ArgumentOutOfRangeException("errorType");
			}
		}
	}
}