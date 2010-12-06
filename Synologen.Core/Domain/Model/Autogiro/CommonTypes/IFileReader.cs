namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface IFileReader<TFile, TItem> where TFile : IAutogiroFile<TItem>
	{
		TFile Read(IItemReader<TItem> itemReader);
	}
}