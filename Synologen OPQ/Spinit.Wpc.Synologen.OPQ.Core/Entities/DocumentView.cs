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
	public class DocumentView
	{
		#region Column Mappings
		
		public int Id { get; set; }
		
		public DateTime? HistoryDate { get; set; }
		
		public int NdeId { get; set; }
		
		public int? ShpId { get; set; }
		
		public int? CncId { get; set; }
		
		public int DocTpeId { get; set; }
		
		public string DocumentContent { get; set; }
		
		public bool IsActive { get; set; }
		
		public int? ApprovedById { get; set; }
		
		public string ApprovedByName { get; set; }
		
		public DateTime? ApprovedDate { get; set; }
		
		public int? LockedById { get; set; }
		
		public string LockedByName { get; set; }
		
		public DateTime? LockedDate { get; set; }
		
		#endregion
	}
}
#pragma warning restore 1591
