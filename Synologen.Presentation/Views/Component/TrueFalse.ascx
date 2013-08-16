<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcContentProperty>" %>
<div class="truefalse">
	<div class="name"><%= Html.Encode(Model.DisplayName) %></div>
	<ul>
		<li class="form-item">
			<input type="radio" id="<%= Model.Key %>-True" name="<%= Model.Key %>" value="True"<%= bool.Parse(Model.DefaultValue).ReplaceWith("checked=\"checked\"", "") %> />
			<label for="<%= Model.Key %>-True">Yes</label>
		</li>
		<li class="form-item">
			<input type="radio" id="<%= Model.Key %>-False" name="<%= Model.Key %>" value="False"<%= bool.Parse(Model.DefaultValue).ReplaceWith("", "checked=\"checked\"") %> />
			<label for="<%= Model.Key %>-False">No</label>
		</li>
	</ul><%= Html.DisplayFor(x => x.Description) %>
</div>