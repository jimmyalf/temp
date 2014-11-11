using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Core.Domain.Model.Autogiro.CommonTypes
{
	public interface IAutogiroFile<TItem>
	{
		IEnumerable<TItem> Posts { get; set; }
	}
}