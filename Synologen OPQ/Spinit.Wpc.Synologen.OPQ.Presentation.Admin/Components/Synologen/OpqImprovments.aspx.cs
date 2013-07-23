using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.OPQ.Business;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Presentation;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen
{
	public partial class OpqImprovments : OpqPage
	{
		private const string MailTag = "<a href=\"mailto:{0}?subject={1}&body={2}\">{3}</a>";
		private const string MailToHref = "mailto:{0}?subject={1}&body={2}";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				PopulateImprovments();
			}
		}

		#region Populaters

		private void PopulateImprovments()
		{
			var bDocument = new BDocument(_context);
			var documents = bDocument.GetDocumentsProposal(true, true, false);
			gvImprovments.DataSource = null;
			gvImprovments.DataSource = documents;
			gvImprovments.DataBind();
		}

		private void PopulateViewer(Document document)
		{
			phImprovment.Visible = true;
			if ((document.ShpId > 0) && (document.Shop != null))
			{
				ltShop.Text = document.Shop.ShopName;
			}
			ltBody.Text = Utility.Business.Util.ParseInputField(document.DocumentContent, true);
			hlSender.NavigateUrl= string.Format(MailToHref,
										 document.CreatedBy.Email,
										 HttpUtility.UrlEncode(Configuration.ImprovmentSubject, Encoding.GetEncoding("iso-8859-1")).Replace("+", "%20"),
										 HttpUtility.UrlEncode(document.DocumentContent, Encoding.GetEncoding("iso-8859-1")).Replace("+", "%20"));
			hlSender.Text = document.CreatedBy.Email;


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
						ltEmail.Text = string.Format(MailTag,
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
			var documentId = (int)gvImprovments.DataKeys[e.NewEditIndex].Value;
			if (documentId <= 0) return;
			var bDocument = new BDocument(_context);
			var document = bDocument.GetDocument(documentId, true);
			phImprovment.Visible = false;
			PopulateViewer(document);

		}

		protected void gvImprovments_Deleting(object sender, GridViewDeleteEventArgs e)
		{
			var documentId = (int)gvImprovments.DataKeys[e.RowIndex].Value;
			if (documentId <= 0) return;
			var bDocument = new BDocument(_context);
			bDocument.DeleteDocument(documentId, true);
			PopulateImprovments();
		}

		/// <summary>
		/// Add delete confirmation
		/// </summary>
		/// <param Name="sender">The sending object.</param>
		/// <param Name="e">The event arguments.</param>
		protected void btnDelete_AddConfirmDelete(object sender, EventArgs e)
		{
			var cc = new ClientConfirmation();
			cc.AddConfirmation(ref sender, GetLocalResourceObject("ImprovmentDeleteConfirmation").ToString());
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
