<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameGlassTypeEditView>" %>
	<% Html.EnableClientValidation(); %>
	<%= Html.ValidationSummary(true) %>
	<% using (Html.BeginForm()) {%>
		<fieldset>
			<legend><%=Html.Encode(Model.FormLegend) %></legend>
			<p class="formItem">
				<%= Html.LabelFor(x => x.Name) %>
				<%= Html.EditorFor(x => x.Name) %>
				<%= Html.ValidationMessageFor(x => x.Name) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.IncludeAdditionParametersInOrder) %>
				<%= Html.EditorFor(x => x.IncludeAdditionParametersInOrder) %>
				<%= Html.ValidationMessageFor(x => x.IncludeAdditionParametersInOrder) %>
			</p>
			<p class="formItem clearLeft">
				<%= Html.LabelFor(x => x.IncludeHeightParametersInOrder) %>
				<%= Html.EditorFor(x => x.IncludeHeightParametersInOrder) %>
				<%= Html.ValidationMessageFor(x => x.IncludeHeightParametersInOrder) %>
			</p>						
			<p class="formCommands">
				<%= Html.AntiForgeryToken() %>
				<%= Html.HiddenFor(x => x.Id) %>
				<input type="submit" value="Save" class="btnBig" />
			</p>
		</fieldset>
    <% } %>
	<p>
		<%= Html.ActionLink("Tillbaka till glastyper", "GlassTypes") %>
	</p>
	<% Html.RenderPartial("ClientValidationScripts"); %>
