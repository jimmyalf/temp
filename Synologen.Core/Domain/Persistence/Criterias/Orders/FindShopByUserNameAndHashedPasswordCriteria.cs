using Spinit.Data;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders
{
	public class FindShopByUserNameAndHashedPasswordCriteria : IActionCriteria
	{
		public string UserName { get; set; }
		public string HashedPassword { get; set; }

		public FindShopByUserNameAndHashedPasswordCriteria(string userName, string hashedPassword)
		{
			UserName = userName;
			HashedPassword = hashedPassword;
		}
	}
}