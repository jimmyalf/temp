<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Site.News" Codebehind="News.ascx.cs" %>
<asp:Label id="lblCategoryName" runat="server" visible="false" Text="Senaste nytt från " />
<asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound">
<HeaderTemplate><div id="newsContainer"></HeaderTemplate>
<ItemTemplate>
    <asp:Image ID="imgSpot" runat="server" visible="false"/>
    <b><asp:Label ID="lblHeading" runat="server"><%# DataBinder.Eval(Container.DataItem, "cHeading") %></asp:Label></b>
    <asp:HyperLink ID="hlNewsLink" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "cId") %>' runat="server">
        <%# DataBinder.Eval(Container.DataItem, "cHeading")%>
    </asp:HyperLink>
    <br />
<%# DataBinder.Eval(Container.DataItem, "cSummary") %><br/><br/>
</ItemTemplate>
<FooterTemplate></div></FooterTemplate>
</asp:Repeater>
