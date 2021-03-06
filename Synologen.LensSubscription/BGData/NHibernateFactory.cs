using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;

namespace Synologen.LensSubscription.BGData
{
	public class NHibernateFactory
	{
		private readonly ISessionFactory _sessionFactory;
		private readonly Configuration _configuration;

		public NHibernateFactory()
		{
			_configuration = Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("BGServer")))
				.Mappings(config => 
				{
					config.FluentMappings.Conventions.Setup(s => s.Add(AutoImport.Never()));
					config.FluentMappings.AddFromAssembly(GetType().Assembly);
				})
				.BuildConfiguration();

			_sessionFactory = _configuration.BuildSessionFactory();
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

		public Configuration GetConfiguration()
		{
			return _configuration;
		}
	}
}