using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Spinit.Wpc.Utility.Business;
using Spinit.Common.Cryptography;

namespace Spinit.Wpc.Forum.Presentation.Site {
    public partial class Forum : System.Web.UI.UserControl {

        private string _secretUsername = "";

        protected void Page_Load(object sender, EventArgs e) {

            try {

                Cryptography crypto
				= new Cryptography("spinit");

                PublicUser wpcContext = PublicUser.Current;
                //CxUser wpcContext = CxUser.Current;
                //int userId = wpcContext.User.Id;

                //_secretUsername = HttpUtility.UrlEncode(crypto.encryptHex("andny"));
                _secretUsername = HttpUtility.UrlEncode(crypto.encryptHex(wpcContext.User.UserName));
                
            }
            catch { }

                //Response.Write(_secretUsername);
                //Response.Write("-------------------");
                //Response.Write(HttpUtility.UrlEncode(_secretUsername));

        }

        public string SecretUsername {
            get { return _secretUsername; }
        }
    }
}