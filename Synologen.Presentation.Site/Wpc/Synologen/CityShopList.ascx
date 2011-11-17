<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CityShopList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.CityShopList" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Business.Domain.Entities"%>
<asp:PlaceHolder ID="plShopFilter" runat="server">
<div id="shop-content">
<div id="shop-filter">
<label>Utrustning</label>
<asp:DropDownList ID="drpEquipment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpEquipmentSelectedIndexChanged" />
</div>
</asp:PlaceHolder>
<asp:Repeater ID="rptCities" runat="server" >
<HeaderTemplate><div id="sub-nav" class="sub-nav-columnize city-list-container"><ul class="city-list"></HeaderTemplate>
<ItemTemplate>
<li><asp:LinkButton ID="lnkbtnOpenCityShops" runat="server" CommandArgument='<%# Eval("City")%>' OnCommand="lnkBtnOpenCityShops_OnCommand"><%# Eval("City")%></asp:LinkButton></li>
</ItemTemplate>
<FooterTemplate></ul></div></FooterTemplate>
</asp:Repeater>
</div>

<asp:Repeater ID="rptShops" runat="server"  OnItemDataBound="rptShops_ItemDataBound">
<HeaderTemplate>
<div class="pop-up-box"><a href="#" class="close-button" title="Stäng">Stäng</a><div class="list-container-content"><h2><%# SelectedCity %></h2><ul class="shop-list">
</HeaderTemplate>
<ItemTemplate>
<asp:PlaceHolder ID="plLink" runat="server" Visible="true">
	<li class="shop-link"><h3><a href="<%# Eval("Url") %>"><%# Eval("Name") %></a></h3></li>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoLink" runat="server" Visible="false">
	<li class="shop-no-link"><h3><%# Eval("Name") %></h3></li>
</asp:PlaceHolder>
<li><%# Eval("Phone") %></li>
<li><%# DataBinder.Eval(Container.DataItem, "Address2") %></li>
<li><a href='mailto:<%# Eval("Email") %>'><%# Eval("Email") %></a></li>
<asp:PlaceHolder ID="plEquipment" runat="server" Visible="false">
<li>Vi erbjuder:&nbsp;<%#FormatEquipmentString(((Shop)Container.DataItem).Equipment)%></li>

</asp:PlaceHolder>
</ItemTemplate>

<FooterTemplate></ul>
</div>
</div>
</FooterTemplate>
</asp:Repeater>