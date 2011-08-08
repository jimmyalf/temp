<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ArticleList.ascx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.Site.wpc.Commerce.ArticleList" %>
<%@ Import Namespace="Spinit.Wpc.Commerce.Core"%>
<asp:Repeater runat="server" ID="rptArticles" OnItemDataBound="rptArticles_ItemDataBound">
<HeaderTemplate><ul id="articles" class="clear-fix"></HeaderTemplate>
<ItemTemplate>
	<li class="item<%# DataBinder.Eval(Container.DataItem, "Id") %>">
		<div class="article-image">
			<asp:Image ID="picArticle" Visible="false" runat="server" AlternateText=" " />
		</div>
		<div class="article-summary">
				<div class="article-head clear-fix">
					<h2 class="article-title">
						<%# DataBinder.Eval(Container.DataItem, "Name") %>					
					</h2>
					<div class="article-description"><asp:Literal ID="ltDescription" runat="server" Text="Description" /></div>
				</div>								
		</div>
		<div class="article-details">
			<div class="article-data">
				<h3>Produktdata</h3>
				<dl>
					<dt>ArtikelNr:</dt><dd><asp:Literal ID="ltArticleNr" runat="server" Text="-"/></dd>
					<asp:Repeater ID="rptArticleAttribute" runat="server">
						<HeaderTemplate></HeaderTemplate>
						<ItemTemplate>					
									<dt><%# ((ProductAttribute) Container.DataItem).Attribute.Name %>:</dt>
									<dd><%# DataBinder.Eval(Container.DataItem, "Value") %></dd>
						</ItemTemplate>
						<FooterTemplate></FooterTemplate>
					</asp:Repeater>
				</dl>
			</div>
			<asp:PlaceHolder ID="phSheets" runat="server">
				<asp:Repeater ID="rptSheets" runat="server" OnItemDataBound="rptSheets_ItemDataBound">
					<HeaderTemplate>
						<div class="article-sheets"><h4>Teknisk info</h4><ul>
					</HeaderTemplate>
					<ItemTemplate>
						<li>
							<asp:HyperLink ID="hlSheetLink" runat="server"/>
						</li>
					</ItemTemplate>
					<FooterTemplate></ul></div></FooterTemplate>
				</asp:Repeater>
			</asp:PlaceHolder>
		</div>		
	</li>
</ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>

