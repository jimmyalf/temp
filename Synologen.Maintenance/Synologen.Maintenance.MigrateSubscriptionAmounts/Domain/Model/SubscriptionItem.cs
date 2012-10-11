using System.Data;
using Synologen.Maintenance.MigrateSubscriptionAmounts.Persistence;

namespace Synologen.Maintenance.MigrateSubscriptionAmounts.Domain.Model
{
    public class SubscriptionItem
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal FeePrice { get; set; }

        public static SubscriptionItem Parse(IDataRecord record)
        {
            return Data.CreateParser<SubscriptionItem>(record)
                .Parse(x => x.Id)
                .Parse(x => x.SubscriptionId)
                .Parse(x => x.ProductPrice)
                .Parse(x => x.FeePrice)
                .GetValue();
        }
    }
}
