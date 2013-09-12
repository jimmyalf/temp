
function StatisticsViewModel(model) {
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
    
    self.selectedCompanyName = ko.computed(function () {
        if (self.selectedCompany()) {
            return ko.utils.arrayFirst(self.companies(), function (item) { return item.Id == self.selectedCompany(); }).Name;
        }
        return null;
    }, self);
    
    self.selectedContractName = ko.computed(function () {
        if(self.selectedContract()) {
            return ko.utils.arrayFirst(self.contracts(), function (item) { return item.Id == self.selectedContract(); }).Name;
        }
        return null;
    }, self);
}