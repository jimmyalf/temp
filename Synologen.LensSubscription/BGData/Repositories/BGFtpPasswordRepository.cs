using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.BGServer;

namespace Synologen.LensSubscription.BGData.Repositories
{
	public class BGFtpPasswordRepository : IBGFtpPasswordRepository
	{
		private readonly ISession _session;

		public BGFtpPasswordRepository(ISession session)
		{
			_session = session;
		}
		public void Add(BGFtpPassword password)
		{
			_session.Save(password);
		}
		public BGFtpPassword GetLast()
		{
			return _session
				.CreateCriteriaOf<BGFtpPassword>()
				.Sort(x => x.Created, false)
				.SetMaxResults(1)
				.UniqueResult<BGFtpPassword>();
		}

		public bool PasswordExists(string password)
		{
			var list = _session.CreateCriteriaOf<BGFtpPassword>().FilterEqual(x => x.Password, password).List();
			if(list == null) return false;
			return list.Count > 0;
		}
	}
}