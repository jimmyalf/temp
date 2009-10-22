#pragma warning disable 1591

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Spinit's LINQ to SQL template for T4 C#
//     Generated at 10/22/2009 15:41:29
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Opq.Core.Entities
{	
	public class SynologenOpqDocumentHistory
	{
		#region Column Mappings
		
		public int Id { get; set; }
		
		public DateTime HistoryDate { get; set; }
		
		public int HistoryId { get; set; }
		
		public string HistoryName { get; set; }
		
		public int NdeId { get; set; }
		
		public int? ShpId { get; set; }
		
		public int? CncId { get; set; }
		
		public int DocTpeId { get; set; }
		
		public string Document { get; set; }
		
		public bool IsActive { get; set; }
		
		public int CreatedById { get; set; }
		
		public string CreatedByName { get; set; }
		
		public DateTime CreatedDate { get; set; }
		
		public int? ChangedById { get; set; }
		
		public string ChangedByName { get; set; }
		
		public DateTime? ChangedDate { get; set; }
		
		public int? ApprovedById { get; set; }
		
		public string ApprovedByName { get; set; }
		
		public DateTime? ApprovedDate { get; set; }
		
		public int? LockedById { get; set; }
		
		public string LockedByName { get; set; }
		
		public DateTime? LockedDate { get; set; }
		
		#endregion
		
		#region Associations
		
		public SynologenOpqDocument SynologenOpqDocument { get; set; }
		
		#endregion
		
		#region Overrides
		
		/// <summary>
		/// Returnes the string representation of this class.
		/// </summary>
		/// <returns>The string.</returns>

		public override string ToString ()
		{
			string str = string.Empty;
				 				 				 				  
			str += string.Concat (HistoryName, " ") ?? string.Empty;
				 				 				 				 				  
			str += string.Concat (Document, " ") ?? string.Empty;
				 				 				  
			str += string.Concat (CreatedByName, " ") ?? string.Empty;
				 				 				  
			str += string.Concat (ChangedByName, " ") ?? string.Empty;
				 				 				  
			str += string.Concat (ApprovedByName, " ") ?? string.Empty;
				 				 				  
			str += string.Concat (LockedByName, " ") ?? string.Empty;
				 
			return str.Length > 1 ? str.Substring(0, str.Length - 1) : str;
		}		
		
		/// <summary>
		/// Overrides the equals base method.
		/// </summary>
		/// <param name="o">The compare object.</param>
		/// <returns>If match, true.</returns>

		public override bool Equals (object o)
		{
			if (!(o is SynologenOpqDocumentHistory )) {
				return false;
			}

			SynologenOpqDocumentHistory eq = o as SynologenOpqDocumentHistory;

			return Id == eq.Id && HistoryDate == eq.HistoryDate;
		}

		/// <summary>
		/// Overrides the GetHashCode base method.
		/// </summary>
		/// <returns>The hash-code for this object.</returns>

		public override int GetHashCode ()
		{
			return Id.GetHashCode () + HistoryDate.GetHashCode ();
		}

		#endregion
	}
}
#pragma warning restore 1591
