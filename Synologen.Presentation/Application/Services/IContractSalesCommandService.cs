using Spinit.Wpc.Synologen.Business.Domain.Entities;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public interface IContractSalesCommandService
	{
		void CancelOrder(int orderId);
		void AddArticle(Article article);
		void UpdateArticle(Article article);
	}
}