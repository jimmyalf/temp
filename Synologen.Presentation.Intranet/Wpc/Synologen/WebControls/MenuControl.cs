using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Services;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Helpers;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.WebControls
{
	[ParseChildren(typeof(MenuItem), DefaultProperty = "Items", ChildrenAsProperties = true)]
	public class MenuControl : WebControl, INamingContainer
	{
		private readonly IRoutingService _routingService;
		private bool _stopRenderingLinks;
		public bool DisableLinksAfterSelectedItem { get; set; }
		public bool IncludeCurrentQuery { get; set; }

		public MenuControl()
		{
			_routingService = Spinit.Wpc.Core.UI.ServiceLocator.Current.GetInstance<IRoutingService>();
			InitializeComponent();
		}

		public MenuControl(IRoutingService routingService)
		{
			_routingService = routingService;
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			Items = new MenuItemCollection();
			HeaderTemplate = new MenuItemDefaultTemplate("<ul>");
			MenuItemTemplate = new MenuItemDefaultTemplate("<li><a href=\"{Url}\">{Text}</a></li>");
			SelectedMenuItemTemplate = new MenuItemDefaultTemplate("<li class=\"selected\"><a href=\"{Url}\">{Text}</a></li>");
			AfterSelectedMenuItemTemplate = new MenuItemDefaultTemplate("<li>{Text}</li>");
			FooterTemplate = new TemplateControlImplementation("</ul>");
		}

		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual MenuItemCollection Items { get; private set; }

		[Description("Header template."), TemplateContainer(typeof(HeaderFooterModel)), PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate HeaderTemplate { get; set; }

		[Description("Footer template."), TemplateContainer(typeof(HeaderFooterModel)), PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate FooterTemplate { get; set; }

		[Description("Menu item template."), TemplateContainer(typeof(MenuModel)), PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate MenuItemTemplate { get; set; }

		[Description("Selected menu item template."), TemplateContainer(typeof(MenuModel)), PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate SelectedMenuItemTemplate { get; set; }

		[Description("After selected menu item template."), TemplateContainer(typeof(MenuModel)), PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate AfterSelectedMenuItemTemplate { get; set; }

		protected virtual string GetUrl(MenuItem menuItem)
		{
			if(!string.IsNullOrEmpty(menuItem.Url)) return menuItem.Url;
			return !menuItem.PageId.HasValue ? null : _routingService.GetPageUrl(menuItem.PageId.Value);
		}

		protected virtual bool IsCurrentPage(string url)
		{
			return (url ?? string.Empty).ToLower().Equals(CurrentPath.ToLower());
		}

		protected virtual string CurrentPath
		{
			get { return Page.Request.Url.AbsolutePath; }
		}

		protected override void CreateChildControls()
		{
			_stopRenderingLinks = false;
			Controls.Clear();
			RenderHeaderFooterContent(HeaderTemplate, Controls);
			foreach (var menuItem in Items)
			{
				RenderItem(Controls, menuItem);
			}
			RenderHeaderFooterContent(FooterTemplate, Controls);
		}

		public override void DataBind()
		{
			CreateChildControls();
			ChildControlsCreated = true;
			base.DataBind();
		}

		protected virtual void RenderItem(ControlCollection collection, MenuItem item)
		{
		    var url = GetUrl(item);
		    var isCurrentPage = IsCurrentPage(url);
			var outputUrl = url + (IncludeCurrentQuery ? Page.Request.Url.Query : string.Empty);
			var menuData = new MenuModel(item.PageId, outputUrl, item.Text);
			if(_stopRenderingLinks)
			{
				AfterSelectedMenuItemTemplate.InstantiateIn(menuData);
			}
			else if(isCurrentPage)
			{
				SelectedMenuItemTemplate.InstantiateIn(menuData);
			}
			else
			{
				MenuItemTemplate.InstantiateIn(menuData);	
			}
			collection.Add(menuData);
			if(isCurrentPage && DisableLinksAfterSelectedItem)
		    {
		        _stopRenderingLinks = true;
		    }
		}

		protected virtual void RenderHeaderFooterContent(ITemplate template, ControlCollection collection)
		{
			var headerItem = new HeaderFooterModel();
			template.InstantiateIn(headerItem);
			collection.Add(headerItem);
		}

		public override void RenderBeginTag(HtmlTextWriter writer) { }

		public override void RenderEndTag(HtmlTextWriter writer) { }
	}

	[ToolboxItem(false)] 
	internal class MenuModel : WebControl, INamingContainer
	{
		internal MenuModel(int? pageId, string url, string text)
		{
			PageId = pageId;
			Url = url;
			Text = text;
		}
		public string Url { get; private set; }
		public string Text { get; private set; }
		public int? PageId { get; private set; }

		public override void RenderBeginTag(HtmlTextWriter writer) { }
		public override void RenderEndTag(HtmlTextWriter writer) { }
	}

	[ToolboxItem(false)] 
	internal class HeaderFooterModel : WebControl, INamingContainer
	{
		public override void RenderBeginTag(HtmlTextWriter writer) { }
		public override void RenderEndTag(HtmlTextWriter writer) { }
	}

	internal class MenuItemDefaultTemplate : ITemplate
	{
		private readonly string _html;

		internal MenuItemDefaultTemplate(string html)
		{
			_html = html;
		}

		public void InstantiateIn(Control container)
		{
			string output;
			if(container is MenuModel)
			{
				var model = container as MenuModel;
				output = _html.ReplaceWith(new {model.Url, model.Text, model.PageId});
			}
			else
			{
				output =  _html;
			}
			container.Controls.Add(new Literal{Text = output});
		}
	}

	public class MenuItemCollection : List<MenuItem> { }
	
	[ParseChildren(true, DefaultProperty = "Text")]
	public class MenuItem : ITextControl
	{
		public int? PageId { get; set; }
		[DefaultValue(""), PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public string Text { get; set; }
		public string Url { get; set; }
	}

}