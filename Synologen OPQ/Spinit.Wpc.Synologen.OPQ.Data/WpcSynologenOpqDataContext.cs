
#pragma warning disable 1591

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Spinit's LINQ to SQL template for T4 C#
//     Generated at 10/22/2009 15:41:28
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.Opq.Data.Entities;

namespace Spinit.Wpc.Synologen.Opq.Data
{
	[DatabaseAttribute(Name=@"dbWpcSynologen")]
	public partial class WpcSynologenOpqDataContext : SpinitDataContext
	{
		#region Construction
	
		public WpcSynologenOpqDataContext (string connection) :
			base(connection)
		{
			OnCreated();
		}
		
		public WpcSynologenOpqDataContext (IDbConnection connection) :
			base(connection)
		{
			OnCreated();
		}
		
		public WpcSynologenOpqDataContext (string connection, MappingSource mappingSource) :
			base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WpcSynologenOpqDataContext (IDbConnection connection, MappingSource mappingSource) :
			base(connection, mappingSource)
		{
			OnCreated();
		}
		
		#endregion
		
		#region Extensibility Method Definitions
		
		partial void OnCreated();
		partial void InsertSynologenOpqDocumentHistory(ESynologenOpqDocumentHistory instance);
		partial void UpdateSynologenOpqDocumentHistory(ESynologenOpqDocumentHistory instance);
		partial void DeleteSynologenOpqDocumentHistory(ESynologenOpqDocumentHistory instance);
		partial void InsertSynologenOpqDocument(ESynologenOpqDocument instance);
		partial void UpdateSynologenOpqDocument(ESynologenOpqDocument instance);
		partial void DeleteSynologenOpqDocument(ESynologenOpqDocument instance);
		partial void InsertSynologenOpqDocumentType(ESynologenOpqDocumentType instance);
		partial void UpdateSynologenOpqDocumentType(ESynologenOpqDocumentType instance);
		partial void DeleteSynologenOpqDocumentType(ESynologenOpqDocumentType instance);
		partial void InsertSynologenOpqFileCategory(ESynologenOpqFileCategory instance);
		partial void UpdateSynologenOpqFileCategory(ESynologenOpqFileCategory instance);
		partial void DeleteSynologenOpqFileCategory(ESynologenOpqFileCategory instance);
		partial void InsertSynologenOpqFile(ESynologenOpqFile instance);
		partial void UpdateSynologenOpqFile(ESynologenOpqFile instance);
		partial void DeleteSynologenOpqFile(ESynologenOpqFile instance);
		partial void InsertSynologenOpqNode(ESynologenOpqNode instance);
		partial void UpdateSynologenOpqNode(ESynologenOpqNode instance);
		partial void DeleteSynologenOpqNode(ESynologenOpqNode instance);
		partial void InsertSynologenOpqNodeSupplierConnection(ESynologenOpqNodeSupplierConnection instance);
		partial void UpdateSynologenOpqNodeSupplierConnection(ESynologenOpqNodeSupplierConnection instance);
		partial void DeleteSynologenOpqNodeSupplierConnection(ESynologenOpqNodeSupplierConnection instance);
		
		#endregion
		
		#region Tables

		public Table<ESynologenOpqDocumentHistory> SynologenOpqDocumentHistories
		{
			get { return GetTable<ESynologenOpqDocumentHistory>(); }
		}
		
		public Table<ESynologenOpqDocument> SynologenOpqDocuments
		{
			get { return GetTable<ESynologenOpqDocument>(); }
		}
		
		public Table<ESynologenOpqDocumentType> SynologenOpqDocumentTypes
		{
			get { return GetTable<ESynologenOpqDocumentType>(); }
		}
		
		public Table<ESynologenOpqFileCategory> SynologenOpqFileCategories
		{
			get { return GetTable<ESynologenOpqFileCategory>(); }
		}
		
		public Table<ESynologenOpqFile> SynologenOpqFiles
		{
			get { return GetTable<ESynologenOpqFile>(); }
		}
		
		public Table<ESynologenOpqNode> SynologenOpqNodes
		{
			get { return GetTable<ESynologenOpqNode>(); }
		}
		
		public Table<ESynologenOpqNodeSupplierConnection> SynologenOpqNodeSupplierConnections
		{
			get { return GetTable<ESynologenOpqNodeSupplierConnection>(); }
		}
		
		#endregion
	}
	
	#region Result Classes
		

	#endregion
	
}
#pragma warning restore 1591
 
