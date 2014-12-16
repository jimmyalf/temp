using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public class ValidationResult
	{
		protected IList<ValidationError> _errors;

		public ValidationResult()
		{
			_errors = new List<ValidationError>();
		}
		public ValidationResult Add(ValidationError error)
		{
			_errors.Add(error);
			return this;
		}

		public ValidationResult AddError(string message)
		{
			return Add(new ValidationError {ErrorMessage = message});
		}
		public IEnumerable<ValidationError> Errors { get { return _errors; } }

		public bool HasErrors{ get { return _errors.Any(); } }

		public string GetErrorMessage(string delimiter = ", ")
		{
			return !HasErrors 
				? null 
				: Errors.Select(x => x.ErrorMessage).Aggregate((item, next) => item + delimiter + next);
		}
	}

	public class ValidationResult<TType> : ValidationResult where TType : class
	{
		public ValidationResult<TType> AddMissingPropertyError(Expression<Func<TType,object>> property)
		{
			var propertyName = property.GetName();
			AddError("{0} is required".FormatWith(propertyName));
			return this;
		}
	}
}