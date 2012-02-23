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
		public void Generated_password_does_not_contain_å()
		{
			generatedPassword.Contains("å").ShouldBe(false);
		}

		[Test]
		public void Generated_password_does_not_contain_ä()
		{
			generatedPassword.Contains("ä").ShouldBe(false);
		}

		[Test]
		public void Generated_password_does_not_contain_ö()
		{
			generatedPassword.Contains("ö").ShouldBe(false);
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
		private string passwordContaining_å;
		private string passwordContaining_ä;
		private string passwordContaining_ö;
		private string passwordContaining_Å;
		private string passwordContaining_Ä;
		private string passwordContaining_Ö;

		public When_storing_new_active_password_with_invalid_characters()
		{
			Context = () =>
			{
				passwordContaining_å = "ABCDEFå";
				passwordContaining_ä = "ABCDEFä";
				passwordContaining_ö = "ABCDEFö";
				passwordContaining_Å = "ABCDEFÅ";
				passwordContaining_Ä = "ABCDEFÄ";
				passwordContaining_Ö = "ABCDEFÖ";
			};
			Because = service => { };
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_å()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_å));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_ä()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_ä));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_ö()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_ö));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_Å()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_Å));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_Ä()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_Ä));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}

		[Test]
		public void Service_throws_BGFtpPasswordServiceException_when_storing_password_containing_Ö()
		{
			caughtException = TryGetException(() => GetTestEntity().StoreNewActivePassword(passwordContaining_Ö));
			caughtException.ShouldNotBe(null);
			caughtException.ShouldBeTypeOf(typeof(BGFtpPasswordServiceException));
		}
	}

}