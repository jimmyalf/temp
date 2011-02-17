using NHibernate;
using Spinit.Data.NHibernate;
using Spinit.Wpc.Synologen.Core.Domain.Model.BGServer;

namespace Synologen.LensSubscription.BGData.Repositories
{
	public class FileSectionToSendRepository : NHibernateRepository<FileSectionToSend>
	{
		public FileSectionToSendRepository(ISession session) : base(session) {}
	}
}