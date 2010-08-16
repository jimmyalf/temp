<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameEditView>" %>

	<% Html.EnableClientValidation(); %>
	<% using (Html.BeginForm()) {%>
		<fieldset>
			<legend><%=Html.DisplayFor(x => x.FormLegend) %></legend>
			
			<p class="formItem">
				<%= Html.LabelFor(x => x.Name) %>
				<%= Html.EditorFor(x => x.Name) %>
				<%= Html.ValidationMessageFor(x => x.Name) %>
			</p>
			<p class="formItem">
				<%= Html.LabelFor(x => x.ArticleNumber) %>
				<%= Html.EditorFor(x => x.ArticleNumber) %>
				<%= Html.ValidationMessageFor(x => x.ArticleNumber) %>
			</p>
			<p class="formItem">
				<%= Html.LabelFor(x => x.ColorId) %>
				<%= Html.DropDownListFor(x => x.ColorId, new SelectList(Model.AvailableFrameColors, "Id", "Name", Model.ColorId), "-- Välj Färg --")%>
				<%= Html.ValidationMessageFor(x => x.ColorId) %>
			</p>
			<p class="formItem">
				<%= Html.LabelFor(x => x.BrandId) %>
				<%= Html.DropDownListFor(x => x.BrandId, new SelectList(Model.AvailableFrameBrands, "Id", "Name", Model.BrandId), "-- Välj Märke --")%>
				<%= Html.ValidationMessageFor(x => x.BrandId) %>
			</p>
			<fieldset class="interval-formItem">
				<legend>Pupilldistans (PD)</legend>
				<p class="formItem">
					<%= Html.LabelFor(x => x.PupillaryDistanceMinValue) %>
					<%= Html.EditorFor(x => x.PupillaryDistanceMinValue) %>
					<%= Html.ValidationMessageFor(x => x.PupillaryDistanceMinValue) %>
				</p>
				<p class="formItem">
					<%= Html.LabelFor(x => x.PupillaryDistanceMaxValue) %>
					<%= Html.EditorFor(x => x.PupillaryDistanceMaxValue) %>
					<%= Html.ValidationMessageFor(x => x.PupillaryDistanceMaxValue) %>
				</p>
				<p class="formItem">
					<%= Html.LabelFor(x => x.PupillaryDistanceIncrementation) %>
					<%= Html.EditorFor(x => x.PupillaryDistanceIncrementation) %>
					<%= Html.ValidationMessageFor(x => x.PupillaryDistanceIncrementation) %>
				</p>
			</fieldset>
			<p class="formItem">
				<%= Html.LabelFor(x => x.AllowOrders) %>
				<%= Html.EditorFor(x => x.AllowOrders) %>
				<%= Html.ValidationMessageFor(x => x.AllowOrders) %>
			</p>
			<p class="formCommands">
				<%= Html.AntiForgeryToken() %>
				<%= Html.HiddenFor(x => x.Id) %>
				<input type="submit" value="Save" class="btnBig" />
			</p>
		</fieldset>
    <% } %>
	<p>
		<%= Html.ActionLink("Tillbaka till bågar", "Index") %>
	</p>
		