<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductList.ascx.cs" Inherits="Spinit.Wpc.Ogonapoteket.Presentation.Site.Wpc.Ogonapoteket.ProductList" %>
	<div></div>																					
<asp:PlaceHolder ID="phCategories" runat="server" />

<div id="meny2" style="position: absolute; z-index: 5; width: 200px; left: 13px; top: 13px;">
</div>
<asp:PlaceHolder ID="phCategoryName" Visible="false" runat="server">
<h1 id="hCatecoryName"><asp:Literal ID="ltCategoryName" runat="server" /></h1>
</asp:PlaceHolder> 

<!-- CREATE PRODUCTS -->
<asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound" OnItemCommand="rptProducts_ItemCommand">
<HeaderTemplate><ul id="Products" class="clear-fix"></HeaderTemplate>
<ItemTemplate>
	<li>
		<div class="product-details">
				<div class="product-head clear-fix">
						<asp:HyperLink ID="hlProductName" runat="server" Text="Produktnamn" />
				</div>				
															
			<asp:PlaceHolder ID="phMainProduct" runat="server" Visible="false">
				<p class="go-to-article"><asp:LinkButton ID="lbGoToArticle" cssclass="readmore" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' CommandName="GoToArticles" runat="server" Visible="false">Visa varor</asp:LinkButton></p>
			</asp:PlaceHolder>
		</div>
		<div class="product-image">
			<asp:Image ID="picProduct" Visible="false" runat="server" AlternateText=" " Width="50px" Height="50px" />
		</div>
	</li>
</ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>
