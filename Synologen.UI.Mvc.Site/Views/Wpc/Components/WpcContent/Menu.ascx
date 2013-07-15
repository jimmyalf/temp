<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcMenu>" %>
<% if (Model.Items.Length > 0) { %><ul<%= Model.Id.ToAttribute(" id") %><%= Model.Class.ToAttribute(" class") %>><% foreach (var item in Model.Items) { %>
	<% if (item.Selected) Html.RenderPartial("MenuItemSelected", item); else Html.RenderPartial("MenuItemNormal", item); %>
<% } %></ul><% } %>