<%@ Control Inherits="System.Web.Mvc.ViewUserControl<Spinit.Wpc.Synologen.Presentation.Models.FrameSupplierEditView>" %>
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
            <p class="formItem">
				<%= Html.LabelFor(x => x.Email) %>
				<%= Html.EditorFor(x => x.Email) %>
				<%= Html.ValidationMessageFor(x => x.Email) %>
			</p>
			<p class="formCommands">
				<%= Html.AntiForgeryToken() %>
				<%= Html.HiddenFor(x => x.Id) %>
				<input type="submit" value="Save" class="btnBig" />
			</p>
		</fieldset>
    <% } %>
	<p>
		<%= Html.ActionLink("Tillbaka till leverantörer", "Index") %>
	</p>
	<% Html.RenderPartial("ClientValidationScripts"); %>