namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public interface ICustomerValidator
	{
		 ValidationResult Validate(Customer customer);
	}
}