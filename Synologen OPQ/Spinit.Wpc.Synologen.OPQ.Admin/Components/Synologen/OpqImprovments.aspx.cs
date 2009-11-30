using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Presentation;

namespace Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen
{
	public partial class OpqImprovments : OpqPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				PopulateImprovments();
			}
		}

		#region

		private void PopulateImprovments()
		{
			var bDocument = new BDocument(_context);
			var documents = bDocument.GetDocuments(null, null, null, DocumentTypes.Proposal, null, true, true, false);
			gvImprovments.DataSource = documents;
			gvImprovments.DataBind();
		}

		#endregion

		#region Page events

		protected void gvImprovments_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				var documentId = (int)gvImprovments.DataKeys[e.Row.RowIndex].Value;
				if (documentId <= 0) return;
				var ltRoutine = (Literal)e.Row.FindControl("ltRoutine");
				var ltShopSender = (Literal)e.Row.FindControl("ltShopSender");
				var ltEmail = (Literal)e.Row.FindControl("ltEmail");
				var ltDate = (Literal)e.Row.FindControl("ltDate");
				var bDocument = new BDocument(_context);
				var document = bDocument.GetDocument(documentId, true);
				if (ltRoutine != null)
				{
					string routinePath = OpqUtility.GetNodePathFromId(document.NdeId, "/");
					ltRoutine.Text = routinePath;
				}
				if ((ltShopSender != null) && (document.Shop != null))
				{

					ltShopSender.Text = document.Shop.ShopName;
				}
				if (ltEmail != null)
				{
					if (document.CreatedBy != null)
					{
						const string mailtag = "<a href=\"mailto:{0}?subject={1}&body={2}\">{3}</a>";
						ltEmail.Text = string.Format(mailtag,
						                             document.CreatedBy.Email,
													 HttpUtility.UrlEncode(Configuration.ImprovmentSubject, Encoding.GetEncoding("iso-8859-1")).Replace("+", "%20"),
													 HttpUtility.UrlEncode(document.DocumentContent, Encoding.GetEncoding("iso-8859-1")).Replace("+", "%20"),
						                             document.CreatedBy.Email);
					}
				}
				if (ltDate != null)
				{
					ltDate.Text = document.CreatedDate.ToShortDateString();
				}
			}
		}

		protected void gvImprovments_Editing(object sender, GridViewEditEventArgs e)
		{
		}

		protected void gvImprovments_Deleting(object sender, GridViewDeleteEventArgs e)
		{
		}

		protected void gvImprovments_RowCommand(object sender, GridViewCommandEventArgs e)
		{
		}

		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e)
		{
		}

		#endregion

		#region Helpers

		private string EncodeForMail(string text)
		{
			string sOut = string.Empty;
			foreach (Byte b in Encoding.GetEncoding("iso-8859-1").GetBytes(text))
			{
				sOut += "%" + b.ToString("X");
			}
			return text;
		}

		#endregion
	}
}
