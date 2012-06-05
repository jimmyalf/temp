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
	[Table(Name=@"dbo.tblSynologenShopGroup")]
	public partial class EShopGroup : EntityBase
	{
		#region Spinit search extension
		
		/// <summary>
		/// Creates a lambda-expression for use with the data-load-option feature,
		/// </summary>
		/// <param name="property">The property to search-for.</param>
		/// <returns>A lambda-expression.</returns>

		public override LambdaExpression BuildSearchExpression (string property)
		{
			ParameterExpression parameter = Expression.Parameter (GetType (), "eShopGroup");
			return Expression.Lambda<Func<EShopGroup, object>> (
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
		public EShopGroup()
		{
			_SynologenOpqFiles = new EntitySet<EFile>(AttachFiles, DetachFiles);
			_SynologenOpqDocuments = new EntitySet<EDocument>(AttachDocuments, DetachDocuments);
			_tblSynologenShops = new EntitySet<EShop>(AttachShops, DetachShops);
			OnCreated();
		}
		#endregion

		#region Column Mappings
		partial void OnIdChanging(int value);
		partial void OnIdChanged();
		private int _Id;
		[Column(Storage=@"_Id", AutoSync=AutoSync.OnInsert, DbType=@"Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true, UpdateCheck=UpdateCheck.Never)]
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
		[Column(Storage=@"_Name", DbType=@"NVarChar(150) NOT NULL", CanBeNull=false)]
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
		
		#endregion
		
		#region Associations
		private EntitySet<EFile> _SynologenOpqFiles;
		[Association(Name=@"tblSynologenShopGroup_SynologenOpqFile", Storage=@"_SynologenOpqFiles", ThisKey=@"Id", OtherKey=@"ShopGroupId")]
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
			entity.ShopGroup = this;
		}
		
		private void DetachFiles(EFile entity)
		{
			SendPropertyChanging();
			entity.ShopGroup = null;
		}
		private EntitySet<EDocument> _SynologenOpqDocuments;
		[Association(Name=@"tblSynologenShopGroup_SynologenOpqDocument", Storage=@"_SynologenOpqDocuments", ThisKey=@"Id", OtherKey=@"ShopGroupId")]
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
			entity.ShopGroup = this;
		}
		
		private void DetachDocuments(EDocument entity)
		{
			SendPropertyChanging();
			entity.ShopGroup = null;
		}
		private EntitySet<EShop> _tblSynologenShops;
		[Association(Name=@"tblSynologenShopGroup_tblSynologenShop", Storage=@"_tblSynologenShops", ThisKey=@"Id", OtherKey=@"ShopGroupId")]
		public EntitySet<EShop> Shops
		{
			get {
				return _tblSynologenShops;
			}
			set {
				_tblSynologenShops.Assign(value);
			}
		}

		private void AttachShops(EShop entity)
		{
			SendPropertyChanging();
			entity.ShopGroup = this;
		}
		
		private void DetachShops(EShop entity)
		{
			SendPropertyChanging();
			entity.ShopGroup = null;
		}
		#endregion

		#region Converters
		
		/// <summary>
		/// Converts from EShopGroup to ShopGroup.
		/// </summary>
		/// <param name="eShopGroup">The EShopGroup.</param>
		/// <returns>The converted ShopGroup.</returns>

		public static ShopGroup Convert (EShopGroup eShopGroup)
		{
			ShopGroup shopGroup = new ShopGroup
			{
				Id = eShopGroup.Id,
				Name = eShopGroup.Name,
			};
			
			return shopGroup;
		}
		
		/// <summary>
		/// Converts from ShopGroup to EShopGroup.
		/// </summary>
		/// <param name="shopGroup">The ShopGroup.</param>
		/// <returns>The converted EShopGroup.</returns>

		public static EShopGroup Convert (ShopGroup shopGroup)
		{		
			return new EShopGroup
			{
				Id = shopGroup.Id,
				Name = shopGroup.Name,
			};
		}

		#endregion
	}
}
#pragma warning restore 1591