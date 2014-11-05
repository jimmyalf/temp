namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
    public class OrderStatisticsSummaryRow
    {
	    public string Ort { get; set; }
        public string Butik { get; set; }
        public string Beställare { get; set; }
        public string Artikel { get; set; }
        public int Kvantitet { get; set; }
        public decimal Värde { get; set; }
    }
}