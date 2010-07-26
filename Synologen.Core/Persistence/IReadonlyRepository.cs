using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Persistence
{
	public interface IReadonlyRepository<TEntity> where TEntity : class
	{
		TEntity Get(int id);
		IEnumerable<TEntity> GetAll();
		IEnumerable<TEntity> FindBy<TActionCriteria>(TActionCriteria criteria) where TActionCriteria : IActionCriteria;
	}
}