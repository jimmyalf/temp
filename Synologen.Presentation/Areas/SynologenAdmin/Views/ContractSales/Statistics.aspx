<%@ Page MasterPageFile="~/Areas/SynologenAdmin/Views/Shared/SynologenMVC.Master" Inherits="System.Web.Mvc.ViewPage<Spinit.Wpc.Synologen.Presentation.Models.ContractSales.StatisticsView>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="SubMenu" runat="server">
<% Html.RenderPartial("ContractSalesSubMenu"); %>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script>
    $(document).ready(function () {
        function statisticsViewModel(model) {
            var self = this;
            self.selectedContract = ko.observable(model.SelectedContractId);
            self.selectedCompany = ko.observable(model.SelectedContractCompanyId);            
            self.contracts = ko.observableArray(model.Contracts);
            self.companies = ko.observableArray(model.Companies);    
            self.selectableCompanies = ko.computed(function () {
                if (self.selectedContract()) {
                    return ko.utils.arrayFilter(self.companies(), function (item) {
                        return item.ContractId === self.selectedContract();
                    });
                } else {
                    return ko.observableArray([]);
                }
            }, self);
        }

        var viewModel = new statisticsViewModel(<%= new JavaScriptSerializer().Serialize(Model) %>);
        ko.applyBindings(viewModel);
    });
</script>    
<div id="dCompMain" class="Components-Synologen-ContractSales-OrderView-aspx">
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
					<%= Html.LabelFor(x => x.From) %>
					<%= Html.TextBoxFor(x => x.From, new { type = "date", @class = "datepicker" }) %>
				</div>
				<div class="formItem">
					<%= Html.LabelFor(x => x.To) %>
					<%= Html.TextBoxFor(x => x.To, new { type = "date", @class = "datepicker"}) %>
				</div>
                <div class="formCommands">
                    <button type="submit" class="btnBig" data-bind="enable: selectedContract()">Generera</button>
                </div>
			</fieldset>
            <% } %>		
		</div>
	</div>
</div>	
</asp:Content>
