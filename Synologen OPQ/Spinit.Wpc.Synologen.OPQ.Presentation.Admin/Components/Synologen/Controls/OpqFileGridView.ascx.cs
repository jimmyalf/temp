using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen.Controls
{
    public partial class OpqFileGridView : UserControl
    {
        protected void gvFiles_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var opqFile = (File)e.Row.DataItem;
                if (opqFile == null) return;
                var ltFile = (Literal)e.Row.FindControl("ltFile");
                var ltFileDate = (Literal)e.Row.FindControl("ltFileDate");
                if (ltFile != null)
                {
                    if (opqFile.BaseFile != null)
                    {
                        const string tag = "<a href=\"{0}\">{1}</a>";
                        var link = String.Concat(Utility.Business.Globals.FilesUrl, opqFile.BaseFile.Name);
                        var fileName = opqFile.BaseFile.Description.IsNotNullOrEmpty()
                                              ? opqFile.BaseFile.Description.Substring(opqFile.BaseFile.Description.LastIndexOf("/") + 1)
                                              : opqFile.BaseFile.Name.Substring(opqFile.BaseFile.Name.LastIndexOf("/") + 1);
                        ltFile.Text = String.Format(tag, link, fileName);
                    }
                }
                if (ltFileDate != null)
                {
                    ltFileDate.Text = opqFile.CreatedDate.ToShortDateString();
                }
            }
        }

        public string HeaderText
        {
            get { return headerText.Text; }
            set { headerText.Text = value; }
        }

        public void SetDataSource(object dataSource)
        {
            gvFiles.DataSource = null;
            gvFiles.DataSource = dataSource;
            gvFiles.DataBind();
        }
    }
}