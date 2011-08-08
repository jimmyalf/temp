<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetMemberSearchResults.ascx.cs" Inherits="Spinit.Wpc.Member.Presentation.Intranet.Wpc.Member.IntranetMemberSearchResults" %>
<asp:Repeater ID="rptResults" runat="server">
<HeaderTemplate><dl class="search-results"></HeaderTemplate>
<ItemTemplate>
    <dt><a href='<%# DataBinder.Eval(Container.DataItem, "Url")%>'><%# DataBinder.Eval(Container.DataItem, "Heading")%></a></dt>
	<dd><blockquote><p><%# DataBinder.Eval(Container.DataItem, "Extract")%></p></blockquote></dd>
	<dd class="meta"><cite><a href='<%# DataBinder.Eval(Container.DataItem, "Url")%>'><%# DataBinder.Eval(Container.DataItem, "Url")%></a></cite><span class="date"> - <%# DataBinder.Eval(Container.DataItem, "LastModified")%></span></dd>
</ItemTemplate>
<FooterTemplate></dl></FooterTemplate>
</asp:Repeater>