<%@ Page MasterPageFile="~/Components/Synologen/SynologenMain.master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.FrameEditView>" %>
<asp:Content ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-Frame-Add-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.EnableClientValidation(); %>
			<%= Html.ValidationSummary(true) %>
			<% using (Html.BeginForm()) {%>
				<fieldset>
					<legend>Skapa ny båge</legend>
					<%--
					<p class="formItem">
						<%= Html.LabelFor(x => x.Id) %>
						<%= Html.EditorFor(x => x.Id) %>
						<%= Html.ValidationMessageFor(x => x.Id) %>
					</p>
					--%>
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
						<%--<%= Html.EditorFor(x => x.Color) %>--%>
						<%= Html.DropDownListFor(x => x.ColorId, new SelectList(Model.AvailableFrameColors, "Id", "Name", Model.ColorId), "-- Välj Färg --")%>
						<%= Html.ValidationMessageFor(x => x.ColorId) %>
					</p>
					<p class="formItem">
						<%= Html.LabelFor(x => x.BrandId) %>
						<%--<%= Html.EditorFor(x => x.Brand) %>--%>
						<%= Html.DropDownListFor(x => x.BrandId, new SelectList(Model.AvailableFrameBrands, "Id", "Name", Model.BrandId), "-- Välj Märke --")%>
						<%= Html.ValidationMessageFor(x => x.BrandId) %>
					</p>
					<fieldset class="interval-formItem">
						<legend>Index</legend>
						<p class="formItem">
							<%= Html.LabelFor(x => x.IndexMinValue) %>
							<%= Html.EditorFor(x => x.IndexMinValue) %>
							<%= Html.ValidationMessageFor(x => x.IndexMinValue) %>
						</p>
						<p class="formItem">
							<%= Html.LabelFor(x => x.IndexMaxValue) %>
							<%= Html.EditorFor(x => x.IndexMaxValue) %>
							<%= Html.ValidationMessageFor(x => x.IndexMaxValue) %>
						</p>
						<p class="formItem">
							<%= Html.LabelFor(x => x.IndexIncrementation) %>
							<%= Html.EditorFor(x => x.IndexIncrementation) %>
							<%= Html.ValidationMessageFor(x => x.IndexIncrementation) %>
						</p>
					</fieldset>
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
				<%= Html.ActionLink("Back to index", "Index") %>
			</p>
		</div>
	</div>
</div>	
</asp:Content>