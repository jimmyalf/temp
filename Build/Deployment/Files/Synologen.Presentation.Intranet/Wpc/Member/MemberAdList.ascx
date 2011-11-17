<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberAdList" Codebehind="MemberAdList.ascx.cs" %>
<asp:Repeater ID="rptAds" runat="server">
<HeaderTemplate><div id="rightlink"><ul></HeaderTemplate>
<ItemTemplate>

<li class="rightlink">
<a href='<%=InfoPage%>?adId=<%# DataBinder.Eval(Container.DataItem, "Id") %>'><%# DataBinder.Eval(Container.DataItem, "Heading") %></a>
</li>

</ItemTemplate>
<FooterTemplate></ul></div></FooterTemplate>
</asp:Repeater>