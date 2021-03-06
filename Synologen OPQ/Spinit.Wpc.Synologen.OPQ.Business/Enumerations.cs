﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The nodeFill move actions.
	/// </summary>
	public enum NodeMoveActions
	{
		/// <summary>
		/// No action.
		/// </summary>
		None = 0,
		/// <summary>
		/// Moves one position up.
		/// </summary>
		MoveUp = 1,
		/// <summary>
		/// Moves one position down.
		/// </summary>
		MoveDown = 2,
		/// <summary>
		/// Moves to one position after selected
		/// </summary>
		MoveAfter = 3,
		/// <summary>
		/// Moves to first leaf position
		/// </summary>
		MoveInto = 4,
		/// <summary>
		/// Moves to one position before selected
		/// </summary>
		MoveBefore = 5,
	}
	
	/// <summary>
	/// The nodeFill move actions.
	/// </summary>
	public enum FileMoveActions
	{
		/// <summary>
		/// No action.
		/// </summary>
		None = 0,
		/// <summary>
		/// Moves one position up.
		/// </summary>
		MoveUp = 1,
		/// <summary>
		/// Moves one position down.
		/// </summary>
		MoveDown = 2
	}
}
