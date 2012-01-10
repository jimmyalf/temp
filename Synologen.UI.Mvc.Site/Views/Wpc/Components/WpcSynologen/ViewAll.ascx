<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.ShopListView>" %>

<%= Html.Partial("Map", ViewData.Model.Shops) %>

<h1>Alla våra synologer</h1>

<table>
    <tr>
        <th>Namn</th>
        <th>Adress</th>
        <th>Ort</th>
        <th>Email</th>
    </tr>

    <% foreach (var item in ViewData.Model.Shops) { %>
    <tr>
        <td class="name-column"><a href="/butiker/visa-butik?id=<%= item.Id %>"><%= item.Name %></a></td>
        <td class="address-column"><%= item.StreetAddress %></td>
        <td class="city-column"><%= item.City %></td>
        <td class="email-column"><a href="mailto:<%= item.Email %>"><%= item.Email %></a></td>
    </tr>
    <% } %>
</table>

