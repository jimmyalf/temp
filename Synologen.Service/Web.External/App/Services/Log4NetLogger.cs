using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using log4net;

namespace Synologen.Service.Web.External.App.Services
{
	public class Log4NetLogger : ILoggingService
	{
		private static ILog _logger;

		public Log4NetLogger(ILog logger)
		{
			_logger = logger;
			log4net.Config.XmlConfigurator.Configure();
		}

		public void LogDebug(string format, params object[] parameters)
		{
			DoWithOrWithoutFormat(format, parameters, _logger.Debug, _logger.DebugFormat);
		}

		public void LogInfo(string format, params object[] parameters)
		{
			DoWithOrWithoutFormat(format, parameters, _logger.Info, _logger.InfoFormat);
		}

		public void LogWarning(string format, params object[] parameters)
		{
			DoWithOrWithoutFormat(format, parameters, _logger.Warn, _logger.WarnFormat);
		}

		public void LogError(string message, Exception ex)
		{
			_logger.Error(message, ex);
		}

		public void LogError(string format, params object[] parameters)
		{
			DoWithOrWithoutFormat(format, parameters, _logger.Error, _logger.ErrorFormat);
		}

		public void LogFatal(string format, params object[] parameters)
		{
			DoWithOrWithoutFormat(format, parameters, _logger.Fatal, _logger.FatalFormat);
		}

		public void LogFatal(string message, Exception ex)
		{
			_logger.Fatal(message, ex);
		}

		private void DoWithOrWithoutFormat(string format, object[] parameters, Action<string> withoutFormat, Action<string,object[]> withFormat)
		{
			if (parameters == null || parameters.Length <= 0)
			{
				withoutFormat(format);
			}
			else
			{
				withFormat(format, parameters);
			}
		}
	}
}