using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public class UrlFriendlyRenamingService : IUrlFriendlyRenamingService
	{
		public string ValidCharacterPattern { get; protected set; }
		public string InvalidCharacterPattern { get; protected set; }
		public IEnumerable<CharacterReplacement> Replacements { get; protected set; }

		public UrlFriendlyRenamingService(string validCharacterPattern = @"[a-z0-9+\-\._\/]", string invalidCharacterPattern = @"[^a-z0-9+\-\._\/]", IEnumerable<CharacterReplacement> characterReplacements = null)
		{
			ValidCharacterPattern = validCharacterPattern;
			InvalidCharacterPattern = invalidCharacterPattern;
			Replacements = characterReplacements ?? DefaultCharacterReplacements;
		}

		public static IEnumerable<CharacterReplacement> DefaultCharacterReplacements
		{
			get
			{
				return CharacterReplacementCollection.Create()
					.Add("[àáâãäåâăⱥæ]", "a")
					.Add("[çč]","c")
					.Add("ð","d")
					.Add("[èéêěë]", "e")
					.Add("[ìíîï]","i")
					.Add("ľ","l")
					.Add("ñ","n")
					.Add("[òóôõöø]", "o")
					.Add("[șş]","s")
					.Add("[țţ]","t")
					.Add("[ûüúùů]","u")
					.Add("ÿ","y")
					.Add("ž","z")
					.Add(" ","-")
					.Add("%20","-")
					.Add("%C3%A4","a");
			}
		}

		protected virtual string ReplaceTokens(string input)
		{
			return Replacements.Aggregate(input, (current, replacement) => current
				.RegexReplace(replacement.ReplacementPattern.ToLower(), replacement.Replacement.ToLower())
				.RegexReplace(replacement.ReplacementPattern.ToUpper(), replacement.Replacement.ToUpper()));
		}

		protected virtual string RemoveInvalidCharacters(string input)
		{
			return Regex.Replace(input, InvalidCharacterPattern, string.Empty, RegexOptions.IgnoreCase);
		}

		public virtual string Rename(string input)
		{
			var temp = ReplaceTokens(input);
			return RemoveInvalidCharacters(temp);
		}

		public virtual bool IsValid(string input)
		{
			var pattern = "^{0}+?$".FormatWith(ValidCharacterPattern);
			return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
		}
	}

	public interface IUrlFriendlyRenamingService
	{
		string Rename(string input);
		bool IsValid(string input);
	}

	public class CharacterReplacementCollection : IEnumerable<CharacterReplacement>
	{
		protected IList<CharacterReplacement> _list;

		public CharacterReplacementCollection()
		{
			_list = new List<CharacterReplacement>();
		}
		public static CharacterReplacementCollection Create()
		{
			return new CharacterReplacementCollection();
		}

		public CharacterReplacementCollection Add(string replacementPattern, string replacement)
		{
			_list.Add(new CharacterReplacement(replacementPattern, replacement));
			return this;
		}

		public CharacterReplacementCollection Add(KeyValuePair<string,string> replacement)
		{
			_list.Add(new CharacterReplacement(replacement.Key, replacement.Value));
			return this;
		}

		public IEnumerator<CharacterReplacement> GetEnumerator()
		{
			return _list.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	public class CharacterReplacement
	{
		public string ReplacementPattern { get; set; }
		public string Replacement { get; set; }

		public CharacterReplacement(string replacementPattern, string replacement)
		{
			ReplacementPattern = replacementPattern;
			Replacement = replacement;
		}
	}
}