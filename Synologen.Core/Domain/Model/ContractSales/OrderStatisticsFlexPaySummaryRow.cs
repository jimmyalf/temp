namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
    using System;

    public class OrderStatisticsFlexPaySummaryRow
    {
        public int LeverantörsId { get; set; }
        public long Fakturanr { get; set; }
        public int? FörmånsId { get; set; }
        
        public string Period { get; set; }
        public string Arbetsgivare { get; set; }
        public string ArbgivOrgnr { get; set; }
        public string Personnr { get; set; }
        public int? TjänstEllerProdukt { get; set; }
        public decimal PrisExklMoms { get; set; }
    }
}
