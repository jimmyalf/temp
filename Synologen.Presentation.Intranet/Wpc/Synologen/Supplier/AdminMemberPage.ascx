<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier.AdminMemberPage" Codebehind="AdminMemberPage.ascx.cs" %>
<div>
    <fieldset>
	    <legend><asp:Label ID="lblHeader" runat="server" /></legend>
		<WPC:WpcWysiwyg ID="txtBody" runat="server" Width="500" Mode="Advanced" />
		<div class="formCommands">					    
		    <asp:button ID="btnSave" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Spara sida" SkinId="Big"/>
		</div>
	</fieldset>
</div>