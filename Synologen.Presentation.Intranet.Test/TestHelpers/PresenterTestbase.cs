using System;
using Moq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Intranet.Test.MockHelpers;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Test.TestHelpers
{
	[TestFixture]
	public abstract class PresenterTestbase<TPresenter,TView, TModel> 
		where TView : class, IView<TModel> 
		where TModel : class, new()
		where TPresenter: Presenter<TView>
	{
		protected PresenterTestbase()
		{
			Context = () => { };
			SetUp = () => { };
			Because = presenter => { throw new AssertionException("An action for Because has not been set!"); };
			GetPresenter = () => { throw new AssertionException("An action for GetPresenter has not been set!"); };
		}

		[TestFixtureSetUp]
		protected void SetUpTest()
		{
			MockedView = MvpHelpers.GetMockedView<TView, TModel>();
			MockedHttpContext = new HttpContextMock();
			SetUp();
			Presenter = GetPresenter();
			Presenter.HttpContext = MockedHttpContext.Object;
			Context();
			Because(Presenter);
		}

		protected Func<TPresenter> GetPresenter;
		protected Action Context;
		protected Action SetUp;
		protected Action<TPresenter> Because;
		protected Mock<TView> MockedView;
		protected HttpContextMock MockedHttpContext;
		protected TPresenter Presenter;

		protected void AssertUsing(Action<TView> action)
		{
			action(MockedView.Object);
		}

		protected TView View
		{
			get { return MockedView.Object; }
		}

		protected TResult GetResult<TResult>(Func<TView, TResult> function)
		{
			return function(MockedView.Object);
		}
	}
}