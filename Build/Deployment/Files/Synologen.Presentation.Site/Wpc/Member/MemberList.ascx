<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberList" Codebehind="MemberList.ascx.cs" %>
<asp:Repeater ID="rptMembers" runat="server">
<ItemTemplate>
<div id="memberitem">
<a href='<%=InfoPage%>?memberId=<%# DataBinder.Eval(Container.DataItem, "cMemberId") %>'><%# DataBinder.Eval(Container.DataItem, "cOrgName") %></a>
</div>
</ItemTemplate>
</asp:Repeater>
