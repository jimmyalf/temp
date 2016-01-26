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

namespace Spinit.Wpc.Base.Presentation.Common {
    public partial class Copyright : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            if (!String.IsNullOrEmpty(Spinit.Wpc.Base.Business.Globals.CopyrightInfo))
                litCopyright.Text = Spinit.Wpc.Base.Business.Globals.CopyrightInfo;
        }
    }
}