namespace Spinit.Wpc.Synologen.OPQ.Core
{
	/// <summary>
	/// The document types.
	/// Implements table SynologenOpqDocumentTypes.
	/// </summary>
	
	public enum DocumentTypes
	{
		/// <summary>
		/// No document type.
		/// </summary>
	
		None = 0,
	
		/// <summary>
		/// A routine.
		/// </summary>
	
		Routine = 1,
	
		/// <summary>
		/// A proposal.
		/// </summary>
	
		Proposal = 2,
	}

	/// <summary>
	/// The file categories
	/// Implements table SynologenOpqFileCategories
	/// </summary>

	public enum FileCategories
	{
		/// <summary>
		/// No document type.
		/// </summary>

		None = 0,

		/// <summary>
		/// The system-routine documents.
		/// </summary>

		SystemRoutineDocuments = 1,

		/// <summary>
		/// The shop-routine documents.
		/// </summary>

		ShopRoutineDocuments = 2,

		/// <summary>
		/// The shop documents.
		/// </summary>

		ShopDocuments = 3
	}
}
