<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcMenuItem>" %>
<% if (Model != null && Model.Children != null && Model.Children.Count > 0) { %><ul><% foreach (var item in Model.Children) { %>
	<% if (item.Selected) Html.RenderPartial("MenuItemSelected", item); else Html.RenderPartial("MenuItemNormal", item); %>
<% } %></ul><% } %>