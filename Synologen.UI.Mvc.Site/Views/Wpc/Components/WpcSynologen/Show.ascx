<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.ShopListItem>" %>

<%= Html.Partial("Map", ViewData.Model.Shops) %>

<div id="search-results">
    <article class="store-information">
        <h2><%= ViewData.Model.Name %></h2>
        <p class="tags">Vi erbjuder: <em>Ögonapoteket</em>, <em>Ögonhälsoundersökning</em></p>
        <p>Nullam sit amet adipiscing nisi. Duis viverra nisi non lorem adipiscing consequat. Maecenas sodales placerat lacinia.</p>
        <p>Tfn: <%= ViewData.Model.Telephone %><br />
        Adress: <%= ViewData.Model.StreetAddress%><br />
        E-post: <a href="mailto:<%= ViewData.Model.Email %>"><%= ViewData.Model.Email%></a>
        </p>
    </article>
</div>