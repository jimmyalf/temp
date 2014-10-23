using System;
using System.Collections.Generic;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Presentation.Intranet.Code;

namespace Spinit.Wpc.Synologen.Test.Site {
	[TestFixture]
	public class TestCustomOrderValidation {
		private string errorMessage1;
		private string errorMessage2;
		private string errorMessage3;
		private string errorMessage4;
		private const string RequiredErrorMessage = "Test text is mandatory and cannot be empty";
		private const string RegexErrorMessage = "Test has to consist of 4 digits between 0-9";
		private const string ValidationRegex = "^[0-9]{4}$";
		private const string PersonalIDNumberInvalid = "19700101-5314";
		private const string PersonalIDNumberValid = "19700101-5374";

		[TestFixtureSetUp]
		public void Setup() {
			errorMessage1 = null;
			errorMessage2 = null;
			errorMessage3 = null;
			errorMessage4 = null;
		}

		[Test]
		public void Test_No_ValidationRules_Validates() {
			var validationRules = new List<ICompanyValidationRule>();
			var isValid = CustomOrderValidation.IsValid("txtTest", "123", validationRules, out errorMessage1);
			Assert.IsTrue(isValid);
			Assert.IsNull(errorMessage1);
		}
		[Test]
		public void Test_ValidationType_NotRequired_Validates() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule { ControlToValidate = "txtTest" }
			};
			var isValid = CustomOrderValidation.IsValid("txtTest", "123", validationRules, out errorMessage1);
			Assert.IsTrue(isValid);
			Assert.IsNull(errorMessage1);
		}
		[Test]
		public void Test_ValidationType_Required_Validates() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule
				{
					ControlToValidate = "txtTest", 
					ValidationType = ValidationType.Required,
					ErrorMessage = RequiredErrorMessage
				}
			};
			var isValid = CustomOrderValidation.IsValid("txtTest", "123", validationRules, out errorMessage1);
			Assert.IsTrue(isValid);
			Assert.IsNull(errorMessage1);
		}
		[Test]
		public void Test_ValidationType_Required_Fails_Validation() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtTest", 
					ValidationType = ValidationType.Required,
					ErrorMessage = RequiredErrorMessage
				}
			};
			var isValidEmptyString = CustomOrderValidation.IsValid("txtTest", String.Empty, validationRules, out errorMessage1);
			var isValidNull = CustomOrderValidation.IsValid("txtTest", null, validationRules, out errorMessage2);
			Assert.IsFalse(isValidEmptyString);
			Assert.IsFalse(isValidNull);
			Assert.AreEqual(RequiredErrorMessage, errorMessage1);
			Assert.AreEqual(RequiredErrorMessage, errorMessage2);
		}
		[Test]
		public void Test_ValidationType_Regex_Validates() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtTest", 
					ValidationType = ValidationType.RegEx,
					ValidationRegex = ValidationRegex,
					ErrorMessage = RegexErrorMessage
				}
			};
			var isValidEmptyString = CustomOrderValidation.IsValid("txtTest", String.Empty, validationRules, out errorMessage1);
			var isValidNull = CustomOrderValidation.IsValid("txtTest", null, validationRules, out errorMessage2);
			var isValidRegex = CustomOrderValidation.IsValid("txtTest", "1234", validationRules, out errorMessage3);
			Assert.IsTrue(isValidEmptyString);
			Assert.IsTrue(isValidNull);
			Assert.IsTrue(isValidRegex);
			Assert.IsNull(errorMessage1);
			Assert.IsNull(errorMessage2);
			Assert.IsNull(errorMessage3);
		}
		[Test]
		public void Test_ValidationType_Regex_Fails_Validation() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtTest", 
					ValidationType = ValidationType.RegEx,
					ValidationRegex = ValidationRegex,
					ErrorMessage = RegexErrorMessage
				}
			};
			var isValidRegex = CustomOrderValidation.IsValid("txtTest", "123", validationRules, out errorMessage1);
			Assert.IsFalse(isValidRegex);
			Assert.IsNotNullOrEmpty(errorMessage1);
		}
		[Test]
		public void Test_ValidationType_RequiredAndRegex_Validates() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtTest", 
					ValidationType = ValidationType.RequiredAndRegex,
					ValidationRegex = ValidationRegex,
					ErrorMessage = RegexErrorMessage
				}
			};
			var isValidRegex = CustomOrderValidation.IsValid("txtTest", "1234", validationRules, out errorMessage1);
			Assert.IsTrue(isValidRegex);
			Assert.IsNull(errorMessage1);
		}
		[Test]
		public void Test_ValidationType_RequiredAndRegex_Fails_Validation() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtTest", 
					ValidationType = ValidationType.RequiredAndRegex,
					ValidationRegex = ValidationRegex,
					ErrorMessage = RegexErrorMessage
				}
			};
			var isValidEmptyString = CustomOrderValidation.IsValid("txtTest", String.Empty, validationRules, out errorMessage1);
			var isValidNull = CustomOrderValidation.IsValid("txtTest", null, validationRules, out errorMessage2);
			var isValidRegex = CustomOrderValidation.IsValid("txtTest", "123", validationRules, out errorMessage3);
			Assert.IsFalse(isValidEmptyString);
			Assert.IsFalse(isValidNull);
			Assert.IsFalse(isValidRegex);
			Assert.IsNotNullOrEmpty(errorMessage1);
			Assert.IsNotNullOrEmpty(errorMessage2);
			Assert.IsNotNullOrEmpty(errorMessage3);
		}
		[Test]
		public void Test_ValidationType_PersonalIDNumber_Validates() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtPersonalIDNumber", 
					ValidationType = ValidationType.PersonalIdNumber,
					ErrorMessage = "Personal ID Number check failed."
				}
			};
			var isValidNull = CustomOrderValidation.IsValid("txtPersonalIDNumber", null, validationRules, out errorMessage1);
			var isValidEmpty = CustomOrderValidation.IsValid("txtPersonalIDNumber", string.Empty, validationRules, out errorMessage2);
			var isValidPersonalIdNumber = CustomOrderValidation.IsValid("txtPersonalIDNumber", PersonalIDNumberValid, validationRules, out errorMessage3);
			Assert.IsTrue(isValidNull);
			Assert.IsTrue(isValidEmpty);
			Assert.IsTrue(isValidPersonalIdNumber);
			Assert.IsNull(errorMessage1);
			Assert.IsNull(errorMessage2);
			Assert.IsNull(errorMessage3);
		}
		[Test]
		public void Test_ValidationType_PersonalIDNumber_Fails_Validation() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtPersonalIDNumber", 
					ValidationType = ValidationType.PersonalIdNumber,
					ErrorMessage = "Personal ID Number check failed."
				}
			};
			var isValidPersonalIdNumber = CustomOrderValidation.IsValid("txtPersonalIDNumber", PersonalIDNumberInvalid, validationRules, out errorMessage1);
			Assert.IsFalse(isValidPersonalIdNumber);
			Assert.AreEqual("Personal ID Number check failed.", errorMessage1);
		}
		[Test]
		public void Test_ValidationType_RequiredPersonalIDNumber_Validates() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtPersonalIDNumber", 
					ValidationType = ValidationType.PersonalIdNumber,
					ErrorMessage = "Personal ID Number check failed."
				}
			};
			var isValidPersonalIdNumber = CustomOrderValidation.IsValid("txtPersonalIDNumber", PersonalIDNumberValid, validationRules, out errorMessage1);
			Assert.IsTrue(isValidPersonalIdNumber);
			Assert.IsNull(errorMessage1);
		}
		[Test]
		public void Test_ValidationType_RequiredPersonalIDNumber_Fails_Validation() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtPersonalIDNumber", 
					ValidationType = ValidationType.RequiredAndPersonalIdNumber,
					ErrorMessage = "Personal ID Number check failed."
				}
			};
			var isValidNull = CustomOrderValidation.IsValid("txtPersonalIDNumber", null, validationRules, out errorMessage1);
			var isValidEmpty = CustomOrderValidation.IsValid("txtPersonalIDNumber", string.Empty, validationRules, out errorMessage2);
			var isValidPersonalIdNumber = CustomOrderValidation.IsValid("txtPersonalIDNumber", PersonalIDNumberInvalid, validationRules, out errorMessage3);
			Assert.IsFalse(isValidNull);
			Assert.IsFalse(isValidEmpty);
			Assert.IsFalse(isValidPersonalIdNumber);
			Assert.AreEqual("Personal ID Number check failed.", errorMessage1);
			Assert.AreEqual("Personal ID Number check failed.", errorMessage2);
			Assert.AreEqual("Personal ID Number check failed.", errorMessage3);
		}
		[Test]
		public void Test_ValidationType_PersonalIDNumberAndRegex_Validates() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtPersonalIDNumber", 
					ValidationType = ValidationType.PersonalIdNumberAndRegex,
					ValidationRegex = "^[0-9]{8}-[0-9]{4}$",
					ErrorMessage = "Personal ID Number check and regex check failed."
				}
			};
			var isValidNull = CustomOrderValidation.IsValid("txtPersonalIDNumber", null, validationRules, out errorMessage1);
			var isValidEmpty = CustomOrderValidation.IsValid("txtPersonalIDNumber", string.Empty, validationRules, out errorMessage2);
			var isValidPersonalIdNumberAndRegex = CustomOrderValidation.IsValid("txtPersonalIDNumber", PersonalIDNumberValid, validationRules, out errorMessage3);
			Assert.IsTrue(isValidNull);
			Assert.IsTrue(isValidEmpty);
			Assert.IsTrue(isValidPersonalIdNumberAndRegex);
			Assert.IsNull(errorMessage1);
			Assert.IsNull(errorMessage2);
			Assert.IsNull(errorMessage3);
		}
		[Test]
		public void Test_ValidationType_PersonalIDNumberAndRegex_Fails_Validation() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtPersonalIDNumber", 
					ValidationType = ValidationType.PersonalIdNumberAndRegex,
					ValidationRegex = "^[0-9]{8}-[0-9]{4}$",
					ErrorMessage = "Personal ID Number check and regex check failed."
				}
			};
			var isValidPersonalIdNumber = CustomOrderValidation.IsValid("txtPersonalIDNumber", PersonalIDNumberInvalid, validationRules, out errorMessage1);
			var isValidRegex = CustomOrderValidation.IsValid("txtPersonalIDNumber", "197001001-574", validationRules, out errorMessage2);
			Assert.IsFalse(isValidPersonalIdNumber);
			Assert.IsFalse(isValidRegex);
			Assert.AreEqual("Personal ID Number check and regex check failed.", errorMessage1);
			Assert.AreEqual("Personal ID Number check and regex check failed.", errorMessage2);
		}
		[Test]
		public void Test_ValidationType_RequiredAndPersonalIdNumberAndRegex_Validates() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtPersonalIDNumber", 
					ValidationType = ValidationType.RequiredAndPersonalIdNumberAndRegex,
					ValidationRegex = "^[0-9]{8}-[0-9]{4}$",
					ErrorMessage = "Personal ID Number check and regex check failed."
				}
			};
			var isValidPersonalIdNumberAndRegex = CustomOrderValidation.IsValid("txtPersonalIDNumber", PersonalIDNumberValid, validationRules, out errorMessage1);
			Assert.IsTrue(isValidPersonalIdNumberAndRegex);
			Assert.IsNull(errorMessage1);
		}
		[Test]
		public void Test_ValidationType_RequiredAndPersonalIdNumberAndRegex_Fails_Validation() {
			var validationRules = new List<ICompanyValidationRule> {
				new CompanyValidationRule {
					ControlToValidate = "txtPersonalIDNumber", 
					ValidationType = ValidationType.RequiredAndPersonalIdNumberAndRegex,
					ValidationRegex = "^[0-9]{8}-[0-9]{4}$",
					ErrorMessage = "Personal ID Number check and regex check failed."
				}
			};
			var isValidNull = CustomOrderValidation.IsValid("txtPersonalIDNumber", null, validationRules, out errorMessage1);
			var isValidEmpty = CustomOrderValidation.IsValid("txtPersonalIDNumber", string.Empty, validationRules, out errorMessage2);
			var isValidPersonalIdNumber = CustomOrderValidation.IsValid("txtPersonalIDNumber", PersonalIDNumberInvalid, validationRules, out errorMessage3);
			var isValidRegex = CustomOrderValidation.IsValid("txtPersonalIDNumber", "1970101-53674", validationRules, out errorMessage4);
			Assert.IsFalse(isValidNull);
			Assert.IsFalse(isValidEmpty);
			Assert.IsFalse(isValidPersonalIdNumber);
			Assert.IsFalse(isValidRegex);
			Assert.AreEqual("Personal ID Number check and regex check failed.", errorMessage1);
			Assert.AreEqual("Personal ID Number check and regex check failed.", errorMessage2);
			Assert.AreEqual("Personal ID Number check and regex check failed.", errorMessage3);
			Assert.AreEqual("Personal ID Number check and regex check failed.", errorMessage4);
		}
	}
}