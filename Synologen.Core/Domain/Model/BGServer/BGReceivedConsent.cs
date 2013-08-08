using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
    public class BGReceivedConsent : ReceivedEntity
    {
		public virtual AutogiroPayer Payer { get; set;} 
        public virtual DateTime ActionDate { get; set; }
        public virtual DateTime? ConsentValidForDate { get; set; }
        public virtual ConsentInformationCode? InformationCode { get; set; }
        public virtual ConsentCommentCode CommentCode { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}
