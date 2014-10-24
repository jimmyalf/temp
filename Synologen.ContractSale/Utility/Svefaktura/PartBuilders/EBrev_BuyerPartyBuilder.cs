using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.PartBuilders
{
    public class EBrev_BuyerPartyBuilder : BuyerPartyBuilder
    {
        public EBrev_BuyerPartyBuilder(ISvefakturaConversionSettings settings, ISvefakturaFormatter formatter)
            : base(settings, formatter) { }

        protected override List<SFTIPartyIdentificationType> GetPartyIdentification(ICompany company)
        {
            return GetPartyIdentification(company, x => x.OrganizationNumber);
        }
    }
}