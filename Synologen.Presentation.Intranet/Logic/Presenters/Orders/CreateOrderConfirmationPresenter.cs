using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class CreateOrderConfirmationPresenter : Presenter<ICreateOrderConfirmationView>
	{
		private readonly IRoutingService _routingService;
		private readonly IOrderRepository _orderRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ITransactionRepository _transactionRepository;

		public CreateOrderConfirmationPresenter(ICreateOrderConfirmationView view, IRoutingService routingService, IOrderRepository orderRepository, ISubscriptionRepository subscriptionRepository, ITransactionRepository transactionRepository) : base(view)
		{
			_orderRepository = orderRepository;
			_subscriptionRepository = subscriptionRepository;
			_transactionRepository = transactionRepository;
			_routingService = routingService;
			WireupEvents();
		}

		private void WireupEvents()
		{
			View.Previous += View_Previous;
			View.Abort += View_Abort;
			View.Load += View_Load;
			View.Submit += View_Submit;
		}

		public void View_Load(object sender, EventArgs e)
		{
			if (!RequestOrderId.HasValue) return;
			var order = _orderRepository.Get(RequestOrderId.Value);
			View.Model.CustomerName = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
			View.Model.Address = order.Customer.ParseName(x => x.AddressLineOne, x => x.AddressLineTwo);
			View.Model.City = order.Customer.City;
			View.Model.Email = order.Customer.Email ?? "";
			View.Model.FirstName = order.Customer.FirstName;
			View.Model.LastName = order.Customer.LastName;
			View.Model.MobilePhone = order.Customer.MobilePhone ?? "";
			View.Model.PersonalIdNumber = order.Customer.PersonalIdNumber;
			View.Model.PostalCode = order.Customer.PostalCode;
			View.Model.Telephone = order.Customer.Phone ?? "";

			
			View.Model.LeftAddition = order.LensRecipe.Addition != null ? order.LensRecipe.Addition.Left.ToString() : "";
			View.Model.LeftAxis = order.LensRecipe.Axis != null ? order.LensRecipe.Axis.Left.ToString() : "";
			View.Model.LeftPower = order.LensRecipe.Power != null ? order.LensRecipe.Power.Left.ToString() : "";
			View.Model.LeftBaseCurve = order.LensRecipe.BaseCurve != null ? order.LensRecipe.BaseCurve.Left.ToString() : "";
			View.Model.LeftDiameter = order.LensRecipe.Diameter != null ? order.LensRecipe.Diameter.Left.ToString() : "";
			View.Model.LeftCylinder = order.LensRecipe.Cylinder != null ? order.LensRecipe.Cylinder.Left.ToString() : "";
			View.Model.RightAddition = order.LensRecipe.Addition != null ? order.LensRecipe.Addition.Right.ToString() : "";
			View.Model.RightAxis = order.LensRecipe.Axis != null ? order.LensRecipe.Axis.Right.ToString() : "";
			View.Model.RightPower = order.LensRecipe.Power != null ? order.LensRecipe.Power.Right.ToString() : "";
			View.Model.RightBaseCurve = order.LensRecipe.BaseCurve != null ? order.LensRecipe.BaseCurve.Right.ToString() : "";
			View.Model.RightDiameter = order.LensRecipe.Diameter != null ? order.LensRecipe.Diameter.Right.ToString() : "";
			View.Model.RightCylinder = order.LensRecipe.Cylinder != null ? order.LensRecipe.Cylinder.Right.ToString() : "";

			View.Model.Article = order.Article.Name;
			View.Model.DeliveryOption = order.ShippingType.GetEnumDisplayName();
			View.Model.TaxedAmount = GetCurrencyString(order.SubscriptionPayment.TaxedAmount);
			View.Model.TaxfreeAmount = GetCurrencyString(order.SubscriptionPayment.TaxFreeAmount);
			View.Model.Monthly = GetCurrencyString(order.SubscriptionPayment.AmountForAutogiroWithdrawal);
			View.Model.TotalWithdrawal = GetCurrencyString(order.OrderTotalWithdrawalAmount);
			View.Model.SubscriptionTime = GetSubscriptionTimeString(order.SubscriptionPayment.WithdrawalsLimit);
		}

		private static string GetCurrencyString(decimal? value)
		{
			return value.HasValue ? value.Value.ToString("C") : 0.ToString("C");
		}

		public void View_Submit(object o, EventArgs eventArgs)
		{
			if(!RequestOrderId.HasValue)
			{
				throw new ApplicationException("View cannot be submitted without order id");
			}
			var order = _orderRepository.Get(RequestOrderId.Value);
			TryCreateTransaction(order);
			TryActivateSubscription(order);
			UpdateOrderStatus(order);
			Redirect(View.NextPageId, new { order = RequestOrderId });
		}

		private void TryActivateSubscription(Order order)
		{
			if (order.SelectedPaymentOption.Type != PaymentOptionType.Subscription_Autogiro_New) return;
			var subscription = order.SubscriptionPayment.Subscription;
			subscription.Active = true;
			_subscriptionRepository.Save(subscription);
		}

		private void TryCreateTransaction(Order order)
		{
			if(order.ShippingType == OrderShippingOption.DeliveredInStore) return;
			var transaction = new SubscriptionTransaction
			{
				Amount = order.OrderTotalWithdrawalAmount ?? default(decimal),
				Reason = TransactionReason.Withdrawal,
				Subscription = order.SubscriptionPayment.Subscription,
				Type = TransactionType.Withdrawal
			};
			_transactionRepository.Save(transaction);
		}

		private void UpdateOrderStatus(Order order)
		{
			order.Status = OrderStatus.Confirmed;
			_orderRepository.Save(order);
		}

		public void View_Previous(object sender, EventArgs e)
		{
			Redirect(View.PreviousPageId, new {order = RequestOrderId});
		}

		public void View_Abort(object sender, EventArgs e)
		{
			if (!RequestOrderId.HasValue) return;
			var order = _orderRepository.Get(RequestOrderId.Value);
			var isNewSubscription = order.SelectedPaymentOption.Type.Equals(PaymentOptionType.Subscription_Autogiro_New);
			var subscription = order.SubscriptionPayment.Subscription;

			_orderRepository.DeleteOrderAndSubscriptionItem(order);
			if (isNewSubscription) _subscriptionRepository.Delete(subscription);

			Redirect(View.AbortPageId);
		}

		private int? RequestOrderId
		{
			get { return HttpContext.Request.Params["order"].ToNullableInt(); }
		}

		private void Redirect(int pageId, object requestParameters = null)
		{
			var url = _routingService.GetPageUrl(pageId, requestParameters);
			HttpContext.Response.Redirect(url);
		}

		public override void ReleaseView()
		{
			View.Previous -= View_Previous;
			View.Abort -= View_Abort;
		}

		private static string GetSubscriptionTimeString(int? numberOfPayments)
		{
			return numberOfPayments == null 
				? "Fortlöpande" 
				: String.Format("{0} månader", numberOfPayments.Value);
		}

	}
}