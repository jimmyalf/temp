using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Synologen.LensSubscription.BGServiceCoordinator.App.Services
{
	public class BGFileIOService : IFileIOService
	{
		public void WriteFile(string filePath, string contents) 
		{
			System.IO.File.WriteAllText(filePath, contents);
		}
		public bool FileExists(string filePath)
		{
			return System.IO.File.Exists(filePath);
		}
	}
}