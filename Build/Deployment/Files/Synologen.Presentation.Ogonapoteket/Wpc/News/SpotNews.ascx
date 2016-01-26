<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Site.SpotNews" Codebehind="SpotNews.ascx.cs" %>

<asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound">
<HeaderTemplate><div id="newsContainer<%=CategoryClass %>"></HeaderTemplate>
<ItemTemplate>
    <div style="padding:0; border:#156a54 1px solid; margin-bottom: 0; overflow: auto;">
<div id="newsImage<%# DataBinder.Eval(Container.DataItem, "cSpotAlign") %>" ><asp:Image ID="imgSpot" runat="server" visible="false"/></div>
    <div id="newsText<%# DataBinder.Eval(Container.DataItem, "cSpotAlign") %>" >
    <h4><asp:Label ID="lblHeading" runat="server"><%# DataBinder.Eval(Container.DataItem, "cHeading") %></asp:Label></h4><br />
    <asp:Label ID="lblSummary" runat="server"><%# DataBinder.Eval(Container.DataItem, "cSummary") %></asp:Label>
    <asp:HyperLink ID="hlNewsLink" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "cId") %>' runat="server">
    <%=GetLocalResourceObject("hlNewsLink.Text").ToString()%><br />
    </asp:HyperLink>
    </div>
    </div>
</ItemTemplate>
<FooterTemplate></div></FooterTemplate>
</asp:Repeater>

