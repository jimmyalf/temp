using System;
using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;

namespace Spinit.Wpc.Synologen.Svefaktura.CustomTypes {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class PropertyValidationRule : Attribute {
        public PropertyValidationRule(string errorMessage, ValidationType type){
            ErrorMessage = errorMessage;
            ValidationType = type;
        }
        public PropertyValidationRule(string errorMessage, ValidationType type, int minNumberOfItemsInEnumerable){
            ErrorMessage = errorMessage;
            ValidationType = type;
        	MinNumberOfItemsInEnumerable = minNumberOfItemsInEnumerable;
        }
		public PropertyValidationRule(string errorMessage, ValidationType type, int minNumberOfItemsInEnumerable, int maxNumberOfItemsInEnumerable){
            ErrorMessage = errorMessage;
            ValidationType = type;
			MinNumberOfItemsInEnumerable = minNumberOfItemsInEnumerable;
        	MaxNumberOfItemsInEnumerable = maxNumberOfItemsInEnumerable;
        }
		public string ErrorMessage { get; set; }
		public int? MaxNumberOfItemsInEnumerable { get; set; }
		public int? MinNumberOfItemsInEnumerable { get; set; }
        public ValidationType ValidationType { get; set; }
    }
}