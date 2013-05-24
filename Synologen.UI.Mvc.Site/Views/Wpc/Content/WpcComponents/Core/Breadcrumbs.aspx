<%@ Page MasterPageFile="/CommonResources/Templates/Master/Mvc 2 column.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<h1>Breadcrumbs</h1>
	
	<h2>WPC HTML editor code</h2>
	<code>&lt;a href=&quot;http:///#component=2?name=BreadCrumb&amp;amp;ShowDefaultPage=false&amp;amp;ShowPage=false&amp;amp;ShowLanguageLevel=false&amp;amp;LinkPage=false&amp;amp;RootName=Home&amp;amp;MaxLength=-1&amp;amp;Separator= / &quot;&gt;&lt;img alt=&quot;&quot; src=&quot;/wysiwyg/ComponentImage.aspx?ComponentId=2&quot; style=&quot;border-width: 0px; border-style: solid;&quot; /&gt;&lt;/a&gt;</code>
	
	<h2>WPC template code</h2>
	<code>&lt;%= Html.Action(&quot;Breadcrumbs&quot;, &quot;WpcContent&quot;, new { area = &quot;WpcContent&quot;, settings = new { Class = &quot;breadcrumbs&quot;, RootLevelName = &quot;Hem&quot; }}) %&gt;</code>
	
	<h2>Example</h2>
	<%= Html.Action("Breadcrumbs", "WpcContent", new { area = "WpcContent", settings = new { Class = "breadcrumbs", RootLevelName = "Hem" }}) %>
</asp:Content>