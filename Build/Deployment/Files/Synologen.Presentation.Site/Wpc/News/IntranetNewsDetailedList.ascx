<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetNewsDetailedList.ascx.cs" Inherits="Spinit.Wpc.News.Presentation.Intranet.wpc.News.IntranetNewsDetailedList" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:PlaceHolder ID="phFilter" Visible="false" runat="server">
<div class="entries-filter">
Visa:&nbsp;
<asp:DropDownList ID="ddlStatusFilter" runat="server" OnSelectedIndexChanged="ddlStatusFilter_SelectedIndexChanged" AutoPostBack="true">
	<asp:ListItem  Value="1">Ol&#228;sta</asp:ListItem>
	<asp:ListItem Selected="True" Value="0">Alla</asp:ListItem>
</asp:DropDownList>
</div>
</asp:PlaceHolder>
<div class="entries-actions">
<img src="/commonresources/files/intranet.falkenbergskonstskola.se/graphics/read-icon.gif" width="18" height="18" alt="" />
<asp:Button ID="btnMarkAllAsRead" runat="server" Visible="false" Text="Markera alla som lästa" OnClick="btnMarkAllAsRead_Click" />
<img src="/commonresources/files/intranet.falkenbergskonstskola.se/graphics/unread-icon.gif" width="18" height="18" alt="" />
<asp:Button ID="btnMarkAsUnread" runat="server" Visible="false" Text="Markera valda som olästa" OnClick="btnMarkAsUnread_Click" />
</div>
<div class="wpcPager"><WPC:PAGER id="pager" runat="server" HideIfNoExtraPages="true" PageSizeSetByUser="false" /></div>
<asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound">
<HeaderTemplate>
<table class="entries-overview clear-both">
    <tbody>
		<tr><th scope="col">Ämne</th>
            <th scope="col">Avsändare</th>
            <th scope="col">Datum</th>
            <th scope="col">Status</th>
            <th scope="col">Välj</th>
        </tr>
</HeaderTemplate>
<ItemTemplate>
<tr class='<%# GetClass((bool)DataBinder.Eval(Container.DataItem, "IsRead")) %>'>
	<td><a href="<%=DetailUrl%>?newsid=<%# DataBinder.Eval(Container.DataItem, "Id") %>"><%# DataBinder.Eval(Container.DataItem, "Heading") %></a></td>
	<td><a href="<%=AuthorPageUrl%>?userId=<%# DataBinder.Eval(Container.DataItem, "AuthorUserId") %>"><%# DataBinder.Eval(Container.DataItem, "Author") %></a></td>
	<td><%# ((DateTime) DataBinder.Eval(Container.DataItem, "StartDate")).ToString("dd MMM yyyy")%>, kl. <%# ((DateTime) DataBinder.Eval(Container.DataItem, "StartDate")).ToString("HH:mm")%></td>
	<td><img src="<%# GetReadImage((bool)DataBinder.Eval(Container.DataItem, "IsRead")) %>" alt="<%# GetReadImageAlt((bool)DataBinder.Eval(Container.DataItem, "IsRead")) %>" /></td>
	<td><asp:HiddenField ID="hfNewsId" runat="server" />
		<asp:CheckBox ID="chkSelected" runat="server" /></td>  
</tr>
</ItemTemplate>
<FooterTemplate></tbody>
</table>
</FooterTemplate>
</asp:Repeater>
<div class="entries-actions">
<img src="/commonresources/files/intranet.falkenbergskonstskola.se/graphics/read-icon.gif" width="18" height="18" alt="" />
<asp:Button ID="btnMarkAllAsRead2" Visible="false" runat="server" Text="Markera alla som lästa" OnClick="btnMarkAllAsRead_Click" />
<img src="/commonresources/files/intranet.falkenbergskonstskola.se/graphics/unread-icon.gif" width="18" height="18" alt="" />
<asp:Button ID="btnMarkAsUnread2" Visible="false" runat="server" Text="Markera valda som olästa" OnClick="btnMarkAsUnread_Click" />
</div>
