<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListProductFiles.ascx.cs" Inherits="Spinit.Wpc.Commerce.Presentation.components.Commerce.ListProductFiles" %>
<asp:Repeater ID="rptProductFiles" OnItemDataBound="rptProductFiles_ItemDataBound" runat="server" >
	<HeaderTemplate><table id="product-files-list"></HeaderTemplate>
	<ItemTemplate>
	<tr>
		<td><span><asp:Literal ID="ltFileNameAndPath" runat="server"/></span></td>
		<td><asp:Button ID="btnRemove" Text="Remove" runat="server" OnCommand="btnRemove_Click" /></td>
		<td><asp:Button ID="btnEdit" Text="Edit" runat="server" OnCommand="btnEdit_Click" /></td>
		<td><asp:Button ID="btnMoveUp" Text="Up" runat="server" OnCommand="btnMoveUp_Click" /></td>
		<td><asp:Button ID="btnMoveDown" Text="Down" runat="server" OnCommand="btnMoveDown_Click" /></td>
	</tr>
	</ItemTemplate>
	<FooterTemplate></table></FooterTemplate> 
</asp:Repeater>
<br />
<asp:PlaceHolder ID="plEditProductFileConnection" runat="server" Visible="false">
<div class="product-file-connection">
	<fieldset><legend><asp:Literal ID="ltEditHeading" runat="server"/></legend>
	<div class="formItem">
		<label for="<%=drpProductFileCategory.ClientID%>" class="labelLong">File category</label>
		<asp:DropDownList ID="drpProductFileCategory" runat="server" />
	</div>
	<div class="formItem">
		<label for="<%=txtProductFileConnectionName.ClientID%>" class="labelLong">Name</label>
		<asp:TextBox ID="txtProductFileConnectionName" runat="server"/>
	</div>
	<div class="formItem">	
		<label for="<%=txtProductFileConnectionDescription.ClientID%>" class="labelLong">Description</label>
		<asp:TextBox ID="txtProductFileConnectionDescription" runat="server"/>
	</div>
	</br>
	<asp:Button ID="btnSaveProductFileConnection" runat="server" Text="Save Attachment Settings" OnCommand="btnSave_Click" />
	<asp:Button ID="btnCancelProductFileConnection" runat="server" Text="Cancel" OnCommand="btnCancel_Click" />
	</fieldset>
</div>
</asp:PlaceHolder>


