using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Utils;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
	[TestFixture]
	[Category("ReceivedFileRepositoryTester")]
	public class When_adding_a_section : BaseRepositoryTester<ReceivedFileRepository>
	{
		private ReceivedFileSection _sectionToSave;

		public When_adding_a_section()
		{
			Context = session =>
			{
				_sectionToSave = ReceivedFileSectionFactory.Get();
			};

			Because = repository => repository.Save(_sectionToSave);
		}

		[Test]
		public void Should_save_the_section()
		{
			AssertUsing(session =>
			{
				var savedSection = new ReceivedFileRepository(session).Get(_sectionToSave.Id);
				savedSection.ShouldBe(_sectionToSave);
				savedSection.CreatedDate.ShouldBe(_sectionToSave.CreatedDate);
				savedSection.HandledDate.ShouldBe(_sectionToSave.HandledDate);
				savedSection.SectionData.ShouldBe(_sectionToSave.SectionData);
				savedSection.Type.ShouldBe(_sectionToSave.Type);
				savedSection.TypeName.ShouldBe(_sectionToSave.TypeName);
                savedSection.HasBeenHandled.ShouldBe(_sectionToSave.HasBeenHandled);
			});
		}
	}


    [TestFixture]
    public class When_fetching_sections_by_AllUnhandledReceivedConsentFileSectionsCriteria : BaseRepositoryTester<ReceivedFileRepository>
    {
        private IEnumerable<ReceivedFileSection> sections;
        private int expectedNumberOfFetchedSections;

        public When_fetching_sections_by_AllUnhandledReceivedConsentFileSectionsCriteria()
        {
            Context = session =>
            {
                sections = ReceivedFileSectionFactory.GetList();
                expectedNumberOfFetchedSections = sections
                    .Where(x => Equals(x.HandledDate, null))
                    .Where(x => Equals(x.Type, SectionType.ReceivedConsents))
                    .Count();
            };
            Because = repository => sections.Each(repository.Save);
        }

        [Test]
        public void Should_get_all_consent_file_sections_that_has_not_been_handled()
        {
            AssertUsing(session =>
            {
                var fetchedSections = CreateRepository(session).FindBy(new AllUnhandledReceivedConsentFileSectionsCriteria());
                fetchedSections.Count().ShouldBe(expectedNumberOfFetchedSections);
                fetchedSections.Each(section => section.HandledDate.ShouldBe(null));
            });
        }
    }

    [TestFixture]
    public class When_fetching_sections_by_AllUnhandledReceivedPaymentFileSectionsCriteria : BaseRepositoryTester<ReceivedFileRepository>
    {
        private IEnumerable<ReceivedFileSection> sections;
        private int expectedNumberOfFetchedSections;

        public When_fetching_sections_by_AllUnhandledReceivedPaymentFileSectionsCriteria()
        {
            Context = session =>
            {
                sections = ReceivedFileSectionFactory.GetList();
                expectedNumberOfFetchedSections = sections
                    .Where(x => Equals(x.HandledDate, null))
                    .Where(x => Equals(x.Type, SectionType.ReceivedPayments))
                    .Count();
            };
            Because = repository => sections.Each(repository.Save);
        }

        [Test]
        public void Should_get_all_payment_file_sections_that_has_not_been_handled()
        {
            AssertUsing(session =>
            {
                var fetchedSections = CreateRepository(session).FindBy(new AllUnhandledReceivedPaymentFileSectionsCriteria());
                fetchedSections.Count().ShouldBe(expectedNumberOfFetchedSections);
                fetchedSections.Each(section => section.HandledDate.ShouldBe(null));
            });
        }
    }
}