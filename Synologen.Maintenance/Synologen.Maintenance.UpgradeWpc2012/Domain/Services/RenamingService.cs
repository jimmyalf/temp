using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Spinit.Extensions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Extensions;
using Synologen.Maintenance.UpgradeWpc2012.Domain.Model.Entities;

namespace Synologen.Maintenance.UpgradeWpc2012.Domain.Services
{
	public class RenamingService
	{
		private readonly IDictionary<string, string> _replacements;

		public RenamingService()
		{
			_replacements = new Dictionary<string, string>
			{
				{"[àáâãäåâăⱥæ]", "a"},
				{"[çč]","c"},
				{"ð","d"},
				{"[èéêěë]", "e"},
				{"[ìíîï]","i"},
				{"ľ","l"},
				{"ñ","n"},
				{"[òóôõöø]", "o"},
				{"[șş]","s"},
				{"[țţ]","t"},
				{"[ûüúùů]","u"},
				{"ÿ","y"},
				{"ž","z"},
				{" ","_"},
				{"%20","_"},
				{"%C3%A4","a"}
			};
		}

		public RenamingService(IDictionary<string,string> replacements)
		{
			_replacements = replacements;
		}

		public string ReplaceTokens(string input)
		{
			return _replacements.Aggregate(input, (current, replacement) => current
				.RegexReplace(replacement.Key.ToLower(), replacement.Value.ToLower())
				.RegexReplace(replacement.Key.ToUpper(), replacement.Value.ToUpper()));
		}

		public string RemoveInvalidCharacters(string input)
		{
			return Regex.Replace(input, Settings.Settings.InvalidCharacterPattern, string.Empty, RegexOptions.IgnoreCase);
		}

		public string Rename(string input)
		{
			var temp = ReplaceTokens(input);
			return RemoveInvalidCharacters(temp);
		}

		public bool FileNeedsRenaming(FileEntity file)
		{
			return !IsValid(file.Name);
		}

		public bool IsValid(string input)
		{
			var pattern = "^{0}+?$".FormatWith(Settings.Settings.ValidCharacterPattern);
			return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
		}
	}
}