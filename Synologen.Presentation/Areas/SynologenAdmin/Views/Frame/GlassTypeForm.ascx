<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameGlassTypeEditView>" %>
	<% Html.EnableClientValidation(); %>
	<%= Html.ValidationSummary(true) %>
	<% using (Html.BeginForm()) {%>
		<fieldset>
			<legend><%=Html.Encode(Model.FormLegend) %></legend>
			<p class="formItem">
				<%= Html.LabelFor(x => x.SupplierId) %>
				<%= Html.WpcDropDownListFor(x => x.SupplierId, x => x.AvailableFrameSuppliers, x => x.Id, x => x.Name, "-- Välj Leverantör --") %>
				<%= Html.ValidationMessageFor(x => x.SupplierId) %>
			</p>
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
			<fieldset class="interval-formItem">
				<legend>Sfär</legend>
				<p class="formItem">
					<%= Html.LabelFor(x => x.SphereMinValue) %>
					<%= Html.EditorFor(x => x.SphereMinValue) %>
					<%= Html.ValidationMessageFor(x => x.SphereMinValue) %>
				</p>
				<p class="formItem">
					<%= Html.LabelFor(x => x.SphereMaxValue) %>
					<%= Html.EditorFor(x => x.SphereMaxValue) %>
					<%= Html.ValidationMessageFor(x => x.SphereMaxValue) %>
				</p>
				<p class="formItem">
					<%= Html.LabelFor(x => x.SphereIncrementation) %>
					<%= Html.EditorFor(x => x.SphereIncrementation) %>
					<%= Html.ValidationMessageFor(x => x.SphereIncrementation) %>
				</p>
			</fieldset>
			
			<fieldset class="interval-formItem">
				<legend>Cylinder</legend>
				<p class="formItem">
					<%= Html.LabelFor(x => x.CylinderMinValue) %>
					<%= Html.EditorFor(x => x.CylinderMinValue) %>
					<%= Html.ValidationMessageFor(x => x.CylinderMinValue) %>
				</p>
				<p class="formItem">
					<%= Html.LabelFor(x => x.CylinderMaxValue) %>
					<%= Html.EditorFor(x => x.CylinderMaxValue) %>
					<%= Html.ValidationMessageFor(x => x.CylinderMaxValue) %>
				</p>
				<p class="formItem">
					<%= Html.LabelFor(x => x.CylinderIncrementation) %>
					<%= Html.EditorFor(x => x.CylinderIncrementation) %>
					<%= Html.ValidationMessageFor(x => x.CylinderIncrementation) %>
				</p>
			</fieldset>			
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
