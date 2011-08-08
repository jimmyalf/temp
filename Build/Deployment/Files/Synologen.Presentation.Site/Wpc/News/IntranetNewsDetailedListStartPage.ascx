<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetNewsDetailedList.ascx.cs" Inherits="Spinit.Wpc.News.Presentation.Intranet.wpc.News.IntranetNewsDetailedList" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:PlaceHolder ID="phFilter" Visible="false" runat="server">
<div class="entries-filter">
Visa:
<asp:DropDownList ID="ddlStatusFilter" runat="server">
	<asp:ListItem Selected="True" Value="1">Ol&#228;sta</asp:ListItem>
	<asp:ListItem Value="0">Alla</asp:ListItem>
</asp:DropDownList>
</div>
</asp:PlaceHolder>
<div class="entries-actions">
<asp:Button ID="btnMarkAllAsRead" runat="server" Visible="false"  Text="Markera alla som lästa" OnClick="btnMarkAllAsRead_Click" />
<asp:Button ID="btnMarkAsUnread" runat="server" Visible="false" Text="Markera valda som olästa" OnClick="btnMarkAsUnread_Click" />
</div>
<div class="wpcPager"><WPC:PAGER id="pager" runat="server" HideIfNoExtraPages="true" PageSizeSetByUser="false" /></div>
<asp:Repeater ID="rptNews" runat="server">
<HeaderTemplate><table class="entries-overview clear-both">
    <tbody>
        <tr>
            <th scope="col">Ämne</th>
            <th scope="col">Avsändare</th>

            <th scope="col">Datum</th>
            
        </tr>
</HeaderTemplate>
<ItemTemplate>
<tr  class='<%# GetClass((bool)DataBinder.Eval(Container.DataItem, "IsRead")) %>'>
	<td><a href="<%=DetailUrl%>?newsid=<%# DataBinder.Eval(Container.DataItem, "Id") %>"><%# DataBinder.Eval(Container.DataItem, "Heading") %></a></td>
	<td><a href="<%=AuthorPageUrl%>?userId=<%# DataBinder.Eval(Container.DataItem, "AuthorUserId") %>"><%# DataBinder.Eval(Container.DataItem, "Author") %></a></td>
	<td><%# ((DateTime) DataBinder.Eval(Container.DataItem, "StartDate")).ToString("dd MMM yyyy")%>, kl. <%# ((DateTime) DataBinder.Eval(Container.DataItem, "StartDate")).ToString("HH:mm")%></t
</tr>
</ItemTemplate>
<FooterTemplate></tbody>
</table>
</FooterTemplate>
</asp:Repeater>
<div class="entries-actions">
<asp:Button ID="btnMarkAllAsRead2" runat="server" Visible="false" Text="Markera alla som lästa" OnClick="btnMarkAllAsRead_Click" />
<asp:Button ID="btnMarkAsUnread2" runat="server" Visible="false" Text="Markera valda som olästa" OnClick="btnMarkAsUnread_Click" />
</div>
