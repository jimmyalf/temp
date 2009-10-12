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
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;
using PercentType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Utility {
	public static partial class Convert {
		private static void TryAddTaxTotal(SFTIInvoiceType invoice, decimal? VATAmount) {
			if(VATAmount == 0) return;
			if(!VATAmount.HasValue) return;
			if(invoice.TaxTotal == null) invoice.TaxTotal = new List<SFTITaxTotalType>();
			invoice.TaxTotal.Add( 
				new SFTITaxTotalType {
					TaxSubTotal = new List<SFTITaxSubTotalType> {
						new SFTITaxSubTotalType {
							TaxCategory = new SFTITaxCategoryType {
								ID = new IdentifierType { Value = "S" }, 
								Percent = new PercentType { Value = VATAmount.Value * 100 }
							}
						}
					}
				}				
			);
		}

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

		#region SellerParty
		private static void TryAddSellerParty(SFTIInvoiceType invoice, SvefakturaConversionSettings settings) {
			if (invoice.SellerParty == null) invoice.SellerParty = new SFTISellerPartyType();
			invoice.SellerParty.Party = new SFTIPartyType {
				PartyName = String.IsNullOrEmpty(settings.SellingOrganizationName) ? null : new List<NameType> {new NameType {Value = settings.SellingOrganizationName}},
				Address = new SFTIAddressType {
					StreetName = String.IsNullOrEmpty(settings.SellingOrganizationStreetName) ? null : new StreetNameType {Value = settings.SellingOrganizationStreetName},
					PostalZone = String.IsNullOrEmpty(settings.SellingOrganizationPostalCode) ? null : new ZoneType {Value = settings.SellingOrganizationPostalCode},
					CityName = String.IsNullOrEmpty(settings.SellingOrganizationCity) ? null : new CityNameType {Value = settings.SellingOrganizationCity}
				},
				Contact = GetSellerPartyContact(settings),
				PartyTaxScheme = GetSellerPartyTaxScheme(settings),
                PartyIdentification = (String.IsNullOrEmpty(settings.SellingOrganizationNumber)) ? null : new List<SFTIPartyIdentificationType>{new SFTIPartyIdentificationType{ID = new IdentifierType{Value = settings.SellingOrganizationNumber}}}
			};
		}

		private static SFTIContactType GetSellerPartyContact(SvefakturaConversionSettings settings) {
			if (AllAreNullOrEmpty( settings.SellingOrganizationContactEmail, settings.SellingOrganizationContactName, settings.SellingOrganizationFax, settings.SellingOrganizationTelephone )) return null;
			//if (invoice.SellerParty == null) invoice.SellerParty = new SFTISellerPartyType();
			//if (invoice.SellerParty.Party == null) invoice.SellerParty.Party = new SFTIPartyType();
			return new SFTIContactType {
				ElectronicMail = (String.IsNullOrEmpty(settings.SellingOrganizationContactEmail)) ? null : new MailType {Value = settings.SellingOrganizationContactEmail},
				Name = (String.IsNullOrEmpty(settings.SellingOrganizationContactName)) ? null : new NameType {Value = settings.SellingOrganizationContactName},
				Telefax = (String.IsNullOrEmpty(settings.SellingOrganizationFax)) ? null : new TelefaxType {Value = settings.SellingOrganizationFax},
				Telephone = (String.IsNullOrEmpty(settings.SellingOrganizationTelephone)) ? null : new TelephoneType {Value = settings.SellingOrganizationTelephone}
			};
		}

		private static List<SFTIPartyTaxSchemeType> GetSellerPartyTaxScheme(SvefakturaConversionSettings settings) {
			var returnList = new List<SFTIPartyTaxSchemeType>();
			if(OneOrMoreHaveValue(settings.TaxAccountingCode, settings.SellingOrganizationNumber)){
				returnList.Add(
					new SFTIPartyTaxSchemeType {
							RegistrationAddress = (!settings.SellingOrganizationCountryCode.HasValue) ? null : new SFTIAddressType {Country = new SFTICountryType {IdentificationCode = new CountryIdentificationCodeType {Value = settings.SellingOrganizationCountryCode.Value}}},
							CompanyID = (String.IsNullOrEmpty(settings.TaxAccountingCode)) ? null : new IdentifierType {Value = settings.TaxAccountingCode},
							TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "VAT"}}
					}
				);
			}
			if (OneOrMoreHaveValue(settings.SellingOrganizationCountryCode, settings.ExemptionReason, settings.SellingOrganizationNumber, settings.SellingOrganizationCity, settings.SellingOrganizationStreetName, settings.SellingOrganizationPostBox, settings.SellingOrganizationPostalCode, settings.SellingOrganizationNumber )){
				returnList.Add(
					new SFTIPartyTaxSchemeType {
						ExemptionReason = (String.IsNullOrEmpty(settings.ExemptionReason)) ? null : new ReasonType {Value = settings.ExemptionReason},
						CompanyID = (String.IsNullOrEmpty(settings.SellingOrganizationNumber)) ? null : new IdentifierType {Value = settings.SellingOrganizationNumber},
						RegistrationAddress = new SFTIAddressType {
							CityName = (String.IsNullOrEmpty(settings.SellingOrganizationCity)) ? null : new CityNameType {Value = settings.SellingOrganizationCity},
							StreetName = (String.IsNullOrEmpty(settings.SellingOrganizationStreetName)) ? null : new StreetNameType {Value = settings.SellingOrganizationStreetName},
							Postbox = (String.IsNullOrEmpty(settings.SellingOrganizationPostBox)) ? null : new PostboxType {Value = settings.SellingOrganizationPostBox},
							PostalZone = (String.IsNullOrEmpty(settings.SellingOrganizationPostalCode)) ? null : new ZoneType {Value = settings.SellingOrganizationPostalCode},
							Country = (!settings.SellingOrganizationCountryCode.HasValue) ? null : new SFTICountryType {IdentificationCode = new CountryIdentificationCodeType {Value = settings.SellingOrganizationCountryCode.Value}},
						},
						TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "SWT"}}
					}
				);
			}
			return (returnList.Count <= 0) ? null : returnList;
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
			var returnList = new List<SFTIPartyTaxSchemeType>();
			if(OneOrMoreHaveValue(company.OrganizationCountryCode, company.TaxAccountingCode)){
				returnList.Add(
					new SFTIPartyTaxSchemeType {
							RegistrationAddress = (!company.OrganizationCountryCode.HasValue) ? null : new SFTIAddressType {Country = new SFTICountryType {IdentificationCode = new CountryIdentificationCodeType {Value = company.OrganizationCountryCode.Value}}},
							CompanyID = (String.IsNullOrEmpty(company.TaxAccountingCode)) ? null : new IdentifierType {Value = company.TaxAccountingCode},
							TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "VAT"}}
					}
				);
			}
			if (OneOrMoreHaveValue(company.ExemptionReason,company.OrganizationNumber,company.City, company.StreetName, company.PostBox,company.Zip,company.OrganizationCountryCode)){
				returnList.Add(
					new SFTIPartyTaxSchemeType {
						ExemptionReason = (String.IsNullOrEmpty(company.ExemptionReason)) ? null : new ReasonType {Value = company.ExemptionReason},
						CompanyID = (String.IsNullOrEmpty(company.OrganizationNumber)) ? null : new IdentifierType {Value = company.OrganizationNumber},
						RegistrationAddress = new SFTIAddressType {
							CityName = (String.IsNullOrEmpty(company.City)) ? null : new CityNameType {Value = company.City},
							StreetName = (String.IsNullOrEmpty(company.StreetName)) ? null : new StreetNameType {Value = company.StreetName},
							Postbox = (String.IsNullOrEmpty(company.PostBox)) ? null : new PostboxType {Value = company.PostBox},
							PostalZone = (String.IsNullOrEmpty(company.Zip)) ? null : new ZoneType {Value = company.Zip},
							Country = (!company.OrganizationCountryCode.HasValue) ? null : new SFTICountryType {IdentificationCode = new CountryIdentificationCodeType {Value = company.OrganizationCountryCode.Value}},
						},
						TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = "SWT"}}
					}
				);
			}
			return (returnList.Count <= 0) ? null : returnList;
		}

		private static SFTIAddressType GetBuyerPartyAddress(ICompany company, IOrder orderRow) {
			if (AllAreNullOrEmpty(company.PostBox, company.StreetName, company.Zip, company.City, orderRow.CompanyUnit)) return null;
			return new SFTIAddressType {
				Postbox = (String.IsNullOrEmpty(company.PostBox)) ? null : new PostboxType{ Value = company.PostBox },
                StreetName = (String.IsNullOrEmpty(company.StreetName)) ? null : new StreetNameType{Value = company.StreetName},
                PostalZone = (String.IsNullOrEmpty(company.Zip)) ? null : new ZoneType {Value = company.Zip},
				Department = (String.IsNullOrEmpty(orderRow.CompanyUnit)) ? null : new DepartmentType{Value=orderRow.CompanyUnit},
				CityName = (String.IsNullOrEmpty(company.City)) ? null : new CityNameType{Value=company.City}
			};
		}
		#endregion

		private static void TryAddGeneralInvoiceInformation(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, OrderRow order ) {
			if(invoice == null) invoice = new SFTIInvoiceType();

			invoice.IssueDate = (settings.InvoiceIssueDate == DateTime.MinValue) ? null : new IssueDateType {Value = settings.InvoiceIssueDate};
			invoice.InvoiceTypeCode = (String.IsNullOrEmpty(settings.InvoiceTypeCode)) ? null : new CodeType {Value = settings.InvoiceTypeCode};
			invoice.ID = (order.InvoiceNumber <= 0) ? null : new SFTISimpleIdentifierType {Value = order.InvoiceNumber.ToString()};

			if (order.InvoiceSumIncludingVAT > 0 || order.InvoiceSumExcludingVAT > 0){
				invoice.LegalTotal = new SFTILegalTotalType {
					TaxExclusiveTotalAmount = (order.InvoiceSumExcludingVAT <= 0) ? null : new TotalAmountType { Value = (decimal)order.InvoiceSumExcludingVAT, amountCurrencyID = "SEK" },
					TaxInclusiveTotalAmount = (order.InvoiceSumIncludingVAT <= 0) ? null : new TotalAmountType { Value = (decimal)order.InvoiceSumIncludingVAT, amountCurrencyID = "SEK" }
				};
			}
			if(order.InvoiceSumIncludingVAT > 0 && order.InvoiceSumExcludingVAT > 0){
				var totalTaxAmount = order.InvoiceSumIncludingVAT - order.InvoiceSumExcludingVAT;
				invoice.TaxTotal = new List<SFTITaxTotalType>{new SFTITaxTotalType{TotalTaxAmount = new TaxAmountType {Value = (decimal) totalTaxAmount, amountCurrencyID = "SEK"}}};
			}
			invoice.RequisitionistDocumentReference = (String.IsNullOrEmpty(order.CustomerOrderNumber)) ? null : new List<SFTIDocumentReferenceType> {
				new SFTIDocumentReferenceType {ID = new IdentifierType{Value = order.CustomerOrderNumber}}
			};
		}

		private static void TryAddOrderItems(SFTIInvoiceType invoice, IEnumerable<IOrderItem> orderItems ) {
			if(invoice.InvoiceLine == null) invoice.InvoiceLine = new List<SFTIInvoiceLineType>();
			foreach (var orderItem in orderItems){
				invoice.InvoiceLine.Add(
					new SFTIInvoiceLineType {
						Item = new SFTIItemType {
							Description = (String.IsNullOrEmpty(orderItem.ArticleDisplayName)) ? null : new DescriptionType {Value = orderItem.ArticleDisplayName},
							StandardItemIdentification = (String.IsNullOrEmpty(orderItem.ArticleDisplayNumber)) ? null : new SFTIItemIdentificationType {ID = new IdentifierType {Value = orderItem.ArticleDisplayNumber}},
							BasePrice = new SFTIBasePriceType {
								PriceAmount = new PriceAmountType { Value = (decimal) orderItem.SinglePrice, amountCurrencyID="SEK" }
							}
						},
						InvoicedQuantity = new QuantityType{Value = orderItem.NumberOfItems, quantityUnitCode = "styck"},
                        LineExtensionAmount = new ExtensionAmountType { Value = (decimal) orderItem.DisplayTotalPrice, amountCurrencyID="SEK" }
					}
				);
			}
			
		}

		private static void TryAddPaymentTermsInformation(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, CompanyRow company) {
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
		
	}
}