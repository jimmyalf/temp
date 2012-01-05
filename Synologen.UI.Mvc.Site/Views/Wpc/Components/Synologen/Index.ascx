<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.SearchShopView>" %>

<form action="/" method="get">
	<fieldset>
        <p><%= Html.TextBox("Search", null, new { placeholder = "Postnummer eller ort" })%><input type="submit" value="Sök" /></p>
	</fieldset>
</form>

<ul>
    <% foreach (var item in ViewData.Model.Shops) { %>
        
        <li>
            <dl>
                <dd>Id</dd><dt><%= item.Id %></dt>
                <dd>Namn</dd><dt><%= item.Name %></dt>
                <dd>Hemsida</dd><dt><%= item.HomePage %></dt>
                <dd>Telefonnummer</dd><dt><%= item.Telephone %></dt>
                <dd>Karta</dd><dt><%= item.Map %></dt>
                <dd>Email</dd><dt><%= item.Email %></dt>
                <dd>Longitude</dd><dt><%= item.Longitude %></dt>
                <dd>Latitude</dd><dt><%= item.Latitude %></dt>
                <dd>Gatuadress</dd><dt><%= item.StreetAddress %></dt>
            </dl>
        </li>

     <% } %>
</ul>


