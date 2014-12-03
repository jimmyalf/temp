namespace Spinit.Wpc.Synologen.Core.Domain.Services.Web.External
{
	public interface IValidator<in TType>
	{
		ValidationResult Validate(TType customer);
	}
}