#pragma warning disable 1591

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Spinit's LINQ to SQL template for T4 C#
//     Generated at 10/27/2009 15:39:30
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
	[Table(Name=@"dbo.tblBaseFile")]
	public partial class EBaseFile : EntityBase
	{
		#region Spinit search extension
		
		/// <summary>
		/// Creates a lambda-expression for use with the data-load-option feature,
		/// </summary>
		/// <param name="property">The property to search-for.</param>
		/// <returns>A lambda-expression.</returns>

		public override LambdaExpression BuildSearchExpression (string property)
		{
			ParameterExpression parameter = Expression.Parameter (GetType (), "eBaseFile");
			return Expression.Lambda<Func<EBaseFile, object>> (
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
		public EBaseFile()
		{
			_SynologenOpqFiles = new EntitySet<EFile>(AttachFiles, DetachFiles);
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
		[Column(Storage=@"_Name", Name=@"cName", DbType=@"NText NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
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
		
		partial void OnDirectoryChanging(bool? value);
		partial void OnDirectoryChanged();
		private bool? _Directory;
		[Column(Storage=@"_Directory", Name=@"cDirectory", DbType=@"Bit")]
		public bool? Directory
		{
			get { return _Directory; }
			set {
				if (_Directory != value) {
					OnDirectoryChanging(value);
					SendPropertyChanging();
					_Directory = value;
					SendPropertyChanged("Directory");
					OnDirectoryChanged();
				}
			}
		}
		
		partial void OnContentInfoChanging(string value);
		partial void OnContentInfoChanged();
		private string _ContentInfo;
		[Column(Storage=@"_ContentInfo", Name=@"cContentInfo", DbType=@"NVarChar(256)")]
		public string ContentInfo
		{
			get { return _ContentInfo; }
			set {
				if (_ContentInfo != value) {
					OnContentInfoChanging(value);
					SendPropertyChanging();
					_ContentInfo = value;
					SendPropertyChanged("ContentInfo");
					OnContentInfoChanged();
				}
			}
		}
		
		partial void OnKeyWordsChanging(string value);
		partial void OnKeyWordsChanged();
		private string _KeyWords;
		[Column(Storage=@"_KeyWords", Name=@"cKeyWords", DbType=@"NVarChar(256)")]
		public string KeyWords
		{
			get { return _KeyWords; }
			set {
				if (_KeyWords != value) {
					OnKeyWordsChanging(value);
					SendPropertyChanging();
					_KeyWords = value;
					SendPropertyChanged("KeyWords");
					OnKeyWordsChanged();
				}
			}
		}
		
		partial void OnDescriptionChanging(string value);
		partial void OnDescriptionChanged();
		private string _Description;
		[Column(Storage=@"_Description", Name=@"cDescription", DbType=@"NVarChar(256)")]
		public string Description
		{
			get { return _Description; }
			set {
				if (_Description != value) {
					OnDescriptionChanging(value);
					SendPropertyChanging();
					_Description = value;
					SendPropertyChanged("Description");
					OnDescriptionChanged();
				}
			}
		}
		
		partial void OnCreatedByChanging(string value);
		partial void OnCreatedByChanged();
		private string _CreatedBy;
		[Column(Storage=@"_CreatedBy", Name=@"cCreatedBy", DbType=@"NVarChar(100) NOT NULL", CanBeNull=false)]
		public string CreatedBy
		{
			get { return _CreatedBy; }
			set {
				if (_CreatedBy != value) {
					OnCreatedByChanging(value);
					SendPropertyChanging();
					_CreatedBy = value;
					SendPropertyChanged("CreatedBy");
					OnCreatedByChanged();
				}
			}
		}
		
		partial void OnCreatedDateChanging(DateTime value);
		partial void OnCreatedDateChanged();
		private DateTime _CreatedDate;
		[Column(Storage=@"_CreatedDate", Name=@"cCreatedDate", DbType=@"SmallDateTime NOT NULL", CanBeNull=false)]
		public DateTime CreatedDate
		{
			get { return _CreatedDate; }
			set {
				if (_CreatedDate != value) {
					OnCreatedDateChanging(value);
					SendPropertyChanging();
					_CreatedDate = value;
					SendPropertyChanged("CreatedDate");
					OnCreatedDateChanged();
				}
			}
		}
		
		partial void OnChangedByChanging(string value);
		partial void OnChangedByChanged();
		private string _ChangedBy;
		[Column(Storage=@"_ChangedBy", Name=@"cChangedBy", DbType=@"NVarChar(100)")]
		public string ChangedBy
		{
			get { return _ChangedBy; }
			set {
				if (_ChangedBy != value) {
					OnChangedByChanging(value);
					SendPropertyChanging();
					_ChangedBy = value;
					SendPropertyChanged("ChangedBy");
					OnChangedByChanged();
				}
			}
		}
		
		partial void OnChangedDateChanging(DateTime? value);
		partial void OnChangedDateChanged();
		private DateTime? _ChangedDate;
		[Column(Storage=@"_ChangedDate", Name=@"cChangedDate", DbType=@"SmallDateTime")]
		public DateTime? ChangedDate
		{
			get { return _ChangedDate; }
			set {
				if (_ChangedDate != value) {
					OnChangedDateChanging(value);
					SendPropertyChanging();
					_ChangedDate = value;
					SendPropertyChanged("ChangedDate");
					OnChangedDateChanged();
				}
			}
		}
		
		partial void OnIconTypeChanging(int? value);
		partial void OnIconTypeChanged();
		private int? _IconType;
		[Column(Storage=@"_IconType", Name=@"cIconType", DbType=@"Int")]
		public int? IconType
		{
			get { return _IconType; }
			set {
				if (_IconType != value) {
					OnIconTypeChanging(value);
					SendPropertyChanging();
					_IconType = value;
					SendPropertyChanged("IconType");
					OnIconTypeChanged();
				}
			}
		}
		
		#endregion
		
		#region Associations
		private EntitySet<EFile> _SynologenOpqFiles;
		[Association(Name=@"tblBaseFile_SynologenOpqFile", Storage=@"_SynologenOpqFiles", ThisKey=@"Id", OtherKey=@"FleId")]
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
			entity.BaseFile = this;
		}
		
		private void DetachFiles(EFile entity)
		{
			SendPropertyChanging();
			entity.BaseFile = null;
		}
		#endregion

		#region Converters
		
		/// <summary>
		/// Converts from EBaseFile to BaseFile.
		/// </summary>
		/// <param name="eBaseFile">The EBaseFile.</param>
		/// <returns>The converted BaseFile.</returns>

		public static BaseFile Convert (EBaseFile eBaseFile)
		{
			BaseFile baseFile = new BaseFile
			{
				Id = eBaseFile.Id,
				Name = eBaseFile.Name,
				Directory = eBaseFile.Directory,
				ContentInfo = eBaseFile.ContentInfo,
				KeyWords = eBaseFile.KeyWords,
				Description = eBaseFile.Description,
				CreatedBy = eBaseFile.CreatedBy,
				CreatedDate = eBaseFile.CreatedDate,
				ChangedBy = eBaseFile.ChangedBy,
				ChangedDate = eBaseFile.ChangedDate,
				IconType = eBaseFile.IconType,
			};
			
			return baseFile;
		}
		
		/// <summary>
		/// Converts from BaseFile to EBaseFile.
		/// </summary>
		/// <param name="baseFile">The BaseFile.</param>
		/// <returns>The converted EBaseFile.</returns>

		public static EBaseFile Convert (BaseFile baseFile)
		{		
			return new EBaseFile
			{
				Id = baseFile.Id,
				Name = baseFile.Name,
				Directory = baseFile.Directory,
				ContentInfo = baseFile.ContentInfo,
				KeyWords = baseFile.KeyWords,
				Description = baseFile.Description,
				CreatedBy = baseFile.CreatedBy,
				CreatedDate = baseFile.CreatedDate,
				ChangedBy = baseFile.ChangedBy,
				ChangedDate = baseFile.ChangedDate,
				IconType = baseFile.IconType,
			};
		}

		#endregion
	}
}
#pragma warning restore 1591
