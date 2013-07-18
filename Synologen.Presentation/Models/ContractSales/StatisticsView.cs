using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Data.Queries.ContractSales;

namespace Spinit.Wpc.Synologen.Presentation.Models.ContractSales
{
    public class StatisticsView
    {
        public StatisticsView()
        {
            Contracts = new List<ContractListItem>();
            Companies = new List<CompanyListItem>();
        }

        [DisplayName("Avtal"), Required]
        public int SelectedContractId { get; set; }
        public List<ContractListItem> Contracts { get; set; }
        public string SelectedContractName { get; set; }

        [DisplayName("F�retag")]
        public int? SelectedContractCompanyId { get; set; }
        public List<CompanyListItem> Companies { get; set; }
        public string SelectedContractCompanyName { get; set; }

        [DisplayName("Fr�n")]
        public DateTime? From { get; set; }

        [DisplayName("To")]
        public DateTime? To { get; set; }

        public SelectList GetContractsSelectList()
        {
            return new SelectList(Contracts, "Id", "Name");
        }

        public SelectList GetCompaniesSelectList()
        {
            return new SelectList(Companies, "Id", "Name");
        }

        public StatisticsQueryArgument GetQueryArgument()
        {
            return new StatisticsQueryArgument
            {
                CompanyId = SelectedContractCompanyId,
                ContractId = SelectedContractId,
                From = From,
                To = To.HasValue ? To.Value.AddDays(1) : (DateTime?)null
            };
        }

        public string Serialize()
        {
            return new JavaScriptSerializer().Serialize(this);
        }

        public string CreateFileName()
        {
            return "Statistik{SelectedContractCompany}{Interval}.xlsx"
                .ReplaceWith(new
                {
                    SelectedContractCompany = GetContractCompanyFormat(), 
                    Interval = GetCurrentIntervalFormat()
                });
        }

        protected string GetContractCompanyFormat()
        {
            if (!string.IsNullOrEmpty(SelectedContractCompanyName) && !string.IsNullOrEmpty(SelectedContractName))
            {
                return string.Format(" {0} - {1}", SelectedContractName, SelectedContractCompanyName);
            }

            if (!string.IsNullOrEmpty(SelectedContractName))
            {
                return string.Format(" {0}", SelectedContractName);
            }
            return null;
        }

        protected string GetCurrentIntervalFormat()
        {
            if (From.HasValue && To.HasValue)
            {
                return string.Format(" {0} - {1}", From.Value.ToShortDateString(), To.Value.ToShortDateString());
            }

            if (From.HasValue)
            {
                return string.Format(" Fr�n {0}", From.Value.ToShortDateString());
            }

            if (To.HasValue)
            {
                return string.Format(" Till {0}", To.Value.ToShortDateString());
            }
            return null;
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