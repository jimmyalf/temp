using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using StoryQ.sv_SE;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest
{
	[TestFixture, Category("SPCS konto förvalt vid artikelval vid avtal artikel koppling")]
	public class SPCS_Default_Account_On_Contract_Article_Connection : SpecTestbase
	{
		private ContractSalesController _controller;

		public SPCS_Default_Account_On_Contract_Article_Connection()
		{
			Context = () =>
			{
				_controller = GetController<ContractSalesController>();
			};
			Story = () =>
			{
				return new Berättelse("Få SPCS-konto förvalt vid artikelval vid avtal-artikel-koppling")
					.FörAtt("automatiskt få rätt SPCS konto")
					.Som("administratör")
					.VillJag("få SPCS kontonummer förifyllt när artikel väljs");
			};
		}

		[Test]
		public void SkapaNyAvtalsArtikelKoppling()
		{
			SetupScenario(scenario => scenario
  				.Givet(AttAdministratörenSkaparEnNyAvtalsArtikelKoppling)
  				.När(AdministratörenVäljerEnArtikelIListan)
  				.Så(UppdaterasSPCSKontoFältetMedDefaultKontoFrånValdArtikel)
			);
		}

		private void UppdaterasSPCSKontoFältetMedDefaultKontoFrånValdArtikel()
		{
			throw new NotImplementedException();
		}

		private void AdministratörenVäljerEnArtikelIListan()
		{
			throw new NotImplementedException();
		}

		private void AttAdministratörenSkaparEnNyAvtalsArtikelKoppling()
		{
			throw new NotImplementedException();
		}

		[Test]
		public void RedigeraAvtalsArtikelKoppling()
		{
			SetupScenario(scenario => scenario
              	.Givet(AttAdministratörenRedigerarEnAvtalsArtikelKoppling)
              	.När(AdministratörenVäljerEnArtikelIListan)
              	.Så(UppdaterasSPCSKontoFältetMedDefaultKontoFrånValdArtikel)
			);
		}

		private void AttAdministratörenRedigerarEnAvtalsArtikelKoppling()
		{
			throw new NotImplementedException();
		}
	}
}