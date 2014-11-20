namespace Spinit.Wpc.Synologen.Core.Domain.Model.ContractSales
{
    public class EdiAddress
    {
        public EdiAddress(string address)
        {
            Address = address;
            Quantifier = null;
        }

        public EdiAddress(string address, string quantifier)
            : this(address)
        {
            Quantifier = quantifier;
        }

        public string Address { get; set; }
        public string Quantifier { get; set; }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Quantifier)
                ? Address
                : string.Format("{0}:{1}", Address, Quantifier);
        }
    }
}