<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.ShopListItem>" %>

<article class="store-information">
    <h2><a href="/butiker/visa-butik?id=<%= ViewData.Model.Id %>"><%= ViewData.Model.Name %></a></h2>
    <% if (!String.IsNullOrEmpty(ViewData.Model.Provides)) %><p class="tags">Vi erbjuder: <%= ViewData.Model.Provides%></p>
    <p><%= ViewData.Model.Description %></p>
    <p>Tfn: <%= ViewData.Model.Telephone %><br />
    Adress: <%= ViewData.Model.StreetAddress%>, <%= ViewData.Model.City %><br />
    E-post: <a href="mailto:<%= ViewData.Model.Email %>"><%= ViewData.Model.Email%></a><br />
    Hemsida: <a href="<%=ViewData.Model.HomePage %>"><%=ViewData.Model.HomePage %></a>
    </p>
</article>

