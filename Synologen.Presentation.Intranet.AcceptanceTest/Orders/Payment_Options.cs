using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders;
using Spinit.Wpc.Synologen.Core.Domain.Model.Orders.SubscriptionTypes;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.TestHelpers;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Orders;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Orders;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.AcceptanceTest.Orders
{
    [TestFixture, Category("Payment_Options")]
    public class When_selecting_payment_options : OrderSpecTestbase<PaymentOptionsPresenter, IPaymentOptionsView>
    {
        private PaymentOptionsPresenter _presenter;
    	private PaymentOptionsEventArgs _submitEventArgs;
    	private Order _order;
    	private string _abortPageUrl, _nextPageUrl, _previousPageUrl;
    	private int _selectedSubscriptionId;
    	private IEnumerable<Subscription> _subsciptions;
    	private OrderCustomer _customer;

    	public When_selecting_payment_options()
        {
            Context = () =>
            {
				SetupDataContext();
            	_abortPageUrl = "/test/abort";
				_nextPageUrl = "/test/next";
            	_previousPageUrl = "/test/previous";
				SetupNavigationEvents(_previousPageUrl, _abortPageUrl, _nextPageUrl);
                _presenter = GetPresenter();
            };

            Story = () => new Ber�ttelse("Ange betalningss�tt")
                .F�rAtt("Ange betalningss�tt f�r best�llningen")
                .Som("inloggad anv�ndare p� intran�tet")
                .VillJag("kunna v�lja betalningss�tt");
        }

    	private void SetupDataContext()
    	{
    		_customer = CreateCustomer();
			_subsciptions = CreateSubscriptions(_customer);
        	_order = CreateOrder(customer:_customer);
		}

		[Test]
		public void VisaBefintligaKontonOchKundNamn()
		{
			SetupScenario(scenario => scenario
				.Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
				.N�r(SidanVisas)
				.S�(SkallKundensBefintligaAbonnemangListas)
					.Och(KundNamnVisas)
			);
		}

    	[Test]
        public void AngeBetalningss�ttMedNyttKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
					.Och(AttAnv�ndarenValtAttBetalaMedNyttKonto)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
				.S�(SparasValtBetalningsAlternativ)
					.Och(F�rflyttasAnv�ndarenTillN�staSteg)
            );
        }

        [Test]
        public void AngeBetalningss�ttMedBefintligtKonto()
        {
            SetupScenario(scenario => scenario
				.Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
					.Och(AttAnv�ndarenValtAttBetalaBefintligtKonto)
                .N�r(Anv�ndarenF�rs�kerForts�ttaTillN�staSteg)
				.S�(SparasValtBetalningsAlternativ)
					.Och(F�rflyttasAnv�ndarenTillN�staSteg)
            );
        }

    	[Test]
        public void Avbryt()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
                .N�r(Anv�ndarenAvbryterBest�llningen)
                .S�(TasBest�llningenBort)
                    .Och(Anv�ndarenFlyttasTillAvbrytsidan));
        }

    	[Test]
        public void Bak�t()
        {
            SetupScenario(scenario => scenario
                .Givet(EnBest�llningHarSkapatsIF�reg�endeSteg)
                .N�r(Anv�ndarenF�rs�kerG�TillF�reg�endeSteg)
                .S�(F�rflyttasAnv�ndarenTillF�reg�endeSteg));
        }

    	#region Arrange
        private void EnBest�llningHarSkapatsIF�reg�endeSteg()
        {
        	var article = CreateWithRepository<IArticleRepository, Article>(() => OrderFactory.GetArticle(null, null));
        	var customer = CreateWithRepository<IOrderCustomerRepository, OrderCustomer>(() => OrderFactory.GetCustomer());
        	_order = CreateWithRepository<IOrderRepository, Order>(() => OrderFactory.GetOrder(article, customer));
        	HttpContext.SetupRequestParameter("order", _order.Id.ToString());

        }
    	private void AttAnv�ndarenValtAttBetalaMedNyttKonto()
    	{
			_selectedSubscriptionId = default(int);
    		_submitEventArgs = new PaymentOptionsEventArgs();
    	}

    	private void AttAnv�ndarenValtAttBetalaBefintligtKonto()
    	{
    		_selectedSubscriptionId = _subsciptions.First().Id;
    		_submitEventArgs = new PaymentOptionsEventArgs{ SubscriptionId = _selectedSubscriptionId};
    	}
        #endregion

        #region Act
        private void Anv�ndarenAvbryterBest�llningen()
        {
            _presenter.View_Abort(null, new EventArgs());
        }
    	private void Anv�ndarenF�rs�kerForts�ttaTillN�staSteg()
    	{
    		_presenter.View_Submit(null, _submitEventArgs);
    	}
    	private void Anv�ndarenF�rs�kerG�TillF�reg�endeSteg()
    	{
    		_presenter.View_Previous(null, new EventArgs());
    	}
    	private void SidanVisas()
    	{
    		_presenter.View_Load(null, new EventArgs());
    	}
        #endregion

        #region Assert
        private void Anv�ndarenFlyttasTillAvbrytsidan()
        {
			var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _abortPageUrl, OrderId = _order.Id});
        	HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
        }

        private void TasBest�llningenBort()
        {
            WithRepository<IOrderRepository>().Get(_order.Id).ShouldBe(null);
        }

    	private void SparasValtBetalningsAlternativ()
    	{
    		var expectedType = (_selectedSubscriptionId == default(int)) ? PaymentOptionType.Subscription_Autogiro_New : PaymentOptionType.Subscription_Autogiro_Existing;
			var expectedSubscriptionId = (_selectedSubscriptionId == default(int)) ? null : (int?) _selectedSubscriptionId;

			WithRepository<IOrderRepository>().Get(_order.Id).SelectedPaymentOption.Type.ShouldBe(expectedType);
			WithRepository<IOrderRepository>().Get(_order.Id).SelectedPaymentOption.SubscriptionId.ShouldBe(expectedSubscriptionId);
    	}

    	private void F�rflyttasAnv�ndarenTillN�staSteg()
    	{
    		var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _nextPageUrl, OrderId = _order.Id});
    		HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}

    	private void F�rflyttasAnv�ndarenTillF�reg�endeSteg()
    	{
    		var expectedUrl = "{Url}?order={OrderId}".ReplaceWith(new {Url = _previousPageUrl, OrderId = _order.Id});
    		HttpContext.ResponseInstance.RedirectedUrl.ShouldBe(expectedUrl);
    	}

    	private void SkallKundensBefintligaAbonnemangListas()
    	{
			var viewModelsubscriptions = View.Model.Subscriptions.Take(View.Model.Subscriptions.Count() - 1);
    		var matchingSubscriptions = _subsciptions.Where(x => x.Active && x.ConsentStatus == SubscriptionConsentStatus.Accepted);
    		viewModelsubscriptions.And(matchingSubscriptions).Do((viewItem, domainItem) =>
    		{
    			viewItem.Value.ShouldBe(domainItem.Id.ToString());
				viewItem.Text.ShouldBe(domainItem.BankAccountNumber);
    		});
			View.Model.Subscriptions.Last().Value.ShouldBe("0");
			View.Model.Subscriptions.Last().Text.ShouldBe("Skapa nytt konto");
    	}

		private void KundNamnVisas()
		{
			View.Model.CustomerName.ShouldBe("{FirstName} {LastName}".ReplaceWith(new {_customer.FirstName, _customer.LastName}));
		}

		//public class TestObject
		//{
		//    public override string ToString()
		//    {
		//        return "Testobject";
		//    }
		//}


		//public static void CheckLengthsDiffer<T1,T2>(IEnumerable<T1> expression1, IEnumerable<T2> expression2)
		//{
		//    var objectOneInfo = new ExpressionInfo<IEnumerable<T1>>(expression1);
		//    var objectTwoInfo = new ExpressionInfo<IEnumerable<T2>>(expression2);
		//    throw new ApplicationException("{" + objectOneInfo.BodyName + "}[" + objectOneInfo.Value.Count() + "] has a different length than {" + objectTwoInfo.BodyName + "}[" + objectTwoInfo.Value.Count() + "]");
		//    var properties = typeof(IEnumerable<T1>).GetProperties();
		//    if(properties.Length != 1) return;
		//    var name = properties[0].Name;
		//}


		//public static void CheckBothAreNull<T1,T2>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2) where T1:class where T2:class
		//{
		//    var objectOneInfo = new ExpressionInfo<T1>(expression1);
		//    var objectTwoInfo = new ExpressionInfo<T2>(expression2);
		//    if(objectOneInfo.Value == null && objectTwoInfo.Value == null)
		//    {
		//        throw new ApplicationException("Both {" + objectOneInfo.BodyName + "} and {" + objectOneInfo.BodyName + "} are null");
		//    }
		//}

		//public class ExpressionInfo<T>
		//{
		//    public string BodyName { get; private set; }
		//    public T Value { get; private set; }
		//    public ExpressionInfo(Expression<Func<T>> expression)
		//    {
		//        var body = ((MemberExpression) expression.Body);
		//        BodyName = body.Member.Name;
		//        Value = (T)((FieldInfo) body.Member).GetValue(((ConstantExpression) body.Expression).Value);
		//    }
		//}

		//public class TypeInfo<T>
		//{
		//    public string BodyName { get; private set; }
		//    public T Value { get; private set; }
		//    public TypeInfo(T input)
		//    {
		//        Value = input;
		//        var properties = typeof(T).GetProperties();
		//        if(properties.Length != 1) return;
		//        var name = properties[0].Name;
		//    }
		//}

		
		//public static void CheckEitherIsNull<T1,T2>(Expression<Func<T1>> expression1, Expression<Func<T2>> expression2) where T1:class where T2:class
		//{
		//    var objectOneInfo = new ExpressionInfo<T1>(expression1);
		//    var objectTwoInfo = new ExpressionInfo<T2>(expression2);
		//    if(objectOneInfo.Value == null)
		//    {
		//        throw new ApplicationException("{" + objectOneInfo.BodyName + "}(null) is null but {" + objectTwoInfo.BodyName + "}(" + objectTwoInfo.Value + ") is not");
		//    }
		//    if(objectTwoInfo.Value == null)
		//    {
		//        throw new ApplicationException("{" + objectOneInfo.BodyName + "}(" + objectOneInfo.Value + ") is not null but {" + objectTwoInfo.BodyName + "}(null) is");
		//    }
		//}
        #endregion
    }
}