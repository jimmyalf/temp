using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
    public class BGReceivedPayment : Entity
    {
        public virtual int PayerNumber { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual PaymentResult Result { get; set; }
        public virtual string Reference { get; set; }
        public virtual DateTime PaymentDate { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}
