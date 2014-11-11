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
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Campaign.Presentation
{
    public partial class CampaignMain : System.Web.UI.MasterPage
    {
        // Internal objects
        SmartMenu smartMenu = new SmartMenu();
        SmartMenu.Menu campaignMenu;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Fires when the page is initiated.
        /// </summary>
        /// <param name="sender">The sending object.</param>
        /// <param name="e">The event-arguments.</param>

        protected void Page_Init(object sender, EventArgs e)
        {
            string baseUrl = Spinit.Wpc.Utility.Business.Util.ApplicationPath +
                                Spinit.Wpc.Campaign.Business.Globals.ComponentApplicationPath;

            Literal link = new Literal();
            link.Text = String.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Css/ComponentSpecific.css\" media=\"screen,projection\" />", baseUrl);
            link.Text += String.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Css/PageSpecific.css\" media=\"screen,projection\" />", baseUrl);
            Page.Header.Controls.Add(link);

            campaignMenu = smartMenu.Get("CampaignMenu");

            if (!Page.IsPostBack
                && (Session["SelectItemByUrl"] == null
                || ((bool)Session["SelectItemByUrl"] == true)))
            {
                if (campaignMenu != null)
                    campaignMenu.SelectItemByUrl(Request.Url.PathAndQuery,
                                                true);
            }
            else
            {
                Session["SelectItemByUrl"] = true;
            }

            if (campaignMenu != null)
            {
                smartMenu.Render(campaignMenu, phCampaignMenu);
            }
            else
            {
                CreateCampaignMenu();
            }
        }


        /// <summary>
        /// Creates the Campaign menu
        /// </summary>

        protected void CreateCampaignMenu()
        {
            campaignMenu = smartMenu.Create("campaignMenu");

            campaignMenu.ControlType = "ul";
            campaignMenu.ItemControlType = "li";
            campaignMenu.ItemWrapperElement = "span";
            campaignMenu.ItemSelectedCssClass = "Selected";

            SmartMenu.ItemCollection itemCollection = new SmartMenu.ItemCollection();

            itemCollection.AddItem("List", null, "List", "List Campaigns", null, "~/Components/Campaign/Index.aspx", null, null, true, true);
            itemCollection.AddItem("Category", null, "Category", "List Categories", null, "~/Components/Campaign/Category.aspx", null, null, false, true);
            itemCollection.AddItem("File Category", null, "File Category", "List Filecategories", null, "~/Components/Campaign/FileCategories.aspx", null, null, false, true);
            //            itemCollection.AddItem("Settings", null, "Settings", "Configure News Component", null, "~/Components/News/Settings.aspx", null, null, false, true);

            campaignMenu.MenuItems = itemCollection;
            smartMenu.Update(campaignMenu);

            campaignMenu.SelectItemByUrl(Request.Url.PathAndQuery, true);
            smartMenu.Render(campaignMenu, phCampaignMenu);
        }

        /// <summary>
        /// Renders the submenu. Called from child pages.
        /// </summary>
        /// <param name="deleteEventHandler">The delete EventHandler. Null if delete menu should be hidden.</param>

        public void RenderLayoutSubMenu(string deleteEventHandler)
        {
            SmartMenu.Menu subMenu = new SmartMenu.Menu();
            subMenu.ID = "SubMenu";
            subMenu.ControlType = "ul";
            subMenu.ItemControlType = "li";
            subMenu.ItemWrapperElement = "span";

            SmartMenu.ItemCollection itemCollection = new SmartMenu.ItemCollection();
            itemCollection.AddItem("New", null, "New", "Create new template", null, "btnNew_Click", false, null);
            itemCollection.AddItem("Upload", null, "Upload", "Upload new template", null, "btnUpload_Click", false, null);

            if (!String.IsNullOrEmpty(deleteEventHandler))
                itemCollection.AddItem("Delete", null, "Delete", "Delete templates", null, deleteEventHandler, false, null);

            subMenu.MenuItems = itemCollection;
            smartMenu.Render(subMenu, phCampaignSubMenu);
        }

        public SmartMenu CampaignSmartMenu
        {
            get { return smartMenu; }
        }

        public PlaceHolder SubMenu
        {
            get { return phCampaignSubMenu; }
        }
    }
}
