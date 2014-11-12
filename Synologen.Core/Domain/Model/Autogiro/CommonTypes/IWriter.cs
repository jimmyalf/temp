namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface IWriter<TModel>
	{
		string Write(TModel item);
	}
}