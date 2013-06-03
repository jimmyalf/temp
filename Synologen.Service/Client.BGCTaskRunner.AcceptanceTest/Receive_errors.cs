using System;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro;
using Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Synologen.LensSubscription.BGServiceCoordinator.Task.ReceiveErrors;
using Synologen.Service.Client.BGCTaskRunner.AcceptanceTest.TestHelpers;

namespace Synologen.Service.Client.BGCTaskRunner.AcceptanceTest
{

    /* Feature: Hämta nya avvisade betalningsuppdrag

	Scenario: Nya filsektioner av typen fellista registerkontroll finns sparade i databas
		Verifiera att nya avvisade betalningsuppdrag sparas i DB
		Verifiera att lästa filsektioner uppdateras som hanterade/lästa
	 */

    [TestFixture, Category("Feature: Receive errors from BGC")]
    public class When_receiving_errors : TaskTestBase
    {
        private Task task;
        private ITaskRunnerService taskRunnerService;
        private ReceivedFileSection errorsFileSection;
        private AutogiroPayer payer;

        public When_receiving_errors()
        {
            Context = () =>
            {
                payer = new AutogiroPayer { Name = "Adam Bertil", ServiceType = AutogiroServiceType.LensSubscription };
                autogiroPayerRepository.Save(payer);
                errorsFileSection = Factory.GetReceivedErrorFileSection(payer.Id);
                receivedFileRepository.Save(errorsFileSection);
                task = ResolveTask<Task>();
                taskRunnerService = GetTaskRunnerService(task);
            };
            Because = () =>
            {
                taskRunnerService.Run();
            };  
        }

        [Test]
        public void Task_stores_errors()
        {
            var errors = bgReceivedErrorRepository.GetAll().GetLast(4).ToArray();

            errors.ForElementAtIndex(0, error =>
            {
                error.Amount.ShouldBe(500m);
                error.CommentCode.ShouldBe(ErrorCommentCode.ConsentMissing);
                error.Reference.ShouldBe("");
            });

            errors.ForElementAtIndex(1, error =>
            {
                error.Amount.ShouldBe(200m);
                error.CommentCode.ShouldBe(ErrorCommentCode.ConsentStopped);
                error.Reference.ShouldBe("");
            });

            errors.ForElementAtIndex(2, error =>
            {
                error.Amount.ShouldBe(100m);
                error.CommentCode.ShouldBe(ErrorCommentCode.AccountNotYetApproved);
                error.Reference.ShouldBe("");
            });

            errors.ForElementAtIndex(3, error =>
            {
                error.Amount.ShouldBe(150m);
                error.CommentCode.ShouldBe(ErrorCommentCode.NotYetDebitable);
                error.Reference.ShouldBe("TESTREF");
            });

            foreach (var error in errors)
            {
                error.Payer.Id.ShouldBe(payer.Id);
                error.CreatedDate.Date.ShouldBe(DateTime.Now.Date);
                error.PaymentDate.ShouldBe(new DateTime(2004, 10, 23));
                error.Handled.ShouldBe(false);
            }
        }

        [Test]
        public void Task_updates_filesections_as_handled()
        {
            var fetchedError = ResolveRepository<IReceivedFileRepository>().Get(errorsFileSection.Id);
            fetchedError.HandledDate.Value.Date.ShouldBe(DateTime.Now.Date);
            fetchedError.HasBeenHandled.ShouldBe(true);                
        }
    }
}
