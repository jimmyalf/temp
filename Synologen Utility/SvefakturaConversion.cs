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
		private static void TryAddTaxTotal(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, OrderRow order, IEnumerable<IOrderItem> orderItems) {
			if(invoice.TaxTotal == null) invoice.TaxTotal = new List<SFTITaxTotalType>();
			var taxSubTotal = GetDefaultTaxCategories(settings, orderItems, order);
			invoice.TaxTotal.Add(
				new SFTITaxTotalType {
					TaxSubTotal = taxSubTotal,
					TotalTaxAmount = new TaxAmountType{ Value = (decimal)(order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT), amountCurrencyID="SEK"}
				} 
			);
		}

		private static void TryAddGeneralInvoiceInformation(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, OrderRow order, List<IOrderItem> orderItems, CompanyRow company ) {
			if(invoice == null) invoice = new SFTIInvoiceType();
			var freeTextRows = CommonConversion.GetFreeTextRowsAsString(company, order);
			invoice.Note = TryGetValue(freeTextRows, new NoteType {Value = freeTextRows});
			invoice.IssueDate = TryGetValue(settings.InvoiceIssueDate, new IssueDateType {Value = settings.InvoiceIssueDate});
			invoice.InvoiceTypeCode = TryGetValue(settings.InvoiceTypeCode, new CodeType {Value = settings.InvoiceTypeCode});
			invoice.ID = TryGetValue(order.InvoiceNumber, new SFTISimpleIdentifierType {Value = order.InvoiceNumber.ToString()});
			if (order.InvoiceSumIncludingVAT > 0 || order.InvoiceSumExcludingVAT > 0 || (order.RoundOffAmount.HasValue && order.RoundOffAmount.Value > 0)){
				invoice.LegalTotal = new SFTILegalTotalType {
					LineExtensionTotalAmount = TryGetLineExtensionAmount(orderItems),
					TaxExclusiveTotalAmount = TryGetValue(order.InvoiceSumExcludingVAT, new TotalAmountType { Value = (decimal) order.InvoiceSumExcludingVAT, amountCurrencyID = "SEK" }),
					TaxInclusiveTotalAmount = TryGetValue(order.InvoiceSumIncludingVAT, new TotalAmountType { Value = (decimal) order.InvoiceSumIncludingVAT, amountCurrencyID = "SEK" }),
					RoundOffAmount = TryGetValue(order.RoundOffAmount, new AmountType{Value = order.RoundOffAmount.GetValueOrDefault(), amountCurrencyID = "SEK"})
				};
			}
			//if(order.InvoiceSumIncludingVAT > 0 && order.InvoiceSumExcludingVAT > 0){
			//    var totalTaxAmount = order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT;
			//    invoice.TaxTotal = new List<SFTITaxTotalType>{new SFTITaxTotalType{TotalTaxAmount = new TaxAmountType {Value = (decimal) totalTaxAmount, amountCurrencyID = "SEK"}}};
			//}
			invoice.RequisitionistDocumentReference = TryGetValue(order.CustomerOrderNumber, new List<SFTIDocumentReferenceType> {
					new SFTIDocumentReferenceType {ID = new IdentifierType{Value = order.CustomerOrderNumber}}
				}
			);
			invoice.InvoiceCurrencyCode = TryGetValue(settings.InvoiceCurrencyCode, new CurrencyCodeType {Value = settings.InvoiceCurrencyCode.GetValueOrDefault()});
		}

		private static void TryAddInvoiceLines(SFTIInvoiceType invoice, IEnumerable<IOrderItem> orderItems, decimal? VATAmount  ) {
			if(invoice.InvoiceLine == null) invoice.InvoiceLine = new List<SFTIInvoiceLineType>();
			var lineItemCount = 0;
			foreach (var orderItem in orderItems){
				lineItemCount++;
				invoice.InvoiceLine.Add(
					new SFTIInvoiceLineType {
						Item = new SFTIItemType {
							Description = TryGetValue(orderItem.ArticleDisplayName, new DescriptionType {Value = orderItem.ArticleDisplayName}),
							SellersItemIdentification = TryGetValue(orderItem.ArticleDisplayNumber, new SFTIItemIdentificationType {ID = new IdentifierType {Value = orderItem.ArticleDisplayNumber}}),
							BasePrice = new SFTIBasePriceType {
								PriceAmount = new PriceAmountType {Value = (decimal) orderItem.SinglePrice, amountCurrencyID = "SEK"}
							},
							TaxCategory = new List<SFTITaxCategoryType> {
								(orderItem.NoVAT || !VATAmount.HasValue) ?  GetTaxCategory("E", 0, "VAT") : GetTaxCategory("S", VATAmount.Value*100, "VAT")
							}
						},
						InvoicedQuantity = new QuantityType{Value = orderItem.NumberOfItems, quantityUnitCode = "styck"},
                        LineExtensionAmount = new ExtensionAmountType { Value = (decimal) orderItem.DisplayTotalPrice, amountCurrencyID="SEK" },
                        ID = new SFTISimpleIdentifierType{Value = lineItemCount.ToString()},
						Note = TryGetValue(orderItem.Notes, new NoteType{Value=orderItem.Notes})
					}
				);
			}
			invoice.LineItemCountNumeric = (lineItemCount <= 0) ? null : new LineItemCountNumericType {Value = lineItemCount};
		}

		#region SellerParty
		private static void TryAddSellerParty(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, ShopRow shop) {
			if (invoice.SellerParty == null) invoice.SellerParty = new SFTISellerPartyType();
			invoice.SellerParty.Party = new SFTIPartyType {
				PartyName = TryGetValue(settings.SellingOrganizationName, new List<NameType> {new NameType {Value = settings.SellingOrganizationName}}),
				Address = GetSellerPartyAddress(settings),
				Contact = GetSellerPartyContact(shop),
				PartyTaxScheme = GetSellerPartyTaxScheme(settings),
                PartyIdentification = TryGetValue(settings.SellingOrganizationNumber, new List<SFTIPartyIdentificationType>{new SFTIPartyIdentificationType{ID = new IdentifierType{Value = settings.SellingOrganizationNumber}}})
			};
			invoice.SellerParty.AccountsContact = GetSellerAccountContact(settings);
		}

		private static SFTIContactType GetSellerPartyContact(ShopRow shop) {
			return GetSFTIContact(shop.Email, shop.ContactCombinedName, shop.Fax, shop.Phone);
		}

		private static SFTIContactType GetSellerAccountContact(SvefakturaConversionSettings settings) {
			return GetSFTIContact(settings.SellingOrganizationContactEmail, settings.SellingOrganizationContactName, settings.SellingOrganizationFax, settings.SellingOrganizationTelephone);
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
			return GetSFTIAddress(
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
				PartyName = TryGetValue(company.InvoiceCompanyName, new List<NameType> {new NameType {Value = company.InvoiceCompanyName}}),
				Address = GetBuyerPartyAddress(company, orderRow),
				Contact = GetBuyerPartyContact(orderRow),
				PartyTaxScheme = GetBuyerPartyTaxScheme(company),
				PartyIdentification = TryGetValue(company.OrganizationNumber, new List<SFTIPartyIdentificationType> { new SFTIPartyIdentificationType { ID = new IdentifierType { Value = company.OrganizationNumber } } })
			};
		}

		private static SFTIContactType GetBuyerPartyContact(OrderRow row) {
			return GetSFTIContact(row.Email, row.CustomerCombinedName, null, row.Phone);
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
			return GetSFTIAddress(
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
			if (HasNotBeenSet(settings.InvoiceIssueDate) || HasNotBeenSet(giroNumber)) return;
			if (invoice.PaymentMeans == null) invoice.PaymentMeans = new List<SFTIPaymentMeansType>();
			invoice.PaymentMeans.Add(
				new SFTIPaymentMeansType {
					PaymentMeansTypeCode = new PaymentMeansCodeType { Value = PaymentMeansCodeContentType.Item1 },
					PayeeFinancialAccount = new SFTIFinancialAccountType {
						ID = new IdentifierType {Value = giroNumber},
						FinancialInstitutionBranch = TryGetValue(giroBIC, new SFTIBranchType {FinancialInstitution = new SFTIFinancialInstitutionType {ID = new IdentifierType {Value = giroBIC}}})
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
				Note = TryGetValue(text, new NoteType {Value = text}),
				PenaltySurchargePercent = TryGetValue(settings.InvoiceExpieryPenaltySurchargePercent, new SurchargePercentType {Value = settings.InvoiceExpieryPenaltySurchargePercent.GetValueOrDefault()})
			};
		}

		private static string ParseInvoicePaymentTermsFormat(string format, CompanyRow company) {
			if(format == null || company == null) return null;
			format = format.Replace("{InvoiceNumberOfDueDays}", company.PaymentDuePeriod.ToString());
			return format;
		}
		#endregion

		#region Helper Methods

		private static T TryGetValue<T>(bool test, T falseValue, T trueFalue ) {
			return  test ? falseValue : trueFalue;
		}
		private static T TryGetValue<T>(bool test, T properValue ) {
			return  test ? default(T) : properValue;
		}
		private static T TryGetValue<T>(string valueToSet, T properValue) {
			return String.IsNullOrEmpty(valueToSet) ? default(T) : properValue;
		}
		private static T TryGetValue<T>(CountryIdentificationCodeContentType? valueToSet, T properValue) {
			return  !valueToSet.HasValue ? default(T) : properValue;
		}
		private static T TryGetValue<T>(CurrencyCodeContentType? valueToSet, T properValue) {
			return  !valueToSet.HasValue ? default(T) : properValue;
		}
		private static T TryGetValue<T>(decimal? valueToSet, T properValue) {
			return valueToSet.HasValue && valueToSet.Value != 0 ? properValue : default(T);
		}
		private static T TryGetValue<T>(long valueToSet, T properValue) {
			return valueToSet == 0 ? default(T) : properValue;
		}
		private static T TryGetValue<T>(double valueToSet, T properValue) {
			return valueToSet == 0 ? default(T) : properValue;
		}
		private static T TryGetValue<T>(IEquatable<DateTime> valueToSet, T properValue) {
			return valueToSet.Equals(DateTime.MinValue) ? default(T) : properValue;
		}
		
		private static List<SFTIPartyTaxSchemeType> GetPartyTaxScheme(string taxAccountingCode, string orgNumber, CountryIdentificationCodeContentType? countryCode, string exemptionReason, string city, string postBox, string streetName, string postalCode  ) {
			var returnList = new List<SFTIPartyTaxSchemeType>();
			if(OneOrMoreHaveValue(taxAccountingCode)){
				returnList.Add(
					new SFTIPartyTaxSchemeType {
						CompanyID = TryGetValue(taxAccountingCode, new IdentifierType {Value = taxAccountingCode}),
						TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "VAT"}}
					}
				);
			}
			//if (OneOrMoreHaveValue(countryCode, exemptionReason, orgNumber, city, streetName, postBox, postalCode )){
			if (OneOrMoreHaveValue(exemptionReason, orgNumber)){
				returnList.Add(
					new SFTIPartyTaxSchemeType {
						ExemptionReason = TryGetValue(exemptionReason, new ReasonType {Value = exemptionReason}),
						CompanyID = TryGetValue(orgNumber, new IdentifierType {Value = orgNumber}),
						RegistrationAddress = GetSFTIAddress(postBox, streetName, postalCode, city, null, countryCode),
						TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "SWT" } }
					}
				);
			}
			return (returnList.Count <= 0) ? null : returnList;
		}

		private static SFTIAddressType GetSFTIAddress(string postBox, string streetName,string zip,  string city, string companyUnit, CountryIdentificationCodeContentType? countryCode) {
			if (AllAreNullOrEmpty(postBox, streetName, zip, city, companyUnit, countryCode)) return null;
			return new SFTIAddressType {
				Postbox = TryGetValue(postBox, new PostboxType {Value = postBox}),
				StreetName = TryGetValue(streetName, new StreetNameType {Value = streetName}),
				PostalZone = TryGetValue(zip, new ZoneType {Value = zip}),
				Department = TryGetValue(companyUnit, new DepartmentType {Value = companyUnit}),
				CityName = TryGetValue(city, new CityNameType {Value = city}),
				Country = TryGetValue(countryCode, new SFTICountryType {IdentificationCode = new CountryIdentificationCodeType{ Value = countryCode.GetValueOrDefault()}})
			};
		}

		private static SFTITaxCategoryType GetTaxCategory(string identifier, decimal percent, string TaxScheme) {
			return new SFTITaxCategoryType {
				ID = new IdentifierType {Value = identifier},
				Percent = new PercentType {Value = percent},
				TaxScheme = new SFTITaxSchemeType{ ID = new IdentifierType{Value = TaxScheme}}
			};
		}

		private static SFTIContactType GetSFTIContact(string email, string name, string fax, string phone) {
			if (AllAreNullOrEmpty( email, name, fax, phone)) return null;
			return new SFTIContactType {
				ElectronicMail = TryGetValue(email, new MailType {Value = email}),
				Name = TryGetValue(name, new NameType {Value = name}),
				Telefax = TryGetValue(fax, new TelefaxType {Value = fax}),
				Telephone = TryGetValue(phone, new TelephoneType {Value = phone})
			};
		}

		private static List<SFTITaxSubTotalType> GetDefaultTaxCategories(SvefakturaConversionSettings settings, IEnumerable<IOrderItem> orderItems, OrderRow order) {
			var returnList = new List<SFTITaxSubTotalType>();
			decimal taxableAmount, taxAmount, taxFreeAmount;
			CalculateTaxParameters(orderItems, order, settings, out taxableAmount, out taxAmount, out taxFreeAmount);
			if(settings.VATAmount>0){
				returnList.Add(
					new SFTITaxSubTotalType {
						TaxCategory = GetTaxCategory("S", settings.VATAmount*100, "VAT"),
						TaxableAmount = TryGetValue(taxableAmount, new AmountType{Value = taxableAmount, amountCurrencyID ="SEK"}),
						TaxAmount = TryGetValue(taxAmount, new TaxAmountType{Value = taxAmount, amountCurrencyID ="SEK"})
					}
				);
			}
			returnList.Add(new SFTITaxSubTotalType {
				TaxCategory = GetTaxCategory("E", 0.00m, "VAT"),
				TaxableAmount = TryGetValue(taxFreeAmount, new AmountType{Value = taxFreeAmount, amountCurrencyID ="SEK"}),
                TaxAmount = new TaxAmountType{Value=0.00m, amountCurrencyID ="SEK"}
			});
			return returnList;
		}

		private static void CalculateTaxParameters(IEnumerable<IOrderItem> orderItems, OrderRow order, SvefakturaConversionSettings settings,  out decimal taxableAmount, out decimal taxAmount, out decimal taxFreeAmount) {
			taxableAmount = 0;
			taxAmount = (decimal) (order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT);
			taxFreeAmount = 0;
			foreach (var orderItem in orderItems){
				var orderItemValue = GetOrderItemTotalValue(orderItem, settings.VATAmount);
				taxFreeAmount += (orderItem.NoVAT)? orderItemValue : 0;
				taxableAmount += (orderItem.NoVAT)? 0 : orderItemValue;
			}
		}
		private static decimal GetOrderItemTotalValue(IOrderItem orderItem, decimal vatAmount){
			return (orderItem.NoVAT) ? (decimal) orderItem.DisplayTotalPrice : (decimal) orderItem.DisplayTotalPrice*(1 + vatAmount);
		}

		private static ExtensionTotalAmountType TryGetLineExtensionAmount(List<IOrderItem> orderItems) {
			var result = 0m;
			orderItems.ForEach( x => result += (decimal) x.DisplayTotalPrice);
			return (result <= 0) ? null : new ExtensionTotalAmountType {Value = result, amountCurrencyID ="SEK"};
		}
		#endregion
	}
}