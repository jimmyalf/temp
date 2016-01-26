<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginHistoryControl.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Site.LoginHistoryControl" %>
<asp:Repeater ID="rptGroups" runat="server" OnItemDataBound="rptGroups_ItemDataBound">
	<ItemTemplate>
		<h4><%# DataBinder.Eval(Container.DataItem, "cName") %></h4>
<asp:Repeater ID="rptUsers" runat="server">
<HeaderTemplate><ul></HeaderTemplate>
<ItemTemplate><li><a href='<%=ProfilePage %>?userId=<%# DataBinder.Eval(Container.DataItem, "cId") %>'><%# DataBinder.Eval(Container.DataItem, "cFirstName") %>&nbsp;<%# DataBinder.Eval(Container.DataItem, "cLastName") %></a></li></ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
		</asp:Repeater>
	</ItemTemplate>
</asp:Repeater>

