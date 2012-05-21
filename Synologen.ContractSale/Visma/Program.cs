using System;
using System.Collections.Generic;
using System.Text;
using Spinit.Wpc.Synologen.Visma;

namespace VismaTest
{
	class Program
	{
		static void Main(string[] args)
		{
			string conn = "Initial Catalog=dbWpcSynologen;Data Source=BLACK;uid=sa;pwd=RICE17A;Pooling=true;Connect Timeout=15;";
			string vismaCommonFilesPath = @"\\moccasin\SPCS_Administration\Gemensamma filer";
			string vismaCompanyName = @"\\moccasin\SPCS_Administration\Företag\Ovnbol";


			AdkHandler adk = new AdkHandler(conn, vismaCommonFilesPath, vismaCompanyName);

			try
			{


				adk.OpenCompany();

				System.Console.WriteLine("Please wait...");
				int numberCreated = adk.ExportInvoices();
				
				System.Console.WriteLine("Done! " + numberCreated.ToString() + " exported.");
			}
			catch (VismaException vex)
			{
				System.Console.WriteLine(vex.Message);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.Message);
			}
			try
			{
				adk.CloseCompany();
			}
			catch
			{
				// Ignore errors
			}

			System.Console.WriteLine("Press any key to close.");
			System.Console.ReadKey();
		}
	}
}
