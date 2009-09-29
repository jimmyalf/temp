using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.EDI;
using Spinit.Wpc.Synologen.EDI.Common.Types;
using Spinit.Wpc.Synologen.EDI.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility.Types;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;
using PercentType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;

namespace Spinit.Wpc.Synologen.Utility {
	public static class Convert {

		public static Invoice ToEDIInvoice(EDIConversionSettings EDISettings, OrderRow order, List<IOrderItem> orderItems, CompanyRow company, IShop shop) {
			var invoiceValueIncludingVAT = System.Convert.ToSingle(order.InvoiceSumIncludingVAT);
			var invoiceValueExcludingVAT = System.Convert.ToSingle(order.InvoiceSumExcludingVAT);
			var interchangeHeader = new InterchangeHeader {RecipientId = company.EDIRecipientId, SenderId = EDISettings.SenderId};
			var invoiceExpieryDate = interchangeHeader.DateOfPreparation.AddDays(company.PaymentDuePeriod);
			var invoice = new Invoice(EDISettings.VATAmount, EDISettings.NumberOfDecimalsUsedAtRounding, invoiceValueIncludingVAT, invoiceValueExcludingVAT) {
				Articles = ToEDIArticles(orderItems, order),
				Buyer = GetBuyerInformation(company.EDIRecipientId, company),
             	BuyerOrderNumber = String.Empty,
             	BuyerRSTNumber = order.RstText,
             	DocumentNumber = order.InvoiceNumber.ToString(),
				InterchangeHeader = interchangeHeader,
             	InvoiceCreatedDate = order.CreatedDate,
				InvoiceSetting = new InvoiceSetting { InvoiceCurrency = EDISettings.InvoiceCurrencyCode, InvoiceExpiryDate = invoiceExpieryDate },
             	VendorOrderNumber = order.Id.ToString(),
				Supplier = GetSupplierInformation(EDISettings.SenderId, EDISettings.BankGiro,EDISettings.Postgiro, shop)
             };
			return invoice;
		}

		public static SFTIInvoiceType ToSvefakturaInvoice(SvefakturaConversionSettings settings, OrderRow order, List<IOrderItem> orderItems, CompanyRow company, IShop shop) {
			var invoice = new SFTIInvoiceType();
			TryAddPaymentMeans(invoice, settings.BankGiro, settings.BankGiroBankIdentificationCode);
			TryAddPaymentMeans(invoice, settings.Postgiro, settings.PostgiroBankIdentificationCode);
			TryAddTaxInformation(invoice, settings.VATAmount);
			TryAddSellerPartyInformation(invoice, settings);
			return invoice;
		}



		#region Svefaktura Helper Methods
		private static void TryAddTaxInformation(SFTIInvoiceType invoice, decimal VATAmount) {
			if (VATAmount == 0) return;
			if(invoice.TaxTotal == null) invoice.TaxTotal = new List<SFTITaxTotalType>();
			invoice.TaxTotal.Add( 
				new SFTITaxTotalType {
					TaxSubTotal = new List<SFTITaxSubTotalType> {
						new SFTITaxSubTotalType {
							TaxCategory = new SFTITaxCategoryType {
								ID = new IdentifierType { Value = "S" }, 
								Percent = new PercentType { Value = VATAmount * 100 }
							}
						}
					}
				}				
			);
		}

		private static void TryAddPaymentMeans(SFTIInvoiceType invoice, string giroNumber, string giroBIC) {
			if (String.IsNullOrEmpty(giroNumber)) return;
			if (invoice.PaymentMeans == null) invoice.PaymentMeans = new List<SFTIPaymentMeansType>();
				invoice.PaymentMeans.Add(
					new SFTIPaymentMeansType {
						PaymentMeansTypeCode = new PaymentMeansCodeType {
							Value = PaymentMeansCodeContentType.Item1
						},
						PayeeFinancialAccount = new SFTIFinancialAccountType {
							ID = new IdentifierType { Value = giroNumber },
							FinancialInstitutionBranch = (String.IsNullOrEmpty(giroBIC)) ? null :
							new SFTIBranchType { 
								FinancialInstitution = new SFTIFinancialInstitutionType {
									ID = new IdentifierType { Value = giroBIC }
								}
							}
						}
					}
				);
		}

		private static void TryAddSellerPartyInformation(SFTIInvoiceType invoice, SvefakturaConversionSettings settings) {
			if (invoice.SellerParty == null) invoice.SellerParty = new SFTISellerPartyType();
			invoice.SellerParty.Party = new SFTIPartyType {
      			PartyName =  String.IsNullOrEmpty(settings.SellingOrganizationName)? null : new List<NameType>{new NameType{Value=settings.SellingOrganizationName}},
				Address = new SFTIAddressType {
					StreetName = String.IsNullOrEmpty(settings.SellingOrganizationAddress) ? null : new StreetNameType { Value = settings.SellingOrganizationAddress },
					PostalZone = String.IsNullOrEmpty(settings.SellingOrganizationPostalCode) ? null : new ZoneType{Value=settings.SellingOrganizationPostalCode},
                    CityName = String.IsNullOrEmpty(settings.SellingOrganizationCity) ? null : new CityNameType{Value=settings.SellingOrganizationCity}
				},
				PartyTaxScheme =  new List<SFTIPartyTaxSchemeType> {
               		new SFTIPartyTaxSchemeType {
                   		RegistrationAddress = (!settings.SellingOrganizationCountryCode.HasValue)? null : new SFTIAddressType{Country= new SFTICountryType{IdentificationCode = new CountryIdentificationCodeType{Value = settings.SellingOrganizationCountryCode.Value}}},
						CompanyID = (String.IsNullOrEmpty(settings.SellingOrganizationNumber))? null : new IdentifierType{Value = settings.SellingOrganizationNumber},
						TaxScheme = (String.IsNullOrEmpty(settings.SellingOrganizationNumber))? null : new SFTITaxSchemeType{ID=new IdentifierType{Value="VAT"}}
					}
				}
			};
		}
		#endregion

		#region EDI Helper Methods
		private static Supplier GetSupplierInformation(string supplierId, string bankGiro, string postGiro, IShop shop) {
			var supplier = new Supplier {
				BankGiroNumber = bankGiro,
				PostGiroNumber = postGiro,
				Contact = new Contact {
					ContactInfo = shop.Name,
					Email = shop.Email,
					Fax = shop.Fax,
					Telephone = shop.Phone
				},
				SupplierIdentity = supplierId
			};
			return supplier;
		}

		private static Buyer GetBuyerInformation(string buyerId, ICompany company) {
			var buyer = new Buyer {                	
				BuyerIdentity = buyerId,
                InvoiceIdentity = company.BankCode,
				DeliveryAddress = new Address {
      				Address1 = company.Address1, 
					Address2 = company.Address2, 
					City = company.City, 
					Zip = company.Zip
				  }
			};
			return buyer;
		}

		public static InvoiceRow ToEDIArticle(IOrderItem orderItem, int invoiceRowNumber) {
			var EDIitem = new InvoiceRow {
              	ArticleName = orderItem.ArticleDisplayName,
              	ArticleNumber = orderItem.ArticleDisplayNumber,
              	Quantity = orderItem.NumberOfItems,
              	RowNumber = invoiceRowNumber,
              	SinglePriceExcludingVAT = orderItem.SinglePrice,
              	ArticleDescription = orderItem.Notes,
				NoVAT = orderItem.NoVAT
			  };

			return EDIitem;
		}

		public static InvoiceRow ToEDIFreeTextInformationRow(List<string> listOfFreeTextRows) {
			var eDIitem = new InvoiceRow {FreeTextRows = listOfFreeTextRows, UseInvoiceRowAsFreeTextRow = true, RowNumber = 1};
			return eDIitem;
		}

		public static List<string> GetOrderBuyerInformation(OrderRow order) {
			var listOfStrings = new List<string>();
			if (!String.IsNullOrEmpty(order.CustomerFirstName) && !String.IsNullOrEmpty(order.CustomerLastName)){
				listOfStrings.Add(String.Format("Beställare Namn, {0} {1}", order.CustomerFirstName, order.CustomerLastName));
			}
			if (!String.IsNullOrEmpty(order.PersonalIdNumber)){
				listOfStrings.Add(String.Format("Beställare Personnummer, {0}", order.PersonalIdNumber));
			}
			if(!String.IsNullOrEmpty(order.CompanyUnit)){
				listOfStrings.Add(String.Format("Beställare Enhet, {0}", order.CompanyUnit));
			}
			return listOfStrings;
		}

		//public static List<InvoiceRow> ToEDIArticles(List<IOrderItem> orderItems) {
		//    var EDIArticles = new List<InvoiceRow>();
		//    var articleCounter = 1;
		//    foreach(var item in orderItems) {
		//        EDIArticles.Add(ToEDIArticle(item, articleCounter));
		//        articleCounter++;
		//    }
		//    return EDIArticles;
		//}

		public static List<InvoiceRow> ToEDIArticles(List<IOrderItem> orderItems, OrderRow order) {
			var EDIArticles = new List<InvoiceRow>();
			var articleCounter = 1;
			//Add one freetextRow if any information is available
			var listOfBuyerData = GetOrderBuyerInformation(order);
			if(listOfBuyerData!=null && listOfBuyerData.Count>0){
				var freeTextBuyerInvoiceRow = ToEDIFreeTextInformationRow(listOfBuyerData);
				EDIArticles.Add(freeTextBuyerInvoiceRow);
				articleCounter = 2;
			}
			foreach (var item in orderItems) {
				EDIArticles.Add(ToEDIArticle(item, articleCounter));
				articleCounter++;
			}
			return EDIArticles;
		}
		#endregion
	}
}