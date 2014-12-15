namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IFileWriterService
	{
		void WriteFileToDisk(string fileContents, string fileName);
	}

	public class WriteToDiskResult
	{
		public WriteToDiskResult(bool success) : this(success, null) { } 

		public WriteToDiskResult(bool success, string errorMessage)
		{
			Success = success;
			ErrorMessage = errorMessage;
		}

		public bool Success { get; private set; }
		public string ErrorMessage { get; private set; }
	}
}