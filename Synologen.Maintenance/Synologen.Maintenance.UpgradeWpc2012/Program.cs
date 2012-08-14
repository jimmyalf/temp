using System;

namespace Synologen.Maintenance.UpgradeWpc2012
{
	class Program
	{
		static void Main(string[] args)
		{

			var migrator = new Migrator();
			migrator.RenameDatabaseEntries();
			migrator.RenameDirectories();
			Console.ReadKey();
		}
	}
}
