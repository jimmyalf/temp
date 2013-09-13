using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.BGServer
{
    public enum SectionType
    {
        [EnumDisplayName("ReceivedConsents")]
        ReceivedConsents = 1,

        [EnumDisplayName("ReceivedPayments")]
        ReceivedPayments = 2,

        [EnumDisplayName("ReceivedErrors")]
        ReceivedErrors = 3,

        [EnumDisplayName("PaymentsToSend")]
        PaymentsToSend = 4,

        [EnumDisplayName("ConsentsToSend")]
        ConsentsToSend = 5
    }
}
