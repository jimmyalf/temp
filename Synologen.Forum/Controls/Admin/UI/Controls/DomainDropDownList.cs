using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;
using System.Collections;

namespace Spinit.Wpc.Forum.Controls {
    
    public class DomainDropDownList : DropDownList {

        public DomainDropDownList() {
            ArrayList applications = SiteSettings.AllSiteSettings();

            foreach (SiteSettings settings in applications) {
                Items.Add(new ListItem(settings.SiteDomain, settings.SiteID.ToString()));
            }

            if (Items.FindByText(ForumContext.GetApplicationName()) == null) {
                Items.Add(new ListItem(ForumContext.GetApplicationName(), "0"));
                Items.FindByValue("0").Selected = true;
            } else {
                Items.FindByText(ForumContext.GetApplicationName()).Selected = true;
            }
        }

        public int SelectedSiteID {
            get {
                return int.Parse(base.SelectedValue);
            }
        }

        public string SelectedDomain {
            get {
                return base.SelectedItem.Text;
            }
        }

    }
}
