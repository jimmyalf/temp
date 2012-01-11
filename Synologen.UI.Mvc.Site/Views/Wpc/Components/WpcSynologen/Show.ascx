<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.UI.Mvc.Site.Models.ShopListItem>" %>

<%= Html.Partial("Map", ViewData.Model.Shops) %>

<div id="search-results">
    <%= Html.Partial("ShopDetails", ViewData.Model) %>
</div>