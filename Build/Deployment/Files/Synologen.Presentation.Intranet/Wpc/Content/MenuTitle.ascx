<%@ Control Language="C#" Inherits="Spinit.Wpc.Utility.Business.UserControlCache" %>
<%@Import Namespace="System"%>
<%@Import Namespace="System.Collections"%>
<%@Import Namespace="System.ComponentModel"%>
<%@Import Namespace="System.Data"%>
<%@Import Namespace="System.Drawing"%>
<%@Import Namespace="System.Web"%>
<%@Import Namespace="System.Web.UI.WebControls"%>
<%@Import Namespace="System.Web.UI.HtmlControls"%>
<%@Import Namespace="System.Text"%>
<%@Import Namespace="Spinit.Wpc.Content.Data"%>
<%@Import Namespace="Spinit.Wpc.Base.Data"%>
<%@Import Namespace="Spinit.Wpc.Content.Business"%>
<%@Import Namespace="Spinit.Wpc.Utility.Business"%>
<%@ Import Namespace="System.Collections.Generic"%>
<script language="C#" runat="server">
    
    private enum MenuTitleType
    {
        Page,
        ParentMenu,
        RootMenu
    }

    private string _connectionString = null;
    private string _className = String.Empty;
    private int _pageId = -1;
    private MenuTitleType _titleType = MenuTitleType.Page;

    void Page_Load(object sender, System.EventArgs e)
    {
        _connectionString = Spinit.Wpc.Base.Business.Globals.ConnectionString;
        Tree tree = new Tree(_connectionString);
        Spinit.Wpc.Content.Data.Page pge
                = new Spinit.Wpc.Content.Data.Page(_connectionString);
        try
        {
            if (_pageId == -1)
                _pageId = ((Spinit.Wpc.Content.Presentation.Site.Code.ContentSiteMaster)Page.Master).PageId;
        }
        catch { }
        if (_pageId > 0)
        {
            Label ltMenuTitle = new Label();
            ArrayList parentList = tree.GetTreeUpLst(_pageId);
            TreeRow treeRow = (TreeRow)tree.GetTree(_pageId);
            if (treeRow != null)
            {
                switch (_titleType)
                {
                    case MenuTitleType.Page :                    
                        PageRow pgeRow = (PageRow)pge.GetPage(treeRow.Page);
                        if (pgeRow != null)
                            ltMenuTitle.Text = pgeRow.Name;
                        break;
                    case MenuTitleType.ParentMenu :
                        TreeRow parentRow = (TreeRow) tree.GetTree(treeRow.Parent);
                        if (parentRow != null)
                            ltMenuTitle.Text = parentRow.Name; 
                        break;
                    case MenuTitleType.RootMenu :
                        if (parentList.Count > 1)
                        {
                            ltMenuTitle.Text = ((TreeRow)parentList[1]).Name;
                        }
                        break;
                }
                if ((_className != null) && (_className != String.Empty))
                    ltMenuTitle.CssClass = _className;
                phMenuTitle.Controls.Add(ltMenuTitle);
            }
        }
		if (!this.CacheEnabled) {
			this.InitCache(
				Spinit.Wpc.Content.Business.Globals.CacheSetting.Enabled,
				Spinit.Wpc.Content.Business.Globals.CacheSetting.Duration,
				Spinit.Wpc.Content.Business.Globals.CacheSetting.VaryByParams,
				Spinit.Wpc.Content.Business.Globals.CacheSetting.DatabaseServerName,
				Spinit.Wpc.Content.Business.Globals.CacheSetting.TableTrigger,
				Spinit.Wpc.Content.Business.Globals.CacheSetting.VaryByCustomString);
		}
    }

    /// <summary>
    /// Get/Set the pageid 
    /// </summary>
    public int PageId
    {
        set { _pageId = value; }
    }

    /// <summary>
    /// Get/set the class used for the root ul
    /// </summary>
    public string ClassName
    {
        get { return _className; }
        set { _className = value; }
    }
    
    /// <summary>
    /// Set the Titletype to show
    /// </summary>
    public int TitleType
    {
        set { _titleType = (MenuTitleType) value; }
    }
    
    

</script>
<asp:PlaceHolder ID="phMenuTitle" runat="server"/>
