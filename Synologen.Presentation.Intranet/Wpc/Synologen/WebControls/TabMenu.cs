using System.Collections.Generic;
using System.Web.UI;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.WebControls
{
	[ParseChildren(typeof(TabMenuItem), DefaultProperty = "Items",ChildrenAsProperties = true)]
	public class TabMenu : Control
	{
		public TabMenu()
		{
			Items = new TabMenuItemCollection();
		}

		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TabMenuItemCollection Items { get; private set; }

		protected override void Render(HtmlTextWriter writer)
		{
			writer.WriteFullBeginTag("nav");
			writer.WriteFullBeginTag("ul");
			foreach (var tabMenuItem in Items)
			{
				tabMenuItem.RenderItem(writer);
			}
			writer.WriteEndTag("li");
			writer.WriteEndTag("nav");

/*<nav id="tab-navigation">
	<ul>
		<li><a href="#"><span>1</span> Välj Kund</a></li>
		<li><span>2</span> Skapa Beställning</li>
		<li><span>3</span> Betalningssätt</li>
		<li><span>4</span> Autogiro Information</li>
		<li><span>5</span> Bekräfta</li>
	</ul>
</nav>*/
		}
	}

	public class TabMenuItemCollection : List<TabMenuItem>{ }

	public class TabMenuItem
	{
		public int PageId { get; set; }
		public string Text { get; set; }

		public void RenderItem(HtmlTextWriter writer)
		{
			writer.WriteFullBeginTag("li");
			writer.WriteBeginTag("a");
			writer.WriteAttribute("href","#");
			writer.Write(HtmlTextWriter.TagRightChar);
			writer.Write(Text);
			writer.WriteEndTag("a");
			writer.WriteEndTag("li");
		}
	}
}