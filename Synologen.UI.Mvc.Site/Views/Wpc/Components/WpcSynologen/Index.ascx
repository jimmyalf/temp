<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.SearchShopView>" %>

<form action="/" method="get">
	<fieldset>
        <p><%= Html.TextBox("Search", null, new { placeholder = "Postnummer eller ort" })%><input type="submit" value="Sök" /></p>
	</fieldset>
</form>

<table>
    <tr>
        <th>Id</th>
        <th>Namn</th>
        <th>Beskrivning</th>
        <th>Hemsida</th>
        <th>Telefonnummer</th>
        <th>Email</th>
        <th>Adress</th>
    </tr>

    <% foreach (var item in ViewData.Model.Shops) { %>
    <tr>
        <td><%= item.Id %></td>
        <td><%= item.Name %></td>
        <td><%= item.Description %></td>
        <td><%= item.HomePage %></td>
        <td><%= item.Telephone %></td>
        <td><a href="<%= item.Email %>"><%= item.Email %></a></td>
        <td><%= item.StreetAddress %></td>
    </tr>
    <% } %>
</table>

