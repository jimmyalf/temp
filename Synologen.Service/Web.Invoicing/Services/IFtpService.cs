namespace Synologen.Service.Web.Invoicing.Services
{
    public interface IFtpService
    {
        string UploadTextFileToFTP(string fileName, string fileContent, int companyId);
    }
}