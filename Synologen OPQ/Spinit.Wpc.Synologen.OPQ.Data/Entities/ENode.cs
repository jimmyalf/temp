#pragma warning disable 1591

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Spinit's LINQ to SQL template for T4 C#
//     Generated at 10/27/2009 10:18:45
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
	[Table(Name=@"dbo.SynologenOpqNodes")]
	public partial class ENode : EntityBase
	{
		#region Spinit search extension
		
		/// <summary>
		/// Creates a lambda-expression for use with the data-load-option feature,
		/// </summary>
		/// <param name="property">The property to search-for.</param>
		/// <returns>A lambda-expression.</returns>

		public override LambdaExpression BuildSearchExpression (string property)
		{
			ParameterExpression parameter = Expression.Parameter (GetType (), "eNode");
			return Expression.Lambda<Func<ENode, object>> (
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
		public ENode()
		{
			_SynologenOpqNodeSupplierConnections = new EntitySet<ENodeSupplierConnection>(AttachNodeSupplierConnections, DetachNodeSupplierConnections);
			_SynologenOpqDocuments = new EntitySet<EDocument>(AttachDocuments, DetachDocuments);
			_SynologenOpqFiles = new EntitySet<EFile>(AttachFiles, DetachFiles);
			_SynologenOpqNodes = new EntitySet<ENode>(AttachNodes, DetachNodes);
			_SynologenOpqNode1 = default(EntityRef<ENode>); 
			_tblBaseUser = default(EntityRef<EBaseUser>); 
			_tblBaseUser1 = default(EntityRef<EBaseUser>); 
			_tblBaseUser2 = default(EntityRef<EBaseUser>); 
			_tblBaseUser3 = default(EntityRef<EBaseUser>); 
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
		
		partial void OnParentChanging(int? value);
		partial void OnParentChanged();
		private int? _Parent;
		[Column(Storage=@"_Parent", DbType=@"Int")]
		public int? Parent
		{
			get { return _Parent; }
			set {
				if (_Parent != value) {
					if (_SynologenOpqNode1.HasLoadedOrAssignedValue) {
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
					OnParentChanging(value);
					SendPropertyChanging();
					_Parent = value;
					SendPropertyChanged("Parent");
					OnParentChanged();
				}
			}
		}
		
		partial void OnOrderChanging(int? value);
		partial void OnOrderChanged();
		private int? _Order;
		[Column (Storage = @"_Order", Name = @"[Order]", AutoSync = AutoSync.OnInsert, DbType = @"Int")]
		public int? Order
		{
			get { return _Order; }
			set {
				if (_Order != value) {
					OnOrderChanging(value);
					SendPropertyChanging();
					_Order = value;
					SendPropertyChanged("Order");
					OnOrderChanged();
				}
			}
		}
		
		partial void OnNameChanging(string value);
		partial void OnNameChanged();
		private string _Name;
		[Column(Storage=@"_Name", DbType=@"NVarChar(512) NOT NULL", CanBeNull=false)]
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
					if (_tblBaseUser.HasLoadedOrAssignedValue) {
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
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
					if (_tblBaseUser1.HasLoadedOrAssignedValue) {
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
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
					if (_tblBaseUser2.HasLoadedOrAssignedValue) {
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
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
					if (_tblBaseUser3.HasLoadedOrAssignedValue) {
						throw new ForeignKeyReferenceAlreadyHasValueException();
					}
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
		private EntitySet<ENodeSupplierConnection> _SynologenOpqNodeSupplierConnections;
		[Association(Name=@"SynologenOpqNode_SynologenOpqNodeSupplierConnection", Storage=@"_SynologenOpqNodeSupplierConnections", ThisKey=@"Id", OtherKey=@"NdeId")]
		public EntitySet<ENodeSupplierConnection> NodeSupplierConnections
		{
			get {
				return _SynologenOpqNodeSupplierConnections;
			}
			set {
				_SynologenOpqNodeSupplierConnections.Assign(value);
			}
		}

		private void AttachNodeSupplierConnections(ENodeSupplierConnection entity)
		{
			SendPropertyChanging();
			entity.Node = this;
		}
		
		private void DetachNodeSupplierConnections(ENodeSupplierConnection entity)
		{
			SendPropertyChanging();
			entity.Node = null;
		}
		private EntitySet<EDocument> _SynologenOpqDocuments;
		[Association(Name=@"SynologenOpqNode_SynologenOpqDocument", Storage=@"_SynologenOpqDocuments", ThisKey=@"Id", OtherKey=@"NdeId")]
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
			entity.Node = this;
		}
		
		private void DetachDocuments(EDocument entity)
		{
			SendPropertyChanging();
			entity.Node = null;
		}
		private EntitySet<EFile> _SynologenOpqFiles;
		[Association(Name=@"SynologenOpqNode_SynologenOpqFile", Storage=@"_SynologenOpqFiles", ThisKey=@"Id", OtherKey=@"NdeId")]
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
			entity.Node = this;
		}
		
		private void DetachFiles(EFile entity)
		{
			SendPropertyChanging();
			entity.Node = null;
		}
		private EntitySet<ENode> _SynologenOpqNodes;
		[Association(Name=@"SynologenOpqNode_SynologenOpqNode", Storage=@"_SynologenOpqNodes", ThisKey=@"Id", OtherKey=@"Parent")]
		public EntitySet<ENode> Childs
		{
			get {
				return _SynologenOpqNodes;
			}
			set {
				_SynologenOpqNodes.Assign(value);
			}
		}

		private void AttachNodes(ENode entity)
		{
			SendPropertyChanging();
			entity.ParentNode = this;
		}
		
		private void DetachNodes(ENode entity)
		{
			SendPropertyChanging();
			entity.ParentNode = null;
		}
		private EntityRef<ENode> _SynologenOpqNode1;
		[Association(Name=@"SynologenOpqNode_SynologenOpqNode", Storage=@"_SynologenOpqNode1", ThisKey=@"Parent", OtherKey=@"Id", IsForeignKey=true)]
		public ENode ParentNode
		{
			get {
				return _SynologenOpqNode1.Entity;
			}
			set {
				ENode previousValue = _SynologenOpqNode1.Entity;
				if ((previousValue != value) || (!_SynologenOpqNode1.HasLoadedOrAssignedValue)) {
					SendPropertyChanging();
					if (previousValue != null) {
						_SynologenOpqNode1.Entity = null;
						previousValue.Childs.Remove(this);
					}
					_SynologenOpqNode1.Entity = value;
					if (value != null) {
						value.Childs.Add(this);
						_Parent = value.Id;
					}
					else {
						_Parent = default(int?);
					}
					SendPropertyChanged("ParentNode");
				}
			}
		}

		private EntityRef<EBaseUser> _tblBaseUser;
		[Association(Name=@"tblBaseUser_SynologenOpqNode", Storage=@"_tblBaseUser", ThisKey=@"CreatedById", OtherKey=@"Id", IsForeignKey=true)]
		public EBaseUser CreatedBy
		{
			get {
				return _tblBaseUser.Entity;
			}
			set {
				EBaseUser previousValue = _tblBaseUser.Entity;
				if ((previousValue != value) || (!_tblBaseUser.HasLoadedOrAssignedValue)) {
					SendPropertyChanging();
					if (previousValue != null) {
						_tblBaseUser.Entity = null;
						previousValue.CreatedByNodes.Remove(this);
					}
					_tblBaseUser.Entity = value;
					if (value != null) {
						value.CreatedByNodes.Add(this);
						_CreatedById = value.Id;
					}
					else {
						_CreatedById = default(int);
					}
					SendPropertyChanged("CreatedBy");
				}
			}
		}

		private EntityRef<EBaseUser> _tblBaseUser1;
		[Association(Name=@"tblBaseUser_SynologenOpqNode1", Storage=@"_tblBaseUser1", ThisKey=@"ChangedById", OtherKey=@"Id", IsForeignKey=true)]
		public EBaseUser ChangedBy
		{
			get {
				return _tblBaseUser1.Entity;
			}
			set {
				EBaseUser previousValue = _tblBaseUser1.Entity;
				if ((previousValue != value) || (!_tblBaseUser1.HasLoadedOrAssignedValue)) {
					SendPropertyChanging();
					if (previousValue != null) {
						_tblBaseUser1.Entity = null;
						previousValue.ChangedByNodes.Remove(this);
					}
					_tblBaseUser1.Entity = value;
					if (value != null) {
						value.ChangedByNodes.Add(this);
						_ChangedById = value.Id;
					}
					else {
						_ChangedById = default(int?);
					}
					SendPropertyChanged("ChangedBy");
				}
			}
		}

		private EntityRef<EBaseUser> _tblBaseUser2;
		[Association(Name=@"tblBaseUser_SynologenOpqNode2", Storage=@"_tblBaseUser2", ThisKey=@"ApprovedById", OtherKey=@"Id", IsForeignKey=true)]
		public EBaseUser ApprovedBy
		{
			get {
				return _tblBaseUser2.Entity;
			}
			set {
				EBaseUser previousValue = _tblBaseUser2.Entity;
				if ((previousValue != value) || (!_tblBaseUser2.HasLoadedOrAssignedValue)) {
					SendPropertyChanging();
					if (previousValue != null) {
						_tblBaseUser2.Entity = null;
						previousValue.ApprovedByNodes.Remove(this);
					}
					_tblBaseUser2.Entity = value;
					if (value != null) {
						value.ApprovedByNodes.Add(this);
						_ApprovedById = value.Id;
					}
					else {
						_ApprovedById = default(int?);
					}
					SendPropertyChanged("ApprovedBy");
				}
			}
		}

		private EntityRef<EBaseUser> _tblBaseUser3;
		[Association(Name=@"tblBaseUser_SynologenOpqNode3", Storage=@"_tblBaseUser3", ThisKey=@"LockedById", OtherKey=@"Id", IsForeignKey=true)]
		public EBaseUser LockedBy
		{
			get {
				return _tblBaseUser3.Entity;
			}
			set {
				EBaseUser previousValue = _tblBaseUser3.Entity;
				if ((previousValue != value) || (!_tblBaseUser3.HasLoadedOrAssignedValue)) {
					SendPropertyChanging();
					if (previousValue != null) {
						_tblBaseUser3.Entity = null;
						previousValue.LockedByNodes.Remove(this);
					}
					_tblBaseUser3.Entity = value;
					if (value != null) {
						value.LockedByNodes.Add(this);
						_LockedById = value.Id;
					}
					else {
						_LockedById = default(int?);
					}
					SendPropertyChanged("LockedBy");
				}
			}
		}

		#endregion

		#region Converters
		
		/// <summary>
		/// Converts from ENode to Node.
		/// </summary>
		/// <param name="eNode">The ENode.</param>
		/// <returns>The converted Node.</returns>

		public static Node Convert (ENode eNode)
		{
			Node node = new Node
			{
				Id = eNode.Id,
				Parent = eNode.Parent,
				Order = eNode.Order,
				Name = eNode.Name,
				IsActive = eNode.IsActive,
				CreatedById = eNode.CreatedById,
				CreatedByName = eNode.CreatedByName,
				CreatedDate = eNode.CreatedDate,
				ChangedById = eNode.ChangedById,
				ChangedByName = eNode.ChangedByName,
				ChangedDate = eNode.ChangedDate,
				ApprovedById = eNode.ApprovedById,
				ApprovedByName = eNode.ApprovedByName,
				ApprovedDate = eNode.ApprovedDate,
				LockedById = eNode.LockedById,
				LockedByName = eNode.LockedByName,
				LockedDate = eNode.LockedDate,
			};
			
			return node;
		}
		
		/// <summary>
		/// Converts from Node to ENode.
		/// </summary>
		/// <param name="node">The Node.</param>
		/// <returns>The converted ENode.</returns>

		public static ENode Convert (Node node)
		{		
			return new ENode
			{
				Id = node.Id,
				Parent = node.Parent,
				Order = node.Order,
				Name = node.Name,
				IsActive = node.IsActive,
				CreatedById = node.CreatedById,
				CreatedByName = node.CreatedByName,
				CreatedDate = node.CreatedDate,
				ChangedById = node.ChangedById,
				ChangedByName = node.ChangedByName,
				ChangedDate = node.ChangedDate,
				ApprovedById = node.ApprovedById,
				ApprovedByName = node.ApprovedByName,
				ApprovedDate = node.ApprovedDate,
				LockedById = node.LockedById,
				LockedByName = node.LockedByName,
				LockedDate = node.LockedDate,
			};
		}

		#endregion
	}
}
#pragma warning restore 1591
