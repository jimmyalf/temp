using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Synologen.Data;

namespace Spinit.Wpc.Synologen.Presentation.Site.App
{
	public static class Factory
	{
		 public static ISqlProvider GetSqlProvider()
		 {
		 	return new SqlProvider(Base.Business.Globals.ConnectionString);
		 }
	}
}