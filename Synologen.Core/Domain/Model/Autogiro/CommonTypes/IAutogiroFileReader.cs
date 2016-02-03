namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface IAutogiroFileReader<TFile,TItem> : IAutogiroFileReader<TFile> 
		where TFile : IAutogiroFile<TItem> { }

	public interface IAutogiroFileReader<TFile>
	{
		TFile Read(string fileContents);
	}
}