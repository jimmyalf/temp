using System.Text;

namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IFileService
    {
        void TrySaveContentToDisk(string fileName, string fileContent);
        void TrySaveContentToDisk(string fileName, string fileContent, Encoding encoding);
    }
}