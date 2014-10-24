using System;
using NUnit.Framework;
using Moq;
using Synologen.Service.Client.SubscriptionTaskRunner.Test.TestHelpers;

namespace Synologen.Service.Client.SubscriptionTaskRunner.Test
{
	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_debug_level : LoggingServiceTestBase
	{
		private const string Message = "Loggningsmeddelande";

		public When_logging_debug_level()
		{
			Because = logger => logger.LogDebug(Message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(Message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_debug_level_with_string_format : LoggingServiceTestBase
	{
		public When_logging_debug_level_with_string_format()
		{		
			Because = logger => logger.LogDebug(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.DebugFormat(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level : LoggingServiceTestBase
	{
		private const string Message = "Loggningsmeddelande";

		public When_logging_info_level()
		{
			Because = logger => logger.LogInfo(Message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Info(Message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level_with_string_format : LoggingServiceTestBase
	{

		public When_logging_info_level_with_string_format()
		{
			Because = logger => logger.LogInfo(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.InfoFormat(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level : LoggingServiceTestBase
	{
		private const string Message = "Loggningsmeddelande";

		public When_logging_warning_level()
		{
			Because = logger => logger.LogWarning(Message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Warn(Message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level_with_string_format : LoggingServiceTestBase
	{
		public When_logging_warning_level_with_string_format()
		{
			Because = logger => logger.LogWarning(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.WarnFormat(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level : LoggingServiceTestBase
	{
		private const string Message = "Loggningsmeddelande";

		public When_logging_error_level()
		{
			Because = logger => logger.LogError(Message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Error(Message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_with_string_format : LoggingServiceTestBase
	{
		public When_logging_error_level_with_string_format()
		{
			Because = logger => logger.LogError(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.ErrorFormat(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_with_exception : LoggingServiceTestBase
	{
		private const string Description = "Loggningsmeddelande";
		private readonly Exception _testException;

		public When_logging_error_level_with_exception()
		{
			_testException = new Exception("Test"){ Source = "Source A"};
			Because = logger => logger.LogError(Description, _testException);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Error(Description, _testException), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level : LoggingServiceTestBase
	{
		private const string Message = "Loggningsmeddelande";

		public When_logging_fatal_level()
		{
			Because = logger => logger.LogFatal(Message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Fatal(Message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level_with_string_format : LoggingServiceTestBase
	{
		public When_logging_fatal_level_with_string_format()
		{
			Because = logger => logger.LogFatal(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.FatalFormat(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level_with_exception : LoggingServiceTestBase
	{
		private const string Description = "Loggningsmeddelande";
		private readonly Exception _testException;

		public When_logging_fatal_level_with_exception()
		{
			_testException = new Exception("Test") {Source = "Source B"};
			Because = logger => logger.LogFatal(Description, _testException);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Fatal(Description, _testException), Times.Once());
		}
	}
}