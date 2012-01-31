using System.Linq;
using System.ServiceModel;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Criterias.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
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
		private readonly IValidator<Customer> _customerValidator;
		private readonly ILoggingService _loggingService;

		public Service(
			IShopAuthenticationService shopAuthenticationService, 
			IOrderCustomerRepository customerRepository,
			IShopRepository shopRepository,
			ICustomerParser customerParser, 
			IValidator<Customer> customerValidator,
			ILoggingService loggingService)
		{
			_shopAuthenticationService = shopAuthenticationService;
			_customerRepository = customerRepository;
			_shopRepository = shopRepository;
			_customerParser = customerParser;
			_customerValidator = customerValidator;
			_loggingService = loggingService;
		}

		public AddEntityResponse AddCustomer(AuthenticationContext authenticationContext, Customer customer)
		{
			_loggingService.LogInfo("AddCustomer called");
			var authenticationResult = CheckAuthentication(authenticationContext);
			if(!authenticationResult.IsAuthenticated) return new AddEntityResponse(AddEntityResponseType.AuthenticationFailed);

			var validationResult = ValidateInput(customer);
			if(validationResult.HasErrors) return new AddEntityResponse(AddEntityResponseType.ValidationFailed, validationResult.Errors);

			var foundCustomer = TryGetCustomer(customer);
			if(foundCustomer != null) return new AddEntityResponse(AddEntityResponseType.EntityAlreadyExists);

			var storedCustomer = StoreCustomer(authenticationResult, customer);
			_loggingService.LogInfo("Customer was stored with id {0}", storedCustomer.Id);
			return new AddEntityResponse(AddEntityResponseType.EntityWasAdded);
		}

		private OrderCustomer StoreCustomer(ShopAuthenticationResult authenticationResult, Customer customer)
		{
			var shop = _shopRepository.Get(authenticationResult.ShopId);
			var customerToSave = _customerParser.Parse(customer, shop);
			_customerRepository.Save(customerToSave);
			return customerToSave;
		}

		private ShopAuthenticationResult CheckAuthentication(AuthenticationContext context)
		{
			var authenticationResult = _shopAuthenticationService.Authenticate(context);
			if(!authenticationResult.IsAuthenticated)
			{
				_loggingService.LogWarning("Authentication failed for context " + context);
			}
			return authenticationResult;
		}

		private ValidationResult ValidateInput(Customer customer)
		{
			var validationResult = _customerValidator.Validate(customer);
			if (validationResult.HasErrors)
			{
				_loggingService.LogWarning("Validation failed for customer {0} :\r\nValidationErrors: {1}.", customer, validationResult.GetErrorMessage());	
			}
			return validationResult;
		}

		private OrderCustomer TryGetCustomer(Customer customer)
		{
			return _customerRepository
				.FindBy(new FindCustomerByPersonalNumberCriteria(customer.PersonalNumber))
				.FirstOrDefault();
		}
	}


}