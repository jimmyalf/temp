using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface ILoggingService
	{
		void LogDebug(string message);
		void LogDebug(string format, params string[] parameters);
		void LogInfo(string message);
		void LogInfo(string format, params string[] parameters);
		void LogWarning(string message);
		void LogWarning(string format, params string[] parameters);
		void LogError(string message);
		void LogError(string format, params string[] parameters);
		void LogError(string message, Exception ex);
		void LogFatal(string message);
		void LogFatal(string format, params string[] parameters);
		void LogFatal(string message, Exception ex);
		void LogInfoEventLog(string message);
		void LogInfoEventLog(string format, params string[] parameters);
		void LogWarningEventLog(string message);
		void LogWarningEventLog(string format, params string[] parameters);
		void LogErrorEventLog(string message);
		void LogErrorEventLog(string format, params string[] parameters);
		void LogErrorEventLog(string message, Exception ex);
		void LogSuccessAudit(string message);
		void LogSuccessAudit(string format, params string[] parameters);
		void LogFailureAudit(string message);
		void LogFailureAudit(string format, params string[] parameters);
	}
}
