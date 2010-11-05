using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Core.Domain.Persistence.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Models.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Core.Extensions;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_loading_create_transaction_view
	{

		private readonly ICreateTransactionView _view;
		private readonly Mock<ITransactionRepository> _mockedTransactionRepository;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;

		public When_loading_create_transaction_view()
		{
			// Arrange
			var mockedView = new Mock<ICreateTransactionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateTransactionModel());
			_view = mockedView.Object;

			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection());

			_mockedTransactionRepository = new Mock<ITransactionRepository>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var presenter = new CreateTransactionPresenter(mockedView.Object, _mockedTransactionRepository.Object, _mockedSubscriptionRepository.Object) { HttpContext = mockedHttpContext.Object };

			// Act
			presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			_view.Model.TypeList.Count().Equals(3).ShouldBe(true);
		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_loading_create_withdrawal_transaction_view
	{
		private readonly ICreateTransactionView _view;
		private readonly Mock<ITransactionRepository> _mockedTransactionRepository;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private const int _subscriptionId = 5;
		private const string _reasonId = "2";

		public When_loading_create_withdrawal_transaction_view()
		{
			// Arrange
			var mockedView = new Mock<ICreateTransactionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateTransactionModel());

			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "subscription", _subscriptionId.ToString() }, { "reason", _reasonId } });

			_view = mockedView.Object;
			_mockedTransactionRepository = new Mock<ITransactionRepository>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var presenter = new CreateTransactionPresenter(mockedView.Object, _mockedTransactionRepository.Object, _mockedSubscriptionRepository.Object) { HttpContext = mockedHttpContext.Object };

			// Act
			presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			_view.Model.DisplayChooseReason.ShouldBe(false);
			_view.Model.DisplaySaveCorrection.ShouldBe(false);
			_view.Model.DisplaySaveWithdrawal.ShouldBe(true);
		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_loading_create_correction_transaction_view
	{
		private readonly ICreateTransactionView _view;
		private readonly Mock<ITransactionRepository> _mockedTransactionRepository;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private const int _subscriptionId = 5;
		private const string _reasonId = "3";

		public When_loading_create_correction_transaction_view()
		{
			// Arrange
			var mockedView = new Mock<ICreateTransactionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateTransactionModel());

			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "subscription", _subscriptionId.ToString() }, { "reason", _reasonId } });

			_view = mockedView.Object;
			_mockedTransactionRepository = new Mock<ITransactionRepository>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var presenter = new CreateTransactionPresenter(mockedView.Object, _mockedTransactionRepository.Object, _mockedSubscriptionRepository.Object) { HttpContext = mockedHttpContext.Object };

			// Act
			presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			_view.Model.DisplayChooseReason.ShouldBe(false);
			_view.Model.DisplaySaveCorrection.ShouldBe(true);
			_view.Model.DisplaySaveWithdrawal.ShouldBe(false);
		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_loading_create_choose_transaction_view
	{
		private readonly ICreateTransactionView _view;
		private readonly Mock<ITransactionRepository> _mockedTransactionRepository;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private const int _subscriptionId = 5;
		private const string _reasonId = "";

		public When_loading_create_choose_transaction_view()
		{
			// Arrange
			var mockedView = new Mock<ICreateTransactionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateTransactionModel());

			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "subscription", _subscriptionId.ToString() }, { "reason", _reasonId } });

			_view = mockedView.Object;
			_mockedTransactionRepository = new Mock<ITransactionRepository>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var presenter = new CreateTransactionPresenter(mockedView.Object, _mockedTransactionRepository.Object, _mockedSubscriptionRepository.Object) { HttpContext = mockedHttpContext.Object };

			// Act
			presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			_view.Model.DisplayChooseReason.ShouldBe(true);
			_view.Model.DisplaySaveCorrection.ShouldBe(false);
			_view.Model.DisplaySaveWithdrawal.ShouldBe(false);

		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class Presenter_gets_reason_and_redirects_to_current_page
	{
		
		private readonly Mock<ITransactionRepository> _mockedTransactionRepository;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private const int _subscriptionId = 5;
		private const string _reasonId = "2";
		private readonly string _currentPageUrl;
		private readonly Mock<HttpResponseBase> _mockedHttpResponse;
		public Presenter_gets_reason_and_redirects_to_current_page()
		{
			// Arrange

			_currentPageUrl = "/test/redirect/";
			var currentPageUri = "http://www.test.se" + _currentPageUrl;

			_mockedHttpResponse = new Mock<HttpResponseBase>();
			var mockedView = new Mock<ICreateTransactionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateTransactionModel());

			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "reason", _reasonId } });
			mockedHttpContext.SetupGet(x => x.Response).Returns(_mockedHttpResponse.Object);
			mockedHttpContext.SetupGet(x => x.Request.Url).Returns(new Uri(currentPageUri + string.Format("?subscription={0}", _subscriptionId)));

			_mockedTransactionRepository = new Mock<ITransactionRepository>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var presenter = new CreateTransactionPresenter(
				mockedView.Object,
				_mockedTransactionRepository.Object,
				_mockedSubscriptionRepository.Object)
			                	{
			                		HttpContext = mockedHttpContext.Object
			                	};

			// Act
			var eventArgs = new TransactionReasonEventArgs { Reason = TransactionReason.Withdrawal };
			
			presenter.View_Load(null, new EventArgs());
			presenter.View_SetReason(null, eventArgs);
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			_mockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(string.Format(_currentPageUrl + "?subscription={0}&reason={1}", _subscriptionId, _reasonId)))));

		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class Presenter_gets_cancel_and_redirects_to_current_page
	{
		private readonly Mock<ITransactionRepository> _mockedTransactionRepository;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private const int _subscriptionId = 5;
		private const string _reasonId = "2";
		private readonly string _currentPageUrl;
		private readonly Mock<HttpResponseBase> _mockedHttpResponse;
		public Presenter_gets_cancel_and_redirects_to_current_page()
		{
			// Arrange

			_currentPageUrl = "/test/redirect/";
			var currentPageUri = "http://www.test.se" + _currentPageUrl;

			_mockedHttpResponse = new Mock<HttpResponseBase>();
			var mockedView = new Mock<ICreateTransactionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateTransactionModel());

			var mockedHttpContext = new Mock<HttpContextBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "subscription", _subscriptionId.ToString() }, { "reason", _reasonId } });
			mockedHttpContext.SetupGet(x => x.Response).Returns(_mockedHttpResponse.Object);
			mockedHttpContext.SetupGet(x => x.Request.Url).Returns(new Uri(currentPageUri + string.Format("?subscription={0}", _subscriptionId)));

			_mockedTransactionRepository = new Mock<ITransactionRepository>();
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			var presenter = new CreateTransactionPresenter(
				mockedView.Object,
				_mockedTransactionRepository.Object,
				_mockedSubscriptionRepository.Object)
			{
				HttpContext = mockedHttpContext.Object
			};

			// Act
			presenter.View_Load(null, new EventArgs());
			presenter.View_Cancel(null, null);
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			_mockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(string.Format(_currentPageUrl + "?subscription={0}", _subscriptionId)))));
		}
	}


	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_submitting_create_transaction_view
	{
		private readonly Mock<ITransactionRepository> _mockedTransactionRepository;
		private readonly Mock<ISubscriptionRepository> _mockedSubscriptionRepository;
		private readonly SaveTransactionEventArgs _saveEventArgs;
		private readonly string _currentPageUrl;
		private readonly Mock<HttpResponseBase> _mockedHttpResponse;
		private const int _subscriptionId = 5;
		private readonly string _currentPageUri;

		public When_submitting_create_transaction_view()
		{
			// Arrange
			const int customerId = 5;
			const int countryId = 1;
			const int shopId = 5;
			_currentPageUrl = "/test/redirect/";
			_currentPageUri = "http://www.test.se" + _currentPageUrl;
			var mockedHttpContext = new Mock<HttpContextBase>();
			_mockedHttpResponse = new Mock<HttpResponseBase>();
			mockedHttpContext.SetupGet(x => x.Request.Params).Returns(new NameValueCollection { { "subscription", _subscriptionId.ToString() } });
			mockedHttpContext.SetupGet(x => x.Response).Returns(_mockedHttpResponse.Object);
			mockedHttpContext.SetupGet(x => x.Request.Url).Returns(new Uri(_currentPageUri));

			var mockedView = new Mock<ICreateTransactionView>();
			mockedView.SetupGet(x => x.Model).Returns(new CreateTransactionModel());
			
			_mockedSubscriptionRepository = new Mock<ISubscriptionRepository>();
			_mockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(
				SubscriptionFactory.Get(_subscriptionId, CustomerFactory.Get(customerId, countryId, shopId)));
			
			_mockedTransactionRepository = new Mock<ITransactionRepository>();
			var presenter = new CreateTransactionPresenter(mockedView.Object,
															_mockedTransactionRepository.Object,
			                                               _mockedSubscriptionRepository.Object) { HttpContext = mockedHttpContext.Object };

			// Act
			_saveEventArgs = new SaveTransactionEventArgs
								{
									Amount = 1234.56M,
									TransactionType = "1",
									TransactionReason = "2"
								};

			presenter.View_Load(null, new EventArgs());
			presenter.View_Submit(null, _saveEventArgs);
		}

		[Test]
		public void Presenter_gets_expected_transaction()
		{
			_mockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(t => t.Amount.Equals(_saveEventArgs.Amount))));
			_mockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(t => t.Type.ToInteger().Equals(int.Parse(_saveEventArgs.TransactionType)))));
			_mockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(t => t.Reason.ToInteger().Equals(int.Parse(_saveEventArgs.TransactionReason)))));
			_mockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(t => t.Subscription.Id.Equals(_subscriptionId))));
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			_mockedHttpResponse.Verify(x => x.Redirect(It.Is<string>(url => url.Equals(_currentPageUrl + string.Format("?subscription={0}", _subscriptionId)))));
		}
	}
}
