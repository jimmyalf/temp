using System;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.AcceptanceTest.Helpers;
using Spinit.Wpc.Synologen.Presentation.Controllers;
using StoryQ.sv_SE;

namespace Spinit.Wpc.Synologen.Presentation.AcceptanceTest
{
	[TestFixture, Category("Redigera avtalskoppling")]
	public class EditContractArticleConnection : SpecTestbase
	{
		private ContractSalesController _controller;

		public EditContractArticleConnection()
		{
			Context = () =>
			{
				_controller = GetController<ContractSalesController>();
			};
			Story = () =>
			{
				return new Berättelse("Redigera avtalskoppling")
					.FörAtt("ändra artikeldetaljer för ett visst kontrakt")
					.Som("administratör")
					.VillJag("kunna knyta redigera en avtal-artikel -koppling");
			};
		}

		[Test]
		public void RedigeraAvtalsArtikelKoppling()
		{
			SetupScenario(scenario => scenario
			                          	.Givet(AttAdministratörenUppdateratAvtalsArtikelInformation)
			                          	.När(AdministratörenUppdaterarAvtalsArtikeln)
			                          	.Så(UppdaterasAvtalsArtikeln)
			                          	.Och(AdministratörenSkickasTillAvtalsArtikelListan)
			                          	.Och(EttMeddelandeVisasAttAvtalsArtikelnUppdaterats)
				);
		}

		private void EttMeddelandeVisasAttAvtalsArtikelnUppdaterats()
		{
			throw new NotImplementedException();
		}

		private void AdministratörenSkickasTillAvtalsArtikelListan()
		{
			throw new NotImplementedException();
		}

		private void UppdaterasAvtalsArtikeln()
		{
			throw new NotImplementedException();
		}

		private void AdministratörenUppdaterarAvtalsArtikeln()
		{
			throw new NotImplementedException();
		}

		private void AttAdministratörenUppdateratAvtalsArtikelInformation()
		{
			throw new NotImplementedException();
		}

		[Test]
		public void SpcsKontoFyllsIAutomatisktNärArtikelVäljs()
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

		private void UppdaterasSPCSKontoFältetMedDefaultKontoFrånValdArtikel()
		{
			//Controller action has replaced SPCS Account number with account number from Article
			throw new NotImplementedException();
		}

		private void AdministratörenVäljerEnArtikelIListan()
		{
			//Call controller action
			throw new NotImplementedException();
		}
	}
}