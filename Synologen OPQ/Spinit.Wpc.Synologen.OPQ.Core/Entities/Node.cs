using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.OPQ.Core.Entities
{	
	public class Node
	{
		#region Column Mappings
		
		public int Id { get; set; }
		
		public int? Parent { get; set; }
		
		public int? Order { get; set; }
		
		public string Name { get; set; }

		public bool IsMenu { get; set; }

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
		
		public List<NodeSupplierConnection> NodeSupplierConnections { get; set; }
		
		public List<Document> Documents { get; set; }
		
		public List<File> Files { get; set; }
		
		public List<Node> Childs { get; set; }
		
		public Node ParentNode { get; set; }
		
		public BaseUser CreatedBy { get; set; }
		
		public BaseUser ChangedBy { get; set; }
		
		public BaseUser ApprovedBy { get; set; }
		
		public BaseUser LockedBy { get; set; }
		
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
			if (!(o is Node )) {
				return false;
			}

			Node eq = o as Node;

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