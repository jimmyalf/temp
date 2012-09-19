using System;
using Spinit.Wpc.Member.Data;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier 
{
    public partial class AdminMemberPage : SynologenCommonSupplierControl 
	{
        protected void Page_Load(object sender, EventArgs e) 
		{
        	if (Page.IsPostBack) return;
			var memberRow = Provider.GetMember(MemberId, LocationId, LanguageId);
        	var commonWysiwygPathArray = new[] {GetDirectory(LocationRow, memberRow)};

        	txtBody.ImagesPaths = commonWysiwygPathArray;
        	txtBody.DocumentsPaths = commonWysiwygPathArray;
        	txtBody.FlashPaths = commonWysiwygPathArray;
        	txtBody.MediaPaths = commonWysiwygPathArray;
        	txtBody.UploadImagesPaths = commonWysiwygPathArray;
        	txtBody.UploadDocumentsPaths = commonWysiwygPathArray;
        	txtBody.UploadFlashPaths = commonWysiwygPathArray;
        	txtBody.UploadMediaPaths = commonWysiwygPathArray;

        	if (base.MemberId > 0) PopulateMember(memberRow);
		}

		private string GetDirectory(IBaseLocationRow lrow, MemberRow memberRow)
		{
			var di = GetMemberDirectory(LocationRow, memberRow);
    		if (!di.Exists) di.Create();
			var orgName = UrlFriendlyRenamingService.Rename(memberRow.OrgName);
			return String.Format("~{0}{1}/Member/{2}/", Base.Business.Globals.CommonFileUrl, lrow.Name, orgName);
		}

        private void PopulateMember(IMemberRow memberRow) 
		{
        	if (memberRow == null) return;
        	if (memberRow.Body != null) txtBody.Html = memberRow.Body;
        }

        protected void btnSave_Click(object sender, EventArgs e) 
		{
            var wpcContext = PublicUser.Current;
        	if (base.MemberId <= 0) return;
        	var row = Provider.GetMember(base.MemberId, base.LocationId, base.LanguageId);
        	row.EditedBy = wpcContext.User.UserName;
        	row.ApprovedBy = wpcContext.User.UserName;
        	row.ApprovedDate = DateTime.Now;
        	row.LockedBy = null;
        	row.LockedDate = DateTime.MinValue;
        	row.Body = txtBody.Html;
        	row.FormatedBody = ParseLinks.replaceInternalLinks(
				txtBody.Html,
				LocationId,
				wpcContext.Loc,
				wpcContext.Lang,
				wpcContext.Tree);

        	Provider.AddUpdateDeleteMember(Enumerations.Action.Update, base.LanguageId, ref row);
		}
    }
}
