namespace Spinit.Wpc.Synologen.Core.Domain.Services
{
	public interface IGridSortPropertyMappingService
	{
		string TryFindMapping(string propertyName, string controllerName);
	}
}