using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;

namespace Spinit.Wpc.Synologen.Data.Repositories.NHibernate
{
	public class NHibernateFactory
	{
		private readonly ISessionFactory _sessionFactory;
		private readonly Configuration _configuartion;

		public NHibernateFactory()
		{
			_configuartion = Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2000.ConnectionString(c => c.FromConnectionStringWithKey("WpcServer")))
				.Mappings(config => 
				{
					config.FluentMappings.Conventions.Setup(s => s.Add(AutoImport.Never()));
					config.FluentMappings.AddFromAssembly(GetType().Assembly);
				})
				.BuildConfiguration();

			_sessionFactory = _configuartion.BuildSessionFactory();
		}

		private static NHibernateFactory _instance;
		public static NHibernateFactory Instance
		{
			get
			{
				if (_instance==null)
					_instance = new NHibernateFactory();

				return _instance;
			}
		}

		public ISessionFactory GetSessionFactory()
		{
			return _sessionFactory;
		}
	}
}