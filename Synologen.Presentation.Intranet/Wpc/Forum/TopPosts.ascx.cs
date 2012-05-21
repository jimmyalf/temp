using System;
using System.Data;
using Spinit.Wpc.Forum.Components;
using System.Text.RegularExpressions;

namespace Spinit.Wpc.Forum.Presentation.Site
{
    public partial class TopPosts : System.Web.UI.UserControl
    {
        private int _max = 3;
        private int _maxlength = 20;
    	private bool _checkUserPermissions;

    	protected void Page_Load(object sender, EventArgs e)
        {
           
            Populate();
        }

        private void Populate()
        {
            // Create Instance of the ForumsDataProvider
            ForumsDataProvider dp = ForumsDataProvider.Instance();
        	DataSet ds;
			if(CheckUserPermissions) {
				string userName = Users.GetLoggedOnUsername();
				ds = dp.GetTop25NewPosts(userName);
			}
			else {
				ds = dp.GetTop25NewPosts();
			}
            
            DataTable dtPosts = ds.Tables[0];

        	Regex r = new Regex("<[^>]*>");
            foreach (DataRow tblRow in dtPosts.Rows)
            {
                string body = tblRow["Body"].ToString();
                string newbody = r.Replace(body, "");
                if (newbody.Length > _maxlength)
                    newbody = newbody.Substring(0, _maxlength) + "...";
                tblRow["Body"] = newbody;
            }

            dlPosts.DataSource = dtPosts;
            dlPosts.DataBind();
        }

        #region properties

        public int Max
        {
            set { _max = value; }
            get { return _max;  }

        }

        public int Maxlength
        {
            set { _maxlength = value; }
            get { return _maxlength; }

        }

		public bool CheckUserPermissions {
			set { _checkUserPermissions = value; }
			get { return _checkUserPermissions; }

		}

        #endregion
    
    }
}