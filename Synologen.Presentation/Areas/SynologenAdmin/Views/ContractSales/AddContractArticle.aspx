<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<AddContractArticleView>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.15/jquery-ui.js"></script>
<div id="dCompMain" class="Components-Synologen-ContractSales-AddContractArticle-aspx">
	<div class="fullBox">
		<div class="wrap">
			<% Html.EnableClientValidation(); %>
			<%= Html.ValidationSummary(true) %>
			<% using (Html.BeginForm(null, null, FormMethod.Post, new {id="contract-article-form"})) {%>
			<fieldset>
				<legend>Koppla artikel till <%=Model.ContractName%></legend>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.ArticleId) %>
						<%= Html.DropDownListFor(x => x.ArticleId, Model.Articles, "-- Välj artikel --", new { @class = "postback-enabled" }) %>
						<%= Html.ValidationMessageFor(x => x.ArticleId) %>
					</p>
					<p class="formItem clearLeft">
						<%= Html.LabelFor(x => x.PriceWithoutVAT) %>
						<%= Html.EditorFor(x => x.PriceWithoutVAT) %>
						<%= Html.ValidationMessageFor(x => x.PriceWithoutVAT) %>
					</p>
					<p class="formItem">
						<%= Html.LabelFor(x => x.SPCSAccountNumber) %>
						<%= Html.TextBoxFor(x => x.SPCSAccountNumber, new { id = "spcs-account-number"}) %>
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
						<%= Html.HiddenFor(x => x.ContractId) %>
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

<script type="text/javascript">
	$(document).ready(function() {
		$('.postback-enabled').change(function() {
			var selectedArticle = $(this).attr('value');
			if (selectedArticle > 0) {
				console.log(selectedArticle);
				var url = "/components/synologen/contract-sales/article/".concat(selectedArticle,"/json");
				$.getJSON(url, null, function(data) {
					if (data && data.SPCSAccountNumber) {
						var spcsAccountNumberTextBox = $('#spcs-account-number');
						spcsAccountNumberTextBox.val(data.SPCSAccountNumber);
						spcsAccountNumberTextBox
							.animate({ backgroundColor: '#FFDDDD' }, 500)
							.animate({ backgroundColor: '#FFFFFF' }, 500);
					}
				});
			}
		});
	});
</script>

<% Html.RenderPartial("ClientValidationScripts"); %>
</asp:Content>