
function StatisticsViewModel(model) {
    var self = this;
    self.selectedContract = ko.observable(model.SelectedContractId);
    self.selectedCompany = ko.observable(model.SelectedContractCompanyId);
    self.selectedReportType = ko.observable(model.SelectedReportType);
    self.contracts = ko.observableArray(model.Contracts);
    self.companies = ko.observableArray(model.Companies);
    self.reportTypes = ko.observableArray(model.ReportTypes);
    
    self.selectableCompanies = ko.computed(function () {
        if (self.selectedContract()) {
            return ko.utils.arrayFilter(self.companies(), function (item) {
                return item.ContractId === self.selectedContract();
            });
        } else {
            return ko.observableArray([]);
        }
    }, self);

    self.selectableReportTypes = ko.computed(function () {
        if (self.selectedContract()) {
            return ko.utils.arrayFilter(self.reportTypes(), function (item) {
                return item.Id;
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

    self.selectedReportType = ko.computed(function () {
        return ko.utils.arrayFirst(self.reportTypes(), function (item) { return item.Id == self.selectedReportType(); }).Name;
    }, self);
}