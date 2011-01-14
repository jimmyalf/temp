using System;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IEventLoggingService
	{
		void Info(string message);
		void Info(string format, params string[] parameters);
		void Warning(string message);
		void Warning(string format, params string[] parameters);
		void Error(string message);
		void Error(string format, params string[] parameters);
		void Error(string message, Exception ex);
		void SuccessAudit(string message);
		void SuccessAudit(string format, params string[] parameters);
		void FailureAudit(string message);
		void FailureAudit(string format, params string[] parameters);
	}
}
