using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Business.Domain.Enumerations
{

    public enum StatisticsReportTypes
    {
        [EnumDisplayName("Standard")]
        Default = 1,
        [EnumDisplayName("Utökad/FlexPay")]
        FlexPay= 2
    }
}