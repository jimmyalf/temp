using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Spinit.Wpc.Synologen.Business.Domain.Entities;
using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Invoicing.Types;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using AmountType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.AmountType;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;
using PercentType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.PercentType;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Invoicing
{
	public static partial class Convert
	{

		private static void TryAddTaxTotal(SFTIInvoiceType invoice, SvefakturaConversionSettings settings) 
		{
			if (invoice.InvoiceLine == null) return;
			if(invoice.TaxTotal == null) invoice.TaxTotal = new List<SFTITaxTotalType>();
			var generatedTaxTotal = GetTaxTotal(invoice, settings.VATAmount);
			invoice.TaxTotal.Add(generatedTaxTotal);
		}

		private static void TryAddGeneralInvoiceInformation(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, IOrder order, IEnumerable<OrderItem> orderItems /*, ICompany company*/) {
			if(invoice == null) invoice = new SFTIInvoiceType();
			var freeTextRows = order.ParseFreeText(); //CommonConversion.GetFreeTextRowsAsString(company, order);
			invoice.Note = TryGetValue(freeTextRows, new NoteType {Value = freeTextRows});
			invoice.IssueDate = TryGetValue(settings.InvoiceIssueDate, new IssueDateType {Value = settings.InvoiceIssueDate});
			invoice.InvoiceTypeCode = TryGetValue(settings.InvoiceTypeCode, new CodeType {Value = settings.InvoiceTypeCode});
			invoice.ID = TryGetValue(order.InvoiceNumber, new SFTISimpleIdentifierType {Value = order.InvoiceNumber.ToString()});
			invoice.AdditionalDocumentReference = TryGetValue(order.ContractCompany.Id, new List<SFTIDocumentReferenceType> {new SFTIDocumentReferenceType {ID = new IdentifierType {Value = order.ContractCompany.Id.ToString(), identificationSchemeAgencyName = "SFTI", identificationSchemeID = "ACD"}}});
			if (order.InvoiceSumIncludingVAT > 0 || order.InvoiceSumExcludingVAT > 0){
				invoice.LegalTotal = new SFTILegalTotalType {
					LineExtensionTotalAmount = TryGetLineExtensionAmount(orderItems),
					TaxExclusiveTotalAmount = TryGetValue(order.InvoiceSumExcludingVAT, new TotalAmountType {Value = (decimal) order.InvoiceSumExcludingVAT, amountCurrencyID = "SEK"}),
					TaxInclusiveTotalAmount = TryGetValue(order.InvoiceSumIncludingVAT, new TotalAmountType {Value = (decimal) order.InvoiceSumIncludingVAT, amountCurrencyID = "SEK"}),
				};
			}
			invoice.RequisitionistDocumentReference = TryGetValue(order.CustomerOrderNumber, new List<SFTIDocumentReferenceType> {
			                                                                                                                     	new SFTIDocumentReferenceType {ID = new IdentifierType{Value = order.CustomerOrderNumber}}
			                                                                                                                     }
				);
			invoice.InvoiceCurrencyCode = TryGetValue(settings.InvoiceCurrencyCode, new CurrencyCodeType {Value = settings.InvoiceCurrencyCode.GetValueOrDefault()});
			invoice.TaxPointDate = TryGetValue(order.CreatedDate, new TaxPointDateType {Value = order.CreatedDate});
			//invoice.TaxCurrencyCode = TryGetValue(settings.InvoiceCurrencyCode, new CurrencyCodeType {Value = settings.InvoiceCurrencyCode.GetValueOrDefault()});
		}

		private static void TryAddInvoiceLines(SvefakturaConversionSettings settings,  SFTIInvoiceType invoice, IEnumerable<OrderItem> orderItems , decimal VATAmount) {
			if(invoice.InvoiceLine == null) invoice.InvoiceLine = new List<SFTIInvoiceLineType>();
			var lineItemCount = 0;
			foreach (var orderItem in orderItems){
				lineItemCount++;
				invoice.InvoiceLine.Add(
					new SFTIInvoiceLineType
					{
						Item = new SFTIItemType
						{
							Description = TryGetValue(orderItem.ArticleDisplayName, new DescriptionType {Value = orderItem.ArticleDisplayName}),
							SellersItemIdentification = TryGetValue(orderItem.ArticleDisplayNumber, new SFTIItemIdentificationType {ID = new IdentifierType {Value = orderItem.ArticleDisplayNumber}}),
							BasePrice = new SFTIBasePriceType {
							                                  	PriceAmount = TryGetValue(orderItem.SinglePrice, new PriceAmountType {Value = (decimal) orderItem.SinglePrice, amountCurrencyID = "SEK"})
							                                  },
							TaxCategory = new List<SFTITaxCategoryType> 
							{
								(orderItem.NoVAT) 
									?  GetTaxCategory("E", 0, "VAT", settings.VATFreeReasonMessage) 
									: GetTaxCategory("S", VATAmount*100, "VAT", settings.VATFreeReasonMessage)
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
			TryAddTaxTotal(invoice, settings);
		}

		#region SellerParty
		private static void TryAddSellerParty(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, IShop shop) {
			if (invoice.SellerParty == null) invoice.SellerParty = new SFTISellerPartyType();
			invoice.SellerParty.Party = new SFTIPartyType 
			{
				PartyName = new NameType
				{
					Value = /*settings.SellingOrganizationName*/ shop.Name
				}.ToList(),
				Address = GetSellerPartyAddress(/*settings*/shop),
				Contact = GetSellerPartyContact(shop),
				PartyTaxScheme = GetSellerPartyTaxScheme(settings),
				PartyIdentification = new SFTIPartyIdentificationType
				{
					ID = new IdentifierType
					{
						Value = FormatOrganizationNumber(/*settings.SellingOrganizationNumber*/ shop.OrganizationNumber)
					}
				}.ToList()
			};
			invoice.SellerParty.AccountsContact = GetSellerAccountContact(settings);
		}

		private static List<T> ToList<T>(this T item)
		{
			return new List<T>(new []{item});
		}

		private static SFTIContactType GetSellerPartyContact(IShop shop) {
		    //var contactName = GetSellerPartyContractName(shop);
		    return GetSFTIContact(shop.Email, shop.ContactCombinedName, shop.Fax, shop.Phone);
		}
		private static string GetSellerPartyContractName(IShop shop){
			var shopName = shop.Name ?? String.Empty;
			var salesPerson = shop.ContactCombinedName ?? String.Empty;
			if(String.IsNullOrEmpty(shopName) && String.IsNullOrEmpty(salesPerson)) return String.Empty;
			if(String.IsNullOrEmpty(shopName)) return salesPerson;
			return String.IsNullOrEmpty(salesPerson) ? shopName : String.Format("{0} ({1})", shopName, salesPerson);
		}

		private static SFTIContactType GetSellerAccountContact(SvefakturaConversionSettings settings) {
			return GetSFTIContact(
				settings.SellingOrganizationContactEmail, 
				settings.SellingOrganizationContactName, 
				settings.SellingOrganizationFax,
				settings.SellingOrganizationTelephone
			);
		}

		private static List<SFTIPartyTaxSchemeType> GetSellerPartyTaxScheme(SvefakturaConversionSettings settings) {
			return GetPartyTaxScheme(
				settings.TaxAccountingCode, 
				settings.SellingOrganizationNumber, 
				settings.SellingOrganizationCountry, 
				settings.ExemptionReason, 
				settings.SellingOrganizationCity, 
				settings.SellingOrganizationPostBox, 
				settings.SellingOrganizationStreetName, 
				settings.SellingOrganizationPostalCode,
				settings.SellingOrganizationName
				);
		}

		private static SFTIAddressType GetSellerPartyAddress(/*SvefakturaConversionSettings settings*/ IShop shop)
		{
			return shop.GetSFTIAddress(
				x => x.Address, 
				x => x.Address2, 
				x => x.Zip, 
				x => x.City, 
				x => null, 
				x => GetSwedishCountryType());
			//return GetSFTIAddress(
			//    settings.SellingOrganizationPostBox,
			//    settings.SellingOrganizationStreetName,
			//    settings.SellingOrganizationPostalCode,
			//    settings.SellingOrganizationCity,
			//    null,
			//    settings.SellingOrganizationCountry
			//    );
		}
		#endregion

		#region BuyerParty
		private static void TryAddBuyerParty(SFTIInvoiceType invoice, ICompany company, IOrder order) {
			if (invoice.BuyerParty == null) invoice.BuyerParty = new SFTIBuyerPartyType();
			invoice.BuyerParty.Party = new SFTIPartyType
			{
         		PartyName = TryGetValue(company.InvoiceCompanyName, new List<NameType> {new NameType {Value = company.InvoiceCompanyName}}),
         		Address = GetBuyerPartyAddress(company, order),
         		Contact = GetBuyerPartyContact(order),
         		PartyTaxScheme = GetBuyerPartyTaxScheme(company),
         		PartyIdentification = TryGetValue(company.OrganizationNumber, new List<SFTIPartyIdentificationType> { new SFTIPartyIdentificationType { ID = new IdentifierType { Value = FormatOrganizationNumber(company.OrganizationNumber) } } })
			 };
		}

		private static SFTIContactType GetBuyerPartyContact(IOrder row) {
			return GetSFTIContact(row.Email, row.CustomerCombinedName, null, row.Phone);
		}

		private static List<SFTIPartyTaxSchemeType> GetBuyerPartyTaxScheme(ICompany company) {
			return GetPartyTaxScheme(
				company.TaxAccountingCode, 
				company.OrganizationNumber, 
				(company.Country != null)? TryParseCountry(company.Country) : null,
				null,
				company.City,
				company.PostBox,
				company.StreetName,
				company.Zip,
				company.InvoiceCompanyName
				);
		}

		private static SFTICountryType TryParseCountry(ICountry country){
			if (country == null) return null;
			try{
				return new SFTICountryType {
					IdentificationCode = new CountryIdentificationCodeType {
						Value = (CountryIdentificationCodeContentType) country.OrganizationCountryCodeId,
						name = country.Name
					}
				};
			}
			catch { return null; }
		}

		private static SFTIAddressType GetBuyerPartyAddress(ICompany company, IOrder orderRow) {
			return GetSFTIAddress(
				company.PostBox,
				company.StreetName,
				company.Zip,
				company.City,
				orderRow.CompanyUnit,
				(company.Country != null)? TryParseCountry(company.Country) : null
				);
		}
		#endregion

		#region PaymentMeans
		private static void TryAddPaymentMeans(SFTIInvoiceType invoice, string giroNumber, string giroBIC, ICompany company, SvefakturaConversionSettings settings) {
			if (HasNotBeenSet(settings.InvoiceIssueDate) || HasNotBeenSet(giroNumber)) return;
			if (invoice.PaymentMeans == null) invoice.PaymentMeans = new List<SFTIPaymentMeansType>();
			invoice.PaymentMeans.Add(
				new SFTIPaymentMeansType
				{
					PaymentMeansTypeCode = new PaymentMeansCodeType { Value = PaymentMeansCodeContentType.Item1 },
					PayeeFinancialAccount = new SFTIFinancialAccountType 
					{
						ID = new IdentifierType {Value = FormatGiroNumber(giroNumber)},
						FinancialInstitutionBranch = TryGetValue(giroBIC, new SFTIBranchType
						{
							FinancialInstitution = new SFTIFinancialInstitutionType
							{
								ID = new IdentifierType {Value = giroBIC}
							}
						})
					},
					DuePaymentDate = GetPaymentMeansDuePaymentDate(settings.InvoiceIssueDate, company)
				}
			);
		}

		private static PaymentDateType GetPaymentMeansDuePaymentDate(DateTime invoiceIssueDate, ICompany company) {
			if (company == null ) return null;
			if (company.PaymentDuePeriod <= 0) return null;
			return new  PaymentDateType { Value = invoiceIssueDate.AddDays(company.PaymentDuePeriod) };
		}
		#endregion

		#region PaymentTerms
		private static void TryAddPaymentTerms(SFTIInvoiceType invoice, SvefakturaConversionSettings settings, ICompany company) {
			if (AllAreNullOrEmpty(settings.InvoicePaymentTermsTextFormat, settings.InvoiceExpieryPenaltySurchargePercent)) return;
			var text = ParseInvoicePaymentTermsFormat(settings.InvoicePaymentTermsTextFormat, company);
			invoice.PaymentTerms = new SFTIPaymentTermsType 
			{
            	Note = TryGetValue(text, new NoteType {Value = text}),
            	PenaltySurchargePercent = TryGetValue(
					settings.InvoiceExpieryPenaltySurchargePercent, 
					new SurchargePercentType
					{
						Value = settings.InvoiceExpieryPenaltySurchargePercent.GetValueOrDefault()
					})
            };
		}

		private static string ParseInvoicePaymentTermsFormat(string format, ICompany company) {
			if(format == null || company == null) return null;
			format = format.Replace("{InvoiceNumberOfDueDays}", company.PaymentDuePeriod.ToString());
			return format;
		}
		#endregion

		#region Helper Methods
		private static T TryGetValue<T>(string valueToSet, T properValue) {
			return String.IsNullOrEmpty(valueToSet) ? default(T) : properValue;
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
		
		private static List<SFTIPartyTaxSchemeType> GetPartyTaxScheme(string taxAccountingCode, string orgNumber, SFTICountryType country, string exemptionReason, string city, string postBox, string streetName, string postalCode, string name) {
			var returnList = new List<SFTIPartyTaxSchemeType>();
			if(OneOrMoreHaveValue(taxAccountingCode)){
				returnList.Add(
					new SFTIPartyTaxSchemeType 
					{
                   		CompanyID = TryGetValue(taxAccountingCode, new IdentifierType
                   		{
                   			Value = FormatTaxAccountingCode(taxAccountingCode)
                   		}),
                   		TaxScheme = new SFTITaxSchemeType
                   		{
                   			ID = new IdentifierType {Value = "VAT"}
                   		}
					}
				);
			}
			if (OneOrMoreHaveValue(exemptionReason, orgNumber)){
				returnList.Add(
					new SFTIPartyTaxSchemeType 
					{
                       	ExemptionReason = TryGetValue(exemptionReason, new ReasonType {Value = exemptionReason}),
                       	CompanyID = TryGetValue(orgNumber, new IdentifierType
                       	{
                       		Value = FormatOrganizationNumber(orgNumber),                            
                       	}),
                        RegistrationName = TryGetValue(name, new RegistrationNameType{Value = name}),
                       	RegistrationAddress = GetSFTIAddress(postBox, streetName, postalCode, city, null, country),
                       	TaxScheme = new SFTITaxSchemeType { ID = new IdentifierType { Value = "SWT" } }
					}
				);
			}
			return (returnList.Count <= 0) ? null : returnList;
		}

		private static SFTIAddressType GetSFTIAddress(string postBox, string streetName,string zip,  string city, string companyUnit, SFTICountryType country) {
			if (AllAreNullOrEmpty(postBox, streetName, zip, city, companyUnit, country)) return null;
			return new SFTIAddressType
			{
				Postbox = TryGetValue(postBox, new PostboxType {Value = postBox}),
				StreetName = TryGetValue(streetName, new StreetNameType {Value = streetName}),
				PostalZone = TryGetValue(zip, new ZoneType {Value = zip}),
				Department = TryGetValue(companyUnit, new DepartmentType {Value = companyUnit}),
				CityName = TryGetValue(city, new CityNameType {Value = city}),
				Country = country
			};
		}

		private static SFTIAddressType GetSFTIAddress<T>(this T source, Func<T,string> getPostBox, Func<T,string> getStreetName, Func<T,string> getZip,  Func<T,string> getCity, Func<T,string> getCompanyUnit, Func<T,SFTICountryType> getCountry )
		{
			var postbox = getPostBox(source);
			var streetName = getStreetName(source);
			var zip = getZip(source);
			var city = getCity(source);
			var companyUnit = getCompanyUnit(source);
			var country = getCountry(source);
			return GetSFTIAddress(postbox, streetName, zip, city, companyUnit, country);
		}

		private static SFTITaxCategoryType GetTaxCategory(string identifier, decimal percent, string TaxScheme, string vatFreeReasonMessage) {
			return new SFTITaxCategoryType 
			{
           		ID = new IdentifierType {Value = identifier},
           		Percent = new PercentType {Value = percent},
           		TaxScheme = new SFTITaxSchemeType {ID = new IdentifierType {Value = TaxScheme}},
           		ExemptionReason = (identifier.Equals("E")) ? new ReasonType {Value = (String.IsNullOrEmpty(vatFreeReasonMessage)) ? "Momsfri artikel" : vatFreeReasonMessage} : null
           };
		}

		private static SFTIContactType GetSFTIContact(string email, string name, string fax, string phone) {
			if (AllAreNullOrEmpty( email, name, fax, phone)) return null;
			return new SFTIContactType 
			{
               	ElectronicMail = TryGetValue(email, new MailType {Value = email}),
               	Name = TryGetValue(name, new NameType {Value = name}),
               	Telefax = TryGetValue(fax, new TelefaxType {Value = FormatPhoneNumber(fax)}),
               	Telephone = TryGetValue(phone, new TelephoneType {Value = FormatPhoneNumber(phone)})
			};
		}

		private static string FormatPhoneNumber(string phoneNumber){
			if(phoneNumber == null) return null;
			var returnNumber = phoneNumber.Trim();
			if (returnNumber.StartsWith("+")){
				var zeroIndex = returnNumber.IndexOf('0');
				if (zeroIndex > 0) returnNumber = returnNumber.Remove(zeroIndex, 1);
			}
			return Regex.Replace(returnNumber, @"[^0-9+]", "");
		}
		private static string FormatGiroNumber(string giroNumber) { return RemoveAllButLettersAndDigits(giroNumber); }
		private static string FormatTaxAccountingCode(string taxAccountingCode) { return RemoveAllButLettersAndDigits(taxAccountingCode); }
		private static string FormatOrganizationNumber(string organizationNumber) { return RemoveAllButLettersAndDigits(organizationNumber); }

		private static string RemoveAllButLettersAndDigits(string input){
			if(input == null) return null;
			var returnString = input.ToUpper();
			return Regex.Replace(returnString, @"[^\dA-Ö]", "");
		}

		private static ExtensionTotalAmountType TryGetLineExtensionAmount(IEnumerable<OrderItem> orderItems) {
			//var result = 0m;
			var result = (decimal) orderItems.Sum(x => x.DisplayTotalPrice);
			//orderItems.ToList().ForEach( x => result += (decimal) x.DisplayTotalPrice);
			return (result <= 0) ? null : new ExtensionTotalAmountType {Value = result, amountCurrencyID ="SEK"};
		}

		private static SFTICountryType GetSwedishCountryType()
		{
			return new SFTICountryType
			{
				IdentificationCode = new CountryIdentificationCodeType
				{
					Value = CountryIdentificationCodeContentType.SE, name = "Sverige"
				}
			};
		}
		#endregion

		#region Generate TaxTotal
		private static SFTITaxTotalType GetTaxTotal(SFTIInvoiceType invoice, decimal defaultVATPercent) {
			var subtotals = new List<SFTITaxSubTotalType>(GetTaxSubTotals(invoice, defaultVATPercent));
			var totalTaxAmount = subtotals.Sum(x => x.TaxAmount.Value);
			return new SFTITaxTotalType {TaxSubTotal = subtotals, TotalTaxAmount = new TaxAmountType{Value = totalTaxAmount, amountCurrencyID ="SEK"}};
		}
		private static IEnumerable<SFTITaxSubTotalType> GetTaxSubTotals(SFTIInvoiceType invoice,  decimal defaultVATPercent) {
			var joinedLists = new List<SFTITaxSubTotalType>();
			joinedLists.AddRange(GetTaxSubTotals(invoice.AllowanceCharge, defaultVATPercent));
			joinedLists.AddRange(GetTaxSubTotals(invoice.InvoiceLine, defaultVATPercent));
			var returnList =  from p in joinedLists
			                  group p by p.TaxCategory.ID.Value into g
			                                                    	select new SFTITaxSubTotalType {
			                                                    	                               	TaxCategory = g.First().TaxCategory,
			                                                    	                               	TaxableAmount = new AmountType {Value = g.Sum(p => p.TaxableAmount.Value), amountCurrencyID = "SEK"},
			                                                    	                               	TaxAmount = new TaxAmountType {Value = g.Sum(p => p.TaxAmount.Value), amountCurrencyID = "SEK"}
			                                                    	                               };
			return returnList;
		}
		private static IEnumerable<SFTITaxSubTotalType> GetTaxSubTotals(IEnumerable<SFTIInvoiceLineType> invoiceLines,  decimal defaultVATPercent) {
			if (invoiceLines == null) return new List<SFTITaxSubTotalType>();
			return new List<SFTITaxSubTotalType>(
				from p in invoiceLines
				group p by p.Item.TaxCategory
				into g select new SFTITaxSubTotalType {
				                                      	TaxCategory = g.Key[0],
				                                      	TaxableAmount = new AmountType {Value = g.Sum(p => GetTaxableAmount(p)), amountCurrencyID = "SEK"},
				                                      	TaxAmount = new TaxAmountType {Value = g.Sum(p => GetTaxAmount(p, defaultVATPercent)), amountCurrencyID = "SEK"}
				                                      }
				);
		}
		private static IEnumerable<SFTITaxSubTotalType> GetTaxSubTotals(IEnumerable<SFTIAllowanceChargeType> allowanceCharges,  decimal defaultVATPercent) {
			if (allowanceCharges == null) return new List<SFTITaxSubTotalType>();
			return new List<SFTITaxSubTotalType>(
				from p in allowanceCharges
				group p by p.TaxCategory
				into g select new SFTITaxSubTotalType {
				                                      	TaxCategory = g.Key[0],
				                                      	TaxableAmount = new AmountType {Value = g.Sum(p => GetTaxableAmount(p)), amountCurrencyID = "SEK"},
				                                      	TaxAmount = new TaxAmountType {Value = g.Sum(p => GetTaxAmount(p, defaultVATPercent)), amountCurrencyID = "SEK"}
				                                      }
				);
		}
		private static decimal GetTaxableAmount(SFTIInvoiceLineType invoiceLine) {
			decimal returnValue = 0;
			if(invoiceLine.LineExtensionAmount != null){
				returnValue += invoiceLine.LineExtensionAmount.Value;
			}
			return returnValue;
		}
		private static decimal GetTaxableAmount(SFTIAllowanceChargeType allowanceCharge) {
			decimal returnValue = 0;
			if(allowanceCharge.Amount != null){
				returnValue = GetSignedAllowanceChargeAmount(allowanceCharge);
			}
			return returnValue;
		}
		private static decimal GetSignedAllowanceChargeAmount(SFTIAllowanceChargeType allowanceCharge) {
			if(allowanceCharge == null || allowanceCharge.ChargeIndicator == null || allowanceCharge.Amount == null) return 0;
			return (allowanceCharge.ChargeIndicator.Value) ? allowanceCharge.Amount.Value : (allowanceCharge.Amount.Value*-1);
		}
		private static decimal GetTaxAmount(SFTIInvoiceLineType invoiceLine, decimal defaultVATPercent) {
			var taxableAmount = GetTaxableAmount(invoiceLine);
			return taxableAmount*(GetInvoiceLineTaxPercent(invoiceLine, defaultVATPercent)/100);
		}
		private static decimal GetTaxAmount(SFTIAllowanceChargeType allowanceCharge, decimal defaultVATPercent) {
			var taxableAmount = GetTaxableAmount(allowanceCharge);
			return taxableAmount*(GetInvoiceLineTaxPercent(allowanceCharge, defaultVATPercent)/100);
		}
		private static decimal GetInvoiceLineTaxPercent(SFTIInvoiceLineType invoiceLine, decimal defaultVATPercent) {
			if(invoiceLine == null) return defaultVATPercent;
			if(invoiceLine.Item == null) return defaultVATPercent;
			if(invoiceLine.Item.TaxCategory == null) return defaultVATPercent;
			if(invoiceLine.Item.TaxCategory.Count <= 0) return defaultVATPercent;
			if(invoiceLine.Item.TaxCategory[0].Percent == null) return defaultVATPercent;
			return invoiceLine.Item.TaxCategory[0].Percent.Value;
		}
		private static decimal GetInvoiceLineTaxPercent(SFTIAllowanceChargeType allowanceChargeType, decimal defaultVATPercent) {
			if(allowanceChargeType == null) return defaultVATPercent;
			if(allowanceChargeType.TaxCategory == null) return defaultVATPercent;
			if(allowanceChargeType.TaxCategory.Count <= 0) return defaultVATPercent;
			if(allowanceChargeType.TaxCategory[0].Percent == null) return defaultVATPercent;
			return allowanceChargeType.TaxCategory[0].Percent.Value;
		}
		#endregion
	}
}