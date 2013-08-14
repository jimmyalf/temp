<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberAddressList" Codebehind="MemberAddressList.ascx.cs" %>

<asp:Repeater ID="rptMembers" runat="server" OnItemDataBound="rptMembers_ItemDataBound">
<HeaderTemplate><table></HeaderTemplate>
<ItemTemplate>
<tr class="cityrow"><td colspan="2" style="background-color: rgb(213, 222, 213);"><%# DataBinder.Eval(Container.DataItem, "cCity") %></td></tr>
<tr class="otherrow"><td style="font-weight: bold;">
<asp:HyperLink ID="hlWeb" runat="server"><%# DataBinder.Eval(Container.DataItem, "cOrgName") %></asp:HyperLink></td>
<td><%# DataBinder.Eval(Container.DataItem, "cPhone") %></td></tr>
<tr class="otherrow"><td><%# DataBinder.Eval(Container.DataItem, "cAddress") %></td>
<td><a href='mailto:<%# DataBinder.Eval(Container.DataItem, "cEmail") %>'><%# DataBinder.Eval(Container.DataItem, "cEmail") %></a></td></tr>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>

