<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryItem.ascx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.Site.Wpc.Commerce.Templates.CategoryItem" %>
<asp:PlaceHolder ID="unselected" runat="server" Visible="false">
<li>
</asp:PlaceHolder>
<asp:PlaceHolder ID="selected" runat="server" Visible="false">
<li class="selected">
</asp:PlaceHolder>
	<div class="category-header"><asp:HyperLink ID="hlCategoryName" runat="server" Text="Kategorinamn" /></div>
	<asp:PlaceHolder ID="phSubCategories" runat="server" />
</li>


<asp:HiddenField ID="hfSelectedValue" runat="server" Visible="false"/>
