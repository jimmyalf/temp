using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spinit.Wpc.Synologen.OPQ.Core
{
	/// <summary>
	/// The document types.
	/// Implements table tblSynologenOPQDocumentTypes.
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
	/// The file categories.
	/// Implements table tblSynologenOPQFileCategories.
	/// </summary>
	public enum FileCategories
	{
		/// <summary>
		/// No file category.
		/// </summary>
		None = 0,
		/// <summary>
		/// A template.
		/// </summary>
		Templete = 1,
		/// <summary>
		/// A protocol.
		/// </summary>
		Protocol = 2,
		/// <summary>
		/// A other document.
		/// </summary>
		Other = 3,
	}
}
