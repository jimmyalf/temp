using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.Presentation
{
    public partial class SynologenMain : System.Web.UI.MasterPage
    {
        // Internal objects
        SmartMenu smartMenu = new SmartMenu();
        SmartMenu.Menu synologenMenu;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Fires when the page is initiated.
        /// </summary>
        /// <param Name="sender">The sending object.</param>
        /// <param Name="e">The event-arguments.</param>

        protected void Page_Init(object sender, EventArgs e)
        {
            string baseUrl = Utility.Business.Util.ApplicationPath + Business.Globals.ComponentApplicationPath;

            Literal link = new Literal();
            link.Text = String.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Css/ComponentSpecific.css\" media=\"screen,projection\" />", baseUrl);
            link.Text += String.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Css/PageSpecific.css\" media=\"screen,projection\" />", baseUrl);
			link.Text += String.Format("<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}Css/ComponentPrint.css\" media=\"print\" />", baseUrl);
            Page.Header.Controls.Add(link);
            
            synologenMenu = smartMenu.Get("SynologenMenu");

            if (!Page.IsPostBack && (Session["SelectItemByUrl"] == null|| ((bool)Session["SelectItemByUrl"]))){
                if (synologenMenu != null)
					synologenMenu.SelectItemByUrl(Request.Url.LocalPath,true);
            }
            else{
                Session["SelectItemByUrl"] = true;
            }

            if (synologenMenu != null){
                smartMenu.Render(synologenMenu, phSynologenMenu);
            }
            else{
				CreateSynologenMenu();
            }
        }


        /// <summary>
        /// Creates the News menu
        /// </summary>

        protected void CreateSynologenMenu()
        {
            synologenMenu = smartMenu.Create("synologenMenu");

            synologenMenu.ControlType = "ul";
            synologenMenu.ItemControlType = "li";
            synologenMenu.ItemWrapperElement = "span";
            synologenMenu.ItemSelectedCssClass = "Selected";

            SmartMenu.ItemCollection itemCollection = new SmartMenu.ItemCollection();

            itemCollection.AddItem("Medlemmar", null, "Medlemmar", "Lista Medlemmar", null,ComponentPages.Index, null, null, true, true);
			//itemCollection.AddItem("Kategori", null, "Kategorier", "Lista Kategorier", null,ComponentPages.Category, null, null, false, true);
			itemCollection.AddItem("Butik", null, "Butiker", "Lista Butiker", null, ComponentPages.Shops , null, null, false, true);
			itemCollection.AddItem("Artikel", null, "Artiklar", "Lista Artiklar", null,ComponentPages.Articles, null, null, false, true);
			itemCollection.AddItem("Avtal", null, "Avtal", "Lista Avtal", null,ComponentPages.Contracts, null, null, false, true);
			itemCollection.AddItem("Fakturor", null, "Fakturor", "Lista Fakturor", null, ComponentPages.Orders, null, null, false, true);
			itemCollection.AddItem("Utbetalningar", null, "Utbetalningar", "Lista/Skapa utbetalningar", null, ComponentPages.Settlements, null, null, false, true);
			itemCollection.AddItem("opq", null, "OP-Q", "Administrera OP-Q", null, ComponentPages.OpqIndex, null, null, false, true);
			itemCollection.AddItem("frameOrders", null, "Bågbeställning", "Administrera Bågbeställningar", null, "/components/synologen/frames", null, null, false, true);

            synologenMenu.MenuItems = itemCollection;
            smartMenu.Update(synologenMenu);

            synologenMenu.SelectItemByUrl(Request.Url.LocalPath, true);
            smartMenu.Render(synologenMenu, phSynologenMenu);
        }

        /// <summary>
        /// Renders the submenu. Called from child pages.
        /// </summary>
        /// <param Name="deleteEventHandler">The delete EventHandler. Null if delete menu should be hidden.</param>

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
			subMenu.SelectItemByUrl(Request.Url.LocalPath, true);
            smartMenu.Render(subMenu, phSynologenSubMenu);
        }

        public SmartMenu SynologenSmartMenu
        {
            get { return smartMenu; }
        }

        public PlaceHolder SubMenu
        {
            get { return phSynologenSubMenu; }
        }
    }
}
