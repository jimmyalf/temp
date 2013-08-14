<%@ Page MasterPageFile="/CommonResources/Templates/Master/Mvc 2 column.master" %>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
	<h1>Menu</h1>
	
	<h2>WPC HTML editor code</h2>
	<code>&lt;a href=&quot;http:///#component=2?name=Menu&amp;amp;ShowDefaultPage=false&amp;amp;ShowRootPage=true&amp;amp;StartAtLevel=1&amp;amp;StopAtLevel=2&amp;amp;ClassName=navigation&amp;amp;Location=2&amp;amp;Language=1&quot;&gt;&lt;img alt=&quot;&quot; src=&quot;/wysiwyg/ComponentImage.aspx?ComponentId=2&quot; style=&quot;border-width: 0px; border-style: solid;&quot; /&gt;&lt;/a&gt;</code>
	
	<h2>WPC template code</h2>
	<code>&lt;%= Html.Action(&quot;Menu&quot;,	&quot;WpcContent&quot;, new { area = &quot;WpcContent&quot;, settings = new { Class = &quot;navigation&quot;, ShowRootLevel = false, ShowDefaultPage = false, StartAtLevel = 0, StopAtLevel = 1 } }) %&gt;</code>
	
	<h2>Example</h2>
	<%= Html.Action("Menu",	"WpcContent", new { area = "WpcContent", settings = new { Class = "navigation", ShowRootLevel = false, ShowDefaultPage = false, StartAtLevel = 0, StopAtLevel = 1 } }) %>
</asp:Content>