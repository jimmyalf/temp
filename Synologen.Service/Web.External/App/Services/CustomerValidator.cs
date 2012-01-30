using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Spinit.Wpc.Synologen.Core.Domain.Services.Web.External;

namespace Synologen.Service.Web.External.App.Services
{
	public class CustomerValidator : IValidator<Customer>
	{
		private const string PersonalNumberRegex = @"^(19\d{2}|20\d{2})(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{4}$";
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
			CheckRegex(customer, x => x.PersonalNumber, PersonalNumberRegex, result, "PersonalNumber must have format YYYYMMDDNNNN");
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

		private void CheckRegex<TType>(TType customer, Expression<Func<TType,string>> propertyExpression, string pattern, ValidationResult<TType> validationResult, string errorMessage) where TType : class
		{
			if(customer == null) return;
			var property = propertyExpression.Compile();
			var value = property(customer);
			if(value == null) return;
			if(!Regex.IsMatch(value, pattern))
			{
				validationResult.AddError(errorMessage);
			}
		}

	}
}