using System;
using TestProject.Enumerations;

namespace TestProject.Types
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
    public class PropertyValidationRule : Attribute {
        public PropertyValidationRule(string name, string errorMessage, ValidationType type){
            Name = name;
            ErrorMessage = errorMessage;
            ValidationType = type;
        }
        public string Name { get; set; }
        public string ErrorMessage { get; set; }
        public ValidationType ValidationType { get; set; }
        public Type ObjectType { get; set; }
    }
}