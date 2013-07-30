<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcContentProperty>" %>
<p class="form-item">
	<label for="<%= Model.Key %>"><%= Html.Encode(Model.DisplayName) %></label>
	<input type="text" id="<%= Model.Key %>" name="<%= Model.Key %>" value="<%= Html.Encode(Model.DefaultValue) %>" class="text<%= Model.Required.ReplaceWith(" required", "") %>" />
	<%= Html.DisplayFor(x => x.Description) %>
</p>