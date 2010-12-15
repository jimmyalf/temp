using System;
using System.Web.UI;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Views
{
	public interface IValidationButtonView : IView<ValidationButtonModel>
	{
		event EventHandler ValidateClick;
		event EventHandler<ValidationEventArgs> SubmitClick;
		event EventHandler CloseClick;
		event EventHandler<ValidatedEventArgs> Validation;
		event EventHandler<ValidatedEventArgs> ValidateSuccess;
		event EventHandler<ValidatedEventArgs> ValidateFailure;
		event EventHandler Event;
		string ButtonText{ get; set;}
		string ButtonSubmitText { get; set; }
		string LabelText{ get; set;}
		bool? CloseFormOnValidationFailure { get; set; }
		string CloseButtonText { get; set; }

		ITemplate MessageTemplate { get; set; }
		ITemplate ErrorMessageTemplate { get; set; }
		ITemplate HeaderTemplate { get; set; }
		ITemplate FooterTemplate { get; set; }
	}
}