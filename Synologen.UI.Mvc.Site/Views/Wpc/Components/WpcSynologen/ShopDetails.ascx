<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.ShopListItem>" %>

<article class="store-information">
    <h2>
        <% if (ViewData.Model.IsDetailedView) { %>
            <%=ViewData.Model.Name%>
        <% } else { %>
            <a href="/butiker/visa-butik?id=<%=ViewData.Model.Id%>" title="Se detaljvy"><%=ViewData.Model.Name%></a>
        <% } %>
    </h2>
    <% if (!String.IsNullOrEmpty(ViewData.Model.Provides)) %><p class="tags">Vi erbjuder: <%= ViewData.Model.Provides%></p>
    <p><%= ViewData.Model.Description %></p>
    <p>Tfn: <%= ViewData.Model.Telephone %><br />
    Adress: <%= ViewData.Model.StreetAddress%>, <%= ViewData.Model.City %><br />
    E-post: <a href="mailto:<%= ViewData.Model.Email %>"><%= ViewData.Model.Email%></a><br />
    Hemsida: <a href="<%=ViewData.Model.FormattedHomePage %>"><%=ViewData.Model.HomePage %></a>
    </p>
</article>

