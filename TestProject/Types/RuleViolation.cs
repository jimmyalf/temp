namespace TestProject.Types
{
    public class RuleViolation {
        public RuleViolation(PropertyValidationRule propertyValidationRule) {
            Name = propertyValidationRule.Name;
            ErrorMessage = propertyValidationRule.ErrorMessage;
        }
        public string Name { get; set; }
        public string ErrorMessage { get; set; }
    }
}