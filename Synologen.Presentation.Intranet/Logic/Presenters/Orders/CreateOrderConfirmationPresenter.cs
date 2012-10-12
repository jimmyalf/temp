using System;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders
{
	public class CreateOrderConfirmationPresenter : OrderBasePresenter<ICreateOrderConfirmationView>
	{
		private readonly IRoutingService _routingService;
		private readonly IOrderRepository _orderRepository;
		private readonly ISubscriptionRepository _subscriptionRepository;
		private readonly ITransactionRepository _transactionRepository;
		private readonly OrderWithdrawalService _orderWithdrawalService;

		public CreateOrderConfirmationPresenter(
			ICreateOrderConfirmationView view, 
			IRoutingService routingService, 
			IOrderRepository orderRepository, 
			ISubscriptionRepository subscriptionRepository,
			ISynologenMemberService synologenMemberService,
			ITransactionRepository transactionRepository,
			OrderWithdrawalService orderWithdrawalService) : base(view, synologenMemberService)
		{
			_orderRepository = orderRepository;
			_subscriptionRepository = subscriptionRepository;
			_transactionRepository = transactionRepository;
			_orderWithdrawalService = orderWithdrawalService;
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
			CheckAccess(order.Shop);
			View.Model.CustomerName = order.Customer.ParseName(x => x.FirstName, x => x.LastName);
			View.Model.Address = order.Customer.ParseName(x => x.AddressLineOne, x => x.AddressLineTwo);
			View.Model.City = order.Customer.City;
			View.Model.Email = order.Customer.Email;
			View.Model.FirstName = order.Customer.FirstName;
			View.Model.LastName = order.Customer.LastName;
			View.Model.MobilePhone = order.Customer.MobilePhone;
			View.Model.PersonalIdNumber = order.Customer.PersonalIdNumber;
			View.Model.PostalCode = order.Customer.PostalCode;
			View.Model.Telephone = order.Customer.Phone;

			
			View.Model.LeftAddition = order.LensRecipe.With(x => x.Addition).Return(x => x.Left, null);
			View.Model.LeftAxis = order.LensRecipe.With(x => x.Axis).Return(x => x.Left, null);
			View.Model.LeftPower = order.LensRecipe.With(x => x.Power).Return(x => x.Left, null);
			View.Model.LeftBaseCurve = order.LensRecipe.With(x => x.BaseCurve).With(x => x.Left).Return(x => x.ToString(), null);
			View.Model.LeftDiameter = order.LensRecipe.With(x => x.Diameter).With(x => x.Left).Return(x => x.ToString(), null);
			View.Model.LeftCylinder = order.LensRecipe.With(x => x.Cylinder).Return(x => x.Left, null);

			View.Model.RightAddition = order.LensRecipe.With(x => x.Addition).Return(x => x.Right, null);
			View.Model.RightAxis = order.LensRecipe.With(x => x.Axis).Return(x => x.Right, null);
			View.Model.RightPower = order.LensRecipe.With(x => x.Power).Return(x => x.Right, null);
			View.Model.RightBaseCurve = order.LensRecipe.With(x => x.BaseCurve).With(x => x.Right).Return(x => x.ToString(), null);
			View.Model.RightDiameter = order.LensRecipe.With(x => x.Diameter).With(x => x.Right).Return(x => x.ToString(), null);
			View.Model.RightCylinder = order.LensRecipe.With(x => x.Cylinder).Return(x => x.Right, null);
			View.Model.ArticleLeft = order.LensRecipe.Article.With(x => x.Left).Return(x => x.Name, null);
			View.Model.ArticleRight = order.LensRecipe.Article.With(x => x.Right).Return(x => x.Name, null);
			View.Model.QuantityLeft = order.LensRecipe.Quantity.Left;
			View.Model.QuantityRight = order.LensRecipe.Quantity.Right;

			View.Model.DeliveryOption = order.ShippingType.GetEnumDisplayName();
			View.Model.ProductPrice = GetCurrencyString(order.SubscriptionPayment.Value.Taxed);
			View.Model.FeePrice = GetCurrencyString(order.SubscriptionPayment.Value.TaxFree);
			View.Model.Monthly = GetCurrencyString(order.SubscriptionPayment.MonthlyWithdrawal.Total);
			View.Model.TotalWithdrawal = GetCurrencyString(order.WithdrawalAmount.Total);
			View.Model.SubscriptionTime = GetSubscriptionTime(order.SubscriptionPayment);
			var isAlreadyConsentedSubscription = OrderSubscriptionIsActiveAndConsented(order.SubscriptionPayment.Subscription);
			View.Model.ExpectedFirstWithdrawalDate = _orderWithdrawalService
				.GetExpectedFirstWithdrawalDate(order.SubscriptionPayment.CreatedDate, isAlreadyConsentedSubscription)
				.ToString("yyyy-MM-dd");
		}

		private string GetSubscriptionTime(SubscriptionItem subscriptionItem)
		{
			return subscriptionItem.IsOngoing ? "Löpande" : String.Format("{0} månader", subscriptionItem.WithdrawalsLimit);
		}

		private bool OrderSubscriptionIsActiveAndConsented(Subscription subscription)
		{
			return subscription.ConsentStatus == SubscriptionConsentStatus.Accepted && subscription.Active;
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
			CreateTransaction(order);
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

		private void CreateTransaction(Order order)
		{
			var transaction = new SubscriptionTransaction
			{
				Reason = TransactionReason.Withdrawal,
				Subscription = order.SubscriptionPayment.Subscription,
				Type = TransactionType.Withdrawal
			};
			transaction.SetAmount(order.WithdrawalAmount);
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

	}
}