using System;
using NUnit.Framework;
using Moq;
using Synologen.ServiceCoordinator.Test.TestHelpers;

namespace Synologen.ServiceCoordinator.Test
{

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_debug_level : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_debug_level()
		{
			Because = logger => logger.LogDebug(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(_message), Times.Once());
			MockedLogger.Verify(x => x.Info(_message), Times.Never());
			MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_message), Times.Never());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_debug_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;

		public When_logging_debug_level_with_string_format()
		{		
			_message = string.Format(format, param1, param2, param3, param4);
			Because = logger => logger.LogDebug(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(_message), Times.Once());
			MockedLogger.Verify(x => x.Info(_message), Times.Never());
			MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_message), Times.Never());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_info_level()
		{
			Because = logger => logger.LogInfo(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			MockedLogger.Verify(x => x.Info(_message), Times.Once());
			MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_message), Times.Never());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;

		public When_logging_info_level_with_string_format()
		{
			_message = string.Format(format, param1, param2, param3, param4);
			Because = logger => logger.LogInfo(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			MockedLogger.Verify(x => x.Info(_message), Times.Once());
			MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_message), Times.Never());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_warning_level()
		{
			Because = logger => logger.LogWarning(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Error(_message), Times.Never());
			MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			MockedLogger.Verify(x => x.Info(_message), Times.Never());
			MockedLogger.Verify(x => x.Warn(_message), Times.Once());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;

		public When_logging_warning_level_with_string_format()
		{
			_message = string.Format(format, param1, param2, param3, param4);
			Because = logger => logger.LogWarning(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Error(_message), Times.Never());
			MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			MockedLogger.Verify(x => x.Info(_message), Times.Never());
			MockedLogger.Verify(x => x.Warn(_message), Times.Once());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}


	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_error_level()
		{
			Because = logger => logger.LogError(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			MockedLogger.Verify(x => x.Info(_message), Times.Never());
			MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_message), Times.Once());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;

		public When_logging_error_level_with_string_format()
		{
			_message = string.Format(format, param1, param2, param3, param4);
			Because = logger => logger.LogError(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			MockedLogger.Verify(x => x.Info(_message), Times.Never());
			MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_message), Times.Once());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_with_exception : LoggingServiceTestBase
	{
		private const string _description = "Loggningsmeddelande";
		private static string _message = "Loggningsmeddelande";
		private readonly Mock<Exception> _mockedException;

		public When_logging_error_level_with_exception()
		{
			_mockedException = new Mock<Exception>();
			_mockedException.SetupGet(x => x.Source).Returns("The source");
			_mockedException.SetupGet(x => x.Message).Returns("The message");
			_mockedException.SetupGet(x => x.StackTrace).Returns("The stacktrace");
			
			const string theFormat = "Source: {0}, Message: {1}, Stacktrace: {2}";
			_message = string.Format(theFormat, _mockedException.Object.Source, _mockedException.Object.Message, _mockedException.Object.StackTrace);
			
			Because = logger => logger.LogError(_description, _mockedException.Object);
		}

		public static string Message { get { return _message; } }

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			//MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			//MockedLogger.Verify(x => x.Info(_message), Times.Never());
			//MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			//MockedLogger.Verify(x => x.Error(_message), Times.Once());
			//MockedLogger.Verify(x => x.Fatal(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_description, _mockedException.Object), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_fatal_level()
		{
			Because = logger => logger.LogFatal(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			MockedLogger.Verify(x => x.Info(_message), Times.Never());
			MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_message), Times.Never());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;

		public When_logging_fatal_level_with_string_format()
		{
			_message = string.Format(format, param1, param2, param3, param4);
			Because = logger => logger.LogFatal(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			MockedLogger.Verify(x => x.Info(_message), Times.Never());
			MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			MockedLogger.Verify(x => x.Error(_message), Times.Never());
			MockedLogger.Verify(x => x.Fatal(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level_with_exception : LoggingServiceTestBase
	{
		private const string _description = "Loggningsmeddelande";
		private static string _message;
		private readonly Mock<Exception> _mockedException;

		public When_logging_fatal_level_with_exception()
		{
			_mockedException = new Mock<Exception>();
			_mockedException.SetupGet(x => x.Source).Returns("The source");
			_mockedException.SetupGet(x => x.Message).Returns("The message");
			_mockedException.SetupGet(x => x.StackTrace).Returns("The stacktrace");

			const string theFormat = "Source: {0}, Message: {1}, Stacktrace: {2}";
			_message = string.Format(theFormat, _mockedException.Object.Source, _mockedException.Object.Message, _mockedException.Object.StackTrace);

			Because = logger => logger.LogFatal(_description, _mockedException.Object);
		}

		public static string Message { get { return _message; } }

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			//MockedLogger.Verify(x => x.Debug(_message), Times.Never());
			//MockedLogger.Verify(x => x.Info(_message), Times.Never());
			//MockedLogger.Verify(x => x.Warn(_message), Times.Never());
			//MockedLogger.Verify(x => x.Error(_message), Times.Never());
			//MockedLogger.Verify(x => x.Fatal(_message), Times.Once());
			MockedLogger.Verify(x => x.Fatal(_description, _mockedException.Object), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level_to_eventlog : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_info_level_to_eventlog()
		{
			Because = logger => logger.LogInfoEventLog(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.Info(_message));
			
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		public When_logging_info_level_to_eventlog_with_string_format()
		{
			Because = logger => logger.LogInfoEventLog(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.Info(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level_to_eventlog : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_warning_level_to_eventlog()
		{
			Because = logger => logger.LogWarningEventLog(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.Warning(_message), Times.Once());
		}
	}


	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		public When_logging_warning_level_to_eventlog_with_string_format()
		{
			Because = logger => logger.LogWarningEventLog(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.Warning(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_to_eventlog : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_error_level_to_eventlog()
		{
			Because = logger => logger.LogErrorEventLog(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.Error(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		public When_logging_error_level_to_eventlog_with_string_format()
		{
			Because = logger => logger.LogErrorEventLog(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.Error(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_to_eventlog_with_exception : LoggingServiceTestBase
	{
		private const string _description = "Loggningsmeddelande";
		private readonly Mock<Exception> _mockedException;
		private static Exception _exception;

		public When_logging_error_level_to_eventlog_with_exception()
		{
			_mockedException = new Mock<Exception>();
			_mockedException.SetupGet(x => x.Source).Returns("The source");
			_mockedException.SetupGet(x => x.Message).Returns("The message");
			_mockedException.SetupGet(x => x.StackTrace).Returns("The stacktrace");
			_exception = _mockedException.Object;

			Because = logger => logger.LogErrorEventLog(_description, _mockedException.Object);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.Error(_description, _exception), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_successaudit_level_to_eventlog : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_successaudit_level_to_eventlog()
		{
			Because = logger => logger.LogSuccessAudit(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.SuccessAudit(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_successaudit_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		public When_logging_successaudit_level_to_eventlog_with_string_format()
		{
			Because = logger => logger.LogSuccessAudit(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.SuccessAudit(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_failureaudit_level_to_eventlog : LoggingServiceTestBase
	{
		private const string _message = "Loggningsmeddelande";

		public When_logging_failureaudit_level_to_eventlog()
		{

			Because = logger => logger.LogFailureAudit(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.FailureAudit(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_failureaudit_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		public When_logging_failureaudit_level_to_eventlog_with_string_format()
		{
			Because = logger => logger.LogFailureAudit(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			MockedEventLogger.Verify(x => x.FailureAudit(format, param1, param2, param3, param4), Times.Once());
		}
	}
}
