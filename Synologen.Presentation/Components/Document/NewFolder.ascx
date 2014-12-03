<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewFolder.ascx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.NewFolder" %>
<div class="Components-Document-NewFolder-ascx">
<h2><asp:Literal ID="ltHeader" runat="server" /></h2>
<fieldset>
		<legend>Folder</legend>
<div class="formItem">
	<asp:Label ID="Label1" runat="server" AssociatedControlID="txtDirectory" SkinID="Long">Folder name</asp:Label>
	<asp:TextBox ID="txtDirectory" runat="server" Width="200px"></asp:TextBox>	
</div>
<div class="formItem">
    <asp:Label ID="Label2" runat="server" AssociatedControlID="drpSortType" ToolTip="Select how to sort documents in this folder." SkinID="Long">Sort By</asp:Label>
    <asp:DropDownList ID="drpSortType" runat="server">
    </asp:DropDownList><asp:Label ID="SortTypeErrorLabel" runat="server" ForeColor="red" Text="Sorting not selected" Visible="false"></asp:Label>
</div>
</fieldset>
	<asp:PlaceHolder id="phAccess" runat="server">
		<fieldset>
			<legend>Set access to folder</legend>
			<div class="formItem clearLeft">
					<asp:Label ID="lblGroup" runat="server" AssociatedControlID="drpGroups" SkinId="Long"></asp:Label>
					<asp:DropDownList ID="drpGroups" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpGroups_SelectedIndexChanged" />
			</div>
			<div class="formItem clearLeft">
					<asp:Label ID="lblUsers" runat="server" AssociatedControlID="drpUsers" SkinId="Long"></asp:Label>
					<asp:DropDownList ID="drpUsers" runat="server" />
			</div>
		</fieldset>
	</asp:PlaceHolder>
<div class="formCommands">
	<asp:Button ID="btnCreateDirectory" runat="server" Text="Create" ToolTip="Create" OnClick="btnCreateDirectory_Click" />
	<asp:Button ID="btnCancelDirectory" runat="server" Text="Cancel" ToolTip="Cancel" OnClick="btnCancelDirectory_Click" />
</div>
</div>