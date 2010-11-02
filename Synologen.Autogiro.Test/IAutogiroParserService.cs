namespace Synologen.Autogiro.Test
{
	public interface IAutogiroParserService
	{
		string ParseConsents(Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.ConsentsFile file);
		string ParsePayments(Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Send.PaymentsFile file);
		Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.PaymentsFile ReadPayments(string fileContents);
		Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ConsentsFile ReadConsents(string fileContents);
		Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.Recieve.ErrorsFile ReadErrors(string fileContents);
	}
}