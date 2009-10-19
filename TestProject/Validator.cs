using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TestProject.Enumerations;
using TestProject.Types;

namespace TestProject {
    public class Validator {
        public static IEnumerable<RuleViolation> ValidateObject(object value) {
            if(value == null || value.GetType().IsSealed) yield break;
            if(value is IEnumerable) {
            	foreach (var item in value as IEnumerable){
					foreach (var ruleViolation in ValidateObject(item)) { yield return ruleViolation; }
            	}
                yield break;
            }
            foreach (var propertyInfo in value.GetType().GetProperties()) {
                var propertyValue = propertyInfo.GetValue(value, null);
                foreach(var ruleViolation in GetRuleViolations(propertyValue, propertyInfo)) {
                    yield return ruleViolation;
                }
                if(propertyValue == null) continue;
                foreach(var ruleViolation in ValidateObject(propertyValue)) {
                    yield return ruleViolation;
                }
            }
            yield break;
        }

        private static IEnumerable<RuleViolation> GetRuleViolations(object propertyValue, ICustomAttributeProvider propertyInfo) {
            var properties = propertyInfo.GetCustomAttributes(typeof(PropertyValidationRule),true);
            foreach (PropertyValidationRule validationType in properties) {
                if(propertyValue == null && validationType.ValidationType == ValidationType.RequiredNotNull) {
                    yield return new RuleViolation(validationType);
                }
            }
            yield break;
        }

		//private static IEnumerable<RuleViolation> ValidateObjects(IEnumerable items) {
		//    foreach (var item in items) { 
		//        foreach (var ruleViolation in ValidateObject(item)) {
		//            yield return ruleViolation;
		//        }
		//    }
		//    yield break;
		//}

    }
}