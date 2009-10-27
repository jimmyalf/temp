using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;
using Spinit.Wpc.Synologen.OPQ.Data.Entities;

namespace Spinit.Wpc.Synologen.Opq.Data.Managers
{
	public class NodeManager : EntityManager<WpcSynologenRepository>
	{
		private readonly WpcSynologenDataContext _dataContext;

		private Node _insertedNode;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="manager">The repository-manager.</param>

		public NodeManager (WpcSynologenRepository manager) : base (manager)
		{
			_dataContext = (WpcSynologenDataContext) Manager.Context;
		}

		#region Create

		/// <summary>
		/// Inserts a node.
		/// </summary>
		/// <param name="node">The node.</param>

		private void Insert (ENode node)
		{
			node.CreatedById = Manager.WebContext.UserId ?? 0;
			node.CreatedByName = Manager.WebContext.UserName;
			node.CreatedDate = DateTime.Now;

			if ((node.CreatedById == 0) || (node.CreatedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			_dataContext.SynologenOpqNodes.InsertOnSubmit (node);
		}

		/// <summary>
		/// Inserts a node.
		/// </summary>
		/// <param name="node">The node.</param>

		public void Insert (Node node)
		{
			Insert (ENode.Convert (node));
		}

		#endregion
	}
}
