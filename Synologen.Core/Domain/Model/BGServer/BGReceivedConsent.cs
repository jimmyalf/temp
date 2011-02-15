using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
    public class BGReceivedConsent : Entity
    {
        public int PayerNumber { get; set; }
        public DateTime ActionDate { get; set; }
        public DateTime? ConsentValidForDate { get; set; }
        public ConsentInformationCode? InformationCode { get; set; }
        public ConsentCommentCode CommentCode { get; set; }
    }
}
