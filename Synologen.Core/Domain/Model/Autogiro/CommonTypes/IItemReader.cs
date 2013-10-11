namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface IItemReader<TItem>
	{
		TItem Read(string line);
	}
}