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
	[TestFixture, Category("FileSectionToSendRepositoryTests")]
	public class When_adding_a_file_section_to_send : BaseRepositoryTester<FileSectionToSendRepository>
	{
		private FileSectionToSend savedFileSectionToSend;

		public When_adding_a_file_section_to_send()
		{
			Context = session =>
			{
				savedFileSectionToSend = FileSectionToSendFactory.Get();
			};

			Because = repository => repository.Save(savedFileSectionToSend);
		}

		[Test]
		public void File_section_has_been_added()
		{
			AssertUsing(session =>
			{
				var fetchedFileSection = CreateRepository(session).Get(savedFileSectionToSend.Id);
				fetchedFileSection.CreatedDate.Date.ShouldBe(savedFileSectionToSend.CreatedDate.Date);
				fetchedFileSection.Id.ShouldBe(fetchedFileSection.Id);
				fetchedFileSection.SectionData.ShouldBe(fetchedFileSection.SectionData);
				fetchedFileSection.SentDate.ShouldBe(fetchedFileSection.SentDate);
				fetchedFileSection.Type.ShouldBe(fetchedFileSection.Type);
				fetchedFileSection.TypeName.ShouldBe(fetchedFileSection.TypeName);
			});
		}
	}

	[TestFixture, Category("FileSectionToSendRepositoryTests")]
	public class When_updating_a_file_section_to_send : BaseRepositoryTester<FileSectionToSendRepository>
	{
		private FileSectionToSend fileSectionToSend;

		public When_updating_a_file_section_to_send()
		{
			Context = session =>
			{
				fileSectionToSend = FileSectionToSendFactory.Get();
				CreateRepository(session).Save(fileSectionToSend);
				FileSectionToSendFactory.Edit(fileSectionToSend);
			};

			Because = repository => repository.Save(fileSectionToSend);
		}

		[Test]
		public void File_section_has_been_updated()
		{
			AssertUsing(session =>
			{
				var fetchedFileSection = CreateRepository(session).Get(fileSectionToSend.Id);
				fetchedFileSection.CreatedDate.Date.ShouldBe(fileSectionToSend.CreatedDate.Date);
				fetchedFileSection.Id.ShouldBe(fetchedFileSection.Id);
				fetchedFileSection.SectionData.ShouldBe(fetchedFileSection.SectionData);
				fetchedFileSection.SentDate.ShouldBe(fetchedFileSection.SentDate);
				fetchedFileSection.Type.ShouldBe(fetchedFileSection.Type);
				fetchedFileSection.TypeName.ShouldBe(fetchedFileSection.TypeName);
			});
		}
	}

	[TestFixture, Category("FileSectionToSendRepositoryTests")]
	public class When_deleting_a_file_section_to_send : BaseRepositoryTester<FileSectionToSendRepository>
	{
		private FileSectionToSend fileSectionToSend;

		public When_deleting_a_file_section_to_send()
		{
			Context = session =>
			{
				fileSectionToSend = FileSectionToSendFactory.Get();
				CreateRepository(session).Save(fileSectionToSend);
			};

			Because = repository => repository.Delete(fileSectionToSend);
		}

		[Test]
		public void File_section_has_been_deleted()
		{
			GetResult(session => CreateRepository(session).Get(fileSectionToSend.Id)).ShouldBe(null);
		}
	}

	[TestFixture, Category("FileSectionToSendRepositoryTests")]
	public class When_fetching_file_sections_to_send_by_AllUnhandledFileSectionsToSendCriteria : BaseRepositoryTester<FileSectionToSendRepository>
	{
		private IEnumerable<FileSectionToSend> fileSectionsToSend;
		private int expectedNumberOfFetchedFileSections;

		public When_fetching_file_sections_to_send_by_AllUnhandledFileSectionsToSendCriteria()
		{
			Context = session =>
			{
				fileSectionsToSend = FileSectionToSendFactory.GetList();
				expectedNumberOfFetchedFileSections = fileSectionsToSend.Where(x => Equals(x.SentDate, null)).Count();
			};

			Because = repository => fileSectionsToSend.Each(repository.Save);
		}

		[Test]
		public void Should_get_all_file_sections_that_has_not_been_handled()
		{
			AssertUsing(session =>
			{
				var fetchedFileSections = CreateRepository(session).FindBy(new AllUnhandledFileSectionsToSendCriteria());
				fetchedFileSections.Count().ShouldBe(expectedNumberOfFetchedFileSections);
				fetchedFileSections.Each(fileSection => fileSection.SentDate.ShouldBe(null));
			});
		}
	}
}