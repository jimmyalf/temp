﻿using System;
using System.Collections.Generic;
using Synologen.Maintenance.ExternalShopLogin.Domain.Model;
using Synologen.Maintenance.ExternalShopLogin.Domain.Services;
using Synologen.Maintenance.ExternalShopLogin.Persistence.Commands;
using Synologen.Maintenance.ExternalShopLogin.Persistence.Queries;

namespace Synologen.Maintenance.ExternalShopLogin
{
	class Program
	{
		static void Main(string[] args)
		{
			var shops = new AllShopsQuery().Execute();
			var hashService = new SHA1HashService();
			var passwordService = new PasswordService();
			var fileService = new FileService();
			var results = new List<SetExternalLoginForShopResult>();
			foreach (var shop in shops)
			{
				var result = new SetExternalLoginForShopCommand(shop, hashService, passwordService).Execute();
				results.Add(result);
			}
			fileService.SaveFile(results, @"C:\Users\cber\Desktop\external-logins.csv");
			Console.WriteLine("Press any key to exit... ");
			Console.ReadKey();
		}
	}
}
