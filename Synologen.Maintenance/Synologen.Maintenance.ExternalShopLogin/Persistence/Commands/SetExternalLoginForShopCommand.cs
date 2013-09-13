using Spinit.Data.SqlClient.SqlBuilder;
using Synologen.Maintenance.ExternalShopLogin.Domain.Model;
using Synologen.Maintenance.ExternalShopLogin.Domain.Services;

namespace Synologen.Maintenance.ExternalShopLogin.Persistence.Commands
{
	public class SetExternalLoginForShopCommand : PersistenceBase
	{
		private readonly Shop _shop;
		private readonly IHashService _hashService;
		private readonly IPasswordService _passwordService;

		public SetExternalLoginForShopCommand(Shop shop, IHashService hashService, IPasswordService passwordService)
		{
			_shop = shop;
			_hashService = hashService;
			_passwordService = passwordService;
		}

		public SetExternalLoginForShopResult Execute()
		{
			var userName = _shop.Number;
			var password = _passwordService.GeneratePassword();
			var hashedPassword = _hashService.GetHash(password);
			var command = CommandBuilder.Build(@"UPDATE tblSynologenShop SET 
				cExternalAccessUsername = @UserName,
				cExternalAccessHashedPassword = @HashedPassword 
				WHERE cId = @Id")
				.AddParameters(new {UserName = userName, HashedPassword = hashedPassword, Id = _shop.Id});
			Execute(command);
			return new SetExternalLoginForShopResult
			{
				UserName	= userName,
				HashedPassword = hashedPassword,
				Password = password,
				Shop = _shop,
			};
		}
	}
}