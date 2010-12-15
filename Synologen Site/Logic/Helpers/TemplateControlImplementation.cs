using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers
{
	public class TemplateControlImplementation : ITemplate
	{
		private readonly string _innerHtml;
		public TemplateControlImplementation(string html) { _innerHtml = html; }

		public void InstantiateIn(Control container) 
		{ 
			container.Controls.Add(new Literal{Text = _innerHtml});
		}
	}
}