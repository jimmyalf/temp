using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;
using Synologen.LensSubscription.BGData.Test.Factories;

namespace Synologen.LensSubscription.BGData.Test
{
	[TestFixture, Category("AutogiroPayerRepositoryTester")]
	public class When_adding_an_autogiro_payer : BaseRepositoryTester<AutogiroPayerRepository>
	{
		private AutogiroPayer payer;

		public When_adding_an_autogiro_payer()
		{
			Context = session =>
			{
				payer = PayerFactory.Get();
			};

			Because = repository => repository.Save(payer);
		}

		[Test]
		public void Autogiro_payer_has_been_persisted()
		{
			AssertUsing(session =>
			{
				var fetchedPayer = CreateRepository(session).Get(payer.Id);
				fetchedPayer.Id.ShouldBe(payer.Id);
				fetchedPayer.Name.ShouldBe(payer.Name);
				fetchedPayer.ServiceType.ShouldBe(payer.ServiceType);
			});
			
		}
	}

	[TestFixture, Category("AutogiroPayerRepositoryTester")]
	public class When_updating_an_autogiro_payer : BaseRepositoryTester<AutogiroPayerRepository>
	{
		private AutogiroPayer payer;

		public When_updating_an_autogiro_payer()
		{
			Context = session =>
			{
				payer = PayerFactory.Get();
				CreateRepository(session).Save(payer);
				PayerFactory.Edit(payer);
			};
			Because = repository => repository.Save(payer);
		}

		[Test]
		public void Autogiro_payer_is_updated()
		{
			AssertUsing(session =>
			{
				var fetchedPayer = CreateRepository(session).Get(payer.Id);
				fetchedPayer.Id.ShouldBe(payer.Id);
				fetchedPayer.Name.ShouldBe(payer.Name);
				fetchedPayer.ServiceType.ShouldBe(payer.ServiceType);
			});
		}
	}

	[TestFixture, Category("AutogiroPayerRepositoryTester")]
	public class When_deleting_an_autogiro_payer : BaseRepositoryTester<AutogiroPayerRepository>
	{
		private AutogiroPayer payer;

		public When_deleting_an_autogiro_payer()
		{
			Context = session =>
			{
				payer = PayerFactory.Get();
				CreateRepository(session).Save(payer);
			};

			Because = repository => repository.Delete(payer); 
		}

		[Test]
		public void Autogiro_payer_was_deleted()
		{
			GetResult(session => CreateRepository(session).Get(payer.Id)).ShouldBe(null);
		}
	}
}