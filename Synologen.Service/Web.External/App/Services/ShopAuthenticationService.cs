using System.Linq;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Domain.Services.Web.External;

namespace Synologen.Service.Web.External.App.Services
{
	public class ShopAuthenticationService : IShopAuthenticationService 
	{
		private readonly IShopRepository _shopRepository;
		private readonly IHashService _hashService;

		public ShopAuthenticationService(IShopRepository shopRepository, IHashService hashService)
		{
			_shopRepository = shopRepository;
			_hashService = hashService;
		}

		public ShopAuthenticationResult Authenticate(string username, string password)
		{
			if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return new ShopAuthenticationResult(false);
			var hashedPassword = _hashService.GetHash(password);
			var shop = _shopRepository.FindBy(new FindShopByUserNameAndHashedPasswordCriteria(username, hashedPassword)).FirstOrDefault();
			return shop == null ? new ShopAuthenticationResult(false) : new ShopAuthenticationResult(true, shop.Id);
		}

		public ShopAuthenticationResult Authenticate(AuthenticationContext context)
		{
			return Authenticate(context.With(x => x.UserName), context.With(x => x.Password));
		}
	}
}