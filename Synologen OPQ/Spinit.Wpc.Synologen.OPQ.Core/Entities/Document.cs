using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.OPQ.Core.Entities
{	
	public class Document
	{
		#region Column Mappings
		
		public int Id { get; set; }
		
		public int NdeId { get; set; }
		
		public int? ShpId { get; set; }
		
		public int? CncId { get; set; }
		
		public DocumentTypes DocTpeId { get; set; }
		
		public string DocumentContent { get; set; }
		
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
		
		public List<DocumentHistory> DocumentHistories { get; set; }
		
		public DocumentType DocumentType { get; set; }
		
		public Node Node { get; set; }
		
		#endregion
		
		#region Overrides
		
		/// <summary>
		/// Returnes the string representation of this class.
		/// </summary>
		/// <returns>The string.</returns>

		public override string ToString ()
		{
			string str = string.Empty;
				 				 				 				 				 				  
			str += string.Concat (DocumentContent, " ") ?? string.Empty;
				 				 				  
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
			if (!(o is Document )) {
				return false;
			}

			Document eq = o as Document;

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
