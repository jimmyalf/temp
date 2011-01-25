using System;
using System.Diagnostics;
using NUnit.Framework;
using Moq;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Synologen.ServiceCoordinator.Logging;
using log4net;
using Synologen.ServiceCoordinator.Test.TestHelpers;

namespace Synologen.ServiceCoordinator.Test
{

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_debug_level : LoggingServiceTestBase
	{
		private static string _message = "Loggningsmeddelande";
		private Mock<ILog> _mockedLogger;

		public When_logging_debug_level()
		{
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogDebug(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Once());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_debug_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;
		private Mock<ILog> _mockedLogger;

		public When_logging_debug_level_with_string_format()
		{		
			_message = string.Format(format, param1, param2, param3, param4);
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogDebug(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Once());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level
	{
		private static string _message = "Loggningsmeddelande";
		private Mock<ILog> _mockedLogger;

		public When_logging_info_level()
		{
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogInfo(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Once());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;
		private Mock<ILog> _mockedLogger;

		public When_logging_info_level_with_string_format()
		{
			_message = string.Format(format, param1, param2, param3, param4);
			_mockedLogger = new Mock<ILog>();
			var logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogInfo(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Once());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level
	{
		private static string _message = "Loggningsmeddelande";
		private Mock<ILog> _mockedLogger;

		public When_logging_warning_level()
		{
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogWarning(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Once());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;
		private Mock<ILog> _mockedLogger;

		public When_logging_warning_level_with_string_format()
		{
			_message = string.Format(format, param1, param2, param3, param4);
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogWarning(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Once());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}


	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level
	{
		private static string _message = "Loggningsmeddelande";
		private Mock<ILog> _mockedLogger;

		public When_logging_error_level()
		{
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogError(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Once());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;
		private Mock<ILog> _mockedLogger;

		public When_logging_error_level_with_string_format()
		{
			_message = string.Format(format, param1, param2, param3, param4);
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogError(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Once());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_with_exception
	{
		private static string _description = "Loggningsmeddelande";
		private static string _message = "Loggningsmeddelande";
		private Mock<ILog> _mockedLogger;
		private Mock<Exception> _mockedException;

		public When_logging_error_level_with_exception()
		{
			_mockedException = new Mock<Exception>();
			_mockedException.SetupGet(x => x.Source).Returns("The source");
			_mockedException.SetupGet(x => x.Message).Returns("The message");
			_mockedException.SetupGet(x => x.StackTrace).Returns("The stacktrace");
			
			const string theFormat = "Source: {0}, Message: {1}, Stacktrace: {2}";
			_message = string.Format(theFormat, _mockedException.Object.Source, _mockedException.Object.Message, _mockedException.Object.StackTrace);
			
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogError(_description, _mockedException.Object);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Once());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_description), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level
	{
		private static string _message = "Loggningsmeddelande";
		private Mock<ILog> _mockedLogger;

		public When_logging_fatal_level()
		{
			_mockedLogger = new Mock<ILog>();
			Log4NetLogger logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogFatal(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level_with_string_format : LoggingServiceTestBase
	{
		private static string _message;
		private Mock<ILog> _mockedLogger;

		public When_logging_fatal_level_with_string_format()
		{
			_message = string.Format(format, param1, param2, param3, param4);
			_mockedLogger = new Mock<ILog>();
			var logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogFatal(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_fatal_level_with_exception : LoggingServiceTestBase
	{
		private static string _description = "Loggningsmeddelande";
		private static string _message;
		private Mock<ILog> _mockedLogger;
		private Mock<Exception> _mockedException;

		public When_logging_fatal_level_with_exception()
		{
			_mockedException = new Mock<Exception>();
			_mockedException.SetupGet(x => x.Source).Returns("The source");
			_mockedException.SetupGet(x => x.Message).Returns("The message");
			_mockedException.SetupGet(x => x.StackTrace).Returns("The stacktrace");

			const string theFormat = "Source: {0}, Message: {1}, Stacktrace: {2}";
			_message = string.Format(theFormat, _mockedException.Object.Source, _mockedException.Object.Message, _mockedException.Object.StackTrace);

			_mockedLogger = new Mock<ILog>();
			var logger = new Log4NetLogger(_mockedLogger.Object, false, string.Empty);
			logger.LogFatal(_description, _mockedException.Object);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedLogger.Verify(x => x.Debug(_message), Times.Never());
			_mockedLogger.Verify(x => x.Info(_message), Times.Never());
			_mockedLogger.Verify(x => x.Warn(_message), Times.Never());
			_mockedLogger.Verify(x => x.Error(_message), Times.Never());
			_mockedLogger.Verify(x => x.Fatal(_message), Times.Once());
			_mockedLogger.Verify(x => x.Fatal(_description), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level_to_eventlog
	{
		private const string _message = "Loggningsmeddelande";
		private Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_info_level_to_eventlog()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogInfoEventLog(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.Info(_message));
			
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_info_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_info_level_to_eventlog_with_string_format()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogInfoEventLog(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.Info(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level_to_eventlog
	{
		private const string _message = "Loggningsmeddelande";
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_warning_level_to_eventlog()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();

			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogWarningEventLog(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.Warning(_message), Times.Once());
		}
	}


	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_warning_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_warning_level_to_eventlog_with_string_format()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogWarningEventLog(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.Warning(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_to_eventlog
	{
		private const string _message = "Loggningsmeddelande";
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_error_level_to_eventlog()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogErrorEventLog(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.Error(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_error_level_to_eventlog_with_string_format()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogErrorEventLog(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.Error(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_error_level_to_eventlog_with_exception 
	{
		private static string _description = "Loggningsmeddelande";
		private readonly Mock<IEventLoggingService> _mockedEventLogger;
		private Mock<Exception> _mockedException;
		private static Exception _exception;

		public When_logging_error_level_to_eventlog_with_exception()
		{
			_mockedException = new Mock<Exception>();
			_mockedException.SetupGet(x => x.Source).Returns("The source");
			_mockedException.SetupGet(x => x.Message).Returns("The message");
			_mockedException.SetupGet(x => x.StackTrace).Returns("The stacktrace");
			_exception = _mockedException.Object;

			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogErrorEventLog(_description, _mockedException.Object);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.Error(_description, _exception), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_successaudit_level_to_eventlog
	{
		private const string _message = "Loggningsmeddelande";
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_successaudit_level_to_eventlog()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogSuccessAudit(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.SuccessAudit(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_successaudit_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_successaudit_level_to_eventlog_with_string_format()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogSuccessAudit(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.SuccessAudit(format, param1, param2, param3, param4), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_failureaudit_level_to_eventlog
	{
		private const string _message = "Loggningsmeddelande";
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_failureaudit_level_to_eventlog()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogFailureAudit(_message);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.FailureAudit(_message), Times.Once());
		}
	}

	[TestFixture]
	[Category("LoggingServiceTest")]
	public class When_logging_failureaudit_level_to_eventlog_with_string_format : LoggingServiceTestBase
	{
		private readonly Mock<IEventLoggingService> _mockedEventLogger;

		public When_logging_failureaudit_level_to_eventlog_with_string_format()
		{
			_mockedEventLogger = new Mock<IEventLoggingService>();
			var logger = new Log4NetLogger(_mockedEventLogger.Object);
			logger.LogFailureAudit(format, param1, param2, param3, param4);
		}

		[Test]
		public void Should_call_expected_method_with_expected_params()
		{
			_mockedEventLogger.Verify(x => x.FailureAudit(format, param1, param2, param3, param4), Times.Once());
		}
	}
}
