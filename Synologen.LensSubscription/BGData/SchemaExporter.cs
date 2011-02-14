using System.Data;
using System.IO;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Synologen.LensSubscription.BGData
{
	public class SchemaExporter
	{
		public void Export(Configuration configuration)
		{
			new SchemaExport(configuration).Execute(false, true, false);
		}

		public void Export(Configuration configuration, IDbConnection dbConnection, TextWriter textWriter)
		{
			new SchemaExport(configuration).Execute(false, true, false, dbConnection, textWriter);
		}
	}

	public static class SchemaExporterExtensions
	{
		public static void Export(this Configuration configuration)
		{
			new SchemaExporter().Export(configuration);
		}

		public static void Export(this Configuration configuration, IDbConnection dbConnection, TextWriter textWriter)
		{
			new SchemaExporter().Export(configuration, dbConnection, textWriter);
		}
	}
}