<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.Upload" Codebehind="Upload.ascx.cs" %>
<div class="Files-Upload-ascx">
<h2>Upload</h2>

<fieldset>
<legend>Upload a file</legend>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="uplFile" SkinID="Long">Name</asp:Label>
	<asp:FileUpload ID="uplFile" runat="server"/>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtDescription" SkinID="Long">Description</asp:Label>
	<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textarea"></asp:TextBox>
</div>
<div class="formCommands">
	<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />

    <asp:Label ID="lblOverwrite" runat="server" Text="Overwrite existing file?" Visible="False"></asp:Label>
	<asp:Button ID="btnUploadConfirm" runat="server" Text="Confirm" OnClick="btnUploadConfirm_Click" Visible="False" />
	<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="False" />
</div>
</fieldset>
</div>
