<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetail.ascx.cs" Inherits="Spinit.Wpc.Ogonapoteket.Presentation.Site.Wpc.Ogonapoteket.ProductDetail" %>
	<div></div>																					

<div id="meny2" style="position: absolute; z-index: 5; width: 200px; left: 13px; top: 13px;">
</div>

<!-- CREATE PRODUCTS -->
<asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound" OnItemCommand="rptProducts_ItemCommand">
<HeaderTemplate><ul id="Products" class="clear-fix"></HeaderTemplate>
<ItemTemplate>
	<div class="product-image">
		<asp:Image ID="picProduct" Visible="false" runat="server" AlternateText=" " />
	</div>
	<div class="product-details">
			<div class="product-head clear-fix">
				<h2 class="title">
					<asp:Literal ID="ltTitle" runat="server" Text="Title" />						
				</h2>
				<p class="price"><asp:Literal ID="ltPrice" runat="server" Text="" Visible="false" /></p>
				<p class="description"><asp:Literal ID="ltDescription" runat="server" Text="Description" /></p>
			</div>									
	</div>
</ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>
