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

		MoveToForbidden = 3
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

		UserNotFound = 3,

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

		FileCategoryNotFound = 8
	}
}