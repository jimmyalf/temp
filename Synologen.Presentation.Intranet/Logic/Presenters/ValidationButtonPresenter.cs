using System;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters
{
	public class ValidationButtonPresenter : Presenter<IValidationButtonView>
	{
		private readonly ISynologenMemberService _synologenMemberService;

		public ValidationButtonPresenter(IValidationButtonView view, ISynologenMemberService synologenMemberService) : base(view)
		{
			_synologenMemberService = synologenMemberService;
			View.ValidateClick += View_Click;
			View.SubmitClick += View_SubmitClick;
			View.CloseClick += View_CloseClick;
		}


		private void View_Click(object sender, EventArgs e) 
		{
			View.Model.DisplayValidationForm = true;
		}

		private void View_SubmitClick(object sender, ValidationEventArgs e) 
		{
			View.Model.PasswordText = String.Empty;
			var isValid = _synologenMemberService.ValidateUserPassword(e.Password);
			var userName = _synologenMemberService.GetUserName();
			View.Model.DisplayValidationForm = Equals(View.CloseFormOnValidationFailure, true) ? isValid : !isValid;
			View.Model.DisplayValidationFailureMessage = !isValid;
			var args = new ValidatedEventArgs { UserName = userName, UserIsValidated = isValid };
			e.InvokeValidation(args);
			if(isValid) e.InvokeValidationSuccess(args);
			else e.InvokeValidationFailure(args);
		}

		private void View_CloseClick(object sender, EventArgs e)
		{
			View.Model.DisplayValidationForm = false;
			View.Model.DisplayValidationFailureMessage = false;
		}

		public override void ReleaseView()
		{
			View.ValidateClick -= View_Click;
			View.SubmitClick -= View_SubmitClick;
			View.CloseClick -= View_CloseClick;
		}
	}
}