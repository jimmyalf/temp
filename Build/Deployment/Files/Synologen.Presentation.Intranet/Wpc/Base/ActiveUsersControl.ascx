<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActiveUsersControl.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Site.ActiveUsersControl" %>
<asp:Repeater ID="rptUsers" runat="server">
<HeaderTemplate><ul id="logged-on"></HeaderTemplate>
<ItemTemplate>
<li>
<a href='<%=ProfilePage %>?userId=<%# DataBinder.Eval(Container.DataItem, "cId") %>'>
<img src='/wpc/Member/ViewProfilePicture.aspx?userId=<%# DataBinder.Eval(Container.DataItem, "cId") %>&amp;width=50&amp;height=50'  alt='<%# DataBinder.Eval(Container.DataItem, "cFirstName") %>&nbsp;<%# DataBinder.Eval(Container.DataItem, "cLastName") %>' title='<%# DataBinder.Eval(Container.DataItem, "cFirstName") %>&nbsp;<%# DataBinder.Eval(Container.DataItem, "cLastName") %>' /></a>
</li>
</ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>


