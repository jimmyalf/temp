using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface ILoggingService
	{
		void LogDebug(string message);
		void LogDebug(string format, params object[] parameters);
		void LogInfo(string message);
		void LogInfo(string format, params object[] parameters);
		void LogWarning(string message);
		void LogWarning(string format, params object[] parameters);
		void LogError(string message);
		void LogError(string format, params object[] parameters);
		void LogError(string message, Exception ex);
		void LogFatal(string message);
		void LogFatal(string format, params object[] parameters);
		void LogFatal(string message, Exception ex);
		void LogInfoEventLog(string message);
		void LogInfoEventLog(string format, params object[] parameters);
		void LogWarningEventLog(string message);
		void LogWarningEventLog(string format, params object[] parameters);
		void LogErrorEventLog(string message);
		void LogErrorEventLog(string format, params object[] parameters);
		void LogErrorEventLog(string message, Exception ex);
		void LogSuccessAudit(string message);
		void LogSuccessAudit(string format, params object[] parameters);
		void LogFailureAudit(string message);
		void LogFailureAudit(string format, params object[] parameters);
	}
}
