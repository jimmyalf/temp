using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.OPQ.Core;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code
{
	public class OpqUtility
	{
		/// <summary>
		/// Checks a filepath for allowed extensions
		/// </summary>
		/// <param name="filePath">The current filepath</param>
		/// <returns>true/false if the extension is allowed or not</returns>
		static public bool IsAllowedExtension(string filePath)
		{
			if (filePath.IsNullOrEmpty()) return false;
			if (Configuration.UploadAllowedExtensions.IsNullOrEmpty()) return false;
			if (!Path.HasExtension(filePath)) return false;
			string[] allowedExtensions = Configuration.UploadAllowedExtensions.Split(',');
			foreach (var extension in allowedExtensions)
			{
				if (Path.GetExtension(filePath).ToLower().Equals(string.Concat('.',extension.ToLower())))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Encodes a string to avoid forbidden chars in url
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static public string EncodeStringToUrl(string value)
		{
			value = value.ToLower();
			value = value.Replace('å', 'a');
			value = value.Replace('ä', 'a');
			value = value.Replace('ö', 'o');
			value = value.Replace(' ', '-');
			value = Regex.Replace(value, "[^a-z0-9-]", "");
			value = Regex.Replace(value, "[-]+", "-");
			return value;
		}
	}
}
