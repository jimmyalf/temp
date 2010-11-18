using System;
using System.Web.Mvc;
using NUnit.Framework;

namespace Spinit.Wpc.Synologen.Presentation.Test.TestHelpers
{
	[TestFixture]
	public abstract class ControllerTestbase<TController>
		where TController: Controller
	{
		protected ControllerTestbase()
		{
			Context = () => { };
			SetUp = () => { };
			Because = controller => { throw new AssertionException("An action for Because has not been set!"); };
			GetController = () => { throw new AssertionException("An action for GetController has not been set!"); };
		}

		[SetUp]
		protected virtual void SetUpTest()
		{
			SetUp();
			Context();
			Controller = GetController();
			Because(Controller);
		}

		protected Func<TController> GetController;
		protected Action Context;
		protected Action SetUp;
		protected Action<TController> Because;
		protected TController Controller;
	}

	[TestFixture]
	public abstract class ControllerTestbase<TController,TViewModel> : ControllerTestbase<TController>
		where TController: Controller where TViewModel : class
	{
		protected ControllerTestbase()
		{
			Because = controller => { throw new AssertionException("An action for Because has not been set!"); };
		}

		[SetUp]
		protected override void SetUpTest()
		{
			SetUp();
			Context();
			Controller = GetController();
			var actionResult = Because(Controller);
			ViewModel = GetViewModel(actionResult);
		}
		protected TViewModel ViewModel;
		protected new Func<TController,ActionResult> Because;

		protected TViewModel GetViewModel(ActionResult viewResult)
		{
			if(typeof(TViewModel).Equals(typeof(RedirectToRouteResult)))
			{
				return viewResult as TViewModel;
			}

			var view = (ViewResult) viewResult;
			return (TViewModel) view.ViewData.Model;
		}
	}
}