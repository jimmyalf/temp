<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.SearchShopView>" %>

<%= Html.Partial("Map", ViewData.Model.Shops) %>

<div id="search-results">
    <h1>Sökresultat</h1>

    <h2>Din sökning <%= Model.Search %> gav <%= Model.NrOfResults %> träffar</h2>

    <% foreach (var item in ViewData.Model.Shops) { %>
        <article class="store-information">
            <h2><%= item.Name %></h2>
            <p class="tags">Vi erbjuder: <em>Ögonapoteket</em>, <em>Ögonhälsoundersökning</em></p>
            <p>Nullam sit amet adipiscing nisi. Duis viverra nisi non lorem adipiscing consequat. Maecenas sodales placerat lacinia.</p>
            <p>Tfn: <%= item.Telephone %><br />
            Adress: <%= item.StreetAddress %><br />
            E-post: <a href="mailto:<%= item.Email %>"><%= item.Email %></a>
            </p>
        </article>
    <% } %>
</div>