using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using StructureMap;

namespace Synologen.Service.Client.OrderEmailSender
{
	static class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Initialize();
        	var loggingService = ObjectFactory.GetInstance<ILoggingService>();
			loggingService.LogInfo("*** OrderEmailSender started execution ***");
			try
			{
				var orderSender = ObjectFactory.GetInstance<OrderSender>();
				orderSender.SendOrders();
			}
			catch(Exception ex)
			{
				loggingService.LogError("Encountered an exception while sending orders.", ex);
			}
			finally
			{
				loggingService.LogInfo("*** OrderEmailSender finished execution ***");	
			}
			
        }
    }
}
