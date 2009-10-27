using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spinit.Wpc.Synologen.Business.Enumeration;
using Spinit.Wpc.Synologen.Business.Interfaces;

namespace Spinit.Wpc.Synologen.Presentation.Site.Code {
	public class CustomOrderValidation {
		
		public static bool IsValid(string controlToValidate, string controlValue, List<ICompanyValidationRule> validationRules, out string errorMessage) {
			errorMessage = null;
			var validationRulesForCurrentControl = validationRules.FindAll(x => x.ControlToValidate.Equals(controlToValidate));
			foreach (var validationRule in validationRulesForCurrentControl){
				if(IsValid(controlValue, validationRule)) continue;
				errorMessage = validationRule.ErrorMessage;
				return false;
			}
			return true;
		}

		private static bool IsValid(string controlValue, ICompanyValidationRule validationRule) {
			switch (validationRule.ValidationType){
				case ValidationType.NotRequired: 
					return true;

				case ValidationType.Required: 
					return !String.IsNullOrEmpty(controlValue);

				case ValidationType.RegEx:
					return String.IsNullOrEmpty(controlValue) 
						|| IsValid(controlValue, validationRule.ValidationRegex);

				case ValidationType.RequiredAndRegex:
					return !String.IsNullOrEmpty(controlValue) 
						&& IsValid(controlValue, validationRule.ValidationRegex);

				case ValidationType.PersonalIdNumber:
				    return String.IsNullOrEmpty(controlValue) 
						|| IsValidPersonalIDNumber(controlValue);

				case ValidationType.RequiredAndPersonalIdNumber:
				    return !String.IsNullOrEmpty(controlValue) 
						&& IsValidPersonalIDNumber(controlValue);

				case ValidationType.PersonalIdNumberAndRegex:
					if(String.IsNullOrEmpty(controlValue)) return true;
					return IsValid(controlValue, validationRule.ValidationRegex) 
						&& IsValidPersonalIDNumber(controlValue);

				case ValidationType.RequiredAndPersonalIdNumberAndRegex:
					return !String.IsNullOrEmpty(controlValue) 
						&& IsValid(controlValue, validationRule.ValidationRegex) 
						&& IsValidPersonalIDNumber(controlValue);

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static bool IsValid(string controlValue, string regexPattern) {
			var regex = new Regex(regexPattern);
			return regex.IsMatch(controlValue);
		}

		private static bool IsValidPersonalIDNumber(string personalIDNumber) {
			try {
				personalIDNumber = personalIDNumber.Replace("-", "");
				if (personalIDNumber.Length != 12) throw new ArgumentOutOfRangeException();
				personalIDNumber = personalIDNumber.Remove(0, 2); //remove first two year characters
				var sum = 0;
				for (var i = 0; i < 10; i++) {
					var temp = Convert.ToInt32(personalIDNumber.Substring(i, 1)) * (((i + 1) % 2) + 1);
					if (temp > 9) temp = temp - 9;
					sum += temp;
				}
				return (sum % 10).Equals(0);
			}
			catch {
				return false;
			}
		}

		public static bool IsValidationRuleRequired(ICompanyValidationRule validationRule) {
			switch (validationRule.ValidationType){
				case ValidationType.NotRequired:
					return false;
				case ValidationType.Required:
					return true;
				case ValidationType.RegEx:
					return false;
				case ValidationType.RequiredAndRegex:
					return true;
				case ValidationType.PersonalIdNumber:
					return false;
				case ValidationType.RequiredAndPersonalIdNumber:
					return true;
				case ValidationType.PersonalIdNumberAndRegex:
					return false;
				case ValidationType.RequiredAndPersonalIdNumberAndRegex:
					return true;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}