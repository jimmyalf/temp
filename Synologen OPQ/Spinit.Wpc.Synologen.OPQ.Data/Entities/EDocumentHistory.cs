using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq.Expressions;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Data.Entities
{	
	[Table(Name=@"dbo.SynologenOpqDocumentHistories")]
	public partial class EDocumentHistory : EntityBase
	{
		#region Spinit search extension
		
		/// <summary>
		/// Creates a lambda-expression for use with the data-load-option feature,
		/// </summary>
		/// <param name="property">The property to search-for.</param>
		/// <returns>A lambda-expression.</returns>

		public override LambdaExpression BuildSearchExpression (string property)
		{
			ParameterExpression parameter = Expression.Parameter (GetType (), "eDocumentHistory");
			return Expression.Lambda<Func<EDocumentHistory, object>> (
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
		public EDocumentHistory()
		{
			_SynologenOpqDocument = default(EntityRef<EDocument>); 
			OnCreated();
		}
		#endregion

		#region Column Mappings
		partial void OnIdChanging(int value);
		partial void OnIdChanged();
		private int _Id;
		[Column(Storage=@"_Id", DbType=@"Int NOT NULL", IsPrimaryKey=true, UpdateCheck=UpdateCheck.Never)]
		public int Id
		{
			get { return _Id; }
			set {
				if (_Id != value) {
					if (_SynologenOpqDocument.HasLoadedOrAssignedValue) {
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					OnIdChanging(value);
					SendPropertyChanging();
					_Id = value;
					SendPropertyChanged("Id");
					OnIdChanged();
				}
			}
		}
		
		partial void OnHistoryDateChanging(DateTime value);
		partial void OnHistoryDateChanged();
		private DateTime _HistoryDate;
		[Column(Storage=@"_HistoryDate", DbType=@"DateTime NOT NULL", IsPrimaryKey=true, UpdateCheck=UpdateCheck.Never)]
		public DateTime HistoryDate
		{
			get { return _HistoryDate; }
			set {
				if (_HistoryDate != value) {
					OnHistoryDateChanging(value);
					SendPropertyChanging();
					_HistoryDate = value;
					SendPropertyChanged("HistoryDate");
					OnHistoryDateChanged();
				}
			}
		}
		
		partial void OnHistoryIdChanging(int value);
		partial void OnHistoryIdChanged();
		private int _HistoryId;
		[Column(Storage=@"_HistoryId", DbType=@"Int NOT NULL", CanBeNull=false)]
		public int HistoryId
		{
			get { return _HistoryId; }
			set {
				if (_HistoryId != value) {
					OnHistoryIdChanging(value);
					SendPropertyChanging();
					_HistoryId = value;
					SendPropertyChanged("HistoryId");
					OnHistoryIdChanged();
				}
			}
		}
		
		partial void OnHistoryNameChanging(string value);
		partial void OnHistoryNameChanged();
		private string _HistoryName;
		[Column(Storage=@"_HistoryName", DbType=@"NVarChar(100) NOT NULL", CanBeNull=false)]
		public string HistoryName
		{
			get { return _HistoryName; }
			set {
				if (_HistoryName != value) {
					OnHistoryNameChanging(value);
					SendPropertyChanging();
					_HistoryName = value;
					SendPropertyChanged("HistoryName");
					OnHistoryNameChanged();
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
		private EntityRef<EDocument> _SynologenOpqDocument;
		[Association(Name=@"SynologenOpqDocument_SynologenOpqDocumentHistory", Storage=@"_SynologenOpqDocument", ThisKey=@"Id", OtherKey=@"Id", IsForeignKey=true)]
		public EDocument Document
		{
			get {
				return _SynologenOpqDocument.Entity;
			}
			set {
				EDocument previousValue = _SynologenOpqDocument.Entity;
				if ((previousValue != value) || (!_SynologenOpqDocument.HasLoadedOrAssignedValue)) {
					SendPropertyChanging();
					if (previousValue != null) {
						_SynologenOpqDocument.Entity = null;
						previousValue.DocumentHistories.Remove(this);
					}
					_SynologenOpqDocument.Entity = value;
					if (value != null) {
						value.DocumentHistories.Add(this);
						_Id = value.Id;
					}
					else {
						_Id = default(int);
					}
					SendPropertyChanged("Document");
				}
			}
		}

		#endregion

		#region Converters
		
		/// <summary>
		/// Converts from EDocumentHistory to DocumentHistory.
		/// </summary>
		/// <param name="eDocumentHistory">The EDocumentHistory.</param>
		/// <returns>The converted DocumentHistory.</returns>

		public static DocumentHistory Convert (EDocumentHistory eDocumentHistory)
		{
			DocumentHistory documentHistory = new DocumentHistory
			{
				Id = eDocumentHistory.Id,
				HistoryDate = eDocumentHistory.HistoryDate,
				HistoryId = eDocumentHistory.HistoryId,
				HistoryName = eDocumentHistory.HistoryName,
				NdeId = eDocumentHistory.NdeId,
				ShpId = eDocumentHistory.ShpId,
				CncId = eDocumentHistory.CncId,
				DocTpeId = (DocumentTypes) eDocumentHistory.DocTpeId,
				DocumentContent = eDocumentHistory.DocumentContent,
				IsActive = eDocumentHistory.IsActive,
				CreatedById = eDocumentHistory.CreatedById,
				CreatedByName = eDocumentHistory.CreatedByName,
				CreatedDate = eDocumentHistory.CreatedDate,
				ChangedById = eDocumentHistory.ChangedById,
				ChangedByName = eDocumentHistory.ChangedByName,
				ChangedDate = eDocumentHistory.ChangedDate,
				ApprovedById = eDocumentHistory.ApprovedById,
				ApprovedByName = eDocumentHistory.ApprovedByName,
				ApprovedDate = eDocumentHistory.ApprovedDate,
				LockedById = eDocumentHistory.LockedById,
				LockedByName = eDocumentHistory.LockedByName,
				LockedDate = eDocumentHistory.LockedDate,
			};
			
			return documentHistory;
		}
		
		/// <summary>
		/// Converts from DocumentHistory to EDocumentHistory.
		/// </summary>
		/// <param name="documentHistory">The DocumentHistory.</param>
		/// <returns>The converted EDocumentHistory.</returns>

		public static EDocumentHistory Convert (DocumentHistory documentHistory)
		{		
			return new EDocumentHistory
			{
				Id = documentHistory.Id,
				HistoryDate = documentHistory.HistoryDate,
				HistoryId = documentHistory.HistoryId,
				HistoryName = documentHistory.HistoryName,
				NdeId = documentHistory.NdeId,
				ShpId = documentHistory.ShpId,
				CncId = documentHistory.CncId,
				DocTpeId = (int) documentHistory.DocTpeId,
				DocumentContent = documentHistory.DocumentContent,
				IsActive = documentHistory.IsActive,
				CreatedById = documentHistory.CreatedById,
				CreatedByName = documentHistory.CreatedByName,
				CreatedDate = documentHistory.CreatedDate,
				ChangedById = documentHistory.ChangedById,
				ChangedByName = documentHistory.ChangedByName,
				ChangedDate = documentHistory.ChangedDate,
				ApprovedById = documentHistory.ApprovedById,
				ApprovedByName = documentHistory.ApprovedByName,
				ApprovedDate = documentHistory.ApprovedDate,
				LockedById = documentHistory.LockedById,
				LockedByName = documentHistory.LockedByName,
				LockedDate = documentHistory.LockedDate,
			};
		}

		#endregion
	}
}
