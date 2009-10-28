#pragma warning disable 1591

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Spinit's LINQ to SQL template for T4 C#
//     Generated at 10/27/2009 15:39:31
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.OPQ.Core.Entities
{	
	public class BaseFile
	{
		#region Column Mappings
		
		public int Id { get; set; }
		
		public string Name { get; set; }
		
		public bool? Directory { get; set; }
		
		public string ContentInfo { get; set; }
		
		public string KeyWords { get; set; }
		
		public string Description { get; set; }
		
		public string CreatedBy { get; set; }
		
		public DateTime CreatedDate { get; set; }
		
		public string ChangedBy { get; set; }
		
		public DateTime? ChangedDate { get; set; }
		
		public int? IconType { get; set; }
		
		#endregion
		
		#region Associations
		
		public List<File> Files { get; set; }
		
		#endregion
		
		#region Overrides
		
		/// <summary>
		/// Returnes the string representation of this class.
		/// </summary>
		/// <returns>The string.</returns>

		public override string ToString ()
		{
			string str = string.Empty;
				 				  
			str += string.Concat (Name, " ") ?? string.Empty;
				 				  
			str += string.Concat (ContentInfo, " ") ?? string.Empty;
				  
			str += string.Concat (KeyWords, " ") ?? string.Empty;
				  
			str += string.Concat (Description, " ") ?? string.Empty;
				  
			str += string.Concat (CreatedBy, " ") ?? string.Empty;
				 				  
			str += string.Concat (ChangedBy, " ") ?? string.Empty;
				 				 
			return str.Length > 1 ? str.Substring(0, str.Length - 1) : str;
		}		
		
		/// <summary>
		/// Overrides the equals base method.
		/// </summary>
		/// <param name="o">The compare object.</param>
		/// <returns>If match, true.</returns>

		public override bool Equals (object o)
		{
			if (!(o is BaseFile )) {
				return false;
			}

			BaseFile eq = o as BaseFile;

			return Id == eq.Id;
		}

		/// <summary>
		/// Overrides the GetHashCode base method.
		/// </summary>
		/// <returns>The hash-code for this object.</returns>

		public override int GetHashCode ()
		{
			return Id.GetHashCode ();
		}

		#endregion
	}
}
#pragma warning restore 1591
