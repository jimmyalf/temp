using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator;

namespace Synologen.LensSubscription.BGServiceCoordinator.Task.Test.Helpers
{
    public abstract class ReceiveConsentsBase : TaskTestBase
    {
        protected override ITask GetTask()
        {
            return new Synologen.LensSubscription.BGServiceCoordinator.Task.ReceiveConsents.Task(
                Log4NetLogger,
                ReceivedFileRepository,
                BGReceivedConsentRepository,
                ConsentFileReader);
        }
    }
}
