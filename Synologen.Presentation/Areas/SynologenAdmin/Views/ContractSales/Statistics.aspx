<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.ContractSales.StatisticsView>" %>

<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
<script src="<%=Url.Content("~/Components/Synologen/Scripts/ContractSales/statistics.js")%>"></script> 
<script>
    $(document).ready(function () {
        var viewModel = new StatisticsViewModel(<%=Model.Serialize()%>);
        ko.applyBindings(viewModel);
    });
</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("ContractSalesSubMenu"); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="dCompMain" class="Components-Synologen-ContractSales-Statistics-aspx">
	<div class="fullBox">
		<div class="wrap">
		    <% Html.EnableClientValidation(); %>
		    <%= Html.ValidationSummary(true) %>
	        <% using (Html.BeginForm()) { %>
			<fieldset>
				<legend>Generera statistik</legend>
				<div class="formItem clearLeft">
					<%= Html.LabelFor(x => x.SelectedContractId) %>
                    <%= Html.DropDownListFor(x => x.SelectedContractId, Model.GetContractsSelectList(), "-- Välj avtal --", new Dictionary<string, object> { {"data-bind", "options: contracts, optionsText: 'Name', optionsValue: 'Id', value: selectedContract, optionsCaption: '-- Välj avtal --'"} }) %>
                    <%= Html.ValidationMessageFor(x => x.SelectedContractId) %>
				</div>
				<div class="formItem clearLeft">
					<%= Html.LabelFor(x => x.SelectedContractCompanyId) %>
					<%= Html.DropDownListFor(x => x.SelectedContractCompanyId, Model.GetCompaniesSelectList(), "-- Välj företag --", new Dictionary<string, object> { {"data-bind", "options: selectableCompanies, optionsText: 'Name', optionsValue: 'Id', value: selectedCompany, optionsCaption: '-- Välj företag --'"} }) %>
				</div>
				<div class="formItem clearLeft">
					<%= Html.LabelFor(x => x.SelectedReportTypeId) %>
					<%= Html.DropDownListFor(x => x.SelectedReportTypeId, Model.GetReportTypes()) %>
				</div>
				<div class="formItem clearLeft">
					<%= Html.LabelFor(x => x.From) %>
					<%= Html.TextBoxFor(x => x.From, new { type = "date", @class = "datepicker" }) %>
				</div>
				<div class="formItem">
					<%= Html.LabelFor(x => x.To) %>
					<%= Html.TextBoxFor(x => x.To, new { type = "date", @class = "datepicker"}) %>
				</div>
                <div class="formCommands">
                    <input type="hidden" id="SelectedContractName" name="SelectedContractName" data-bind="value: selectedContractName()"/>
                    <input type="hidden" id="SelectedContractCompanyName" name="SelectedContractCompanyName" data-bind="value: selectedCompanyName()"/>
                    <button type="submit" class="btnBig" data-bind="enable: selectedContract()">Generera</button>
                </div>
			</fieldset>	
            <% } %>
		</div>
	</div>
</div>	
</asp:Content>
