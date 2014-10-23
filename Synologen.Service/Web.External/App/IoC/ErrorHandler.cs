using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.Service.Web.External.App.IoC
{
	public class ErrorHandler : IErrorHandler
	{
		private readonly ILoggingService _loggingService;

		public ErrorHandler(ILoggingService loggingService)
		{
			_loggingService = loggingService;
		}

		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			var faultException = new FaultException("Server error encountered. All details have been logged.");  
			var messageFault = faultException.CreateMessageFault();  
			fault = Message.CreateMessage(version, messageFault, faultException.Action); 
		}

		public bool HandleError(Exception error)
		{
			_loggingService.LogError("Error occured: ", error);
			return true;
		}
	}
}