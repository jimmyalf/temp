using System;
using System.Web.UI;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Extensions;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Presenters;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.Views;
using Spinit.Wpc.Synologen.Presentation.Site.Models;
using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen
{
	[PresenterBinding(typeof(ValidationButtonPresenter))]
	public partial class ValidationButton :  MvpUserControl<ValidationButtonModel>, IValidationButtonView
	{
		protected const string DefaultMessage = "<p>Åtgärden ni försöker slutföra kräver förhöjd säkerhet. Var vänlig verifiera behörighet genom att ange ert lösenord nedan.</p>";
		protected const string DefaultErrorMessage = "<p class=\"error\">Lösenordsverifiering misslyckades</p>";
		protected const string DefaultButtonText = "Spara";
		protected const string DefaultButtonSubmitText = "Skicka";
		protected const string DefaultCloseButtonText = "Avbryt";
		protected const string DefaultLabelText = "Lösenord";
		public event EventHandler<ValidatedEventArgs> Validation;
		public event EventHandler<ValidatedEventArgs> ValidateSuccess;
		public event EventHandler<ValidatedEventArgs> ValidateFailure;
		public event EventHandler ValidateClick;
		public event EventHandler CloseClick;
		public event EventHandler Event;
		public event EventHandler<ValidationEventArgs> SubmitClick;
		public string ButtonText { get; set; }
		public string ButtonSubmitText { get; set; }
		public string LabelText { get; set; }
		public bool? CloseFormOnValidationFailure { get; set; }
		public string CloseButtonText { get; set; }

		[TemplateContainer(typeof(MessageContainer))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate HeaderTemplate { get; set; }

		[TemplateContainer(typeof(MessageContainer))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate FooterTemplate { get; set; }

		[TemplateContainer(typeof(MessageContainer))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate MessageTemplate { get; set; }

		[TemplateContainer(typeof(MessageContainer))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate ErrorMessageTemplate { get; set; }


		public ValidationButton()
		{
			SetupDefaultResources();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			SetupTemplates();
		}

		public void SetupDefaultResources()
		{
			MessageTemplate = new TemplateControlImplementation(DefaultMessage);
			ErrorMessageTemplate = new TemplateControlImplementation(DefaultErrorMessage);
			ButtonText = DefaultButtonText;
			ButtonSubmitText = DefaultButtonSubmitText;
			CloseButtonText = DefaultCloseButtonText;
			LabelText = DefaultLabelText;
		}
	
		public void SetupTemplates()
		{
			plHeader.SetupTemplate(HeaderTemplate);
			plFooter.SetupTemplate(FooterTemplate);
			plMessagePlaceHolder.SetupTemplate(MessageTemplate);
			plErrorMessagePlaceHolder.SetupTemplate(ErrorMessageTemplate);
		}

		protected void btn_Validate(object sender, EventArgs e) 
		{
			Event.TryInvoke(sender, e);
			ValidateClick.TryInvoke(sender, e);
		}

		protected void btn_Submit(object sender, EventArgs e) 
		{
			Event.TryInvoke(sender, e);
			var eventArgs = new ValidationEventArgs(txtPassword.Text);
			if (Validation != null) eventArgs.Validation += Validation;
			if (ValidateFailure != null) eventArgs.ValidationFailure += ValidateFailure;
			if (ValidateSuccess != null) eventArgs.ValidationSuccess += ValidateSuccess;
			SubmitClick.TryInvoke(sender, eventArgs);
		}

		protected void btn_Close(object sender, EventArgs e) 
		{
			Event.TryInvoke(sender, e);
			CloseClick.TryInvoke(sender, e);
		}
	}
}