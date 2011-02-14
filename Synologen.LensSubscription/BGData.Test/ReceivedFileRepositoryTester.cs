using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
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
		public void Should_save_the_transaction()
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
			});
		}
	}
}