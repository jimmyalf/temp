using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.News.Data;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;
using Spinit.Wpc.Utility.Business;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier 
{
    public partial class AdminMemberNews : SynologenCommonSupplierControl
    {
        private int _max = 5;
        private string _infoPage = "#";

        PublicUser _context = null;
        private int _categoryId = -1;

        protected void Page_Load(object sender, EventArgs e) 
        {
            _context = PublicUser.Current;

            if (Page.IsPostBack)
            {
                return;
            }

            var memberRow = Provider.GetMember(MemberId, LocationId, LanguageId);
            var commonWysiwygPathArray = new[] { GetMemberBaseDirectory(LocationRow, memberRow) };

            txtBody.ImagesPaths = commonWysiwygPathArray;
            txtBody.DocumentsPaths = commonWysiwygPathArray;
            txtBody.FlashPaths = commonWysiwygPathArray;
            txtBody.MediaPaths = commonWysiwygPathArray;
            txtBody.UploadImagesPaths = commonWysiwygPathArray;
            txtBody.UploadDocumentsPaths = commonWysiwygPathArray;
            txtBody.UploadFlashPaths = commonWysiwygPathArray;
            txtBody.UploadMediaPaths = commonWysiwygPathArray;

            PopulateNews();
        }

        private void PopulateNews() 
        {
            if (LocationId <= 0 || LanguageId <= 0)
            {
                return;
            }

            var list = Provider.GetPublicNews(0, LocationId, LanguageId, Member.Business.Globals.MemberNewsCategory, MemberId);
            gvNews.DataSource = list.Tables[0];
            gvNews.DataBind();
        }

        protected void btnSetFilter_Click(object sender, EventArgs e) 
        {
            PopulateNews();
        }

        protected void btnShowAll_Click(object sender, EventArgs e) 
        {
            PopulateNews();
        }

        private void SetUpForEdit() 
        {
            var newsprovider = new SqlProvider(Base.Business.Globals.ConnectionString);

            var newsrow = newsprovider.GetNews(NewsId, -1);

            if (NewsId > 0 && newsrow != null) 
            {
                txtHeading.Text = newsrow.Heading;
                txtSummary.Text = newsrow.Summary;
                if (newsrow.Body != null)
                {
                    txtBody.Html = newsrow.Body;
                }
            }
        }


        protected void btnSave_Click(object sender, EventArgs e) 
        {
            var newsprovider = new SqlProvider(Base.Business.Globals.ConnectionString);

            var row = new NewsRow();
            var action = Enumerations.Action.Create;
            var saveButton = (Button)sender;
            var typeOfSave = (News.Data.Enumerations.SaveType)Enum.Parse(typeof(News.Data.Enumerations.SaveType), saveButton.CommandName);
            if (NewsId > 0) 
            {
                row = newsprovider.GetNews(NewsId, -1);
                action = Enumerations.Action.Update;
                row.EditedBy = _context.User.UserName;
            }
            else 
            {
                row.CreatedBy = _context.User.UserName;
            }

            row.ApprovedBy = _context.User.UserName;
            row.ApprovedDate = DateTime.Now;
            row.LockedBy = null;
            row.LockedDate = DateTime.MinValue;
			row.NewsType = News.Data.Enumerations.NewsType.Internal;
            row.Heading = txtHeading.Text;
            if (!string.IsNullOrEmpty(txtSummary.Text))
            {
                row.Summary = txtSummary.Text;
            }

            row.StartDate = DateTime.Now;

            var body = txtBody.Html;
            var formatedBody = body;
            row.Body = body;
            row.FormatedBody = formatedBody;

            newsprovider.AddUpdateDeleteNews(action, ref row);

            // The news is always connected to member category
            // The news is always connected to current location
            // The news is always connected to current language
            // The news is always connected to current language
            if (action == Enumerations.Action.Create)
            {
                newsprovider.ConnectToCategory(row.Id, Member.Business.Globals.MemberNewsCategory);
                newsprovider.ConnectToLocation(row.Id, LocationId);
                newsprovider.ConnectToLanguage(row.Id, LanguageId);
                Provider.ConnectNewsItem(MemberId, row.Id);
            }

            txtHeading.Text = string.Empty;
            txtSummary.Text = string.Empty;
            txtBody.Html = string.Empty;
            NewsId = -1;

            PopulateNews();
        }


        protected void gvNews_Deleting(object sender, GridViewDeleteEventArgs e) 
        {
            var newsId = (int)gvNews.DataKeys[e.RowIndex].Value;
            var newsprovider = new SqlProvider(Base.Business.Globals.ConnectionString);
            Provider.DisconnectNewsItem(MemberId, newsId);

            var row = new NewsRow { Id = newsId };
            newsprovider.AddUpdateDeleteNews(Enumerations.Action.Delete, ref row);

            PopulateNews();
        }

        protected void gvNews_Editing(object sender, GridViewEditEventArgs e) 
        {
            var index = e.NewEditIndex;
            NewsId = (int)gvNews.DataKeys[index].Value;
            SetUpForEdit();
        }

        /// <summary>
        /// Add delete confirmation
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The event arguments.</param>
        protected void AddConfirmDelete(object sender, EventArgs e) 
        {
            var cc = new ClientConfirmation();
            cc.AddConfirmation(ref sender, "Do you really want to delete the file?");
        }

        protected void gvNews_RowDataBound(object sender, GridViewRowEventArgs e) {  }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public string InfoPage
        {
            get { return _infoPage; }
            set { _infoPage = value; }
        }

        public int NewsId 
        {
            get
            {
                if (ViewState["newsId"] != null)
                    return (int)ViewState["newsId"];
                return -1;
            }

            set 
            {
                ViewState["newsId"] = value;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) 
        {
            txtHeading.Text = string.Empty;
            txtSummary.Text = string.Empty;
            txtBody.Html = string.Empty;
            NewsId = -1;
        }
    }
}
