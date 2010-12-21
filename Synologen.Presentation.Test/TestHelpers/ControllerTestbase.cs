using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Presentation.Helpers;

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

		[TestFixtureSetUp]
		protected virtual void SetUpTest()
		{
			SetUp();
			Context();
			Controller = GetController();
			Because(Controller);
			ActionMessages = Controller.GetWpcActionMessages();
		}

		protected IList<IWpcActionMessage> ActionMessages;
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

		[TestFixtureSetUp]
		protected override void SetUpTest()
		{
			SetUp();
			Context();
			Controller = GetController();
			var actionResult = Because(Controller);
			ViewModel = GetViewModel(actionResult);
			ActionMessages = Controller.GetWpcActionMessages();
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