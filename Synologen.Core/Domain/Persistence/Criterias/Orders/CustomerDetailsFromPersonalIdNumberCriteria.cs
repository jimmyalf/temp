using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
    public class CustomerDetailsFromPersonalIdNumberCriteria : IActionCriteria
    {
    	public CustomerDetailsFromPersonalIdNumberCriteria(string personalIdNumber, int shopId)
    	{
    		PersonalIdNumber = personalIdNumber;
    		ShopId = shopId;
    	}

    	public string PersonalIdNumber { get; set; }
		public int ShopId { get; set; }
    }
}