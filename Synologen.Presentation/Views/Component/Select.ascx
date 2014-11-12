<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcContentProperty>" %>
<p class="form-item">
	<label for="<%= Model.Key %>"><%= Html.Encode(Model.DisplayName) %></label>
	<select id="<%= Model.Key %>" name="<%= Model.Key %>" class="text<%= Model.Required.ReplaceWith(" required", "") %>">
		<%= Model.AssociatedData %>
	</select><%= Html.DisplayFor(x => x.Description) %>
</p>