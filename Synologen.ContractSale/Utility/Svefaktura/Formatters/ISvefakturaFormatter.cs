namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura.Formatters
{
    public interface ISvefakturaFormatter
    {
        string FormatPhoneNumber(string phoneNumber);
        string FormatGiroNumber(string giroNumber);
        string FormatTaxAccountingCode(string taxAccountingCode);
        string FormatOrganizationNumber(string organizationNumber);
    }
}