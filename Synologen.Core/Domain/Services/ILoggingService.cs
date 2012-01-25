using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface ILoggingService
	{
		void LogDebug(string format, params object[] parameters);
		void LogInfo(string format, params object[] parameters);
		void LogWarning(string format, params object[] parameters);
		void LogError(string format, params object[] parameters);
		void LogError(string message, Exception ex);
		void LogFatal(string format, params object[] parameters);
		void LogFatal(string message, Exception ex);
	}
}
