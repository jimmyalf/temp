using NUnit.Framework;
using StoryQ.sv_SE;

namespace Web.External.AcceptanceTest
{
	[TestFixture]
	public class Add_Customer : SpecTestbase
	{
		public Add_Customer()
		{
			Context = () =>
			{
				
			};
			Story = () => new Berättelse("Lägg till Kund")
			    .FörAtt("Butiker skall slippa lägga in kunder som finns i andra system")
			    .Som("extern klient")
			    .VillJag("kunna lägga till en kund");
		}

		[Test]
		public void HappyPath()
		{
			Assert.Inconclusive("Add scenario");
		}

		[Test]
		public void InvalidAuthentication()
		{
			Assert.Inconclusive("Add scenario");
		}

		[Test]
		public void InvalidCustomer()
		{
			Assert.Inconclusive("Add scenario");
		}
	}
}