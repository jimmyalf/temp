<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcBreadcrumb>" %>
<% if (Model.Items.Length > 0) { %><div<%= Model.Id.ToAttribute(" id") %><%= Model.Class.ToAttribute(" class") %>>
	<% foreach (var item in Model.Items) { if (item.IsLast) { %><%= item.Text %><% } else { %><a href="<%= item.Url %>"><%= item.Text %></a><%= Model.Separator %><% }} %>
</div><% } %>