<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.SearchShopView>" %>

<%= Html.Partial("Map", ViewData.Model.Shops) %>

<div id="search-results">
    <h1>Butiker</h1>

    <h2>Sökresultat | Din sökning <i><%= Model.Search %></i> gav <i><%= Model.NrOfResults %></i> träffar</h2>
    <p><a href="/butiker/visa-alla">Klicka här lista alla butiker</a> </p>

    <% foreach (var item in ViewData.Model.Shops) { %>
        <%= Html.Partial("ShopDetails", item) %>
    <% } %>
</div>