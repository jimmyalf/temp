using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
    [TestFixture, Category("ReceivedErrorRepositoryTester")]
    public class When_adding_a_receivederror : BaseRepositoryTester<BGReceivedErrorRepository>
    {
        private BGReceivedError _errorToSave;

        public When_adding_a_receivederror()
        {
            Context = session =>
            {
                _errorToSave = ReceivedErrorFactory.Get();
            };
            Because = repository => repository.Save(_errorToSave);
        }

        [Test]
        public void Should_save_the_error()
        {
            AssertUsing(session =>
            {
                var fetchedError = CreateRepository(session).Get(_errorToSave.Id);
                fetchedError.Id.ShouldBe(_errorToSave.Id);
                fetchedError.Amount.ShouldBe(_errorToSave.Amount);
                fetchedError.CommentCode.ShouldBe(_errorToSave.CommentCode);
                fetchedError.CreatedDate.Date.ShouldBe(_errorToSave.CreatedDate.Date);
                fetchedError.PayerNumber.ShouldBe(_errorToSave.PayerNumber);
                fetchedError.PaymentDate.Date.ShouldBe(_errorToSave.PaymentDate.Date);
                fetchedError.Reference.ShouldBe(_errorToSave.Reference);
            });
        }
    }

    [TestFixture, Category("ReceivedErrorRepositoryTester")]
    public class When_updateing_a_receivederror : BaseRepositoryTester<BGReceivedErrorRepository>
    {

        private BGReceivedError _updatedError;

        public When_updateing_a_receivederror()
        {
            Context = session =>
            {
                _updatedError = ReceivedErrorFactory.Get();
                CreateRepository(session).Save(_updatedError);
                ReceivedErrorFactory.Edit(_updatedError);
            };
            Because = repository => repository.Save(_updatedError);
        }

        [Test]
        public void Error_is_updated()
        {
            AssertUsing(session =>
            {
                var fetchedError = CreateRepository(session).Get(_updatedError.Id);
                fetchedError.Amount.ShouldBe(_updatedError.Amount);
                fetchedError.CommentCode.ShouldBe(_updatedError.CommentCode);
                fetchedError.CreatedDate.Date.ShouldBe(_updatedError.CreatedDate.Date);
                fetchedError.PayerNumber.ShouldBe(_updatedError.PayerNumber);
                fetchedError.PaymentDate.Date.ShouldBe(_updatedError.PaymentDate.Date);
                fetchedError.Reference.ShouldBe(_updatedError.Reference);
            });
        }
    }

    [TestFixture, Category("ReceivedErrorRepositoryTester")]
    public class When_deleting_received_error : BaseRepositoryTester<BGReceivedErrorRepository>
    {
        private BGReceivedError _errorToDelete;

        public When_deleting_received_error()
        {
            Context = session =>
            {
                _errorToDelete = ReceivedErrorFactory.Get();
                CreateRepository(session).Save(_errorToDelete);
            };
            Because = repository => repository.Delete(_errorToDelete);
        }

        [Test]
        public void Error_is_deleted()
        {
            AssertUsing(session =>
            {
                var deletedError = CreateRepository(session).Get(_errorToDelete.Id);
                deletedError.ShouldBe(null);
            });
        }
    }
}
