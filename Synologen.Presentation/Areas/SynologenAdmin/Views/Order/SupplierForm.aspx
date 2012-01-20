<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<SupplierFormView>" %>
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
				<legend>Skapa ny leverantör</legend>
				<% } else{%>
				<legend>Redigera leverantör</legend>
				<% } %>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.Name)%>
						<%= Html.EditorFor(x => x.Name)%>
						<%= Html.ValidationMessageFor(x => x.Name) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.ShipToStore)%>
						<%= Html.CheckBoxFor(x => x.ShipToStore)%>
					</p>
					<p class="formItem">
						<%= Html.LabelFor(x => x.ShipToCustomer)%>
						<%= Html.CheckBoxFor(x => x.ShipToCustomer)%>
					</p>
					<p class="formItem">
						<%= Html.LabelFor(x => x.DeliveredOverCounter)%>
						<%= Html.CheckBoxFor(x => x.DeliveredOverCounter)%>
					</p>
					<p class="formItem formCommands">
						<%= Html.AntiForgeryToken() %>
						<%= Html.HiddenFor(x => x.Id) %>
						<input type="submit" value="Spara leverantör" class="btnBig" />
					</p>	
					<p class="display-item clearLeft">
						<a href='<%= Url.Action("Suppliers") %>'>Tillbaka till leverantörer &raquo;</a>
					</p>
				</fieldset>									
				<% } %>		
		</div>
	</div>
</div>	
</asp:Content>