<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcContentProperty>" %>
<p class="form-item">
	<label for="<%= Model.Key %>"><%= Html.Encode(Model.DisplayName) %></label>
	<input type="text" id="<%= Model.Key %>" name="<%= Model.Key %>" value="<%= Model.DefaultValue %>" class="number<%= Model.Required.ReplaceWith(" required", "") %>" />
	<%= Html.DisplayFor(x => x.Description) %>
</p>