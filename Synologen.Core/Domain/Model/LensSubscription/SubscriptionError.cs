using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription
{
	public class SubscriptionError : Entity
	{
		public virtual Subscription Subscription { get; set; }
		public virtual SubscriptionErrorType Type { get; set; }
		public virtual ConsentInformationCode? Code { get; set; }
		public virtual DateTime CreatedDate { get; set; }
		public virtual DateTime? HandledDate { get; set; }
		public virtual bool IsHandled { get; set; }
		public virtual int? BGErrorId { get; set; }
		public virtual int? BGPaymentId { get; set; }
		public virtual int? BGConsentId { get; set; }
	}
}
