<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListProductCategoryFiles.ascx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.components.Commerce.ListProductCategoryFiles" %>
<asp:Repeater ID="rptProductCategoryFiles" OnItemDataBound="rptProductCategoryFiles_ItemDataBound" runat="server" >
	<HeaderTemplate><table id="product-files-list"></HeaderTemplate>
	<ItemTemplate>
	<tr>
		<td><span><asp:Literal ID="ltFileNameAndPath" runat="server"/></span></td>
		<td><asp:Button ID="BtnRemove" Text="Remove" runat="server" OnCommand="BtnRemove_Click" /></td>
		<td><asp:Button ID="BtnEdit" Text="Edit" runat="server" OnCommand="BtnEdit_Click" /></td>
		<td><asp:Button ID="BtnMoveUp" Text="Up" runat="server" OnCommand="BtnMoveUp_Click" /></td>
		<td><asp:Button ID="BtnMoveDown" Text="Down" runat="server" OnCommand="BtnMoveDown_Click" /></td>
	</tr>
	</ItemTemplate>
	<FooterTemplate></table></FooterTemplate> 
</asp:Repeater>
<br />
<asp:PlaceHolder ID="plEditProductCategoryFileConnection" runat="server" Visible="false">
<div class="product-file-connection">
	<fieldset><legend><asp:Literal ID="ltEditHeading" runat="server"/></legend>
	<div class="formItem">
		<label for="<%=drpProductFileCategory.ClientID%>" class="labelLong">File category</label>
		<asp:DropDownList ID="drpProductFileCategory" runat="server" />
	</div>
	<div class="formItem">
		<label for="<%=txtProductCategoryFileConnectionName.ClientID%>" class="labelLong">Name</label>
		<asp:TextBox ID="txtProductCategoryFileConnectionName" runat="server"/>
	</div>
	<div class="formItem">	
		<label for="<%=txtProductCategoryFileConnectionDescription.ClientID%>" class="labelLong">Description</label>
		<asp:TextBox ID="txtProductCategoryFileConnectionDescription" runat="server"/>
	</div>
	</br>
	<asp:Button ID="btnSaveProductCategoryFileConnection" runat="server" Text="Save Attachment Settings" OnCommand="BtnSave_Click" />
	<asp:Button ID="btnCancelProductCategoryFileConnection" runat="server" Text="Cancel" OnCommand="BtnCancel_Click" />
	</fieldset>
</div>
</asp:PlaceHolder>