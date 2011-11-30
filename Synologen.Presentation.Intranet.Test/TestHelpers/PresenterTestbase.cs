using System;
using FakeItEasy;
using Moq;
using NUnit.Framework;
using Spinit.Test.Web;
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
			//MockedView = MvpHelpers.GetMockedView<TView, TModel>();
			_model = new TModel();
			_view = GetView(_model);
			
			HttpContext = new FakeHttpContext();//new HttpContextMock();
			SetUp();
			Presenter = GetPresenter();
			Presenter.HttpContext = HttpContext;
			Context();
			Because(Presenter);
		}

		protected Func<TPresenter> GetPresenter;
		protected Action Context;
		protected Action SetUp;
		protected Action<TPresenter> Because;
		//protected Mock<TView> MockedView;
		protected FakeHttpContext HttpContext;
		protected TPresenter Presenter;
		private TView _view;
		private TModel _model;

		//protected void AssertUsing(Action<TView> action)
		//{
		//    action(View);
		//}

		protected virtual TView GetView(TModel model)
		{
			_view = A.Fake<TView>();
			A.CallTo(() => _view.Model).Returns(model);
			return _view;
		}

		protected TView View
		{
			get { return _view; }
		}

		//protected TResult GetResult<TResult>(Func<TView, TResult> function)
		//{
		//    return function(_view);
		//}
	}
}