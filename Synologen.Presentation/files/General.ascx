<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.General" Codebehind="General.ascx.cs" %>
<div class="Files-General-ascx">
<fieldset>
<legend>General properties</legend>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtName" SkinID="Long">Name *<asp:RequiredFieldValidator ControlToValidate="txtName" ErrorMessage="Name can not be empty" ID="vldReqName" runat="server" SetFocusOnError="True" Display="None" /><asp:RegularExpressionValidator ID="vldxName" runat="server" ControlToValidate="txtName" ErrorMessage="Filename is not valid! Only _ - a-z A-Z 0-9 [space] and . are allowed." ValidationExpression="[-a-zA-Z0-9\._ ]*" SetFocusOnError="True" Display="None" /></asp:Label>
	<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtDescription" SkinID="Long">Description</asp:Label>
	<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textarea"></asp:TextBox>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtModified" SkinID="Long">Modified</asp:Label>
	<asp:TextBox ID="txtModified" runat="server" ReadOnly="True"></asp:TextBox>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtSize" SkinID="Long">Size</asp:Label>
	<asp:TextBox ID="txtSize" runat="server" ReadOnly="True"></asp:TextBox>
</div>
<div class="formCommands">
	<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
</div>
</fieldset>
</div>