<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PropertiesManager.ascx.cs"
	Inherits="Spinit.Wpc.Document.Presentation.components.Document.PropertiesManager" %>
<div class="Components-Document-PropertiesManager-ascx fullBox">
	<h2>
		Properties</h2>
	<fieldset>
		<legend>Modify document properties</legend>
		<div class="formItem">
			<asp:Label ID="Label1" runat="server" AssociatedControlID="txtName" SkinID="Long">Name</asp:Label>
			<asp:TextBox ID="txtName" runat="server"></asp:TextBox>
		</div>
		<div class="formItem clearLeft">
			<asp:Label ID="Label2" runat="server" AssociatedControlID="txtDescription" SkinID="Long">Description</asp:Label>
			<asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textarea"></asp:TextBox>
		</div>
		<div class="formItem clearLeft">
			<asp:Label ID="Label3" runat="server" AssociatedControlID="txtModified" SkinID="Long">Modified</asp:Label>
			<asp:TextBox ID="txtModified" runat="server" ReadOnly="True"></asp:TextBox>
		</div>
		<div class="formItem clearLeft">
			<asp:Label ID="Label4" runat="server" AssociatedControlID="txtSize" SkinID="Long">Size</asp:Label>
			<asp:TextBox ID="txtSize" runat="server" ReadOnly="True"></asp:TextBox>
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
		<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
	</div>
</div>
