<%@ Control Inherits="System.Web.Mvc.ViewUserControl<WpcBasicLogin>" %>
<%= Html.ValidationSummary(true) %>
<% using (Html.BeginForm())
{ %>
<fieldset>
<div class="field">
	<label for="Username">Username:</label>	
	<%= Html.TextBox("Username") %>
	<%= Html.ValidationMessageFor(x => x.Username) %>			
</div>
<div class="field">
	<label for="Password">Password:</label>
	<%= Html.Password("Password") %>
	<%= Html.ValidationMessageFor(x => x.Password) %>			
</div>
<div class="field">
	<label for="RememberMe">Remeber me:</label>
	<%= Html.CheckBoxFor(x => x.Persist) %>
</div>
</fieldset>
<input value="Send" type="submit">

<% } %>
