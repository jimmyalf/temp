using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest.LensSubscription.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.AcceptanceTest.LensSubscription
{
	[TestFixture, Category("View_Shop_Subscription_Summary")]
	public class When_loading_subscription_view_for_a_shop : SpecTestbase
	{
		public When_loading_subscription_view_for_a_shop()
		{
			Context = () => { };
			Story = () =>
			{
				return new Berättelse("Visa abonnemangs-översikt för butik")
					.FörAtt("Snabbt få en överblick över butikens abonnemang")
					.Som("inloggad butikspersonal")
					.VillJag("se en lista över butikens samtliga abonnamang");
			};
		}

		[Test]
		public void ButikenHarAbonnemang()
		{
			SetupScenario(scenario => scenario
				.Givet(AttButikenHarEttEllerFlerLinsabonnemang)
				.När(ButikpersonalenVisarStartsidan)
				.Så(VisasEnListaMedSamtligaLinsabonnemang)
			);
		}

		private void AttButikenHarEttEllerFlerLinsabonnemang()
		{
			throw new NotImplementedException();
		}

		private void ButikpersonalenVisarStartsidan()
		{
			throw new NotImplementedException();
		}

		private void VisasEnListaMedSamtligaLinsabonnemang()
		{
			throw new NotImplementedException();
		}
	}
}
