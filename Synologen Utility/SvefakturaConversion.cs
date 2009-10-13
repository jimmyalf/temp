using System;
using System.Collections.Generic;
using Spinit.Wpc.Synologen.Business.Interfaces;
using Spinit.Wpc.Synologen.Data.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using Spinit.Wpc.Synologen.Utility.Types;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;
using PercentType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Utility {
	public static partial class Convert {
		private static void TryAddTaxTotal(SFTIInvoiceType invoice, decimal? VATAmount) {
			//if(VATAmount == 0) return;
			//if(!VATAmount.HasValue) return;
			if(invoice.TaxTotal == null) invoice.TaxTotal = new List<SFTITaxTotalType>();
			invoice.TaxTotal.Add( new SFTITaxTotalType { TaxSubTotal = GetDefaultTaxCategories(VATAmount) });
		}

		#region SellerParty
		private static void TryAddSellerParty(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, ShopRow shop) {
			if (invoice.SellerParty == null) invoice.SellerParty = new SFTISellerPartyType();
			invoice.SellerParty.Party = new SFTIPartyType {
				PartyName = String.IsNullOrEmpty(settings.SellingOrganizationName) ? null : new List<NameType> {new NameType {Value = settings.SellingOrganizationName}},
				Address = GetSellerPartyAddress(settings),
				Contact = GetSellerPartyContact(shop),
				PartyTaxScheme = GetSellerPartyTaxScheme(settings),
                PartyIdentification = (String.IsNullOrEmpty(settings.SellingOrganizationNumber)) ? null : new List<SFTIPartyIdentificationType>{new SFTIPartyIdentificationType{ID = new IdentifierType{Value = settings.SellingOrganizationNumber}}}
			};
			invoice.SellerParty.AccountsContact = GetSellerAccountContact(settings);
		}

		private static SFTIContactType GetSellerPartyContact(ShopRow shop) {
			if (AllAreNullOrEmpty( shop.Email, shop.ContactCombinedName, shop.Fax, shop.Phone)) return null;
			return new SFTIContactType {
				ElectronicMail = (String.IsNullOrEmpty(shop.Email)) ? null : new MailType {Value = shop.Email},
				Name = (String.IsNullOrEmpty(shop.ContactCombinedName)) ? null : new NameType {Value = shop.ContactCombinedName},
				Telefax = (String.IsNullOrEmpty(shop.Fax)) ? null : new TelefaxType {Value = shop.Fax},
				Telephone = (String.IsNullOrEmpty(shop.Phone)) ? null : new TelephoneType {Value = shop.Phone}
			};
		}

		private static SFTIContactType GetSellerAccountContact(SvefakturaConversionSettings settings) {
			if (AllAreNullOrEmpty( settings.SellingOrganizationContactEmail, settings.SellingOrganizationContactName, settings.SellingOrganizationFax, settings.SellingOrganizationTelephone )) return null;
			return new SFTIContactType {
				ElectronicMail = (String.IsNullOrEmpty(settings.SellingOrganizationContactEmail)) ? null : new MailType {Value = settings.SellingOrganizationContactEmail},
				Name = (String.IsNullOrEmpty(settings.SellingOrganizationContactName)) ? null : new NameType {Value = settings.SellingOrganizationContactName},
				Telefax = (String.IsNullOrEmpty(settings.SellingOrganizationFax)) ? null : new TelefaxType {Value = settings.SellingOrganizationFax},
				Telephone = (String.IsNullOrEmpty(settings.SellingOrganizationTelephone)) ? null : new TelephoneType {Value = settings.SellingOrganizationTelephone}
			};
		}

		private static List<SFTIPartyTaxSchemeType> GetSellerPartyTaxScheme(SvefakturaConversionSettings settings) {
			return GetPartyTaxScheme(
				settings.TaxAccountingCode, 
				settings.SellingOrganizationNumber, 
				settings.SellingOrganizationCountryCode, 
				settings.ExemptionReason, 
				settings.SellingOrganizationCity, 
				settings.SellingOrganizationPostBox, 
				settings.SellingOrganizationStreetName, 
				settings.SellingOrganizationPostalCode
			);
		}

		private static SFTIAddressType GetSellerPartyAddress(SvefakturaConversionSettings settings) {
			return GetPartyAddress(
				settings.SellingOrganizationPostBox,
				settings.SellingOrganizationStreetName,
				settings.SellingOrganizationPostalCode,
				settings.SellingOrganizationCity,
				null,
				settings.SellingOrganizationCountryCode
			);
		}
		#endregion

		#region BuyerParty
		private static void TryAddBuyerParty(SFTIInvoiceType invoice, CompanyRow company, OrderRow orderRow) {
			if (invoice.BuyerParty == null) invoice.BuyerParty = new SFTIBuyerPartyType();
			invoice.BuyerParty.Party = new SFTIPartyType {
				PartyName = String.IsNullOrEmpty(company.InvoiceCompanyName) ? null : new List<NameType> {new NameType {Value = company.InvoiceCompanyName}},
				Address = GetBuyerPartyAddress(company, orderRow),
				Contact = GetBuyerPartyContact(orderRow),
				PartyTaxScheme = GetBuyerPartyTaxScheme(company),
				PartyIdentification = (String.IsNullOrEmpty(company.OrganizationNumber)) ? null : new List<SFTIPartyIdentificationType> { new SFTIPartyIdentificationType { ID = new IdentifierType { Value = company.OrganizationNumber } } }
			};
		}

		private static SFTIContactType GetBuyerPartyContact(OrderRow row) {
			if(AllAreNullOrEmpty(row.CustomerCombinedName, row.Email, row.Phone)) return null;
			return new SFTIContactType {
				ElectronicMail = (String.IsNullOrEmpty(row.Email)) ? null : new MailType {Value = row.Email},
				Name = (String.IsNullOrEmpty(row.CustomerCombinedName)) ? null : new NameType {Value = row.CustomerCombinedName},
				Telephone = (String.IsNullOrEmpty(row.Phone)) ? null : new TelephoneType {Value = row.Phone}
			};
		}

		private static List<SFTIPartyTaxSchemeType> GetBuyerPartyTaxScheme(CompanyRow company) {
			return GetPartyTaxScheme(
				company.TaxAccountingCode, 
				company.OrganizationNumber, 
				company.OrganizationCountryCode,
				company.ExemptionReason,
				company.City,
				company.PostBox,
				company.StreetName,
				company.Zip
			);
		}

		private static SFTIAddressType GetBuyerPartyAddress(CompanyRow company, IOrder orderRow) {
			return GetPartyAddress(
				company.PostBox,
				company.StreetName,
				company.Zip,
				company.City,
				orderRow.CompanyUnit,
				null
			);
		}
		#endregion

		#region PaymentMeans
		private static void TryAddPaymentMeans(SFTIInvoiceType invoice, string giroNumber, string giroBIC, CompanyRow company, SvefakturaConversionSettings settings) {
			if (String.IsNullOrEmpty(giroNumber)) return;
			if (invoice.PaymentMeans == null) invoice.PaymentMeans = new List<SFTIPaymentMeansType>();
			invoice.PaymentMeans.Add(
				new SFTIPaymentMeansType {
					PaymentMeansTypeCode = new PaymentMeansCodeType { Value = PaymentMeansCodeContentType.Item1 },
					PayeeFinancialAccount = new SFTIFinancialAccountType {
						ID = new IdentifierType {Value = giroNumber},
						FinancialInstitutionBranch = (String.IsNullOrEmpty(giroBIC)) ? null : new SFTIBranchType {FinancialInstitution = new SFTIFinancialInstitutionType {ID = new IdentifierType {Value = giroBIC}}}
					},
					DuePaymentDate = GetPaymentMeansDuePaymentDate(settings.InvoiceIssueDate, company)
				}
			);
		}

		private static PaymentDateType GetPaymentMeansDuePaymentDate(DateTime invoiceIssueDate, CompanyRow company) {
			if (company == null ) return null;
		    if (company.PaymentDuePeriod <= 0) return null;
		    return new  PaymentDateType {
				Value = invoiceIssueDate.AddDays(company.PaymentDuePeriod)
			};
		}
		#endregion

		#region PaymentTerms
		private static void TryAddPaymentTerms(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, CompanyRow company) {
			if (AllAreNullOrEmpty(settings.InvoicePaymentTermsTextFormat, settings.InvoiceExpieryPenaltySurchargePercent)) return;
			var text = ParseInvoicePaymentTermsFormat(settings.InvoicePaymentTermsTextFormat, company);
			invoice.PaymentTerms = new SFTIPaymentTermsType {
				Note = (String.IsNullOrEmpty(text)) ? null: new NoteType {Value = text},
				PenaltySurchargePercent = (settings.InvoiceExpieryPenaltySurchargePercent.HasValue) ? new SurchargePercentType {Value = settings.InvoiceExpieryPenaltySurchargePercent.Value} : null
			};
		}

		private static string ParseInvoicePaymentTermsFormat(string format, CompanyRow company) {
			if(format == null || company == null) return null;
			format = format.Replace("{InvoiceNumberOfDueDays}", company.PaymentDuePeriod.ToString());
			return format;
		}
		#endregion

		private static List<SFTIPartyTaxSchemeType> GetPartyTaxScheme(string taxAccountingCode, string orgNumber, CountryIdentificationCodeContentType? countryCode, string exemptionReason, string city, string postBox, string streetName, string postalCode  ) {
			var returnList = new List<SFTIPartyTaxSchemeType>();
			if(OneOrMoreHaveValue(taxAccountingCode)){
				returnList.Add(
					new SFTIPartyTaxSchemeType {
							CompanyID = (String.IsNullOrEmpty(taxAccountingCode)) ? null : new IdentifierType {Value = taxAccountingCode},
							TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "VAT"}}
					}
				);
			}
			if (OneOrMoreHaveValue(countryCode, exemptionReason, orgNumber, city, streetName, postBox, postalCode )){
				returnList.Add(
					new SFTIPartyTaxSchemeType {
						ExemptionReason = (String.IsNullOrEmpty(exemptionReason)) ? null : new ReasonType { Value = exemptionReason },
						CompanyID = (String.IsNullOrEmpty(orgNumber)) ? null : new IdentifierType { Value = orgNumber },
						RegistrationAddress = new SFTIAddressType {
							CityName = (String.IsNullOrEmpty(city)) ? null : new CityNameType { Value = city },
							StreetName = (String.IsNullOrEmpty(streetName)) ? null : new StreetNameType { Value = streetName },
							Postbox = (String.IsNullOrEmpty(postBox)) ? null : new PostboxType { Value = postBox },
							PostalZone = (String.IsNullOrEmpty(postalCode)) ? null : new ZoneType { Value = postalCode },
							Country = (!countryCode.HasValue) ? null : new SFTICountryType { IdentificationCode = new CountryIdentificationCodeType { Value = countryCode.Value } },
						},
						TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "SWT" } }
					}
				);
			}
			return (returnList.Count <= 0) ? null : returnList;
		}

		private static SFTIAddressType GetPartyAddress(string postBox, string streetName,string zip,  string city, string companyUnit, CountryIdentificationCodeContentType? countryCode) {
			if (AllAreNullOrEmpty(postBox, streetName, zip, city, companyUnit, countryCode)) return null;
			return new SFTIAddressType {
				Postbox = (String.IsNullOrEmpty(postBox)) ? null : new PostboxType {Value = postBox},
				StreetName = (String.IsNullOrEmpty(streetName)) ? null : new StreetNameType {Value = streetName},
				PostalZone = (String.IsNullOrEmpty(zip)) ? null : new ZoneType {Value = zip},
				Department = (String.IsNullOrEmpty(companyUnit)) ? null : new DepartmentType {Value = companyUnit},
				CityName = (String.IsNullOrEmpty(city)) ? null : new CityNameType {Value = city},
				Country = (!countryCode.HasValue) ? null : new SFTICountryType {IdentificationCode = new CountryIdentificationCodeType{ Value = countryCode.Value}}
			};
		}

		private static void TryAddGeneralInvoiceInformation(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, OrderRow order ) {
			if(invoice == null) invoice = new SFTIInvoiceType();

			invoice.IssueDate = (settings.InvoiceIssueDate == DateTime.MinValue) ? null : new IssueDateType {Value = settings.InvoiceIssueDate};
			invoice.InvoiceTypeCode = (String.IsNullOrEmpty(settings.InvoiceTypeCode)) ? null : new CodeType {Value = settings.InvoiceTypeCode};
			invoice.ID = (order.InvoiceNumber <= 0) ? null : new SFTISimpleIdentifierType {Value = order.InvoiceNumber.ToString()};

			if (order.InvoiceSumIncludingVAT > 0 || order.InvoiceSumExcludingVAT > 0 || (order.RoundOffAmount.HasValue && order.RoundOffAmount.Value > 0)){
				invoice.LegalTotal = new SFTILegalTotalType {
					TaxExclusiveTotalAmount = (order.InvoiceSumExcludingVAT <= 0) ? null : new TotalAmountType { Value = (decimal)order.InvoiceSumExcludingVAT, amountCurrencyID = "SEK" },
					TaxInclusiveTotalAmount = (order.InvoiceSumIncludingVAT <= 0) ? null : new TotalAmountType { Value = (decimal)order.InvoiceSumIncludingVAT, amountCurrencyID = "SEK" },
					RoundOffAmount = (!order.RoundOffAmount.HasValue || order.RoundOffAmount.Value == 0) ? null : new AmountType{Value = order.RoundOffAmount.Value, amountCurrencyID = "SEK"}
				};
			}
			if(order.InvoiceSumIncludingVAT > 0 && order.InvoiceSumExcludingVAT > 0){
				var totalTaxAmount = order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT;
				invoice.TaxTotal = new List<SFTITaxTotalType>{new SFTITaxTotalType{TotalTaxAmount = new TaxAmountType {Value = (decimal) totalTaxAmount, amountCurrencyID = "SEK"}}};
			}
			invoice.RequisitionistDocumentReference = (String.IsNullOrEmpty(order.CustomerOrderNumber)) ? null : new List<SFTIDocumentReferenceType> {
				new SFTIDocumentReferenceType {ID = new IdentifierType{Value = order.CustomerOrderNumber}}
			};
			invoice.InvoiceCurrencyCode = (!settings.InvoiceCurrencyCode.HasValue) ? null : new CurrencyCodeType {Value = settings.InvoiceCurrencyCode.Value};
		}

		private static void TryAddInvoiceLines(SFTIInvoiceType invoice, IEnumerable<IOrderItem> orderItems, decimal? VATAmount  ) {
			if(invoice.InvoiceLine == null) invoice.InvoiceLine = new List<SFTIInvoiceLineType>();
			var lineItemCount = 0;
			foreach (var orderItem in orderItems){
				lineItemCount++;
				invoice.InvoiceLine.Add(
					new SFTIInvoiceLineType {
						Item = new SFTIItemType {
							Description = (String.IsNullOrEmpty(orderItem.ArticleDisplayName)) ? null : new DescriptionType {Value = orderItem.ArticleDisplayName},
							SellersItemIdentification = (String.IsNullOrEmpty(orderItem.ArticleDisplayNumber)) ? null : new SFTIItemIdentificationType {ID = new IdentifierType {Value = orderItem.ArticleDisplayNumber}},
							BasePrice = new SFTIBasePriceType {
								PriceAmount = new PriceAmountType {Value = (decimal) orderItem.SinglePrice, amountCurrencyID = "SEK"}
							},
							TaxCategory = new List<SFTITaxCategoryType> {
								(orderItem.NoVAT || !VATAmount.HasValue) ?  GetTaxCategory("E", 0, "VAT") : GetTaxCategory("S", VATAmount.Value*100, "VAT")
							}
						},
						InvoicedQuantity = new QuantityType{Value = orderItem.NumberOfItems, quantityUnitCode = "styck"},
                        LineExtensionAmount = new ExtensionAmountType { Value = (decimal) orderItem.DisplayTotalPrice, amountCurrencyID="SEK" },
                        ID = new SFTISimpleIdentifierType{Value = lineItemCount.ToString()}
					}
				);
			}
			invoice.LineItemCountNumeric = (lineItemCount <= 0) ? null : new LineItemCountNumericType {Value = lineItemCount};
		}

		private static SFTITaxCategoryType GetTaxCategory(string identifier, decimal percent, string TaxScheme) {
			return new SFTITaxCategoryType {
				ID = new IdentifierType {Value = identifier},
				Percent = new PercentType {Value = percent},
				TaxScheme = new SFTITaxSchemeType{ ID = new IdentifierType{Value = TaxScheme}}
			};
		}

		private static List<SFTITaxSubTotalType> GetDefaultTaxCategories(decimal? vatAmount) {
			var returnList = new List<SFTITaxSubTotalType>();
			if(vatAmount.HasValue && vatAmount.Value> 0){
				returnList.Add(new SFTITaxSubTotalType {TaxCategory = GetTaxCategory("S", vatAmount.Value*100, "VAT")});
			}
			returnList.Add(new SFTITaxSubTotalType {TaxCategory = GetTaxCategory("E", 0m, "VAT")});
			return returnList;
		}

	}
}