<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<EditContractArticleView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractSales-AddContractArticle-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.EnableClientValidation(); %>
			<%= Html.ValidationSummary(true) %>
			<% using (Html.BeginForm()) {%>
			<fieldset>
				<legend>Uppdatera koppling mellan <%=Model.ArticleName %> och <%=Model.ContractName%></legend>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.PriceWithoutVAT) %>
						<%= Html.EditorFor(x => x.PriceWithoutVAT) %>
						<%= Html.ValidationMessageFor(x => x.PriceWithoutVAT) %>
					</p>
					<p class="formItem">
						<%= Html.LabelFor(x => x.SPCSAccountNumber) %>
						<%= Html.EditorFor(x => x.SPCSAccountNumber) %>
						<%= Html.ValidationMessageFor(x => x.SPCSAccountNumber) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.IsActive) %>
						<%= Html.EditorFor(x => x.IsActive) %>
						<%= Html.ValidationMessageFor(x => x.IsActive) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.AllowCustomPricing) %>
						<%= Html.EditorFor(x => x.AllowCustomPricing) %>
						<%= Html.ValidationMessageFor(x => x.AllowCustomPricing) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.IsVATFreeArticle) %>
						<%= Html.EditorFor(x => x.IsVATFreeArticle) %>
						<%= Html.ValidationMessageFor(x => x.IsVATFreeArticle) %>
					</p>
					<p class="formCommands">
						<%= Html.AntiForgeryToken() %>
						<%= Html.HiddenFor(x => x.Id) %>
						<input type="submit" value="Spara" class="btnBig" />
					</p>
			</fieldset>
			<%} %>
			<p>
				<a href="<%=Model.ContractArticleListUrl %>">Tillbaka till avtals-artiklar</a>
			</p>
		</div>
	</div>
</div>	
<% Html.RenderPartial("ClientValidationScripts"); %>
</asp:Content>