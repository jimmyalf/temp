using System.IO;
using System.Text;

namespace Synologen.Service.Web.Invoicing.Services
{
    public class FileService : IFileService
    {
        private readonly IFileServiceConfiguration _configuration;

        public FileService(IFileServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void TrySaveContentToDisk(string fileName, string fileContent)
        {
            var encoding = _configuration.FTPCustomEncodingCodePage;
            TrySaveContentToDisk(fileName, fileContent, encoding);
        }

        public void TrySaveContentToDisk(string fileName, string fileContent, Encoding encoding)
        {
            var path = _configuration.EDIFilesFolderPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var filePath = Path.Combine(path, fileName);
            File.AppendAllText(filePath, fileContent, encoding);
        }         
    }
}