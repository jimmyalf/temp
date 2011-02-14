using NUnit.Framework;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Enumerations;
using Spinit.Wpc.Synologen.Presentation.Site.Code;

namespace Spinit.Wpc.Synologen.Test.Site {
	[TestFixture]
	public class TestValidationTypeRequired {
		

		[Test]
		public void Test_Validation_Type_NotRequired() {
			var typeIsRequired = CustomOrderValidation.IsValidationRuleRequired(
				new CompanyValidationRule { ValidationType = ValidationType.NotRequired }
			);
			Assert.AreEqual(false, typeIsRequired);
		}
		[Test]
		public void Test_Validation_Type_PersonalIdNumber() {
			var typeIsRequired = CustomOrderValidation.IsValidationRuleRequired(
				new CompanyValidationRule { ValidationType = ValidationType.PersonalIdNumber }
			);
			Assert.AreEqual(false, typeIsRequired);
		}
		[Test]
		public void Test_Validation_Type_PersonalIdNumberAndRegex() {
			var typeIsRequired = CustomOrderValidation.IsValidationRuleRequired(
				new CompanyValidationRule { ValidationType = ValidationType.PersonalIdNumberAndRegex }
			);
			Assert.AreEqual(false, typeIsRequired);
		}
		[Test]
		public void Test_Validation_Type_RegEx() {
			var typeIsRequired = CustomOrderValidation.IsValidationRuleRequired(
				new CompanyValidationRule { ValidationType = ValidationType.RegEx }
			);
			Assert.AreEqual(false, typeIsRequired);
		}
		[Test]
		public void Test_Validation_Type_Required() {
			var typeIsRequired = CustomOrderValidation.IsValidationRuleRequired(
				new CompanyValidationRule { ValidationType = ValidationType.Required }
			);
			Assert.AreEqual(true, typeIsRequired);
		}
		[Test]
		public void Test_Validation_Type_RequiredAndPersonalIdNumber() {
			var typeIsRequired = CustomOrderValidation.IsValidationRuleRequired(
				new CompanyValidationRule { ValidationType = ValidationType.RequiredAndPersonalIdNumber }
			);
			Assert.AreEqual(true, typeIsRequired);
		}
		[Test]
		public void Test_Validation_Type_RequiredAndPersonalIdNumberAndRegex() {
			var typeIsRequired = CustomOrderValidation.IsValidationRuleRequired(
				new CompanyValidationRule { ValidationType = ValidationType.RequiredAndPersonalIdNumberAndRegex }
			);
			Assert.AreEqual(true, typeIsRequired);
		}
		[Test]
		public void Test_Validation_Type_RequiredAndRegex() {
			var typeIsRequired = CustomOrderValidation.IsValidationRuleRequired(
				new CompanyValidationRule { ValidationType = ValidationType.RequiredAndRegex }
			);
			Assert.AreEqual(true, typeIsRequired);
		}
	}
}