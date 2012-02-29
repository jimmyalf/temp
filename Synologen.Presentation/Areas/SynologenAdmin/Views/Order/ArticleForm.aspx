<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<ArticleFormView>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("OrderSubMenu"); %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div id="dCompMain" class="Components-Synologen-Order-Category-Edit-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.EnableClientValidation(); %>
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<%if(Model.IsCreate){ %>
				<legend>Skapa ny artikel</legend>
				<% } else{%>
				<legend>Redigera artikel</legend>
				<% } %>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.SupplierId) %>
						<%= Html.DropDownListFor(x => x.SupplierId, Model.Suppliers, optionLabel: "-- Välj Leverantör --") %>
						<%= Html.ValidationMessageFor(x => x.SupplierId) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.TypeId) %>
						<%= Html.DropDownListFor(x => x.TypeId, Model.Types, optionLabel: "-- Välj Artikeltyp --") %>
						<%= Html.ValidationMessageFor(x => x.TypeId) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.Name)%>
						<%= Html.EditorFor(x => x.Name)%>
						<%= Html.ValidationMessageFor(x => x.Name) %>
					</p>
					<fieldset class="clearLeft">
						<legend>Synliga parametrar</legend>
						<p class="formItem clearLeft">
							<%=Html.LabelFor(x => x.EnableAddition)%>
							<%=Html.EditorFor(x => x.EnableAddition)%>
						</p>
						<p class="formItem">
							<%=Html.LabelFor(x => x.EnableAxis)%>
							<%=Html.EditorFor(x => x.EnableAxis)%>
						</p>
						<p class="formItem">
							<%=Html.LabelFor(x => x.EnableCylinder)%>
							<%=Html.EditorFor(x => x.EnableCylinder)%>
						</p>
					</fieldset>
					<fieldset class="clearLeft">
						<legend><%=Html.GetDisplayNameFor(x => x.BaseCurve)%></legend>
						<p class="formItem clearLeft">
							<%=Html.LabelFor(x => x.BaseCurve.Min)%>
							<%=Html.EditorFor(x => x.BaseCurve.Min)%>
							<%=Html.ValidationMessageFor(x => x.BaseCurve.Min)%>
						</p>
						<p class="formItem">
							<%=Html.LabelFor(x => x.BaseCurve.Max)%>
							<%=Html.EditorFor(x => x.BaseCurve.Max)%>
							<%=Html.ValidationMessageFor(x => x.BaseCurve.Max)%>
						</p>
						<p class="formItem">
							<%=Html.LabelFor(x => x.BaseCurve.Increment)%>
							<%=Html.EditorFor(x => x.BaseCurve.Increment)%>
							<%=Html.ValidationMessageFor(x => x.BaseCurve.Increment)%>
						</p>
					</fieldset>
					<fieldset class="clearLeft">
						<legend><%=Html.GetDisplayNameFor(x => x.Diameter)%></legend>
							<p class="formItem clearLeft">
								<%=Html.LabelFor(x => x.Diameter.Min)%>
								<%=Html.EditorFor(x => x.Diameter.Min)%>
								<%=Html.ValidationMessageFor(x => x.Diameter.Min)%>
							</p>
							<p class="formItem">
								<%=Html.LabelFor(x => x.Diameter.Max)%>
								<%=Html.EditorFor(x => x.Diameter.Max)%>
								<%=Html.ValidationMessageFor(x => x.Diameter.Max)%>
							</p>
							<p class="formItem">
								<%=Html.LabelFor(x => x.Diameter.Increment)%>
								<%=Html.EditorFor(x => x.Diameter.Increment)%>
								<%=Html.ValidationMessageFor(x => x.Diameter.Increment)%>
							</p>
					</fieldset>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.Active)%>
						<%= Html.EditorFor(x => x.Active)%>
					</p>
					<p class="formItem formCommands">
						<%= Html.AntiForgeryToken() %>
						<%= Html.HiddenFor(x => x.Id) %>
						<input type="submit" value="Spara artikel" class="btnBig" />
					</p>	
					<p class="display-item clearLeft">
						<a href='<%= Url.Action("Articles") %>'>Tillbaka till artiklar &raquo;</a>
					</p>
				</fieldset>									
				<% } %>		
		</div>
	</div>
</div>	
</asp:Content>