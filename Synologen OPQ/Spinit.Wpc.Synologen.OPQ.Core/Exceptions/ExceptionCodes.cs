namespace Spinit.Wpc.Synologen.Opq.Core.Exceptions
{
	/// <summary>
	/// The user-errors.
	/// </summary>

	public enum UserErrors
	{
		/// <summary>
		/// No errror.
		/// </summary>

		None = 0,
		
		/// <summary>
		/// The current user does not exist.
		/// </summary>

		NoCurrentExist = 1,
	}

	/// <summary>
	/// The node-errors.
	/// </summary>

	public enum NodeErrors
	{
		/// <summary>
		/// No error.
		/// </summary>

		None = 0,

		/// <summary>
		/// The parent does not exist.
		/// </summary>

		ParentDoesNotExist = 1,

		/// <summary>
		/// The move opertion does not have a new destination.
		/// </summary>

		PositionNotMoved = 2,

		/// <summary>
		/// The move-to position is forbidden.
		/// </summary>

		MoveToForbidden = 3,

		/// <summary>
		/// The name exist on this level.
		/// </summary>

		NameExist = 4,

		/// <summary>
		/// The node is locked by another user.
		/// </summary>

		NodeLockedByOtherUser = 5
	}

	/// <summary>
	/// The document-errors.
	/// </summary>

	public enum DocumentErrors
	{
		/// <summary>
		/// No error.
		/// </summary>

		None = 0,

		/// <summary>
		/// The node is locked by another user.
		/// </summary>

		DocumentLockedByOtherUser = 1,

		/// <summary>
		/// Changing document-type-not allowed.
		/// </summary>

		ChangeOfDocumentTypeNotAllowed = 2,

		/// <summary>
		/// Changing shop not allowed.
		/// </summary>

		ChangeOfShopNotAllowed = 3,

		/// <summary>
		/// Changing concern not allowed.
		/// </summary>

		ChangeOfConcernNotAllowed = 4,

		/// <summary>
		/// Changing node not allowed.
		/// </summary>

		CangeOfNodeNotAllowed = 5
	}

	/// <summary>
	/// The file-errors.
	/// </summary>

	public enum FileErrors
	{
		/// <summary>
		/// No error.
		/// </summary>

		None = 0,

		/// <summary>
		/// The move opertion does not have a new destination.
		/// </summary>

		PositionNotMoved = 1,

		/// <summary>
		/// The move-to position is forbidden.
		/// </summary>

		MoveToForbidden = 2,

		/// <summary>
		/// The file-category exist.
		/// </summary>

		FileCategoryExist = 3,

		/// <summary>
		/// The file-category is in use.
		/// </summary>

		FileCategoryInUse = 4,
	
		/// <summary>
		/// The node is locked by another user.
		/// </summary>

		FileLockedByOtherUser = 5,

		/// <summary>
		/// Changing shop not allowed.
		/// </summary>

		ChangeOfShopNotAllowed = 6,

		/// <summary>
		/// Changing concern not allowed.
		/// </summary>

		ChangeOfConcernNotAllowed = 7,

		/// <summary>
		/// Changing node not allowed.
		/// </summary>

		CangeOfNodeNotAllowed = 8,

		/// <summary>
		/// Changing file-category not allowed.
		/// </summary>

		ChangeOfFileCategoryNotAllowed = 9
	}

	/// <summary>
	/// The fetch resulting in not found objects exception.
	/// </summary>

	public enum ObjectNotFoundErrors
	{
		/// <summary>
		/// No error.
		/// </summary>

		None = 0,

		/// <summary>
		/// The node is not found.
		/// </summary>

		NodeNotFound = 1,

		/// <summary>
		/// The node-suppliers is not found.
		/// </summary>

		NodeSupplierNotFound = 2,

		/// <summary>
		/// The supplier is not found.
		/// </summary>

		BaseUserNotFound = 3,

		/// <summary>
		/// The document is not found.
		/// </summary>

		DocumentNotFound = 4,

		/// <summary>
		/// The document-history is not found.
		/// </summary>

		DocumentHistoryNotFound = 5,

		/// <summary>
		/// The 
		/// </summary>

		DocumentTypeNotFound = 6,

		/// <summary>
		/// The file is not found.
		/// </summary>

		FileNotFound = 7,

		/// <summary>
		/// The file-category is not found.
		/// </summary>

		FileCategoryNotFound = 8,

		/// <summary>
		/// The document-view is not found.
		/// </summary>

		DocumentViewNotFound = 9,

		/// <summary>
		/// Base-file is not found.
		/// </summary>

		BaseFileNotFound = 10,

		/// <summary>
		/// The shop is not found.
		/// </summary>

		ShopNotFound = 11,

		/// <summary>
		/// The concern is not found.
		/// </summary>

		ConcernNotFound = 12
	}
}