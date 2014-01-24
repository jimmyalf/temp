<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberFileCategories" Codebehind="MemberFileCategories.ascx.cs" %>
<asp:Repeater ID="rptFileCategories" runat="server">
<ItemTemplate>

<a href='<%=FilePage%>?memberId=<%=MemberId%>&categoryId=<%# DataBinder.Eval(Container.DataItem, "Id") %>'><%# DataBinder.Eval(Container.DataItem, "Name") %></a>&nbsp;|&nbsp;

</ItemTemplate>
</asp:Repeater>