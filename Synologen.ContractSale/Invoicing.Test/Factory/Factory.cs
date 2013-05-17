using System;
using System.Collections.Generic;
using System.Text;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Test.Factory
{
	public static class Factory
	{
		public static Order GetOrder()
		{
			return GetOrder(new Company());
		}

		public static Order GetOrder(Company company)
		{
			return GetOrder(company, new Shop());
		}

		public static Order GetOrder(Company company, Shop shop)
		{
			return GetOrder(company, shop, new List<OrderItem>());
		}

		public static Order GetOrder(Company company, Shop shop, IEnumerable<OrderItem> orderItems)
		{
			return new Order
			{
				Id = 265,
				InvoiceNumber = 123456,
				CustomerOrderNumber = "1234567",
				ContractCompany = company,
				OrderItems = new List<OrderItem>(orderItems),
				SellingShop = shop,
				CompanyUnit = "Avdelningen för avdelningar",
				CustomerFirstName = "Adam",
				CustomerLastName = "Bertil",
				Phone = "080123456",
				Email = "adam.bertil@saab.se",
				InvoiceSumIncludingVAT = 2850,
				InvoiceSumExcludingVAT = 2315,
				CreatedDate = new DateTime(2009,01,01),
				RstText = "12345",
                CompanyId = 55,
                PersonalIdNumber = "197001011234",
                SalesPersonMemberId = 55,
                SalesPersonShopId = 56,
                StatusId = 5
			};
		}

		public static IEnumerable<OrderItem> GetOrderItems()
		{
			return GetOrderItems(0);
		}

		public static IEnumerable<OrderItem> GetOrderItems(int orderId)
		{
			return new List<OrderItem>
			{
				new OrderItem 
				{
 					ArticleDisplayName = "Synundersökning (momsbefriad)",
					ArticleDisplayNumber = "1000-5",
 					NumberOfItems = 1,
 					SinglePrice = 175,
 					NoVAT = true,
					OrderId = orderId,
					DisplayTotalPrice = 175,
				},
				new OrderItem
				{
					ArticleDisplayName = "Lacryvisc",
					ArticleDisplayNumber = "987654",
					NumberOfItems = 3,
					DisplayTotalPrice = 900,
					SinglePrice = 300,
					OrderId = orderId
				}, 
				new OrderItem
				{
					ArticleDisplayName = "Företagsbåge",
					ArticleDisplayNumber = "2000-8",
					NumberOfItems = 1,
					DisplayTotalPrice = 200,
					SinglePrice = 200,
					OrderId = orderId
				},

				new OrderItem 
				{
 					ArticleDisplayName = "Företagsbåge",
					ArticleDisplayNumber = "2000-2",
 					NumberOfItems = 1,
 					SinglePrice = 340,
 					OrderId = orderId,
					DisplayTotalPrice = 340,
				},
				new OrderItem 
				{
 					ArticleDisplayName = "G närprogressiva plast (standard)",
					ArticleDisplayNumber = "3110-6",
 					NumberOfItems = 2,
 					SinglePrice = 225,
 					OrderId = orderId,
					DisplayTotalPrice = 450,
				},
				new OrderItem 
				{
 					ArticleDisplayName = "Superantireflex plast inkl. hårdyta",
					ArticleDisplayNumber = "3412-4",
 					NumberOfItems = 2,
 					SinglePrice = 125,
 					OrderId = orderId,
					DisplayTotalPrice = 250,
				}
			};
		}

		public static Company GetCompany()
		{
			return new Company
			{
				PostBox = "Box 7774",
				StreetName = "Saab Aircraft Leasing",
				Zip = "10396",
				City = "Stockholm",
				InvoiceCompanyName = "3250Saab Aircraft Leasing Holding AB",
				OrganizationNumber = "556573780501",
				TaxAccountingCode = "SE5560360793",
				Country = new Country
				{
					OrganizationCountryCodeId = (int) CountryIdentificationCodeContentType.SE,
					Name = "Sverige"
				},
				InvoiceFreeTextFormat = GetFreeInvoiceFreeText(),
				PaymentDuePeriod = 30,
                EDIRecipientId = "00075020177753TEST",
				BankCode = "8999",
                Name = "Företag ABC"
			};
		}

		private static string GetFreeInvoiceFreeText()
		{
			return new StringBuilder()
				.AppendLine("Namn {CustomerName}")
				.AppendLine("Personnummer {CustomerPersonalIdNumber}")
				.AppendLine("Enhet {CompanyUnit}")
				.AppendLine("Födelsedatum {CustomerPersonalBirthDateString}")
				.AppendLine("Förnamn {CustomerFirstName}")
				.AppendLine("Efternamn {CustomerLastName}")
				.AppendLine("KundId {BuyerCompanyId}")
				.AppendLine("Rst {RST}")
				.AppendLine("Bankkod {BankCode}")
				.AppendLine("Säljande butik {SellingShopName}")
				.AppendLine("Säljande butiknummer {SellingShopNumber}")
				.ToString();

		}
       
		public static Shop GetShop()
		{
			return new Shop 
			{
				Name = "Storstad Optik AB", 
				OrganizationNumber = "55123456",
				Number = "1234",
                Address = "C/O Bolag 123",
                Address2 = "Storgatan 12",
				Zip = "43632",
				City = "Storstad",
				ContactFirstName = "Herr",
				ContactLastName = "Försäljare",
				Phone = "040123456",
				Fax = "040234567", 
				Email = "info@synbutiken.se",
			};
		}

		public static SvefakturaConversionSettings GetSettings()
		{
			return new SvefakturaConversionSettings
			{
				SellingOrganizationName = "Synhälsan Svenska AB",
				Adress = new SFTIAddressType
				{
					StreetName = new StreetNameType{ Value = "Strandbergsgatan 61" },
                    CityName = new CityNameType{ Value = "Stockholm" },
					Country = GetSwedishSFTICountryType(),
                    Postbox = new PostboxType{ Value = "Box 123" },
					PostalZone = new ZoneType{ Value = "112 51"}

				},
				RegistrationAdress = new SFTIAddressType
				{
                    CityName = new CityNameType{ Value = "Klippan" },
					Country = GetSwedishSFTICountryType(),
				},
				Contact = new SFTIContactType
				{
					ElectronicMail = new MailType{Value = "info@synologen.se"},
                    Name = new NameType{ Value = "Violetta Nordlöf"},
                    Telefax = new TelefaxType{Value = "084407359"},
					Telephone = new TelephoneType{ Value = "084407350" }
				},
				SellingOrganizationNumber = "5562626100",
				ExemptionReason = "Innehar F-skattebevis",
				TaxAccountingCode = "SE556262610001",
				InvoiceIssueDate = new DateTime(2009, 10, 30),
				InvoiceTypeCode = "380",
				InvoiceCurrencyCode = CurrencyCodeContentType.SEK,
				VATAmount = 0.25m,
				BankGiro = "56936677",
				BankgiroBankIdentificationCode = "BGABSESS",
				Postgiro = "123456",
				PostgiroBankIdentificationCode = "PGSISESS",
				InvoicePaymentTermsTextFormat = "{InvoiceNumberOfDueDays} dagar netto",
				InvoiceExpieryPenaltySurchargePercent = 12.5m,
                VATFreeReasonMessage = "Momsfri"
			};
		}

		public static SFTICountryType GetSwedishSFTICountryType()
		{
			return new SFTICountryType
			{
				IdentificationCode = new CountryIdentificationCodeType
				{
					name = "Sverige", 
					Value = CountryIdentificationCodeContentType.SE
				}
			};
		}
	}
}