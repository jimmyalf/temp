using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
    public class BGReceivedPayment : ReceivedEntity
    {
    	public virtual AutogiroPayer Payer { get; set;} 
        public virtual decimal Amount { get; set; }
        public virtual PaymentResult ResultType { get; set; }
        public virtual string Reference { get; set; }
        public virtual DateTime PaymentDate { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}
