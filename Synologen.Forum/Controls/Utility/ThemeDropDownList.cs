using System;
using System.Web;
using System.IO;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Components;
using Spinit.Wpc.Forum.Configuration;

namespace Spinit.Wpc.Forum.Controls {

    public class ThemeDropDownList : DropDownList {

        public ThemeDropDownList() {

            HttpContext context = ForumContext.Current.Context;

            string[] dirs = Directory.GetDirectories( context.Request.PhysicalApplicationPath + ForumConfiguration.GetConfig().ForumFilesPath + "\\themes" );

            foreach (string s in dirs) {

                DirectoryInfo dirInfo = new DirectoryInfo(s);

				// check to ensure we skip any directories that start
				// with an underscore.
				//
				if (dirInfo.Name.Length > 0) {
					if (dirInfo.Name.Substring(0, 1) != "_") {

						// Add directories
						//
						Items.Add(new ListItem(dirInfo.Name, dirInfo.Name));
					}
				}
            }
        }

        public override string SelectedValue {
            get {
                return base.SelectedValue;
            }
            set {
                if (Items.FindByValue(value) == null)
                    Items.FindByValue("default").Selected = true;
                else
                    base.SelectedValue = value;
            }
        }


    }
}
