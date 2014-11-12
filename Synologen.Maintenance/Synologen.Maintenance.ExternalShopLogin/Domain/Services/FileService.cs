using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Synologen.Maintenance.ExternalShopLogin.Domain.Model;

namespace Synologen.Maintenance.ExternalShopLogin.Domain.Services
{
	public class FileService
	{
		private readonly string _separator;

		public FileService()
		{
			_separator = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator;
		}
		public void SaveFile(IList<SetExternalLoginForShopResult> list, string filePath)
		{
			if(File.Exists(filePath)) File.Delete(filePath);
			var rows = AppendHeaderToList(list.Select(ParseResult));
			File.AppendAllLines(filePath, rows, Encoding.UTF8);
		}
		protected string ParseResult(SetExternalLoginForShopResult result)
		{
			return String.Join(_separator, 
				ParseStringValue(result.Shop.Name), 
				ParseStringValue(result.UserName), 
				ParseStringValue(result.Password));
		}

		protected IEnumerable<string> AppendHeaderToList(IEnumerable<string> list)
		{
			return new[] {"Butik;Användarnamn;Lösenord"}.Concat(list);
		}

		protected string ParseStringValue(string input)
		{
			return "\"" + input + "\"";
		}
	}
}