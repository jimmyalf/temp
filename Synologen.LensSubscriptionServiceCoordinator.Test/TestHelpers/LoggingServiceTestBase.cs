using System;
using NUnit.Framework;
using Synologen.ServiceCoordinator.Logging;

namespace Synologen.ServiceCoordinator.Test.TestHelpers
{
	public class LoggingServiceTestBase
	{
		protected string format = "Testar {0} {1} {2} med {3}";
		protected string param1 = "att";
		protected string param2 = "skapa";
		protected string param3 = "meddelande";
		protected string param4 = "string.Format";

		//protected Log4NetLogger logger;

		//protected LoggingServiceTestBase()
		//{
		//    Context = () => { };
		//    Because = logger => { throw new AssertionException("An action for Because has not been set!"); };
		//}

		//protected Action Context;
		//protected Action<Log4NetLogger> Because;
	}
}