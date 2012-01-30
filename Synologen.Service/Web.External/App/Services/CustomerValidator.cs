using System;
using System.Linq.Expressions;
using Spinit.Wpc.Synologen.Core.Domain.Services.Web.External;

namespace Synologen.Service.Web.External.App.Services
{
	public class CustomerValidator : IValidator<Customer>
	{
		public ValidationResult Validate(Customer customer)
		{
			var result = new ValidationResult<Customer>();
			if(customer == null)
			{
				return result.AddError("Customer was not instantiated (null)");
			}
			CheckPropertyIsNotNull(customer, x => x.Address1, result);
			CheckPropertyIsNotNull(customer, x => x.City, result);
			CheckPropertyIsNotNull(customer, x => x.FirstName, result);
			CheckPropertyIsNotNull(customer, x => x.LastName, result);
			CheckPropertyIsNotNull(customer, x => x.PersonalNumber, result);
			CheckPropertyIsNotNull(customer, x => x.PostalCode, result);
			return result;
		}

		private void CheckPropertyIsNotNull<TType>(TType customer, Expression<Func<TType,object>> propertyExpression, ValidationResult<TType> validationResult) where TType : class
		{
			if(customer == null) return;
			var property = propertyExpression.Compile();
			var value = property(customer);
			if(value == null)
			{
				validationResult.AddMissingPropertyError(propertyExpression);
			}
		}

	}
}