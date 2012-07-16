<%@ Control Language="C#" AutoEventWireup="true" Codebehind="Upload.ascx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.Upload" %>
<div class="Components-Document-Upload-ascx">
	<h2>
		Upload</h2>
	<fieldset>
		<legend>File to upload</legend>
		<div class="formItem">
			<asp:Label ID="Label1" runat="server" AssociatedControlID="uplFile" SkinID="Long">File</asp:Label>
			<asp:FileUpload ID="uplFile" runat="server" />
		</div>
		<div class="formItem">
			<asp:Label ID="Label3" runat="server" ToolTip="Leave blank to use filename" AssociatedControlID="txtName" SkinID="Long">New Name (Leave empty to use filename)</asp:Label>
            <asp:TextBox ID="txtName" runat="server" ToolTip="Leave blank to use filename"></asp:TextBox></div>
		<div class="formItem">
			<asp:Label ID="Label2" runat="server" AssociatedControlID="txtDescription" SkinID="Long">Description</asp:Label>
			<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textarea"></asp:TextBox>
		</div>
	</fieldset>
	<asp:PlaceHolder id="phAccess" runat="server">
		<fieldset>
			<legend>Set access to document</legend>
			<div class="formItem clearLeft">
				<asp:Label ID="lblGroup" runat="server" AssociatedControlID="drpGroups" SkinID="Long"></asp:Label>
				<asp:DropDownList ID="drpGroups" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpGroups_SelectedIndexChanged" />
			</div>
			<div class="formItem clearLeft">
				<asp:Label ID="lblUsers" runat="server" AssociatedControlID="drpUsers" SkinID="Long"></asp:Label>
				<asp:DropDownList ID="drpUsers" runat="server" />
			</div>
		</fieldset>
	</asp:PlaceHolder>
	<div class="formCommands">
		<asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
		<asp:Label ID="lblOverwrite" runat="server" Text="Overwrite existing file?" Visible="False"></asp:Label>
		<asp:Button ID="btnUploadConfirm" runat="server" Text="Confirm" OnClick="btnUploadConfirm_Click" Visible="False" />
		<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="True" />
	</div>
</div>
