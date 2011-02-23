namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface ITamperProtectedFileWriter 
	{
		string Write(string fileContents);
		string KeyVerificationToken { get; }
	}
}