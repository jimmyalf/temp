using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;

namespace Spinit.Wpc.Synologen.OPQ.Presentation
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

		public static string GetSessionValue(string sessionName, string defaultValue)
		{
			try
			{
				return HttpContext.Current.Session[sessionName].ToString();
			}
			catch { return defaultValue; }
		}

		public static int GetSessionValue(string sessionName, int defaultValue)
		{
			try
			{
				int returnValue;
				return Int32.TryParse(HttpContext.Current.Session[sessionName].ToString(), out returnValue) ? returnValue : defaultValue;
			}
			catch { return defaultValue; }
		}

		public static bool GetSessionValue(string sessionName, bool defaultValue)
		{
			try { return (bool)(HttpContext.Current.Session[sessionName]); }
			catch { return defaultValue; }
		}

		public static DateTime GetSessionValue(string sessionName, DateTime defaultValue)
		{
			try { return (DateTime)(HttpContext.Current.Session[sessionName]); }
			catch { return defaultValue; }
		}

		public static void SetSessionValue(string sessionName, object value)
		{
			HttpContext.Current.Session[sessionName] = value;
		}

		public static object GetSessionValue(string sessionName)
		{
			try
			{
				object list = HttpContext.Current.Session[sessionName];
				return list ?? new object();
			}
			catch { return new object(); }
		}

		public static List<int> GetSessionIntList(string sessionName)
		{
			try
			{
				var list = (List<int>)HttpContext.Current.Session[sessionName];
				return list ?? new List<int>();
			}
			catch { return new List<int>(); }
		}

		/// <summary>
		/// Finds a Control recursively. 
		/// Note finds the first match and exists
		/// </summary>
		/// <param name="root">The container to start looking</param>
		/// <param name="id">Id to find</param>
		/// <returns></returns>

		public static Control FindControlRecursive(Control root, string id)
		{

			if (root.ID == id)
				return root;
			foreach (Control ctl in root.Controls)
			{
				Control foundCtl = FindControlRecursive(ctl, id);
				if (foundCtl != null)
					return foundCtl;
			}
			return null;
		}

		/// <summary>
		/// Builds a path from root to current node separated with delimiter
		/// </summary>
		/// <param name="nodeId">Id of current node</param>
		/// <param name="delimiter">The delimiter to use</param>
		/// <returns></returns>
		public static string GetNodePathFromId(int nodeId, string delimiter)
		{
			var path = new StringBuilder();
			var bNode = new BNode(SessionContext.CurrentOpq);
			var node = bNode.GetNode(nodeId, false);
			path = path.Insert(0, node.Name);
			while (node.Parent != null)
			{
				node = bNode.GetNode((int) node.Parent, false);
				path = path.Insert(0, delimiter);
				path = path.Insert(0, node.Name);
			}
			return path.ToString();
		}

	}
}
