using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class FindCustomerByPersonalNumberCriteria : IActionCriteria
	{
		public string PersonalNumber { get; set; }

		public FindCustomerByPersonalNumberCriteria(string personalNumber)
		{
			PersonalNumber = personalNumber;
		}
	}
}