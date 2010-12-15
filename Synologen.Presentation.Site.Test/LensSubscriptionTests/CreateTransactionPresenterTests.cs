using System;
using System.Collections.Specialized;
using Moq;
using NUnit.Framework;
using Shouldly;
using Spinit.Wpc.Synologen.Core.Domain.Model.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments.LensSubscription;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.Factories;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests.TestHelpers;

namespace Spinit.Wpc.Synologen.Presentation.Site.Test.LensSubscriptionTests
{
	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_loading_create_transaction_view_without_reason : CreateTransactionTestbase
	{
		public When_loading_create_transaction_view_without_reason()
		{
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Choose_reason_display_should_be_visible()
		{
			AssertUsing(view => 
			{
				view.Model.DisplayChooseReason.ShouldBe(true);
				view.Model.DisplaySaveCorrection.ShouldBe(false);
				view.Model.DisplaySaveWithdrawal.ShouldBe(false);
			});
		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_loading_create_withdrawal_transaction_view : CreateTransactionTestbase
	{
		public When_loading_create_withdrawal_transaction_view()
		{
			const int _subscriptionId = 5;
			var _reasonId =TransactionReason.Withdrawal.ToInteger();

			Context = () => 
			{
				MockedHttpContext.SetupQueryString(new NameValueCollection
				{
					{ "subscription", _subscriptionId.ToString() }, 
					{ "reason", _reasonId.ToString() }
				});
			};
	
			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			AssertUsing( view =>
			{
				view.Model.Type.ShouldBe(TransactionType.Withdrawal);
				view.Model.Reason.ShouldBe(TransactionReason.Withdrawal);
			});
		}

		[Test]
		public void Withdrawal_display_should_be_visible()
		{
			AssertUsing( view =>
			{
				view.Model.DisplayChooseReason.ShouldBe(false);
				view.Model.DisplaySaveCorrection.ShouldBe(false);
				view.Model.DisplaySaveWithdrawal.ShouldBe(true);
			});
		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_loading_create_correction_transaction_view : CreateTransactionTestbase
	{
		public When_loading_create_correction_transaction_view()
		{
			const int _subscriptionId = 5;
			var _reasonId = TransactionReason.Correction.ToInteger();
			Context = () => 
			{
				MockedHttpContext.SetupQueryString(new NameValueCollection
				{
					{ "subscription", _subscriptionId.ToString() }, 
					{ "reason", _reasonId.ToString() }
				});
			};

			Because = presenter => presenter.View_Load(null, new EventArgs());
		}

		[Test]
		public void Model_should_have_expected_values()
		{
			AssertUsing(view => view.Model.Reason.ShouldBe(TransactionReason.Correction));
		}

		[Test]
		public void Correction_display_should_be_visible()
		{
			AssertUsing( view =>
			{
				view.Model.DisplayChooseReason.ShouldBe(false);
				view.Model.DisplaySaveCorrection.ShouldBe(true);
				view.Model.DisplaySaveWithdrawal.ShouldBe(false);
			});
		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class Presenter_gets_reason_and_redirects_to_current_page : CreateTransactionTestbase
	{
		
		private const int _subscriptionId = 5;
		private const string _reasonId = "2";
		private readonly string _currentPageUrl;
		public Presenter_gets_reason_and_redirects_to_current_page()
		{
			_currentPageUrl = "/test/redirect/";
			var currentPagePathAndQuery = _currentPageUrl + string.Format("?subscription={0}", _subscriptionId);

			Context = () => 
			{
				MockedHttpContext.SetupSingleQuery("reason", _reasonId);
				MockedHttpContext.SetupCurrentPathAndQuery(currentPagePathAndQuery);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_SetReason(null, new TransactionReasonEventArgs { Reason = TransactionReason.Withdrawal });	
			};
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			MockedHttpContext.VerifyRedirect("{0}?subscription={1}&reason={2}", _currentPageUrl, _subscriptionId, _reasonId);
		}
	}

	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class Presenter_gets_cancel_and_redirects_to_current_page : CreateTransactionTestbase
	{
		private const int _subscriptionId = 5;
		private const string _reasonId = "2";
		private readonly string _currentPageUrl;

		public Presenter_gets_cancel_and_redirects_to_current_page()
		{
			_currentPageUrl = "/test/redirect/";
			var currentPagePathAndQuery = String.Format("{0}?subscription={1}", _currentPageUrl, _subscriptionId);

			Context = () =>
			{
				MockedHttpContext.SetupQueryString( new NameValueCollection
				{
					{ "subscription", _subscriptionId.ToString() }, 
					{ "reason", _reasonId }
				});

				MockedHttpContext.SetupCurrentPathAndQuery(currentPagePathAndQuery);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_Cancel(null, null);
			};
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			MockedHttpContext.VerifyRedirect("{0}?subscription={1}", _currentPageUrl, _subscriptionId);
		}
	}


	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_submitting_create_transaction_view : CreateTransactionTestbase
	{
		private readonly SaveTransactionEventArgs _saveEventArgs;
		private readonly string _currentPageUrl;
		private readonly TransactionType _expectedTransactionType;
		private readonly TransactionReason _expectedTransactionReason;
		private const int _subscriptionId = 5;

		public When_submitting_create_transaction_view()
		{
			// Arrange
			const int customerId = 5;
			const int countryId = 1;
			const int shopId = 5;
			_currentPageUrl = "/test/redirect/";
			_expectedTransactionType = TransactionType.Deposit;
			_expectedTransactionReason = TransactionReason.Payment;
			var subscription = SubscriptionFactory.Get(_subscriptionId, CustomerFactory.Get(customerId, countryId, shopId));
			_saveEventArgs = new SaveTransactionEventArgs
			{
				Amount = 1234.56M, 
				TransactionType = _expectedTransactionType.ToInteger().ToString(), 
				TransactionReason = _expectedTransactionReason.ToInteger().ToString()
			};

			Context = () => 
			{
				MockedHttpContext.SetupSingleQuery("subscription", _subscriptionId.ToString());
				MockedHttpContext.SetupCurrentPathAndQuery(_currentPageUrl);
				MockedSubscriptionRepository.Setup(x => x.Get(It.IsAny<int>())).Returns(subscription);
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.View_Submit(null, _saveEventArgs);
			};
		}

		[Test]
		public void Presenter_saves_transaction_with_expected_values()
		{
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(t => t.Amount.Equals(_saveEventArgs.Amount))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(t => t.Type.Equals(_expectedTransactionType))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(t => t.Reason.Equals(_expectedTransactionReason))));
			MockedTransactionRepository.Verify(x => x.Save(It.Is<SubscriptionTransaction>(t => t.Subscription.Id.Equals(_subscriptionId))));
		}

		[Test]
		public void Presenter_perfoms_redirect_to_current_page()
		{
			MockedHttpContext.VerifyRedirect("{0}?subscription={1}", _currentPageUrl, _subscriptionId);
		}
	}


	[TestFixture]
	[Category("CreateTransactionPresenterTester")]
	public class When_updating_create_transaction_view : CreateTransactionTestbase
	{
		private UpdateTransactionModelEventArgs _updateEventArgs;

		public When_updating_create_transaction_view()
		{
			Context = () =>
			{
				_updateEventArgs = new UpdateTransactionModelEventArgs
				{
					Amount = "1234.56", 
					TransactionType = TransactionType.Deposit.ToInteger()
				};
			};

			Because = presenter =>
			{
				presenter.View_Load(null, new EventArgs());
				presenter.Form_Update(null, _updateEventArgs);
			};
		}

		[Test]
		public void Model_should_contain_updated_values()
		{
			AssertUsing(view => 
			{
				view.Model.Amount.ShouldBe(_updateEventArgs.Amount);
				view.Model.SelectedTransactionType.ShouldBe(_updateEventArgs.TransactionType);
			});
		}

	}
}
