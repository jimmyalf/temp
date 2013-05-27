using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public class FakeLoggingService : ILoggingService
	{
		private readonly IList<string> _debugMessages;
		private readonly IList<ExceptionAndMessage> _fatalMessages;
		private readonly IList<string> _infoMessages;
		private readonly IList<string> _warningMessages;
		private readonly IList<ExceptionAndMessage> _errorMessages;

		public FakeLoggingService()
		{
			_debugMessages = new List<string>();
			_debugMessages = new List<string>();
			_fatalMessages = new List<ExceptionAndMessage>();
			_infoMessages = new List<string>();
			_warningMessages = new List<string>();
			_errorMessages = new List<ExceptionAndMessage>();
		}

		#region Debug
		public void LogDebug(string format, params object[] parameters)
		{
			_debugMessages.Add(String.Format(format,parameters));
		}

		public FakeLoggingService AssertDebugExact(string format, params object[] parameters)
		{
			return AssertMessageExact(_debugMessages, format, parameters);
		}

		public FakeLoggingService AssertDebug(string format, params object[] parameters)
		{
			return AssertMessageContains(_debugMessages, format, parameters);
		}

		public FakeLoggingService AssertDebug(Expression<Func<IList<string>,bool>> predicateExpression)
		{
			return AssertCustom(_debugMessages, predicateExpression);
		}
		#endregion

		#region Info
		public void LogInfo(string format, params object[] parameters)
		{
			_infoMessages.Add(String.Format(format,parameters));
		}

		public FakeLoggingService AssertInfoExact(string format, params object[] parameters)
		{
			return AssertMessageExact(_infoMessages, format, parameters);
		}

		public FakeLoggingService AssertInfo(string format, params object[] parameters)
		{
			return AssertMessageContains(_infoMessages, format, parameters);
		}

		public FakeLoggingService AssertInfo(Expression<Func<IList<string>,bool>> predicateExpression)
		{
			return AssertCustom(_infoMessages, predicateExpression);
		}
		#endregion

		#region Warning
		public void LogWarning(string format, params object[] parameters)
		{
			_warningMessages.Add(String.Format(format,parameters));
		}

		public FakeLoggingService AssertWarningExact(string format, params object[] parameters)
		{
			return AssertMessageExact(_warningMessages, format, parameters);
		}

		public FakeLoggingService AssertWarning(string format, params object[] parameters)
		{
			return AssertMessageContains(_warningMessages, format, parameters);
		}

		public FakeLoggingService AssertWarning(Expression<Func<IList<string>,bool>> predicateExpression)
		{
			return AssertCustom(_warningMessages, predicateExpression);
		}
		#endregion

		#region Error
		public void LogError(string format, params object[] parameters)
		{
			_errorMessages.Add(new ExceptionAndMessage(String.Format(format,parameters)));
		}

		public void LogError(string message, Exception ex)
		{
			_errorMessages.Add(new ExceptionAndMessage(message, ex));
		}

		public FakeLoggingService AssertErrorExact(string format, params object[] parameters)
		{
			return AssertMessageExact(_errorMessages.Select(x => x.Message).ToList(), format, parameters);
		}

		public FakeLoggingService AssertError(string format, params object[] parameters)
		{
			return AssertMessageContains(_errorMessages.Select(x => x.Message).ToList(), format, parameters);
		}

		public FakeLoggingService AssertErrorExact<TException>(string format, params object[] parameters)
		{
			var message = String.Format(format, parameters);
			if(_errorMessages.Any(x => Equals(x.Message, message) && x.ExceptionIsOfType<TException>())) return this;
			throw new AssertionException(message + " was not found");
		}
		public FakeLoggingService AssertError<TException>(string format, params object[] parameters)
		{
			var message = String.Format(format, parameters);
			if(_errorMessages.Any(x => x.Message.Contains(message) && x.ExceptionIsOfType<TException>())) return this;
			throw new AssertionException(message + " was not found");
		}

		public FakeLoggingService AssertError(Expression<Func<IList<ExceptionAndMessage>,bool>> predicateExpression)
		{
			return AssertCustom(_errorMessages, predicateExpression);
		}
		#endregion

		#region Fatal
		public void LogFatal(string format, params object[] parameters)
		{
			_fatalMessages.Add(new ExceptionAndMessage(String.Format(format,parameters)));
		}

		public void LogFatal(string message, Exception ex)
		{
			_fatalMessages.Add(new ExceptionAndMessage(message, ex));
		}

		public FakeLoggingService AssertFatalExact(string format, params object[] parameters)
		{
			return AssertMessageExact(_fatalMessages.Select(x => x.Message).ToList(), format, parameters);
		}

		public FakeLoggingService AssertFatal(string format, params object[] parameters)
		{
			return AssertMessageContains(_fatalMessages.Select(x => x.Message).ToList(), format, parameters);
		}

		public FakeLoggingService AssertFatalExact<TException>(string format, params object[] parameters)
		{
			var message = String.Format(format, parameters);
			if(_fatalMessages.Any(x => Equals(x.Message, message) && x.ExceptionIsOfType<TException>())) return this;
			throw new AssertionException(message + " was not found");
		}

		public FakeLoggingService AssertFatal<TException>(string format, params object[] parameters)
		{
			var message = String.Format(format, parameters);
			if(_fatalMessages.Any(x => x.Message.Contains(message) && x.ExceptionIsOfType<TException>())) return this;
			throw new AssertionException(message + " was not found");
		}

		public FakeLoggingService AssertFatal(Expression<Func<IList<ExceptionAndMessage>,bool>> predicateExpression)
		{
			return AssertCustom(_fatalMessages, predicateExpression);
		}
		#endregion

		private class AssertionException : Exception
		{
			public AssertionException() { }
			public AssertionException(string message) : base(message) { }
			public AssertionException(string message, Exception ex) : base(message, ex) { }
		}

		private FakeLoggingService AssertMessageExact(IList<string> messages, string format, params object[] parameters)
		{
			var message = String.Format(format, parameters);
			 if(messages.Contains(message)) return this;
			throw new AssertionException("\"" + message + "\" was not found among items:\r\n" + GetListWithDelimiter(messages, "\r\n"));
		}

		private FakeLoggingService AssertMessageContains(IList<string> messages, string format, params object[] parameters)
		{
			var message = String.Format(format, parameters);
			if (messages.Any(messageInList => messageInList.Contains(message))) return this;
			throw new AssertionException("\"" + message + "\" was not found among items:\r\n" + GetListWithDelimiter(messages, "\r\n"));
		}

		private FakeLoggingService AssertCustom<TType>(IList<TType> list, Expression<Func<IList<TType>,bool>> predicateExpression)
		{
			var unaryExpression = Expression.Quote(predicateExpression);
			var predicate = predicateExpression.Compile();
			if (predicate(list)) return this;
			throw new AssertionException("Expression " + unaryExpression.Operand + " failed evaluation");
		}

		private string GetListWithDelimiter(IList<string> list, string delimiter)
		{
			if(list == null || !list.Any()) return string.Empty;
			return list.Aggregate((item, next) => item + delimiter + next);
		}
	}

	public class ExceptionAndMessage
	{
		public string Message { get; set; }
		public Exception Exception { get; set; }

		public ExceptionAndMessage(string message, Exception exception)
		{
			Message = message;
			Exception = exception;
		}

		public ExceptionAndMessage(string message)
		{
			Message = message;
		}

		public bool HasException
		{
			get { return Exception != null; }
		}

		public bool ExceptionIsOfType<TType>()
		{
			if(!HasException) return false;
			return Exception is TType;
		}
	}
}