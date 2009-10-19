<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.ascx.cs" Inherits="Spinit.Wpc.Ogonapoteket.Presentation.Site.Wpc.Ogonapoteket.CategoryList" %>
	<div></div>																					
<asp:PlaceHolder ID="phCategories" runat="server" />

<div id="meny2" style="position: absolute; z-index: 5; width: 200px; left: 13px; top: 13px;">
</div>
<asp:PlaceHolder ID="phCategoryName" Visible="false" runat="server">
<h1 id="hCatecoryName"><asp:Literal ID="ltCategoryName" runat="server" /></h1>
<div class="category-image">
	<asp:Image ID="picCategory" Visible="false" runat="server" AlternateText=" " />
</div>
<div class="category-details">
	<p class="description"><asp:Literal ID="ltCategoryDescription" runat="server" Text="Description" /></p>
</div>
</asp:PlaceHolder> 

<h2 class="title"><asp:Literal ID="ltSearchProducts" runat="server"></asp:Literal></h2>

<!-- CREATE PRODUCTS -->
<asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound" OnItemCommand="rptProducts_ItemCommand">
<HeaderTemplate><ul id="Products" class="clear-fix"></HeaderTemplate>
<ItemTemplate>
	<li>
		<div class="product-image">
			<asp:Image ID="picProduct" Visible="false" runat="server" AlternateText=" " />
		</div>
		<div class="product-details">
				<div class="product-head clear-fix">
					<h2 class="title">
						<asp:Literal ID="ltProductNumber" runat="server" Text="Product Number" />
						<asp:Literal ID="ltTitle" runat="server" Text="Title" />						
					</h2>
					<p class="price"><asp:Literal ID="ltPrice" runat="server" Text="" Visible="false" /></p>
					<p class="description"><asp:Literal ID="ltDescription" runat="server" Text="Description" /></p>
					<table>
						<tr>	
							<asp:Repeater ID="rptProductAttributeName" runat="server" OnItemDataBound="rptProductAttributeName_ItemDataBound">
								<ItemTemplate>					
											<th scope="col"><asp:Literal ID="ltAttributeName" runat="server" Text=""/></th>
								</ItemTemplate>
							</asp:Repeater>
						</tr>
						<tr>
							<asp:Repeater ID="rptProductAttributeValue" runat="server" OnItemDataBound="rptProductAttributeValue_ItemDataBound">
								<ItemTemplate>					
											<td><asp:Literal ID="ltAttributeValue" runat="server" Text=""/></td>
								</ItemTemplate>
							</asp:Repeater>
						</tr>
					</table>
				</div>				
				
				<!-- CREATE ARTICLE TABLE HEADING -->
				<table>
					<tr>
						<th scope="col"><asp:Literal ID="ltArtNr" runat="server" Text="Artikelnummer" Visible="false"/></th>
						<th><asp:Literal ID="ltArtName" runat="server" Text="Namn"/></th>
						<asp:Repeater ID="rptArticleAttributeHeading" runat="server" OnItemDataBound="rptProductAttributeHeading_ItemDataBound">
							<ItemTemplate>					
										<th scope="col"><asp:Literal ID="ltAtricleAttributeName" runat="server" Text=""/></th>
							</ItemTemplate>
						</asp:Repeater>
						<th scope="col"><asp:Literal ID="ltArtDesc" runat="server" Text="Beskrivning" Visible="false"/></th>
					</tr>
													
					<!-- CREATE ARTICLE TABLE ROWS -->			
					<asp:Repeater ID="rptProductArticles" runat="server" OnItemDataBound="rptProductArticles_ItemDataBound" OnItemCommand="rptProducts_ItemCommand">
					<HeaderTemplate></HeaderTemplate>
					<ItemTemplate>
						<tr>
							<th scope="row"><asp:Literal ID="ltProductNumber" runat="server" Text="Product Number" Visible="false"/></th>
							<td><asp:Literal ID="ltTitle" runat="server" Text=""/></td>
							<asp:Repeater ID="rptProductAttributeValue" runat="server" OnItemDataBound="rptProductAttributeValue_ItemDataBound">
								<ItemTemplate>					
											<td><asp:Literal ID="ltAttributeValue" runat="server" Text=""/></td>
								</ItemTemplate>
							</asp:Repeater>
							<td><asp:Literal ID="ltDescription" runat="server" Text="" Visible="false"/></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate></FooterTemplate>
					</asp:Repeater>
				</table>
															
			<asp:PlaceHolder ID="phMainProduct" runat="server" Visible="false">
				<p class="go-to-article"><asp:LinkButton ID="lbGoToArticle" cssclass="readmore" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>' CommandName="GoToArticles" runat="server" Visible="false">Visa varor</asp:LinkButton></p>
			</asp:PlaceHolder>
		</div>
	</li>
</ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>
