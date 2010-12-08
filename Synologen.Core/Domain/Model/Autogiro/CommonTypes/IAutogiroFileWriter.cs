namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface IAutogiroFileWriter<TFile,TItem> : IAutogiroFileWriter<TFile>
		where TFile : IAutogiroFile<TItem> { }

	public interface IAutogiroFileWriter<TFile>
	{
		string Write(TFile file);
	}
}