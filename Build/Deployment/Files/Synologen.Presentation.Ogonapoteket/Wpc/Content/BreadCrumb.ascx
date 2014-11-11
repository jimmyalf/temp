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
<SCRIPT language="C#" runat="server">
    
    private string _connectionString = null;
    private int _pageId = 0;
    private bool _showLanguageLevel = false;
    private bool _showPage = false;
    private bool _showDefaultPage = false;
    private bool _linkPage = true;
    private string _target = null;
    private string _rootName = "Home";
    private string _separator = @"&nbsp;/&nbsp;";
    private string _className = null;
    private int _maxLength = -1;

    void Page_Load(object sender, System.EventArgs e)
    {
        _connectionString = Spinit.Wpc.Base.Business.Globals.ConnectionString;
        try
        {
            _pageId = ((Spinit.Wpc.Content.Presentation.Site.Code.ContentSiteMaster)Page.Master).PageId;
            List<HtmlAnchor> breadCrumbList = getBreadCrumb(_connectionString,
                                               _pageId,
                                               ClassName,
                                               Target,
                                               ShowPage,
                                               ShowLanguageLevel,
                                               ShowDefaultPage,
                                               RootName);

            // If the crumb is too long remove the first element until the crumb is ok
            bool itemRemoved = false;
            if (MaxLength != -1)
            {
                while (CalculateLength(breadCrumbList, Separator) > MaxLength)
                {
                    if (breadCrumbList.Count > 0)
                    {
                        breadCrumbList.RemoveAt(0);
                        itemRemoved = true;
                    }
                    else
                        break;
                }
            }
            foreach (HtmlAnchor anchor in breadCrumbList)
            {
                if (itemRemoved && (anchor == breadCrumbList[0]))
                {
                    Literal tooLongIndicator = new Literal();
                    tooLongIndicator.Text = "... ";
                    this.Controls.Add(tooLongIndicator);
                }
                phBreadCrumb.Controls.Add(anchor);
                if (anchor != breadCrumbList[breadCrumbList.Count - 1])
                {
                    Literal sep = new Literal();
                    sep.Text = Separator;
                    phBreadCrumb.Controls.Add(sep);
                }
            }
            
        }
        catch { }
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
    /// Returns the breadcrumb for the current page
    /// </summary>
    /// <param name="pgeId">Current id for the page</param>
    /// <param name="className">The classname to use for the links</param>
    /// <param name="target">The target to use for the links</param>
    /// <param name="showPage">True if current page should be included</param>
    /// <param name="showLanguageLevel">True if Language level shoud be included</param>
    /// <param name="rootName">The name to use for the root location</param>
    /// <returns>A list of HtmlAnchors</returns>
    public List<HtmlAnchor> getBreadCrumb(string connectionString,
                                 int pgeId,
                                 string className,
                                 string target,
                                 bool showPage,
                                 bool showLanguageLevel,
                                 bool showDefaultPage,
                                 string rootName)
    {
        List<HtmlAnchor> listOfAnchors = new List<HtmlAnchor>();
        Spinit.Wpc.Content.Data.Tree tre
            = new Spinit.Wpc.Content.Data.Tree(connectionString);
        Spinit.Wpc.Content.Data.Page pge
            = new Spinit.Wpc.Content.Data.Page(connectionString);
        Location loc = new Location(Spinit.Wpc.Base.Business
                                        .Globals.ConnectionString);
        Language lng = new Language(Spinit.Wpc.Base.Business
                                        .Globals.ConnectionString);

        ArrayList treeList = tre.GetTreeUpLst(pgeId);
        TreeRow parentRow = null;

        parentRow = (TreeRow)treeList[0];
        listOfAnchors.Add(constructLink(rootName,
                                            ParseLinks
                                                .getUrlPath(parentRow,
                                                             loc,
                                                             lng,
                                                             tre),
                                            className,
                                            target));
        foreach (TreeRow treeRow in treeList)
        {
            bool buildBread = false;
            switch (treeRow.TreeType)
            {
                case Tree.TreeTypes.DefaultPage:
                    {
                        if ((showPage) && (showDefaultPage))
                            buildBread = true;
                        break;
                    }
                case Tree.TreeTypes.Page:
                    {
                        if (showPage)
                            buildBread = true;
                        break;
                    }
                case Tree.TreeTypes.Language:
                    {
                        if (showLanguageLevel)
                            buildBread = true;
                        break;
                    }
                case Tree.TreeTypes.Menu:
                    {
                        buildBread = true;
                        break;
                    }
            }
            if (buildBread)
            {
                listOfAnchors.Add(constructLink(
                    treeRow.Name,
                    ParseLinks.getUrlPath(treeRow, loc, lng, tre),
                    className,
                    target));
            }
        }

        return listOfAnchors;
    }
    
    /// <summary>
    /// Constructs a HtmlAnchor for the parameters served
    /// </summary>
    /// <param name="name">Name of link</param>
    /// <param name="link">The link</param>
    /// <param name="className">Class for the anchor</param>
    /// <param name="target">Target for the anchor</param>
    /// <returns>A HtmlAnchor</returns>
    private HtmlAnchor constructLink(string name, string link, string className, string target)
    {
        HtmlAnchor anchor = new System.Web.UI.HtmlControls.HtmlAnchor();
        if ((target != null) && (target.Length > 0))
        {
            anchor.Target = target;
        }
        if ((className != null) && (className.Length > 0))
        {
            anchor.Attributes.Add("class", className);
        }
        anchor.InnerText = name;
        anchor.HRef = link;
        return anchor;
    }

    /// <summary>
    /// Calculates the length of the list of anchors + the separator
    /// </summary>
    /// <param name="breadCrumbList">The list of anchors</param>
    /// <param name="separator">The separator style</param>
    /// <returns></returns>
    private int CalculateLength(List<HtmlAnchor> breadCrumbList,
        string separator)
    {
        int length = 0;
        foreach (HtmlAnchor anchor in breadCrumbList)
        {
            length += HttpUtility.HtmlDecode(anchor.InnerText).Length;
            if (anchor != breadCrumbList[breadCrumbList.Count - 1])
                length += HttpUtility.HtmlDecode(separator).Length;

        }
        return length;
    }

    /// <summary>
    /// Gets/sets the page-id property.
    /// </summary>

    public int PageId
    {
        get { return _pageId; }
        set { _pageId = value; }
    }

    /// <summary>
    /// Gets/sets if the language level should be included in the breadcrumb.
    /// </summary>

    public bool ShowLanguageLevel
    {
        get { return _showLanguageLevel; }
        set { _showLanguageLevel = Convert.ToBoolean(value); }
    }

    /// <summary>
    /// Gets/sets if the Page should be visible or just the crumb to the page.
    /// </summary>

    public bool ShowPage
    {
        get { return _showPage; }
        set { _showPage = Convert.ToBoolean(value); }
    }

    /// <summary>
    /// Gets/sets if default pages should be visible in the crumb.
    /// </summary>

    public bool ShowDefaultPage
    {
        get { return _showDefaultPage; }
        set { _showDefaultPage = Convert.ToBoolean(value); }
    }

    /// <summary>
    /// Gets/sets if pages are to be linked or not
    /// </summary>
    public bool LinkPage
    {
        get { return _linkPage; }
        set { _linkPage = Convert.ToBoolean(value); }
    }

    /// <summary>
    /// Gets/Sets the target for each link in the breadcrumb
    /// </summary>
    public string Target
    {
        get { return _target; }
        set { _target = value; }
    }

    /// <summary>
    /// Gets/Sets the name of the home (root) link for the crumb
    /// </summary>
    public string RootName
    {
        get { return _rootName; }
        set { _rootName = value; }
    }

    /// <summary>
    /// Gets/Sets the format of the separator to use between the links
    /// </summary>
    public string Separator
    {
        get { return _separator; }
        set { _separator = value; }
    }

    /// <summary>
    /// Gets/Sets the classname to use for each link
    /// </summary>
    public string ClassName
    {
        get { return _className; }
        set { _className = value; }
    }

    /// <summary>
    /// Gets/sets the maximum length of the breadcrumb. -1 indicates no limit
    /// </summary>
    public int MaxLength
    {
        get { return _maxLength; }
        set { _maxLength = value; }
    }    

</SCRIPT>
<asp:PlaceHolder ID="phBreadCrumb" runat="server"/>
