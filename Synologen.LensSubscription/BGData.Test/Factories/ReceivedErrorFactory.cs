using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Synologen.LensSubscription.BGData.Test.Factories
{
    public class ReceivedErrorFactory
    {
        public static BGReceivedError Get()
        {
            return new BGReceivedError
                       {
                           Amount = 555.55M,
                           CommentCode = ErrorCommentCode.AccountNotYetApproved,
                           CreatedDate = DateTime.Now,
                           PayerNumber = 111,
                           PaymentDate = DateTime.Now.AddDays(-1),
                           Reference = "Ref."
                       };
        }

        public static void Edit(BGReceivedError error)
        {
            error.Amount = error.Amount*2;
            error.CommentCode = error.CommentCode.SkipValues(1);
            error.CreatedDate = error.CreatedDate.AddDays(-1);
            error.PayerNumber = error.PayerNumber*2;
            error.PaymentDate = error.PaymentDate.AddDays(-1);
            error.Reference = error.Reference.Reverse();
        }
    }
}
