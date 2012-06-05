#pragma warning disable 1591

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Spinit's LINQ to SQL template for T4 C#
//     Generated at 06/04/2012 12:03:11
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.OPQ.Core.Entities
{	
	public class Shop
	{
		#region Column Mappings
		
		public int Id { get; set; }
		
		public int CategoryId { get; set; }
		
		public string ShopName { get; set; }
		
		public string ShopNumber { get; set; }
		
		public string ShopDescription { get; set; }
		
		public string ContactFirstName { get; set; }
		
		public string ContactLastName { get; set; }
		
		public string Email { get; set; }
		
		public string Phone { get; set; }
		
		public string Phone2 { get; set; }
		
		public string Fax { get; set; }
		
		public string Url { get; set; }
		
		public string MapUrl { get; set; }
		
		public string Address { get; set; }
		
		public string Address2 { get; set; }
		
		public string Zip { get; set; }
		
		public string City { get; set; }
		
		public bool Active { get; set; }
		
		public int? GiroId { get; set; }
		
		public string GiroNumber { get; set; }
		
		public string GiroSupplier { get; set; }
		
		public int? ConcernId { get; set; }
		
		public int ShopAccess { get; set; }
		
		public string OrganizationNumber { get; set; }
		
		public decimal? Latitude { get; set; }
		
		public decimal? Longitude { get; set; }
		
		public string ExternalAccessUsername { get; set; }
		
		public string ExternalAccessHashedPassword { get; set; }
		
		public int? ShopGroupId { get; set; }
		
		#endregion
		
		#region Associations
		
		public List<File> Files { get; set; }
		
		public List<Document> Documents { get; set; }
		
		public List<ShopMemberConnection> ShopMemberConnections { get; set; }
		
		public ShopCategory ShopCategory { get; set; }
		
		public ShopGroup ShopGroup { get; set; }
		
		#endregion
		
		#region Overrides
		
		/// <summary>
		/// Returnes the string representation of this class.
		/// </summary>
		/// <returns>The string.</returns>

		public override string ToString ()
		{
			string str = string.Empty;
				 				 				  
			str += string.Concat (ShopName, " ") ?? string.Empty;
				  
			str += string.Concat (ShopNumber, " ") ?? string.Empty;
				  
			str += string.Concat (ShopDescription, " ") ?? string.Empty;
				  
			str += string.Concat (ContactFirstName, " ") ?? string.Empty;
				  
			str += string.Concat (ContactLastName, " ") ?? string.Empty;
				  
			str += string.Concat (Email, " ") ?? string.Empty;
				  
			str += string.Concat (Phone, " ") ?? string.Empty;
				  
			str += string.Concat (Phone2, " ") ?? string.Empty;
				  
			str += string.Concat (Fax, " ") ?? string.Empty;
				  
			str += string.Concat (Url, " ") ?? string.Empty;
				  
			str += string.Concat (MapUrl, " ") ?? string.Empty;
				  
			str += string.Concat (Address, " ") ?? string.Empty;
				  
			str += string.Concat (Address2, " ") ?? string.Empty;
				  
			str += string.Concat (Zip, " ") ?? string.Empty;
				  
			str += string.Concat (City, " ") ?? string.Empty;
				 				 				  
			str += string.Concat (GiroNumber, " ") ?? string.Empty;
				  
			str += string.Concat (GiroSupplier, " ") ?? string.Empty;
				 				 				  
			str += string.Concat (OrganizationNumber, " ") ?? string.Empty;
				 				 				  
			str += string.Concat (ExternalAccessUsername, " ") ?? string.Empty;
				  
			str += string.Concat (ExternalAccessHashedPassword, " ") ?? string.Empty;
				 
			return str.Length > 1 ? str.Substring(0, str.Length - 1) : str;
		}		
		
		/// <summary>
		/// Overrides the equals base method.
		/// </summary>
		/// <param name="o">The compare object.</param>
		/// <returns>If match, true.</returns>

		public override bool Equals (object o)
		{
			if (!(o is Shop )) {
				return false;
			}

			Shop eq = o as Shop;

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
