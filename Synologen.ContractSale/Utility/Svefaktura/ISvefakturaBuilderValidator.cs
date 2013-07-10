using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura
{
    public interface ISvefakturaBuilderValidator
    {
        void Validate(IOrder order);
        void Validate(ISvefakturaConversionSettings settings);
    }
}