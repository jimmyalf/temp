namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public abstract class AutogiroFileReader<TFile,TItem> : IAutogiroFileReader<TFile,TItem> 
		where TFile : IAutogiroFile<TItem>
	{
		private readonly IFileReader<TFile, TItem> _fileReader;
		private readonly IItemReader<TItem> _itemReader;

		protected AutogiroFileReader(IFileReader<TFile, TItem> fileReader, IItemReader<TItem> itemReader)
		{
			_fileReader = fileReader;
			_itemReader = itemReader;
		}

		public TFile Read()
		{
			return _fileReader.Read(_itemReader);
		}
	}
}