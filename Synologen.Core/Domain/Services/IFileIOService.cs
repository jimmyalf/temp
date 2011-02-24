namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFileIOService
	{
		void WriteFile(string filePath, string contents);
		bool FileExists(string filePath);
	}
}