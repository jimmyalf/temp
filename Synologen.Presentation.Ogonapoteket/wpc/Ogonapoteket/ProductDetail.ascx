﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductDetail.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Ogonapoteket.wpc.Ogonapoteket.ProductDetail" %>

<!-- CREATE PRODUCTS -->
<asp:Repeater ID="rptProducts" runat="server" OnItemDataBound="rptProducts_ItemDataBound" OnItemCommand="rptProducts_ItemCommand">
<HeaderTemplate><ul id="Product-details-list" class="clear-fix"></HeaderTemplate>
<ItemTemplate>
<li>
<asp:Image ID="picProduct" Visible="false" runat="server" AlternateText=" " />
<h2 class="title">
<asp:Literal ID="ltTitle" runat="server" Text="Title" />						
</h2>
<p class="price"><asp:Literal ID="ltPrice" runat="server" Text="" Visible="false" /></p>
<p class="description"><asp:Literal ID="ltDescription" runat="server" Text="Description" /></p>
</li>
</ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>
