<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Site.News" Codebehind="News.ascx.cs" %>
<asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound">
<ItemTemplate>
    <asp:HyperLink cssClass="bluelink" ID="hlNewsLink" runat="server">
    <%# DataBinder.Eval(Container.DataItem, "cHeading") %></asp:HyperLink>
<br />
</ItemTemplate>
</asp:Repeater>
