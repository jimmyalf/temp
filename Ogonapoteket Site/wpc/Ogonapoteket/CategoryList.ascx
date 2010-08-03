<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.ascx.cs" Inherits="Spinit.Wpc.Ogonapoteket.Presentation.Site.Wpc.Ogonapoteket.CategoryList" %>																				
<div id="category-list"><asp:PlaceHolder ID="phCategories" runat="server" /></div>
<asp:PlaceHolder ID="phCategoryName" Visible="false" runat="server">
<h1 id="hCatecoryName"><asp:Literal ID="ltCategoryName" runat="server" /></h1>
<div class="category-image">
<asp:Image ID="picCategory" Visible="false" runat="server" AlternateText=" " />
</div>
<p class="description"><asp:Literal ID="ltCategoryDescription" runat="server" Text="Description" /></p>
</asp:PlaceHolder> 

<h2 class="title"><asp:Literal ID="ltSearchProducts" runat="server" Visible="false"></asp:Literal></h2>
</asp:Repeater>
