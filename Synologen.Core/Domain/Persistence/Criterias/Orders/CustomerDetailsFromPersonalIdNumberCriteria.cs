using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
    public class CustomerDetailsFromPersonalIdNumberCriteria : IActionCriteria
    {
        public string PersonalIdNumber { get; set; }
    }
}