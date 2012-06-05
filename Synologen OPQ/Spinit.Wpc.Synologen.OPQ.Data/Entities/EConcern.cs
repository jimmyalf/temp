#pragma warning disable 1591

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Spinit's LINQ to SQL template for T4 C#
//     Generated at 06/04/2012 12:03:10
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq.Expressions;

using Spinit.Data.Linq;

using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Data.Entities
{	
	[Table(Name=@"dbo.tblSynologenConcern")]
	public partial class EConcern : EntityBase
	{
		#region Spinit search extension
		
		/// <summary>
		/// Creates a lambda-expression for use with the data-load-option feature,
		/// </summary>
		/// <param name="property">The property to search-for.</param>
		/// <returns>A lambda-expression.</returns>

		public override LambdaExpression BuildSearchExpression (string property)
		{
			ParameterExpression parameter = Expression.Parameter (GetType (), "eConcern");
			return Expression.Lambda<Func<EConcern, object>> (
						Expression.Property (parameter, property),
						parameter);
		}
		
		#endregion
		
		#region Extensibility Method Definitions
		partial void OnLoaded();
		partial void OnValidate(ChangeAction action);
		partial void OnCreated();
		#endregion

		#region Construction
		public EConcern()
		{
			_SynologenOpqFiles = new EntitySet<EFile>(AttachFiles, DetachFiles);
			_SynologenOpqDocuments = new EntitySet<EDocument>(AttachDocuments, DetachDocuments);
			OnCreated();
		}
		#endregion

		#region Column Mappings
		partial void OnIdChanging(int value);
		partial void OnIdChanged();
		private int _Id;
		[Column(Storage=@"_Id", Name=@"cId", AutoSync=AutoSync.OnInsert, DbType=@"Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
		public int Id
		{
			get { return _Id; }
			set {
				if (_Id != value) {
					OnIdChanging(value);
					SendPropertyChanging();
					_Id = value;
					SendPropertyChanged("Id");
					OnIdChanged();
				}
			}
		}
		
		partial void OnNameChanging(string value);
		partial void OnNameChanged();
		private string _Name;
		[Column(Storage=@"_Name", Name=@"cName", DbType=@"NVarChar(512) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get { return _Name; }
			set {
				if (_Name != value) {
					OnNameChanging(value);
					SendPropertyChanging();
					_Name = value;
					SendPropertyChanged("Name");
					OnNameChanged();
				}
			}
		}
		
		partial void OnCommonOpqChanging(bool? value);
		partial void OnCommonOpqChanged();
		private bool? _CommonOpq;
		[Column(Storage=@"_CommonOpq", Name=@"cCommonOpq", DbType=@"Bit")]
		public bool? CommonOpq
		{
			get { return _CommonOpq; }
			set {
				if (_CommonOpq != value) {
					OnCommonOpqChanging(value);
					SendPropertyChanging();
					_CommonOpq = value;
					SendPropertyChanged("CommonOpq");
					OnCommonOpqChanged();
				}
			}
		}
		
		#endregion
		
		#region Associations
		private EntitySet<EFile> _SynologenOpqFiles;
		[Association(Name=@"tblSynologenConcern_SynologenOpqFile", Storage=@"_SynologenOpqFiles", ThisKey=@"Id", OtherKey=@"CncId")]
		public EntitySet<EFile> Files
		{
			get {
				return _SynologenOpqFiles;
			}
			set {
				_SynologenOpqFiles.Assign(value);
			}
		}

		private void AttachFiles(EFile entity)
		{
			SendPropertyChanging();
			entity.Concern = this;
		}
		
		private void DetachFiles(EFile entity)
		{
			SendPropertyChanging();
			entity.Concern = null;
		}
		private EntitySet<EDocument> _SynologenOpqDocuments;
		[Association(Name=@"tblSynologenConcern_SynologenOpqDocument", Storage=@"_SynologenOpqDocuments", ThisKey=@"Id", OtherKey=@"CncId")]
		public EntitySet<EDocument> Documents
		{
			get {
				return _SynologenOpqDocuments;
			}
			set {
				_SynologenOpqDocuments.Assign(value);
			}
		}

		private void AttachDocuments(EDocument entity)
		{
			SendPropertyChanging();
			entity.Concern = this;
		}
		
		private void DetachDocuments(EDocument entity)
		{
			SendPropertyChanging();
			entity.Concern = null;
		}
		#endregion

		#region Converters
		
		/// <summary>
		/// Converts from EConcern to Concern.
		/// </summary>
		/// <param name="eConcern">The EConcern.</param>
		/// <returns>The converted Concern.</returns>

		public static Concern Convert (EConcern eConcern)
		{
			Concern concern = new Concern
			{
				Id = eConcern.Id,
				Name = eConcern.Name,
				CommonOpq = eConcern.CommonOpq,
			};
			
			return concern;
		}
		
		/// <summary>
		/// Converts from Concern to EConcern.
		/// </summary>
		/// <param name="concern">The Concern.</param>
		/// <returns>The converted EConcern.</returns>

		public static EConcern Convert (Concern concern)
		{		
			return new EConcern
			{
				Id = concern.Id,
				Name = concern.Name,
				CommonOpq = concern.CommonOpq,
			};
		}

		#endregion
	}
}
#pragma warning restore 1591
