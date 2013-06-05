using System;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Exceptions;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Synologen.Service.Client.BGCTaskRunner.Test.TestHelpers;

namespace Synologen.Service.Client.BGCTaskRunner.Test
{
	[TestFixture, Category("BGFtpPasswordServiceTest")]
	public class When_generating_a_new_password : BGFtpPasswordServiceTestBase
	{
		private string generatedPassword;

		public When_generating_a_new_password()
		{
			Context = () => { };
			Because = service =>
			{
				generatedPassword = service.GenerateNewPassword();
				Console.WriteLine(generatedPassword);
			};
		}

		[Test]
		public void Generated_password_is_6_characters_long_or_more()
		{
			generatedPassword.Length.ShouldBeGreaterThanOrEqualTo(6);
		}

		[Test]
		public void Generated_password_is_8_characters_long_or_less()
		{
			generatedPassword.Length.ShouldBeLessThan(9);
		}

		[Test]
		public void Generated_password_does_not_contain_�()
		{
			generatedPassword.Contains("�").ShouldBe(false);
		}

		[Test]
		public void Generated_password_does_not_contain_�()
		{
			generatedPassword.Contains("�").ShouldBe(false);
		}

		[Test]
		public void Generated_password_does_not_contain_�()
		{
			generatedPassword.Contains("�").ShouldBe(false);
		}
	}

	[TestFixture, Category("BGFtpPasswordServiceTest")]
	public class When_fetching_current_password : BGFtpPasswordServiceTestBase
	{
		private string fetchedCurrentPassword;
		private BGFtpPassword password;

		public When_fetching_current_password()
		{
			Context = () =>
			{
				password = new BGFtpPassword{ Password = "ABC123" };
				A.CallTo(() => BGFtpPasswordRepository.GetLast()).Returns(password);
			};
			Because = service =>
			{
				fetchedCurrentPassword = service.GetCurrentPassword();
			};
		}

		[Test]
		public void Service_fetches_last_password_from_repository()
		{
			A.CallTo(() => BGFtpPasswordRepository.GetLast()).MustHaveHappened();
		}

		[Test]
		public void Service_returns_fetched_password()
		{
			fetchedCurrentPassword.ShouldBe(password.Password);
		}
	}

	[TestFixture, Category("BGFtpPasswordServiceTest")]
	public class When_fetching_current_password_but_no_password_has_been_added : BGFtpPasswordServiceTestBase
	{
		private Exception caughtException;

		public When_fetching_current_password_but_no_password_has_been_added()
		{
			Context = () =>
			{
				A.CallTo(() => BGFtpPasswordRepository.GetLast()).Returns(null as BGFtpPassword);
			};
			Because = service =>
			{
				caughtException = TryGetException(() => service.GetCurrentPassword());
			};
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException()
		{
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}
	}

	[TestFixture, Category("BGFtpPasswordServiceTest")]
	public class When_storing_new_active_password : BGFtpPasswordServiceTestBase
	{
		private string newPassword;

		public When_storing_new_active_password()
		{
			Context = () =>
			{
				newPassword = "ABC3546";
			};
			Because = service => service.StoreNewActivePassword(newPassword);
		}

		[Test]
		public void Service_stores_new_password_in_repository()
		{
			A.CallTo(() => BGFtpPasswordRepository.Add(A<BGFtpPassword>.That.Matches(x => 
				x.Password.Equals(newPassword) && x.Created.Date.Equals(DateTime.Now.Date)
			))).MustHaveHappened();
		}
	}

	[TestFixture, Category("BGFtpPasswordServiceTest")]
	public class When_storing_new_active_password_shorter_than_6_characters : BGFtpPasswordServiceTestBase
	{
		private string _newPassword;
		private Exception _caughtException;

		public When_storing_new_active_password_shorter_than_6_characters()
		{
			Context = () =>
			{
				_newPassword = "A35";
			};
			Because = service =>
			{
				_caughtException = TryGetException(() => service.StoreNewActivePassword(_newPassword));
			};
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException()
		{
			_caughtException.ShouldNotBe(null);
			_caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}
	}

	[TestFixture, Category("BGFtpPasswordServiceTest")]
	public class When_storing_new_active_password_longer_than_8_characters : BGFtpPasswordServiceTestBase
	{
		private string newPassword;
		private Exception caughtException;

		public When_storing_new_active_password_longer_than_8_characters()
		{
			Context = () =>
			{
				newPassword = "ABCDEF123";
			};
			Because = service =>
			{
				caughtException = TryGetException(() => service.StoreNewActivePassword(newPassword));
			};
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException()
		{
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}
	}

	[TestFixture, Category("BGFtpPasswordServiceTest")]
	public class When_storing_new_active_password_with_invalid_characters : BGFtpPasswordServiceTestBase
	{
		private Exception caughtException;
		private string passwordContaining_�;
		private string passwordContaining_�;
		private string passwordContaining_�;
		private string passwordContaining_�;
		private string passwordContaining_�;
		private string passwordContaining_�;

		public When_storing_new_active_password_with_invalid_characters()
		{
			Context = () =>
			{
				passwordContaining_� = "ABCDEF�";
				passwordContaining_� = "ABCDEF�";
				passwordContaining_� = "ABCDEF�";
				passwordContaining_� = "ABCDEF�";
				passwordContaining_� = "ABCDEF�";
				passwordContaining_� = "ABCDEF�";
			};
			Because = service => { };
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_�()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_�));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_�()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_�));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_�()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_�));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_�()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_�));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_�()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_�));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_�()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_�));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}
	}

}