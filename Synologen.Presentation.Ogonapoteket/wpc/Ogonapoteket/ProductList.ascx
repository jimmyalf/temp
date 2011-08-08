<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductList.ascx.cs" Inherits="Spinit.Wpc.Ogonapoteket.Presentation.Site.Wpc.Ogonapoteket.ProductList" %>																			
<asp:PlaceHolder ID="phCategories" runat="server" />
<asp:PlaceHolder ID="phCategoryName" Visible="false" runat="server">
<h1 id="hCatecoryName"><asp:Literal ID="ltCategoryName" runat="server" /></h1>
</asp:PlaceHolder> 

<!-- CREATE PRODUCTS -->
<div id="product-list">
<ul class="clear-fix">
<asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound" OnItemCommand="rptProducts_ItemCommand">
<HeaderTemplate></HeaderTemplate>
<ItemTemplate>
<li>
<p class="product-title"><asp:HyperLink ID="hlProductName" runat="server" Text="Produktnamn" /></p>
<asp:PlaceHolder ID="phMainProduct" runat="server" Visible="false">
<p class="go-to-article"><asp:LinkButton ID="lbGoToArticle" cssclass="readmore" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' CommandName="GoToArticles" runat="server" Visible="false">Visa varor</asp:LinkButton></p>
</asp:PlaceHolder>
<asp:Image ID="picProduct" Visible="false" runat="server" AlternateText="  />
</li>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</asp:Repeater>
</ul>
</div>
