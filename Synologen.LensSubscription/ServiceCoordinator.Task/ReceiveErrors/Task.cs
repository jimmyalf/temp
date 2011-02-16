using System;
using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGWebService;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.ServiceCoordinator.Task.ReceiveErrors
{
	public class Task : TaskBase
	{
		private readonly IBGWebService _bgWebService;
		private readonly ISubscriptionErrorRepository _subscriptionErrorRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;
		

		public Task(ILoggingService loggingService, IBGWebService bgWebService, ISubscriptionErrorRepository subscriptionErrorRepository, ISubscriptionRepository subscriptionRepository) 
			: base("RecieveErrorsTask", loggingService)
		{
			_bgWebService = bgWebService;
			_subscriptionErrorRepository = subscriptionErrorRepository;
			_subscriptionRepository = subscriptionRepository;
		}

		public override void Execute()
		{
			RunLoggedTask(() =>
			{
				var errors = _bgWebService.GetNewErrors() ?? Enumerable.Empty<RecievedError>();
				var errorsToSave = errors.Select(error => ConvertError(error));
				LogDebug("Fetched {0} errors from BG Webservice", errorsToSave.Count());
				errorsToSave.Each(subscriptionError =>
				{
					_subscriptionErrorRepository.Save(subscriptionError);
					LogDebug("Saved subscription error \"{0}\" for subscription \"{1}\"", subscriptionError.Id, subscriptionError.Subscription.Id);
				});
			});
		}
		protected virtual SubscriptionError ConvertError(RecievedError error)
		{
			var subscription = _subscriptionRepository.Get(error.PayerNumber);
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
				case ErrorType.ConsentMissing:
					return SubscriptionErrorType.ConsentMissing;
				case ErrorType.AccountNotYetApproved:
					return SubscriptionErrorType.NotApproved;
				case ErrorType.ConsentStopped:
					return SubscriptionErrorType.CosentStopped;
				case ErrorType.NotYetDebitable:
					return SubscriptionErrorType.NotDebitable;
				default:
					throw new ArgumentOutOfRangeException("errorType");
			}
		}
	}
}