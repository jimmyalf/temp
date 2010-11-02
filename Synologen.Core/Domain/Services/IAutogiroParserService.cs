namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IAutogiroParserService
	{
		string ParseConsents(Model.Autogiro.Send.ConsentsFile file);
		string ParsePayments(Model.Autogiro.Send.PaymentsFile file);
		Model.Autogiro.Recieve.PaymentsFile ReadPayments(string fileContents);
		Model.Autogiro.Recieve.ConsentsFile ReadConsents(string fileContents);
		Model.Autogiro.Recieve.ErrorsFile ReadErrors(string fileContents);
	}
}