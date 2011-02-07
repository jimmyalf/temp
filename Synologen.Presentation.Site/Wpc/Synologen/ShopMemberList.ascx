<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShopMemberList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ShopMemberList" %>
<asp:Repeater ID="rptMembers" runat="server" >
<HeaderTemplate><ul id="synologen-member-list" class="synologen-control"></HeaderTemplate>
<ItemTemplate>
<li>
<span><%# DataBinder.Eval(Container.DataItem, "cFirstName")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "cLastName")%></span>
<a href="<%=EditMemberPage%>?id=<%# DataBinder.Eval(Container.DataItem, "cId") %>"><img src="/Wpc/Synologen/Images/Edit.png" alt="Administrera" title="Administrera" /></a>
</li>
</ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>