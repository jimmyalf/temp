<%@ Control Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Utility.Core.ControlPropertyMapping.ControlPropertyMap>" %>
<% if (Model.Components.Any()) { %>
<ul class="components">
<% foreach (var component in Model.Components) { %>
	<% if (component.Controls.Any(x => x.Properties.Count > 0)) { %><li><%= component.Name %>
		<ul class="controls">
		<% foreach (var control in component.Controls.Where(x => x.Properties.Count > 0)) { %>
			<li><%= Html.ActionLink(control.Name, "Add", new { component = component.Name, control = control.Name }) %></li>
		<% } %>
		</ul>
	</li><% } %>
<% } %>
</ul>
<% } %>