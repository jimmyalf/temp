namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface IAutogiroFileWriter<TFile,TItem> where TFile : IAutogiroFile<TItem>
	{
		string Write(TFile file);
	}
}