using System;
using Synologen.Maintenance.ExternalShopLogin.Persistence.Queries;

namespace Synologen.Maintenance.ExternalShopLogin
{
	class Program
	{
		static void Main(string[] args)
		{
			var shops = new AllShopsQuery().Execute();
			Console.ReadKey();
		}
	}
}
