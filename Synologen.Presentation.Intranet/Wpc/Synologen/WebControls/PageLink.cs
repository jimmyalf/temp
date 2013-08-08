using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Services;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.WebControls
{
	public class PageLink : WebControl
	{
		private readonly IRoutingService _routingService;

		public PageLink()
		{
			_routingService = Spinit.Wpc.Core.UI.ServiceLocator.Current.GetInstance<IRoutingService>();
			InitializeComponent();
		}

		public PageLink(IRoutingService routingService)
		{
			_routingService = routingService;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			LinkTemplate = new LinkModelDefaultTemplate("<a class=\"page-id-{PageId}\" href=\"{Url}\">{InnerHtml}</a>");
		}
		public int PageId { get; set; }
		public string InnerHtml { get; set; }

		[Description("Link template"), TemplateContainer(typeof(LinkModel)), PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate LinkTemplate { get; set; }



		protected override void CreateChildControls()
		{
			Controls.Clear();
			var url = _routingService.GetPageUrl(PageId);
			var model = new LinkModel(PageId, url, InnerHtml);
			LinkTemplate.InstantiateIn(model);
			model.DataBind();
			Controls.Add(model);
		}
	}
	public class LinkModel: WebControl, INamingContainer
	{
		public LinkModel(int pageId, string url, string innerHtml)
		{
			PageId = pageId;
			Url = url;
			InnerHtml = innerHtml;
		}

		public int PageId { get; set; }
		public string Url { get; set; }
		public string InnerHtml { get; set; }
	}

	internal class LinkModelDefaultTemplate : ITemplate
	{
		private readonly string _html;

		internal LinkModelDefaultTemplate(string html)
		{
			_html = html;
		}

		public void InstantiateIn(Control container)
		{
			string output;
			if(container is LinkModel)
			{
				var model = container as LinkModel;
				output = _html.ReplaceWith(new {model.Url, model.InnerHtml, model.PageId});
			}
			else
			{
				output =  _html;
			}
			container.Controls.Add(new Literal{Text = output});
		}
	}
}