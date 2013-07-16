using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
    public class StatisticsView
    {
        public StatisticsView()
        {
            Contracts = new List<ContractListItem>();
            Companies = new List<CompanyListItem>();
            Download = new DownloadContext();
        }

        [DisplayName("Avtal"), Required]
        public int SelectedContractId { get; set; }
        public List<ContractListItem> Contracts { get; set; }

        [DisplayName("Företag")]
        public int? SelectedContractCompanyId { get; set; }
        public List<CompanyListItem> Companies { get; set; }

        [DisplayName("Från")]
        public DateTime? From { get; set; }

        [DisplayName("To")]
        public DateTime? To { get; set; }

        public DownloadContext Download { get; set; }

        public SelectList GetContractsSelectList()
        {
            return new SelectList(Contracts, "Id", "Name");
        }

        public SelectList GetCompaniesSelectList()
        {
            return new SelectList(Companies, "Id", "Name");
        }
    }

    public class DownloadContext
    {
        public bool DisplayUrl { get; protected set; }
        public string Url { get; protected set; }
        public void Set(string url)
        {
            Url = url;
            DisplayUrl = true;
        }

        public void Reset()
        {
            Url = null;
            DisplayUrl = false;
        }
    }

    public class ContractListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CompanyListItem
    {
        public int Id { get; set; }
        public int ContractId { get; set; }
        public string Name { get; set; }
    }
}