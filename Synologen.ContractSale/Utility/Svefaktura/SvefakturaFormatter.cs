using System.Text.RegularExpressions;

namespace Spinit.Wpc.Synologen.Invoicing.Svefaktura
{
    public class SvefakturaFormatter : ISvefakturaFormatter
    {
        public virtual string FormatPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == null)
            {
                return null;
            }

            var returnNumber = phoneNumber.Trim();
            if (returnNumber.StartsWith("+"))
            {
                var zeroIndex = returnNumber.IndexOf('0');
                if (zeroIndex > 0)
                {
                    returnNumber = returnNumber.Remove(zeroIndex, 1);
                }
            }

            return Regex.Replace(returnNumber, @"[^0-9+]", string.Empty);
        }
        public virtual string FormatGiroNumber(string giroNumber)
        {
            return RemoveAllButLettersAndDigits(giroNumber);
        }
        public virtual string FormatTaxAccountingCode(string taxAccountingCode)
        {
            return RemoveAllButLettersAndDigits(taxAccountingCode);
        }
        public virtual string FormatOrganizationNumber(string organizationNumber)
        {
            return RemoveAllButLettersAndDigits(organizationNumber);
        }
        protected virtual string RemoveAllButLettersAndDigits(string input)
        {
            if (input == null)
            {
                return null;
            }

            var returnString = input.ToUpper();

            return Regex.Replace(returnString, @"[^\dA-Ö]", string.Empty);
        }
    }
}