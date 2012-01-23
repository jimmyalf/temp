using System.Collections.Generic;
using System.Linq;

namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public class ValidationResult
	{
		private IList<ValidationError> _errors;

		public ValidationResult()
		{
			_errors = new List<ValidationError>();
		}
		public ValidationResult Add(ValidationError error)
		{
			_errors.Add(error);
			return this;
		}
		public IEnumerable<ValidationError> Errors { get { return _errors; } }

		public bool HasErrors{ get { return _errors.Any(); } }
	}
}