
using System;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;

namespace Spinit.Wpc.Synologen.Utility.Types {
	public class RuleViolation {
		public string ErrorMessage { get; private set; }
		public string PropertyName { get; private set; }

		public RuleViolation(string errorMessage, string propertyName) {
			ErrorMessage = String.Concat(propertyName, ": ", errorMessage);
			PropertyName = propertyName;
		}
		public RuleViolation(PropertyValidationRule validationRule, string propertyName) {
			ErrorMessage = String.Concat(propertyName, ": ", validationRule.ErrorMessage);
			PropertyName = propertyName;
		}
	}
}