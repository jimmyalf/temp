namespace Spinit.Wpc.Synologen.Autogiro.Test
{
	public interface IAutogiroParserService
	{
		string ParseConsents(Core.Domain.Model.Autogiro.Send.ConsentsFile file);
		string ParsePayments(Core.Domain.Model.Autogiro.Send.PaymentsFile file);
		Core.Domain.Model.Autogiro.Recieve.PaymentsFile ReadPayments(string fileContents);
		Core.Domain.Model.Autogiro.Recieve.ConsentsFile ReadConsents(string fileContents);
		Core.Domain.Model.Autogiro.Recieve.ErrorsFile ReadErrors(string fileContents);
	}
}