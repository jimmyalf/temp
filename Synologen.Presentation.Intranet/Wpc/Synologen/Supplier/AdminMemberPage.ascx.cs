using System;
using System.IO;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier 
{
    public partial class AdminMemberPage : SynologenUserControl 
	{
        protected void Page_Load(object sender, EventArgs e) 
		{
        	if (Page.IsPostBack) return;
        	var location = new Location(Base.Business.Globals.ConnectionString);
        	var lrow = (LocationRow)location.GetLocation(base.LocationId);
        	//var commonWysiwygPathArray = new[] {String.Format("~{0}{1}/Member/{2}/", Base.Business.Globals.CommonFileUrl, lrow.Name, base.MemberId)};
        	var commonWysiwygPathArray = new[] {GetDirectory(lrow)};

        	txtBody.ImagesPaths = commonWysiwygPathArray;
        	txtBody.DocumentsPaths = commonWysiwygPathArray;
        	txtBody.FlashPaths = commonWysiwygPathArray;
        	txtBody.MediaPaths = commonWysiwygPathArray;
        	txtBody.UploadImagesPaths = commonWysiwygPathArray;
        	txtBody.UploadDocumentsPaths = commonWysiwygPathArray;
        	txtBody.UploadFlashPaths = commonWysiwygPathArray;
        	txtBody.UploadMediaPaths = commonWysiwygPathArray;

        	if (base.MemberId > 0) PopulateMember();
		}

		private string GetDirectory(IBaseLocationRow lrow)
		{
			var memberRow = Provider.GetMember(MemberId, LocationId, LanguageId);
			var di = new DirectoryInfo(Base.Business.Globals.CommonFilePath + lrow.Name + "\\Member\\" + memberRow.OrgName + "\\");
    		if (!di.Exists) di.Create();
			return String.Format("~{0}{1}/Member/{2}/", Base.Business.Globals.CommonFileUrl, lrow.Name, memberRow.OrgName);
		}

        private void PopulateMember() 
		{
            var row = Provider.GetMember(base.MemberId, base.LocationId, base.LanguageId);
        	if (row == null) return;
        	if (row.Body != null) txtBody.Html = row.Body;
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
