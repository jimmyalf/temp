<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShopList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ShopList" %>
<asp:PlaceHolder ID="plShopFilter" runat="server">
<div id="shop-filter">
<label>Utrustning</label>
<asp:DropDownList ID="drpEquipment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpEquipmentSelectedIndexChanged" />
</div>
</asp:PlaceHolder>
<asp:Repeater ID="rptShops" runat="server"  OnItemDataBound="rptShops_ItemDataBound">
<HeaderTemplate><table class="shop-list"></HeaderTemplate>
<ItemTemplate>
<tr><td colspan="2" class="shop-city"><%# DataBinder.Eval(Container.DataItem, "cCity") %></td></tr>
<tr>
<asp:PlaceHolder ID="plLink" runat="server" Visible="true">
	<td class="shop-link"><a href="<%# DataBinder.Eval(Container.DataItem, "cUrl") %>"><%# DataBinder.Eval(Container.DataItem, "cShopName") %></a></td>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoLink" runat="server" Visible="false">
	<td class="shop-no-link"><%# DataBinder.Eval(Container.DataItem, "cShopName") %></td>
</asp:PlaceHolder>
	<td><%# DataBinder.Eval(Container.DataItem, "cPhone") %></td>
</tr>
<tr class="shop-contact-row">
	<td><%# DataBinder.Eval(Container.DataItem, "cAddress2") %></td>
	<td><a href='mailto:<%# DataBinder.Eval(Container.DataItem, "cEmail") %>'><%# DataBinder.Eval(Container.DataItem, "cEmail") %></a></td>
</tr>
<asp:PlaceHolder ID="plEquipment" runat="server" Visible="false">
<tr class="shop-equipment-row"><td colspan="2">Utrustning:&nbsp;<%# DataBinder.Eval(Container.DataItem, "cEquipment") %></td></tr>
</asp:PlaceHolder>
</ItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>