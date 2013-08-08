<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetNewsList.ascx.cs" Inherits="Spinit.Wpc.News.Presentation.Intranet.wpc.News.IntranetNewsList" %>
<asp:Repeater ID="rptGroups" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
<ItemTemplate>
    <asp:PlaceHolder ID="phGroupHeading" runat="server">
        <h3><%# DataBinder.Eval(Container.DataItem, "Name") %></h3>
    </asp:PlaceHolder>
    <asp:Repeater ID="rptGroupNews" runat="server">
        <HeaderTemplate><ol></HeaderTemplate>
        <ItemTemplate>
        <li>
        <a href="<%=DetailUrl%>?newsid=<%# DataBinder.Eval(Container.DataItem, "cId") %>" title="Läs hela inlägget"><%# DataBinder.Eval(Container.DataItem, "cHeading") %></a>
        <div class="meta"><a href="<%=AuthorPageUrl%>?userId=<%# DataBinder.Eval(Container.DataItem, "AuthorUserId") %>"><%# DataBinder.Eval(Container.DataItem, "Author") %></a> - <%# ((DateTime) DataBinder.Eval(Container.DataItem, "cStartDate")).ToString("dd MMM yyyy")%>, kl. <%# ((DateTime) DataBinder.Eval(Container.DataItem, "cStartDate")).ToString("HH:mm")%></div>
        </li>
        </ItemTemplate>
        <FooterTemplate></ol></FooterTemplate>
    </asp:Repeater>
</ItemTemplate>
</asp:Repeater>



