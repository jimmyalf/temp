<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CityShopList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.CityShopList" %>
<asp:PlaceHolder ID="plShopFilter" runat="server">
<div id="shop-filter">
<label>Utrustning</label>
<asp:DropDownList ID="drpEquipment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpEquipmentSelectedIndexChanged" />
</div>
</asp:PlaceHolder>
<asp:Repeater ID="rptCities" runat="server" >
<HeaderTemplate><div id="sub-nav"><ul class="city-list"></HeaderTemplate>
<ItemTemplate>
<li><asp:LinkButton ID="lnkbtnOpenCityShops" runat="server" CommandArgument='<%# Eval("City")%>' OnCommand="lnkBtnOpenCityShops_OnCommand"><%# Eval("City")%></asp:LinkButton></li>
</ItemTemplate>
<FooterTemplate></ul></div></FooterTemplate>
</asp:Repeater>


<asp:Repeater ID="rptShops" runat="server"  OnItemDataBound="rptShops_ItemDataBound">
<HeaderTemplate>
<div id="list-container"><h2><%# SelectedCity %></h2><table class="shop-list">
</HeaderTemplate>
<ItemTemplate>
<tr>
<asp:PlaceHolder ID="plLink" runat="server" Visible="true">
	<td class="shop-link"><a href="<%# Eval("Url") %>"><%# Eval("Name") %></a></td>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoLink" runat="server" Visible="false">
	<td class="shop-no-link"><%# Eval("Name") %></td>
</asp:PlaceHolder>
	<td><%# Eval("Phone") %></td>
</tr>
<tr class="shop-contact-row">
	<td><%# DataBinder.Eval(Container.DataItem, "Address2") %></td>
	<td><a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></td>
</tr>
<asp:PlaceHolder ID="plEquipment" runat="server" Visible="false">
<tr class="shop-equipment-row"><td colspan="2">Vi erbjuder:&nbsp;<%#FormatEquipmentString(((Spinit.Wpc.Synologen.Data.Types.ShopRow)Container.DataItem).Equipment)%></td></tr>
</div>
</asp:PlaceHolder>
</ItemTemplate>
<FooterTemplate></table></div></FooterTemplate>
</asp:Repeater>