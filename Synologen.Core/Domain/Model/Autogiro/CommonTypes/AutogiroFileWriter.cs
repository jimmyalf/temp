using System.Text;
using Spinit.Extensions;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public abstract class AutogiroFileWriter<TFile,TItem> : IAutogiroFileWriter<TFile,TItem> 
		where TFile : IAutogiroFile<TItem>
	{
		private readonly IHeaderWriter<TFile> _headerWriter;
		private readonly IItemWriter<TItem> _itemWriter;
		private readonly IFooterWriter<TFile> _footerWriter;

		protected AutogiroFileWriter(IHeaderWriter<TFile> headerWriter, IItemWriter<TItem> itemWriter)
		{
			_headerWriter = headerWriter;
			_itemWriter = itemWriter;
		}

		protected AutogiroFileWriter(IHeaderWriter<TFile> headerWriter, IItemWriter<TItem> itemWriter, IFooterWriter<TFile> footerWriter)
		{
			_headerWriter = headerWriter;
			_itemWriter = itemWriter;
			_footerWriter = footerWriter;
		}

		public string Write(TFile file)
		{
			var builder = new StringBuilder();

			if(_headerWriter != null)
			{
				builder.AppendLine(_headerWriter.Write(file));
			}
			if(_itemWriter != null)
			{
				file.Posts.Each(rowItem => builder.AppendLine(_itemWriter.Write(rowItem)));	
			}

			if(_footerWriter != null)
			{
				builder.AppendLine(_footerWriter.Write(file));
			}
			
			return builder.ToString().TrimEnd(new []{'\r','\n'});
		}
	}
}