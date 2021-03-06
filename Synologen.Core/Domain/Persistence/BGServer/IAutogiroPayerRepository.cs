using Spinit.Data;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer
{
	public interface IAutogiroPayerRepository : IRepository<AutogiroPayer>
	{
		/// <summary>
		/// Save method overridden to allow testing of return id
		/// </summary>
		/// <returns>Returns object ID</returns>
		new int Save(AutogiroPayer entity);
	}
}