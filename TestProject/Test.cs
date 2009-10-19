using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestProject.Types;

namespace TestProject {

    [TestFixture]
    public class Test : AssertionHelper {
        [TestFixtureSetUp]
        public void Setup(){}

        [Test]
        public void Test_Validation_Returns_Expected_Number_Of_RuleViolations() {
            var test = new TestClass{
                Name = "objektet", 
                TestObject = new List<TestObject>{new TestObject(), new TestObject(), new TestObject()}
            };
            var validationRules = Validator.ValidateObject(test);
            Expect(validationRules.Count(), Is.EqualTo(3));
        }
    }
}
