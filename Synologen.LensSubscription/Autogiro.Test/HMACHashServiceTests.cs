using NUnit.Framework;
using Shouldly;
using Synologen.LensSubscription.Autogiro.Test.Factories;
using Synologen.LensSubscription.Autogiro.Test.TestHelpers;

namespace Synologen.LensSubscription.Autogiro.Test
{
	[TestFixture, Category("HMACHashServiceTests")]
	public class When_digesting_a_single_line_message : HMACMessageDigestServiceTestBase
	{
		private string key;
		private string message;
		private string expectedMessageDigest;

		public When_digesting_a_single_line_message()
		{
			Context = () =>
			{
				key = "1234567890ABCDEF1234567890ABCDEF";
				expectedMessageDigest = "FF365893D899291C3BF505FB3175E880";
				message = MessageFactory.GetSingleLineMessage();
			};
			SetupKey = () => key;
			Because = service => service.GetHash(message);
			
		}

		[Test]
		public void Message_digest_has_expected_value()
		{
			MessageDigest.ShouldBe(expectedMessageDigest);
		}
	}

	[TestFixture, Category("HMACHashServiceTests")]
	public class When_digesting_a_multiple_line_message : HMACMessageDigestServiceTestBase
	{
		private string key;
		private string message;
		private string expectedMessageDigest;

		public When_digesting_a_multiple_line_message()
		{
			Context = () =>
			{
				key = "1234567890ABCDEF1234567890ABCDEF";
				expectedMessageDigest = "9BA94363739D45256DF4B6FA3B9DE1CD";
				message = MessageFactory.GetMultipleLineMessage();
			};
			SetupKey = () => key;
			Because = service => service.GetHash(message);
			
		}

		[Test]
		public void Message_digest_has_expected_value()
		{
			MessageDigest.ShouldBe(expectedMessageDigest);
		}
	}

	[TestFixture, Category("HMACHashServiceTests")]
	public class When_digesting_a_multiple_line_message_with_line_lengths_of_over_80_characters : HMACMessageDigestServiceTestBase
	{
		private string key;
		private string message;
		private string expectedMessageDigest;

		public When_digesting_a_multiple_line_message_with_line_lengths_of_over_80_characters()
		{
			Context = () =>
			{
				key = "1234567890ABCDEF1234567890ABCDEF";
				expectedMessageDigest = "826CA6CBA33F7E1D3CC9161A0956A35B";
				message = MessageFactory.GetMultipleLinesWithOver80CharsMessage();
			};
			SetupKey = () => key;
			Because = service => service.GetHash(message);
			
		}

		[Test]
		public void Message_digest_has_expected_value()
		{
			MessageDigest.ShouldBe(expectedMessageDigest);
		}
	}

	[TestFixture, Category("HMACHashServiceTests")]
	public class When_digesting_a_message_with_allowed_characters : HMACMessageDigestServiceTestBase
	{
		private string key;
		private string message;
		private string expectedMessageDigest;

		public When_digesting_a_message_with_allowed_characters()
		{
			Context = () =>
			{
				key = "1234567890ABCDEF1234567890ABCDEF";
				expectedMessageDigest = "9473EDFCAA8CD2434D6D76ABFFC991BD";
				message = MessageFactory.GetMessageWithAllowedCharacters();
			};
			SetupKey = () => key;
			Because = service => service.GetHash(message);
			
		}

		[Test]
		public void Message_digest_has_expected_value()
		{
			MessageDigest.ShouldBe(expectedMessageDigest);
		}
	}

	[TestFixture, Category("HMACHashServiceTests")]
	public class When_digesting_a_message_with_allowed_and_disallowed_characters : HMACMessageDigestServiceTestBase
	{
		private string key;
		private string message;
		private string expectedMessageDigest;

		public When_digesting_a_message_with_allowed_and_disallowed_characters()
		{
			Context = () =>
			{
				key = "1234567890ABCDEF1234567890ABCDEF";
				expectedMessageDigest = "20956E44B404C4085446139B2B952D77";
				message = MessageFactory.GetMessageWithAllowedAndDisallowedCharacters();
			};
			SetupKey = () => key;
			Because = service => service.GetHash(message);
			
		}

		[Test]
		public void Message_digest_has_expected_value()
		{
			MessageDigest.ShouldBe(expectedMessageDigest);
		}
	}

	[TestFixture, Category("HMACHashServiceTests")]
	public class When_digesting_a_message_with_international_characters : HMACMessageDigestServiceTestBase
	{
		private string key;
		private string message;
		private string expectedMessageDigest;

		public When_digesting_a_message_with_international_characters()
		{
			Context = () =>
			{
				key = "1234567890ABCDEF1234567890ABCDEF";
				expectedMessageDigest = "515704694958361678194D51850FF157";
				message = MessageFactory.GetMessageWithInternationalCharacters();
			};
			SetupKey = () => key;
			Because = service => service.GetHash(message);
			
		}

		[Test]
		public void Message_digest_has_expected_value()
		{
			MessageDigest.ShouldBe(expectedMessageDigest);
		}
	}

	[TestFixture, Category("HMACHashServiceTests")]
	public class When_digesting_a_message_with_mixed_data : HMACMessageDigestServiceTestBase
	{
		private string key;
		private string message;
		private string expectedMessageDigest;

		public When_digesting_a_message_with_mixed_data()
		{
			Context = () =>
			{
				key = "1234567890ABCDEF1234567890ABCDEF";
				expectedMessageDigest = "68C5954818010DDDCB1EC02963E8C123";
				message = MessageFactory.GetMessageWithMixedData();
			};
			SetupKey = () => key;
			Because = service => service.GetHash(message);
			
		}

		[Test]
		public void Message_digest_has_expected_value()
		{
			MessageDigest.ShouldBe(expectedMessageDigest);
		}
	}
}