using System.Web.UI;
using Spinit.Wpc.Synologen.Presentation.Site.Logic.EventArguments;

namespace Spinit.Wpc.Synologen.Presentation.Site.Testpages
{
	public class TestButtonPage : Page
	{
		protected void OnValidatedPassword_Event(object sender, ValidatedEventArgs e) 
		{
			var userName = e.UserName;
			var userWasValidated = e.UserIsValidated;
		}
	}
}
