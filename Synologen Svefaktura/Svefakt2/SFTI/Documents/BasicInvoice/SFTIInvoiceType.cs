using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:documents:BasicInvoice:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Invoice", Namespace="urn:sfti:documents:BasicInvoice:1:0", IsNullable=false)]
	public partial class SFTIInvoiceType {
    
		private SFTISimpleIdentifierType idField;
    
		private IssueDateType issueDateField;
    
		private CodeType invoiceTypeCodeField;
    
		private NoteType noteField;
    
		private TaxPointDateType taxPointDateField;
    
		private CurrencyCodeType invoiceCurrencyCodeField;
    
		private CurrencyCodeType taxCurrencyCodeField;
    
		private LineItemCountNumericType lineItemCountNumericField;
    
		private List<SFTIDocumentReferenceType> additionalDocumentReferenceField = new List<SFTIDocumentReferenceType>();
    
		private SFTIBuyerPartyType buyerPartyField;
    
		private SFTISellerPartyType sellerPartyField;
    
		private SFTIDeliveryType deliveryField;

		private List<SFTIPaymentMeansType> paymentMeansField = new List<SFTIPaymentMeansType>();
    
		private SFTIPaymentTermsType paymentTermsField;
    
		private List<SFTIAllowanceChargeType> allowanceChargeField = new List<SFTIAllowanceChargeType>();
    
		private SFTIExchangeRateType exchangeRateField;
    
		private List<SFTITaxTotalType> taxTotalField = new List<SFTITaxTotalType>();
    
		private SFTILegalTotalType legalTotalField;
    
		private List<SFTIInvoiceLineType> invoiceLineField = new List<SFTIInvoiceLineType>();
    
		private List<SFTIDocumentReferenceType> requisitionistDocumentReferenceField = new List<SFTIDocumentReferenceType>();

		private List<SFTIDocumentReferenceType> initialInvoiceDocumentReferenceField = new List<SFTIDocumentReferenceType>();
    
		private SFTIDeliveryTermsType deliveryTermsField;
    
		private SFTIPeriodType invoicingPeriodField;
    
		/// <remarks/>
		public SFTISimpleIdentifierType ID {
			get {
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public IssueDateType IssueDate {
			get {
				return this.issueDateField;
			}
			set {
				this.issueDateField = value;
			}
		}
    
		/// <remarks/>
		public CodeType InvoiceTypeCode {
			get {
				return this.invoiceTypeCodeField;
			}
			set {
				this.invoiceTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NoteType Note {
			get {
				return this.noteField;
			}
			set {
				this.noteField = value;
			}
		}
    
		/// <remarks/>
		public TaxPointDateType TaxPointDate {
			get {
				return this.taxPointDateField;
			}
			set {
				this.taxPointDateField = value;
			}
		}
    
		/// <remarks/>
		public CurrencyCodeType InvoiceCurrencyCode {
			get {
				return this.invoiceCurrencyCodeField;
			}
			set {
				this.invoiceCurrencyCodeField = value;
			}
		}
    
		/// <remarks/>
		public CurrencyCodeType TaxCurrencyCode {
			get {
				return this.taxCurrencyCodeField;
			}
			set {
				this.taxCurrencyCodeField = value;
			}
		}
    
		/// <remarks/>
		public LineItemCountNumericType LineItemCountNumeric {
			get {
				return this.lineItemCountNumericField;
			}
			set {
				this.lineItemCountNumericField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AdditionalDocumentReference")]
		public List<SFTIDocumentReferenceType> AdditionalDocumentReference {
			get {
				return this.additionalDocumentReferenceField;
			}
			set {
				this.additionalDocumentReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIBuyerPartyType BuyerParty {
			get {
				return this.buyerPartyField;
			}
			set {
				this.buyerPartyField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTISellerPartyType SellerParty {
			get {
				return this.sellerPartyField;
			}
			set {
				this.sellerPartyField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIDeliveryType Delivery {
			get {
				return this.deliveryField;
			}
			set {
				this.deliveryField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("PaymentMeans", Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public List<SFTIPaymentMeansType> PaymentMeans {
			get {
				return this.paymentMeansField;
			}
			set {
				this.paymentMeansField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIPaymentTermsType PaymentTerms {
			get {
				return this.paymentTermsField;
			}
			set {
				this.paymentTermsField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AllowanceCharge")]
		public List<SFTIAllowanceChargeType> AllowanceCharge {
			get {
				return this.allowanceChargeField;
			}
			set {
				this.allowanceChargeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIExchangeRateType ExchangeRate {
			get {
				return this.exchangeRateField;
			}
			set {
				this.exchangeRateField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("TaxTotal", Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public List<SFTITaxTotalType> TaxTotal {
			get {
				return this.taxTotalField;
			}
			set {
				this.taxTotalField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTILegalTotalType LegalTotal {
			get {
				return this.legalTotalField;
			}
			set {
				this.legalTotalField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InvoiceLine", Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public List<SFTIInvoiceLineType> InvoiceLine {
			get {
				return this.invoiceLineField;
			}
			set {
				this.invoiceLineField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("RequisitionistDocumentReference")]
		public List<SFTIDocumentReferenceType> RequisitionistDocumentReference {
			get {
				return this.requisitionistDocumentReferenceField;
			}
			set {
				this.requisitionistDocumentReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("InitialInvoiceDocumentReference")]
		public List<SFTIDocumentReferenceType> InitialInvoiceDocumentReference {
			get {
				return this.initialInvoiceDocumentReferenceField;
			}
			set {
				this.initialInvoiceDocumentReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
		public SFTIDeliveryTermsType DeliveryTerms {
			get {
				return this.deliveryTermsField;
			}
			set {
				this.deliveryTermsField = value;
			}
		}
    
		/// <remarks/>
		public SFTIPeriodType InvoicingPeriod {
			get {
				return this.invoicingPeriodField;
			}
			set {
				this.invoicingPeriodField = value;
			}
		}
	}
}