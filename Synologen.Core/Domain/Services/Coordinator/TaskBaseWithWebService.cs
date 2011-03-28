using System;
using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Services.BgWebService;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Coordinator
{
	public abstract class TaskBaseWithWebService : TaskBase
	{
		protected IBGWebServiceClient BGWebServiceClient{ get; set; }
		protected TaskBaseWithWebService(string taskName, ILoggingService loggingService, IBGWebServiceClient bgWebServiceClient) 
			: base(taskName, loggingService)
		{
			BGWebServiceClient = bgWebServiceClient;
		}
		protected TaskBaseWithWebService(string taskName, ILoggingService loggingService, IBGWebServiceClient bgWebServiceClient, Enum taskOrder) 
			: base(taskName, loggingService, taskOrder)
		{
			BGWebServiceClient = bgWebServiceClient;
		}

		public override void RunLoggedTask(Action action)
		{
			base.RunLoggedTask(action);
			if (BGWebServiceClient == null || BGWebServiceClient.State == CommunicationState.Closed) return;
			try
			{
				BGWebServiceClient.Close();
			}
			catch (Exception ex)
			{
				BGWebServiceClient.Abort();
				base.LogError("Got exception while attempting to clos web service client", ex);
			}
		}
	}
}