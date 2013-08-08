#pragma warning disable 1591

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Spinit's LINQ to SQL template for T4 C#
//     Generated at 06/07/2012 10:51:23
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.OPQ.Core.Entities
{	
	public class NodeSupplierConnection
	{
		#region Column Mappings
		
		public int NdeId { get; set; }
		
		public int SupId { get; set; }
		
		#endregion
		
		#region Associations
		
		public Node Node { get; set; }
		
		public BaseUser BaseUser { get; set; }
		
		#endregion
		
		#region Overrides
		
		/// <summary>
		/// Returnes the string representation of this class.
		/// </summary>
		/// <returns>The string.</returns>

		public override string ToString ()
		{
			string str = string.Empty;
				 				 
			return str.Length > 1 ? str.Substring(0, str.Length - 1) : str;
		}		
		
		/// <summary>
		/// Overrides the equals base method.
		/// </summary>
		/// <param name="o">The compare object.</param>
		/// <returns>If match, true.</returns>

		public override bool Equals (object o)
		{
			if (!(o is NodeSupplierConnection )) {
				return false;
			}

			NodeSupplierConnection eq = o as NodeSupplierConnection;

			return NdeId == eq.NdeId && SupId == eq.SupId;
		}

		/// <summary>
		/// Overrides the GetHashCode base method.
		/// </summary>
		/// <returns>The hash-code for this object.</returns>

		public override int GetHashCode ()
		{
			return NdeId.GetHashCode () + SupId.GetHashCode ();
		}

		#endregion
	}
}
#pragma warning restore 1591
