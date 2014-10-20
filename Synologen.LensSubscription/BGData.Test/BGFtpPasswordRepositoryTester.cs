using System;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.LensSubscription.BGData.Repositories;
using Synologen.LensSubscription.BGData.Test.BaseTesters;

namespace Synologen.LensSubscription.BGData.Test
{
	[TestFixture, Category("BGFtpPasswordRepositoryTester")]
	public class When_adding_a_password_to_repository : BaseRepositoryTester<BGFtpPasswordRepository>
	{
		private BGFtpPassword password;
		private string passwordNotInRepository;

		public When_adding_a_password_to_repository()
		{
			Context = session =>
			{
				passwordNotInRepository = "AnotherPassword";
				password = new BGFtpPassword{ Password = "test" };
			};
			Because = repository => repository.Add(password);
		}

		[Test]
		public void Password_is_stored()
		{
			AssertUsing(session =>
			{
				var fetchedPassword = CreateRepository(session).GetLast();
				fetchedPassword.Password.ShouldBe(password.Password);
				fetchedPassword.Created.Date.ShouldBe(DateTime.Now.Date);
			});
		}

		[Test]
		public void Password_exists()
		{
			GetResult(session => CreateRepository(session).PasswordExists(password.Password)).ShouldBe(true);
		}

		[Test]
		public void A_Password_not_in_repository_does_not_exist()
		{
			GetResult(session => CreateRepository(session).PasswordExists(passwordNotInRepository)).ShouldBe(false);
		}
	}

	[TestFixture, Category("BGFtpPasswordRepositoryTester")]
	public class When_adding_two_passwords_to_repository : BaseRepositoryTester<BGFtpPasswordRepository>
	{
		private BGFtpPassword firstPassword;
		private BGFtpPassword secondPassword;
		public When_adding_two_passwords_to_repository()
		{
			Context = session =>
			{
				firstPassword = new BGFtpPassword{ Password = "test one" };
				secondPassword = new BGFtpPassword{ Password = "test two" };
			};
			Because = repository => 
			{
				repository.Add(firstPassword);
				repository.Add(secondPassword);
			};
		}

		[Test]
		public void Second_password_is_fetched_with_get_last_method()
		{
			AssertUsing(session =>
			{
				var fetchedPassword = CreateRepository(session).GetLast();
				fetchedPassword.Password.ShouldBe(secondPassword.Password);
				fetchedPassword.Created.Date.ShouldBe(DateTime.Now.Date);
			});
		}
	}
}