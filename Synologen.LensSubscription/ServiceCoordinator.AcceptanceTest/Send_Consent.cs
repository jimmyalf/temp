using NUnit.Framework;
using Synologen.Test.Core;

namespace ServiceCoordinator.AcceptanceTest
{
	[TestFixture, Category("Feature: Sending Consent")]
	public class When_sending_a_consent : BehaviorActionTestbase
	{
		public When_sending_a_consent()
		{
			Context = () => { };
			Because = () => { };
		}
	}
}
