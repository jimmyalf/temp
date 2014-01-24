using Spinit.Wpc.Synologen.Core.Attributes;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes
{
    public enum SubscriptionItemStatus
    {
        [EnumDisplayName("Aktiv")]
        Active = 1,

        [EnumDisplayName("Stoppad")]
        Stopped = 2,

        [EnumDisplayName("Utgången")]
        Expired = 3
    }
}