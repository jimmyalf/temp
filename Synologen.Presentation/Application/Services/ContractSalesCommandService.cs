using Spinit.Wpc.Synologen.Business.Domain.Interfaces;
using Spinit.Wpc.Utility.Business;

namespace Spinit.Wpc.Synologen.Presentation.Application.Services
{
	public class ContractSalesCommandService : IContractSalesCommandService
	{
		private readonly ISqlProvider _synologenSqlProvider;
		private readonly IUserContextService _userContextService;
		private const int InvoiceCanceledStatusId = 7;
		private const string OrderCancelHistoryMessage = "Order makulerad manuellt av användare \"{UserName}\".";

		public ContractSalesCommandService(ISqlProvider synologenSqlProvider, IUserContextService userContextService)
		{
			_synologenSqlProvider = synologenSqlProvider;
			_userContextService = userContextService;
		}

		public void CancelOrder(int orderId)
		{
			var order = _synologenSqlProvider.GetOrder(orderId);
			order.StatusId = InvoiceCanceledStatusId;
			_synologenSqlProvider.AddUpdateDeleteOrder(Enumerations.Action.Update, ref order);
			var userName = _userContextService.GetLoggedInUser().User.UserName;
			_synologenSqlProvider.AddOrderHistory(orderId, OrderCancelHistoryMessage.Replace("{UserName}", userName));
		}
	}

	public interface IContractSalesCommandService
	{
		void CancelOrder(int orderId);
	}
}