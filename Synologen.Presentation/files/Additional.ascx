<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.Additional" Codebehind="Additional.ascx.cs" %>
<div class="Files-Additional-ascx">
<fieldset>
<legend>Additional properties</legend>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtWidth" SkinID="Long">Width <em>(pixels)</em> *<asp:RequiredFieldValidator ID="vldReqWidth" runat="server" ControlToValidate="txtWidth" ErrorMessage="Width can not be empty" SetFocusOnError="True" Display="None" /></asp:Label>
	<asp:TextBox ID="txtWidth" runat="server"></asp:TextBox>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtHeight" SkinID="Long">Height <em>(pixels)</em> *<asp:RequiredFieldValidator ID="vldReqHeight" runat="server" ControlToValidate="txtHeight" ErrorMessage="Height can not be empty" SetFocusOnError="True" Display="None" /></asp:Label>
	<asp:TextBox ID="txtHeight" runat="server"></asp:TextBox>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtResolution" SkinID="Long">Resolution <em>(pixels/inch)</em> *<asp:RequiredFieldValidator ID="vldReqResolution" runat="server" ControlToValidate="txtResolution" ErrorMessage="Resolution can not be empty" SetFocusOnError="True" Display="None" /></asp:Label>
	<asp:TextBox ID="txtResolution" runat="server"></asp:TextBox>
</div>
<div class="formCommands">
	<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
</div>
</fieldset>
</div>