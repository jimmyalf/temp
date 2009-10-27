using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq.Expressions;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Data.Entities
{	
	[Table(Name=@"dbo.SynologenOpqDocuments")]
	public partial class EDocument : EntityBase
	{
		#region Spinit search extension
		
		/// <summary>
		/// Creates a lambda-expression for use with the data-load-option feature,
		/// </summary>
		/// <param name="property">The property to search-for.</param>
		/// <returns>A lambda-expression.</returns>

		public override LambdaExpression BuildSearchExpression (string property)
		{
			ParameterExpression parameter = Expression.Parameter (GetType (), "eDocument");
			return Expression.Lambda<Func<EDocument, object>> (
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
		public EDocument()
		{
			_SynologenOpqDocumentHistories = new EntitySet<EDocumentHistory>(attach_DocumentHistories, detach_DocumentHistories);
			_SynologenOpqDocumentType = default(EntityRef<EDocumentType>); 
			_SynologenOpqNode = default(EntityRef<ENode>); 
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
		
		partial void OnNdeIdChanging(int value);
		partial void OnNdeIdChanged();
		private int _NdeId;
		[Column(Storage=@"_NdeId", DbType=@"Int NOT NULL", CanBeNull=false)]
		public int NdeId
		{
			get { return _NdeId; }
			set {
				if (_NdeId != value) {
					if (_SynologenOpqNode.HasLoadedOrAssignedValue) {
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					OnNdeIdChanging(value);
					SendPropertyChanging();
					_NdeId = value;
					SendPropertyChanged("NdeId");
					OnNdeIdChanged();
				}
			}
		}
		
		partial void OnShpIdChanging(int? value);
		partial void OnShpIdChanged();
		private int? _ShpId;
		[Column(Storage=@"_ShpId", DbType=@"Int")]
		public int? ShpId
		{
			get { return _ShpId; }
			set {
				if (_ShpId != value) {
					OnShpIdChanging(value);
					SendPropertyChanging();
					_ShpId = value;
					SendPropertyChanged("ShpId");
					OnShpIdChanged();
				}
			}
		}
		
		partial void OnCncIdChanging(int? value);
		partial void OnCncIdChanged();
		private int? _CncId;
		[Column(Storage=@"_CncId", DbType=@"Int")]
		public int? CncId
		{
			get { return _CncId; }
			set {
				if (_CncId != value) {
					OnCncIdChanging(value);
					SendPropertyChanging();
					_CncId = value;
					SendPropertyChanged("CncId");
					OnCncIdChanged();
				}
			}
		}
		
		partial void OnDocTpeIdChanging(int value);
		partial void OnDocTpeIdChanged();
		private int _DocTpeId;
		[Column(Storage=@"_DocTpeId", DbType=@"Int NOT NULL", CanBeNull=false)]
		public int DocTpeId
		{
			get { return _DocTpeId; }
			set {
				if (_DocTpeId != value) {
					if (_SynologenOpqDocumentType.HasLoadedOrAssignedValue) {
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					OnDocTpeIdChanging(value);
					SendPropertyChanging();
					_DocTpeId = value;
					SendPropertyChanged("DocTpeId");
					OnDocTpeIdChanged();
				}
			}
		}
		
		partial void OnDocumentContentChanging(string value);
		partial void OnDocumentContentChanged();
		private string _DocumentContent;
		[Column(Storage=@"_DocumentContent", DbType=@"NText NOT NULL", CanBeNull=false, UpdateCheck=UpdateCheck.Never)]
		public string DocumentContent
		{
			get { return _DocumentContent; }
			set {
				if (_DocumentContent != value) {
					OnDocumentContentChanging(value);
					SendPropertyChanging();
					_DocumentContent = value;
					SendPropertyChanged("DocumentContent");
					OnDocumentContentChanged();
				}
			}
		}
		
		partial void OnIsActiveChanging(bool value);
		partial void OnIsActiveChanged();
		private bool _IsActive;
		[Column(Storage=@"_IsActive", DbType=@"Bit NOT NULL", CanBeNull=false)]
		public bool IsActive
		{
			get { return _IsActive; }
			set {
				if (_IsActive != value) {
					OnIsActiveChanging(value);
					SendPropertyChanging();
					_IsActive = value;
					SendPropertyChanged("IsActive");
					OnIsActiveChanged();
				}
			}
		}
		
		partial void OnCreatedByIdChanging(int value);
		partial void OnCreatedByIdChanged();
		private int _CreatedById;
		[Column(Storage=@"_CreatedById", DbType=@"Int NOT NULL", CanBeNull=false)]
		public int CreatedById
		{
			get { return _CreatedById; }
			set {
				if (_CreatedById != value) {
					OnCreatedByIdChanging(value);
					SendPropertyChanging();
					_CreatedById = value;
					SendPropertyChanged("CreatedById");
					OnCreatedByIdChanged();
				}
			}
		}
		
		partial void OnCreatedByNameChanging(string value);
		partial void OnCreatedByNameChanged();
		private string _CreatedByName;
		[Column(Storage=@"_CreatedByName", DbType=@"NVarChar(100) NOT NULL", CanBeNull=false)]
		public string CreatedByName
		{
			get { return _CreatedByName; }
			set {
				if (_CreatedByName != value) {
					OnCreatedByNameChanging(value);
					SendPropertyChanging();
					_CreatedByName = value;
					SendPropertyChanged("CreatedByName");
					OnCreatedByNameChanged();
				}
			}
		}
		
		partial void OnCreatedDateChanging(DateTime value);
		partial void OnCreatedDateChanged();
		private DateTime _CreatedDate;
		[Column(Storage=@"_CreatedDate", DbType=@"DateTime NOT NULL", CanBeNull=false)]
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
		
		partial void OnChangedByIdChanging(int? value);
		partial void OnChangedByIdChanged();
		private int? _ChangedById;
		[Column(Storage=@"_ChangedById", DbType=@"Int")]
		public int? ChangedById
		{
			get { return _ChangedById; }
			set {
				if (_ChangedById != value) {
					OnChangedByIdChanging(value);
					SendPropertyChanging();
					_ChangedById = value;
					SendPropertyChanged("ChangedById");
					OnChangedByIdChanged();
				}
			}
		}
		
		partial void OnChangedByNameChanging(string value);
		partial void OnChangedByNameChanged();
		private string _ChangedByName;
		[Column(Storage=@"_ChangedByName", DbType=@"NVarChar(100)")]
		public string ChangedByName
		{
			get { return _ChangedByName; }
			set {
				if (_ChangedByName != value) {
					OnChangedByNameChanging(value);
					SendPropertyChanging();
					_ChangedByName = value;
					SendPropertyChanged("ChangedByName");
					OnChangedByNameChanged();
				}
			}
		}
		
		partial void OnChangedDateChanging(DateTime? value);
		partial void OnChangedDateChanged();
		private DateTime? _ChangedDate;
		[Column(Storage=@"_ChangedDate", DbType=@"DateTime")]
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
		
		partial void OnApprovedByIdChanging(int? value);
		partial void OnApprovedByIdChanged();
		private int? _ApprovedById;
		[Column(Storage=@"_ApprovedById", DbType=@"Int")]
		public int? ApprovedById
		{
			get { return _ApprovedById; }
			set {
				if (_ApprovedById != value) {
					OnApprovedByIdChanging(value);
					SendPropertyChanging();
					_ApprovedById = value;
					SendPropertyChanged("ApprovedById");
					OnApprovedByIdChanged();
				}
			}
		}
		
		partial void OnApprovedByNameChanging(string value);
		partial void OnApprovedByNameChanged();
		private string _ApprovedByName;
		[Column(Storage=@"_ApprovedByName", DbType=@"NVarChar(100)")]
		public string ApprovedByName
		{
			get { return _ApprovedByName; }
			set {
				if (_ApprovedByName != value) {
					OnApprovedByNameChanging(value);
					SendPropertyChanging();
					_ApprovedByName = value;
					SendPropertyChanged("ApprovedByName");
					OnApprovedByNameChanged();
				}
			}
		}
		
		partial void OnApprovedDateChanging(DateTime? value);
		partial void OnApprovedDateChanged();
		private DateTime? _ApprovedDate;
		[Column(Storage=@"_ApprovedDate", DbType=@"DateTime")]
		public DateTime? ApprovedDate
		{
			get { return _ApprovedDate; }
			set {
				if (_ApprovedDate != value) {
					OnApprovedDateChanging(value);
					SendPropertyChanging();
					_ApprovedDate = value;
					SendPropertyChanged("ApprovedDate");
					OnApprovedDateChanged();
				}
			}
		}
		
		partial void OnLockedByIdChanging(int? value);
		partial void OnLockedByIdChanged();
		private int? _LockedById;
		[Column(Storage=@"_LockedById", DbType=@"Int")]
		public int? LockedById
		{
			get { return _LockedById; }
			set {
				if (_LockedById != value) {
					OnLockedByIdChanging(value);
					SendPropertyChanging();
					_LockedById = value;
					SendPropertyChanged("LockedById");
					OnLockedByIdChanged();
				}
			}
		}
		
		partial void OnLockedByNameChanging(string value);
		partial void OnLockedByNameChanged();
		private string _LockedByName;
		[Column(Storage=@"_LockedByName", DbType=@"NVarChar(100)")]
		public string LockedByName
		{
			get { return _LockedByName; }
			set {
				if (_LockedByName != value) {
					OnLockedByNameChanging(value);
					SendPropertyChanging();
					_LockedByName = value;
					SendPropertyChanged("LockedByName");
					OnLockedByNameChanged();
				}
			}
		}
		
		partial void OnLockedDateChanging(DateTime? value);
		partial void OnLockedDateChanged();
		private DateTime? _LockedDate;
		[Column(Storage=@"_LockedDate", DbType=@"DateTime")]
		public DateTime? LockedDate
		{
			get { return _LockedDate; }
			set {
				if (_LockedDate != value) {
					OnLockedDateChanging(value);
					SendPropertyChanging();
					_LockedDate = value;
					SendPropertyChanged("LockedDate");
					OnLockedDateChanged();
				}
			}
		}
		
		#endregion
		
		#region Associations
		private EntitySet<EDocumentHistory> _SynologenOpqDocumentHistories;
		[Association(Name=@"SynologenOpqDocument_SynologenOpqDocumentHistory", Storage=@"_SynologenOpqDocumentHistories", ThisKey=@"Id", OtherKey=@"Id")]
		public EntitySet<EDocumentHistory> DocumentHistories
		{
			get {
				return _SynologenOpqDocumentHistories;
			}
			set {
				_SynologenOpqDocumentHistories.Assign(value);
			}
		}

		private void attach_DocumentHistories(EDocumentHistory entity)
		{
			SendPropertyChanging();
			entity.Document = this;
		}
		
		private void detach_DocumentHistories(EDocumentHistory entity)
		{
			SendPropertyChanging();
			entity.Document = null;
		}
		private EntityRef<EDocumentType> _SynologenOpqDocumentType;
		[Association(Name=@"SynologenOpqDocumentType_SynologenOpqDocument", Storage=@"_SynologenOpqDocumentType", ThisKey=@"DocTpeId", OtherKey=@"Id", IsForeignKey=true)]
		public EDocumentType DocumentType
		{
			get {
				return _SynologenOpqDocumentType.Entity;
			}
			set {
				EDocumentType previousValue = _SynologenOpqDocumentType.Entity;
				if ((previousValue != value) || (!_SynologenOpqDocumentType.HasLoadedOrAssignedValue)) {
					SendPropertyChanging();
					if (previousValue != null) {
						_SynologenOpqDocumentType.Entity = null;
						previousValue.Documents.Remove(this);
					}
					_SynologenOpqDocumentType.Entity = value;
					if (value != null) {
						value.Documents.Add(this);
						_DocTpeId = value.Id;
					}
					else {
						_DocTpeId = default(int);
					}
					SendPropertyChanged("DocumentType");
				}
			}
		}

		private EntityRef<ENode> _SynologenOpqNode;
		[Association(Name=@"SynologenOpqNode_SynologenOpqDocument", Storage=@"_SynologenOpqNode", ThisKey=@"NdeId", OtherKey=@"Id", IsForeignKey=true)]
		public ENode Node
		{
			get {
				return _SynologenOpqNode.Entity;
			}
			set {
				ENode previousValue = _SynologenOpqNode.Entity;
				if ((previousValue != value) || (!_SynologenOpqNode.HasLoadedOrAssignedValue)) {
					SendPropertyChanging();
					if (previousValue != null) {
						_SynologenOpqNode.Entity = null;
						previousValue.Documents.Remove(this);
					}
					_SynologenOpqNode.Entity = value;
					if (value != null) {
						value.Documents.Add(this);
						_NdeId = value.Id;
					}
					else {
						_NdeId = default(int);
					}
					SendPropertyChanged("Node");
				}
			}
		}

		#endregion

		#region Converters
		
		/// <summary>
		/// Converts from EDocument to Document.
		/// </summary>
		/// <param name="eDocument">The EDocument.</param>
		/// <returns>The converted Document.</returns>

		public static Document Convert (EDocument eDocument)
		{
			Document document = new Document
			{
				Id = eDocument.Id,
				NdeId = eDocument.NdeId,
				ShpId = eDocument.ShpId,
				CncId = eDocument.CncId,
				DocTpeId = (DocumentTypes) eDocument.DocTpeId,
				DocumentContent = eDocument.DocumentContent,
				IsActive = eDocument.IsActive,
				CreatedById = eDocument.CreatedById,
				CreatedByName = eDocument.CreatedByName,
				CreatedDate = eDocument.CreatedDate,
				ChangedById = eDocument.ChangedById,
				ChangedByName = eDocument.ChangedByName,
				ChangedDate = eDocument.ChangedDate,
				ApprovedById = eDocument.ApprovedById,
				ApprovedByName = eDocument.ApprovedByName,
				ApprovedDate = eDocument.ApprovedDate,
				LockedById = eDocument.LockedById,
				LockedByName = eDocument.LockedByName,
				LockedDate = eDocument.LockedDate,
			};
			
			return document;
		}
		
		/// <summary>
		/// Converts from Document to EDocument.
		/// </summary>
		/// <param name="document">The Document.</param>
		/// <returns>The converted EDocument.</returns>

		public static EDocument Convert (Document document)
		{		
			return new EDocument
			{
				Id = document.Id,
				NdeId = document.NdeId,
				ShpId = document.ShpId,
				CncId = document.CncId,
				DocTpeId = (int) document.DocTpeId,
				DocumentContent = document.DocumentContent,
				IsActive = document.IsActive,
				CreatedById = document.CreatedById,
				CreatedByName = document.CreatedByName,
				CreatedDate = document.CreatedDate,
				ChangedById = document.ChangedById,
				ChangedByName = document.ChangedByName,
				ChangedDate = document.ChangedDate,
				ApprovedById = document.ApprovedById,
				ApprovedByName = document.ApprovedByName,
				ApprovedDate = document.ApprovedDate,
				LockedById = document.LockedById,
				LockedByName = document.LockedByName,
				LockedDate = document.LockedDate,
			};
		}

		#endregion
	}
}