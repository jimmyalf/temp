<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.ShopListView>" %>

<%= Html.Partial("Map", ViewData.Model.Shops) %>

<table>
    <tr>
        <th>Id</th>
        <th>Namn</th>
        <th>Hemsida</th>
        <th>Telefonnummer</th>
        <th>Email</th>
        <th>Adress</th>
    </tr>

    <% foreach (var item in ViewData.Model.Shops) { %>
    <tr>
        <td><%= item.Id %></td>
        <td><%= item.Name %></td>
        <td><a href="<%= item.HomePage %>"><%= item.HomePage %></a></td>
        <td><%= item.Telephone %></td>
        <td><a href="mailto:<%= item.Email %>"><%= item.Email %></a></td>
        <td><%= item.StreetAddress %></td>
    </tr>
    <% } %>
</table>

