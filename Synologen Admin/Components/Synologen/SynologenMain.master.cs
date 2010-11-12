using System;
using System.Text;
using System.Web.UI.WebControls;
using Spinit.Wpc.Synologen.Presentation.Code;
using Spinit.Wpc.Utility.Business.SmartMenu;

namespace Spinit.Wpc.Synologen.Presentation.Components.Synologen
{
	public partial class SynologenMain : System.Web.UI.MasterPage
	{
		readonly SmartMenu smartMenu = new SmartMenu();
		SmartMenu.Menu synologenMenu;

		protected void Page_Load(object sender, EventArgs e) { }


		protected void Page_Init(object sender, EventArgs e)
		{
			var baseUrl = Utility.Business.Util.ApplicationPath + Business.Globals.ComponentApplicationPath;
			var styleSheetReferenceFormat = String.Concat("<link rel=\"stylesheet\" type=\"text/css\" href=\"", baseUrl, "Css/{0}\" media=\"{1}\" />");
			var literal = new Literal
			{
				Text = new StringBuilder()
					.AppendLine(String.Format(styleSheetReferenceFormat, "ComponentSpecific.css", "screen,projection"))
					.AppendLine(String.Format(styleSheetReferenceFormat, "PageSpecific.css", "screen,projection"))
					.AppendLine(String.Format(styleSheetReferenceFormat, "ComponentPrint.css", "print"))
					.ToString()
			};

			Page.Header.Controls.Add(literal);
            
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

			var itemCollection = new SmartMenu.ItemCollection();

			itemCollection.AddItem("Medlemmar", null, "Medlemmar", "Lista Medlemmar", null,ComponentPages.Index, null, null, true, true);
			itemCollection.AddItem("Butik", null, "Butiker", "Lista Butiker", null, ComponentPages.Shops , null, null, false, true);
			itemCollection.AddItem("Artikel", null, "Artiklar", "Lista Artiklar", null,ComponentPages.Articles, null, null, false, true);
			itemCollection.AddItem("Avtal", null, "Avtal", "Lista Avtal", null,ComponentPages.Contracts, null, null, false, true);
			itemCollection.AddItem("Fakturor", null, "Fakturor", "Lista Fakturor", null, ComponentPages.Orders, null, null, false, true);
			itemCollection.AddItem("Utbetalningar", null, "Utbetalningar", "Lista/Skapa utbetalningar", null, ComponentPages.Settlements, null, null, false, true);
			itemCollection.AddItem("opq", null, "OP-Q", "Administrera OP-Q", null, ComponentPages.OpqIndex, null, null, false, true);
			itemCollection.AddItem("frameOrders", null, "Bågbeställning", "Administrera Bågbeställningar", null, "/components/synologen/frames", null, null, false, true);
			itemCollection.AddItem("lens-subscriptions", null, "Linsabonnemang", "Administrera Linsabonnemang", null, "/components/synologen/lens-subscriptions", null, null, false, true);

			synologenMenu.MenuItems = itemCollection;
			smartMenu.Update(synologenMenu);

			synologenMenu.SelectItemByUrl(Request.Url.LocalPath, true);
			smartMenu.Render(synologenMenu, phSynologenMenu);
		}

		public void RenderLayoutSubMenu(string deleteEventHandler)
		{
			var subMenu = new SmartMenu.Menu
			{
				ID = "SubMenu",
				ControlType = "ul",
				ItemControlType = "li",
				ItemWrapperElement = "span"
			};

			var itemCollection = new SmartMenu.ItemCollection();
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