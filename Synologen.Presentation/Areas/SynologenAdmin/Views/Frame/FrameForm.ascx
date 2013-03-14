<%@ Control Inherits="System.Web.Mvc.ViewUserControl<FrameEditView>" %>

	<% Html.EnableClientValidation(); %>
	<% using (Html.BeginForm()) {%>
		<fieldset>
			<legend><%=Html.DisplayFor(x => x.FormLegend) %></legend>
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
			<p class="formItem">
				<%= Html.LabelFor(x => x.ArticleNumber) %>
				<%= Html.EditorFor(x => x.ArticleNumber) %>
				<%= Html.ValidationMessageFor(x => x.ArticleNumber) %>
			</p>
			<p class="formItem">
				<%= Html.LabelFor(x => x.ColorId) %>
				<%= Html.WpcDropDownListFor(x => x.ColorId, x => x.AvailableFrameColors, x => x.Id, x => x.Name, "-- Välj Färg --") %>
				<%= Html.ValidationMessageFor(x => x.ColorId) %>
			</p>
			<p class="formItem">
				<%= Html.LabelFor(x => x.BrandId) %>
				<%= Html.WpcDropDownListFor(x => x.BrandId, x=> x.AvailableFrameBrands, x => x.Id, x => x.Name, "-- Välj Märke --")%>
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
			<fieldset>
				<legend>Lagersaldo</legend>
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.CurrentStock) %>
					<%= Html.DisplayFor(x => x.CurrentStock) %>				
				</p>
				<p class="display-item clearLeft">
					<%= Html.LabelFor(x => x.StockDate) %>
					<%= Html.DisplayFor(x => x.StockDate) %>				
				</p>	
				<p class="formItem clearLeft">
					<%= Html.LabelFor(x => x.StockAtStockDate) %>
					<%= Html.EditorFor(x => x.StockAtStockDate) %>	
					<%= Html.ValidationMessageFor(x => x.StockAtStockDate) %>
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
				<input type="submit" value="Spara båge" class="btnBig" />
			</p>
		</fieldset>
    <% } %>
	<p>
		<%= Html.ActionLink("Tillbaka till bågar", "Index") %>
	</p>
		