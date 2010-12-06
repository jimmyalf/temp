namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface IAutogiroFileReader<TFile,TItem> where TFile : IAutogiroFile<TItem>
	{
		TFile Read();
	}
}