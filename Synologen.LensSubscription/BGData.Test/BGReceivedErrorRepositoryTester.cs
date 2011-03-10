using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
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
            	var payer = StoreAutogiroPayer(PayerFactory.Get);
                _errorToSave = ReceivedErrorFactory.Get(payer);
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
                fetchedError.Payer.ShouldBe(_errorToSave.Payer);
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
            	var payer = StoreAutogiroPayer(PayerFactory.Get);
                _updatedError = ReceivedErrorFactory.Get(payer);
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
                fetchedError.Payer.ShouldBe(_updatedError.Payer);
                fetchedError.PaymentDate.Date.ShouldBe(_updatedError.PaymentDate.Date);
                fetchedError.Reference.ShouldBe(_updatedError.Reference);
				fetchedError.Handled.ShouldBe(_updatedError.Handled);
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
            	var payer = StoreAutogiroPayer(PayerFactory.Get);
                _errorToDelete = ReceivedErrorFactory.Get(payer);
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

	[TestFixture, Category("ReceivedErrorRepositoryTester")]
	public class When_fetching_received_errors_by_AllNewReceivedBGErrorsMatchingServiceTypeCriteria : BaseRepositoryTester<BGReceivedErrorRepository>
	{
		private AutogiroPayer payer;
		private IEnumerable<BGReceivedError> errors;
		private IEnumerable<BGReceivedError> expectedErrors;
		private AutogiroServiceType serviceType;

		public When_fetching_received_errors_by_AllNewReceivedBGErrorsMatchingServiceTypeCriteria()
		{
			Context = session =>
			{
				payer = StoreAutogiroPayer(PayerFactory.Get);
				errors = ReceivedErrorFactory.GetList(payer);
				serviceType = AutogiroServiceType.LensSubscription;
				expectedErrors = errors.Where(x => x.Handled == false && x.Payer.ServiceType.Equals(serviceType));
			};
			Because = repository => errors.Each(repository.Save);
		}

		[Test]
		public void Should_get_all_errors_that_has_not_been_handled_and_are_of_given_service_type()
		{
			AssertUsing(session =>
			{
				var fetchedErrors = CreateRepository(session).FindBy(new AllNewReceivedBGErrorsMatchingServiceTypeCriteria(serviceType)).ToList();
				fetchedErrors.Count().ShouldBe(expectedErrors.Count());
				fetchedErrors.Each(fetchedError =>
				{
					fetchedError.Handled.ShouldBe(false);
					fetchedError.Payer.ServiceType.ShouldBe(serviceType);
				});
			});
		}
	}
}
