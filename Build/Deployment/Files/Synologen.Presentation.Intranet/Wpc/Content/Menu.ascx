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
<%@Import Namespace="System.Collections.Generic"%>
<script language="C#" runat="server">
	private string _connectionString = null;
	private bool _showDefaultPage = false;
    private bool _showRootPage = true;
	private int _languageId = 1;
	private int _stopAtLevel = -1;
    private int _startAtLevel = -1;
	private int _locationId = 1;
    private int _pageId = -1;
    private string _className = String.Empty;
    ArrayList _upList = new ArrayList();	
	void Page_Load(object sender, System.EventArgs e) 
	{
		_connectionString = Spinit.Wpc.Base.Business.Globals.ConnectionString;
        Tree tree = new Tree(_connectionString);
        try
        {
            if (_pageId == -1)
                _pageId = ((Spinit.Wpc.Content.Presentation.Site.Code.ContentSiteMaster)Page.Master).PageId;
            _upList = tree.GetTreeUpLst(_pageId);
        }
        catch { }
		HtmlGenericControl menuControl = GetMenu(_pageId, _className);
		if (menuControl != null)
		{
			phMenu.Controls.Add(menuControl);
			if (!this.CacheEnabled)
			{
				this.InitCache(
					Spinit.Wpc.Content.Business.Globals.CacheSetting.Enabled,
					Spinit.Wpc.Content.Business.Globals.CacheSetting.Duration,
					Spinit.Wpc.Content.Business.Globals.CacheSetting.VaryByParams,
					Spinit.Wpc.Content.Business.Globals.CacheSetting.DatabaseServerName,
					Spinit.Wpc.Content.Business.Globals.CacheSetting.TableTrigger,
					Spinit.Wpc.Content.Business.Globals.CacheSetting.VaryByCustomString);
			}
		}
		else
		{
			phMenu.Visible = false;
		}
	}

    /// <summary>
    /// Returns the Control for the Menu
    /// </summary>
    /// <param name="pageId">Current page id</param>
    /// <param name="rootId">The rootId definition if any</param>
    /// <returns>Returns the ul generic control for the menu</returns>
    HtmlGenericControl GetMenu(int pageId, string className)
    {
        HtmlGenericControl ul = new HtmlGenericControl("ul");
        if ((className != null) && (className != String.Empty))
            ul.Attributes.Add("class", className);
        Tree tree = new Tree(_connectionString);
        TreeRow tmpTree = (TreeRow)tree.GetTreeLng(_locationId, _languageId);
        ArrayList rootList = null;
        int level = 0;
        if (_startAtLevel == -1)
            rootList = tree.GetTrees(tmpTree.Id, Tree.TreeTypes.None);
        else
        {
            if (_upList.Count > _startAtLevel)
            {
				rootList = tree.GetTrees(((TreeRow)_upList[_startAtLevel]).Id, Tree.TreeTypes.None);
                level = _startAtLevel;
            }
        }
		if (rootList == null)
		{
			return null;
		}
        AddToItem(ul, rootList, level, pageId);
        return ul;
    }


    /// <summary>
    /// Adds a submenu to the generic control
    /// </summary>
    /// <param name="item">The control to append</param>
    /// <param name="treeList">The list of nodes</param>
    /// <param name="level">The current level</param>
    /// <param name="pageId">The current page Id</param>
    void AddToItem(HtmlGenericControl item, ArrayList treeList, int level, int pageId)
    {
        bool selected = false;
        if ((_stopAtLevel != -1) && (level > _stopAtLevel))
            return;
        if (treeList == null)
            return;
        Tree tree = new Tree(_connectionString);
        Location loc = new Location(_connectionString);
        Language lang = new Language(_connectionString);
		bool isAuthenticated = false;
		try
		{
			isAuthenticated = Spinit.Wpc.Utility.Core.PublicUser.Current.IsAuthenticated();
		}
		catch { }
                
        foreach (Spinit.Wpc.Content.Data.TreeRow treeRow in treeList)
        {
            LocationRow locRow 
                = (LocationRow) loc.GetLocation (treeRow.Location);
			if (treeRow.NeedsAuthentication && !isAuthenticated)
				continue;
			if (!treeRow.HideInMenu)
            {
                selected = IsSelected(pageId, treeRow);
                switch (treeRow.TreeType)
                {
                    case Tree.TreeTypes.DefaultPage:
                        {
                            if (_showDefaultPage || (_showRootPage && IsRootPage(treeRow)))
                            {
                                item.Controls.Add (ConstructListItem
                                                    (treeRow.Name,
                                                     ParseLinks.getUrlPath (treeRow, 
                                                                            loc, 
                                                                            lang, 
                                                                            tree),
                                                     treeRow.Target,
                                                     selected));
                            }
                            break;
                        }
                    case Spinit.Wpc.Content.Data.Tree.TreeTypes.LinkPage:
                    case Tree.TreeTypes.Page: 
                        {
                            item.Controls.Add (ConstructListItem
												(treeRow.Name,
												 ParseLinks.getUrlPath (treeRow,
																		loc,
																		lang,
																		tree),
												 treeRow.Target,
												 selected));
                            break;
                        }
                    case Tree.TreeTypes.Menu:
                        {
                            if (IsTreeRowInUpList(treeRow))
                            {
                                HtmlGenericControl li 
                                    = ConstructListItem (treeRow.Name,
                                                         ParseLinks.getUrlPath (treeRow, 
                                                                                loc, 
                                                                                lang, 
                                                                                tree),
                                                         treeRow.Target,
                                                         selected);
                                ArrayList leafList = tree.GetTrees
                                    (treeRow.Id, Spinit.Wpc.Content.Data.Tree.TreeTypes.None);
                                HtmlGenericControl ul = new HtmlGenericControl("ul");
                                // Check if the menu should be builded == is in the uplist
                                if (_upList.IndexOf(treeRow) != -1)
                                    AddToItem(ul, leafList, level + 1, pageId);
                                if (ul.HasControls())
                                    li.Controls.Add(ul);
                                item.Controls.Add(li);
                            }
                            break;
                        }
                }
            }
        }
    }
    

    /// <summary>
    /// Construct a li item with a HtmlAnchor inside
    /// </summary>
    /// <param name="name">The name of the menu</param>
    /// <param name="link">The link</param>
    /// <param name="target">The target.</param>
    /// <param name="selected">true if the listitem should be selected</param>
    /// <returns>A string with the menupart info</returns>
    HtmlGenericControl ConstructListItem(string name, string link, string target, bool selected)
    {
        HtmlGenericControl li = new HtmlGenericControl("li");
        if (selected)
            li.Attributes.Add("class","selected");
        HtmlAnchor a = new HtmlAnchor();
        a.HRef = link;
        a.Title = name;
        a.InnerText = name;
        if ((target != null) && (target.Length > 0)) {
            a.Target = target;
        }
        li.Controls.AddAt(0, a);
        return li;
    }

    /// <summary>
    /// Examines if a node is selected.
    /// Gets the list for the requested page id and returns true if the treeRow to examine is in the list 
    /// </summary>
    /// <param name="pageId">The page id of the current request</param>
    /// <param name="treeRow">The row to examine</param>
    /// <returns></returns>

    private bool IsSelected(int pageId, Spinit.Wpc.Content.Data.TreeRow treeRow)
    {
        if (pageId > 0)
        {
            Spinit.Wpc.Content.Data.Tree tre
                = new Spinit.Wpc.Content.Data.Tree(_connectionString);
            ArrayList treeList = tre.GetTreeUpLst(pageId);
            if ((treeList != null) && (treeList.IndexOf(treeRow) != -1))
                return true;
        }
        return false;
    }

    /// <summary>
    /// Examines if the current page is the root page
    /// </summary>
    /// <param name="treeRow">The treeRow to examine</param>
    /// <returns>True/False</returns>
    private bool IsRootPage(TreeRow treeRow)
    {
        bool isRoot = false;
        Tree tree = new Tree(_connectionString);
        TreeRow parentRow = (TreeRow) tree.GetTree(treeRow.Parent);
        if ((parentRow.TreeType == Tree.TreeTypes.Language) ||
            (parentRow.TreeType == Tree.TreeTypes.Location))
            isRoot = true;
        return isRoot;
    }

    /// <summary>
    /// Examines if the current row is in the uplist or one of the uplists leafs
    /// </summary>
    /// <param name="treeRow">The row to examine</param>
    /// <returns>True/False if the row is in the list or not</returns>
    private bool IsTreeRowInUpList(TreeRow treeRow)
    {
        bool isInList = false;
        Tree tree = new Tree(_connectionString);
        foreach (TreeRow upRow in _upList)
        {
            ArrayList leafList = tree.GetTrees
                (upRow.Id, Spinit.Wpc.Content.Data.Tree.TreeTypes.None);
            if ((leafList != null) &&(leafList.IndexOf(treeRow) != -1))
            {
                isInList = true;
                break;
            }
        }
        return isInList;
    }

    /// <summary>
    /// Get/set whether to show the default page in the menu or not
    /// </summary>
	public bool ShowDefaultPage
	{
		get { return this._showDefaultPage; }
		set { this._showDefaultPage = value; }
	}
    
    /// <summary>
    /// Get/set whether to show the root page in the menu or not
    /// </summary>
    public bool ShowRootPage
    {
        get { return this._showRootPage; }
        set { this._showRootPage = value; }
    }
    
    /// <summary>
    /// Set the current Language id
    /// </summary>
	public int Language
	{
		set { _languageId = value; }
	}

    /// <summary>
    /// Set the current location id
    /// </summary>
	public int Location
	{
		set { _locationId = value; }
	}

    /// <summary>
    /// Set how many levels should be shown
    /// </summary>
	public int StopAtLevel
	{
		set { _stopAtLevel = value; }

	}

    /// <summary>
    /// Set where to start the menu
    /// </summary>
    public int StartAtLevel
    {
        set { _startAtLevel = value; }

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
    /// Get/Set the pageid 
    /// </summary>
    public int PageId
    {
        set { _pageId = value; }
    }
    
    

</script>
<asp:PlaceHolder ID="phMenu" runat="server"/>
