<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier.AdminMemberPage" Codebehind="AdminMemberPage.ascx.cs" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc1" %>
<div>
    <fieldset>
	    <legend><asp:Label ID="lblHeader" runat="server" /></legend>
		<uc1:WpcWysiwyg ID="txtBody" runat="server" Width="500" ShowComponent="false" ShowInternalLink="false" Mode="BasicText" />
		<div class="formCommands">					    
		    <asp:button ID="btnSave" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Spara sida" SkinId="Big"/>
		</div>
	</fieldset>
</div>