<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Intranet.Wpc.News.IntranetNewsListAlternative" %>
<h5 id="newsheader<%=CategoryClass %>">
<asp:Label id="lblCategoryName" runat="server" visible="false" Text="Senaste nytt från synologerna"/>
</h5>
<asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound"  >
<HeaderTemplate><div id="newsContainer<%=CategoryClass %>"></HeaderTemplate>
<ItemTemplate>
<p class="test">
    <div><asp:Image ID="imgSpot" runat="server" visible="false"/></div>

    <b><asp:Label ID="lblHeading" runat="server"><%# DataBinder.Eval(Container.DataItem, "cHeading") %></asp:Label></b><br />
    <asp:Label ID="lblSummary" runat="server"><%# DataBinder.Eval(Container.DataItem, "cSummary") %></asp:Label>
    <asp:HyperLink ID="hlNewsLink" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "cId") %>' runat="server"><br />
L&auml;s mer &gt;
    </asp:HyperLink>
    <br />
	</p>
</ItemTemplate>
<FooterTemplate></div></FooterTemplate>
</asp:Repeater>
