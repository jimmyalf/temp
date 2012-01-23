using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services.Web.External;

namespace Synologen.Service.Web.External.App
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class Service : IAddCustomerService
	{
		private readonly IShopAuthenticationService _shopAuthenticationService;
		private readonly IOrderCustomerRepository _customerRepository;
		private readonly IShopRepository _shopRepository;
		private readonly ICustomerParser _customerParser;
		private readonly ICustomerValidator _customerValidator;

		public Service(
			IShopAuthenticationService shopAuthenticationService, 
			IOrderCustomerRepository customerRepository,
			IShopRepository shopRepository,
			ICustomerParser customerParser, 
			ICustomerValidator customerValidator)
		{
			_shopAuthenticationService = shopAuthenticationService;
			_customerRepository = customerRepository;
			_shopRepository = shopRepository;
			_customerParser = customerParser;
			_customerValidator = customerValidator;
		}

		public void AddCustomer(AuthenticationContext authenticationContext, Customer customer)
		{
			var result = CheckAuthentication(authenticationContext);
			ValidateInput(customer);
			var shop = _shopRepository.Get(result.ShopId);
			var customerToSave = _customerParser.Parse(customer, shop);
			_customerRepository.Save(customerToSave);
		}

		private ShopAuthenticationResult CheckAuthentication(AuthenticationContext context)
		{
			var authenticationResult = _shopAuthenticationService.Authenticate(context);
			if(!authenticationResult.IsAuthenticated) throw new AuthenticationFailedException("Context cannot be authenticated!");
			return authenticationResult;
		}

		private void ValidateInput(Customer customer)
		{
			var validationResult = _customerValidator.Validate(customer);
			if(validationResult.HasErrors) throw new ValidationFailedException(validationResult);
		}
	}


}