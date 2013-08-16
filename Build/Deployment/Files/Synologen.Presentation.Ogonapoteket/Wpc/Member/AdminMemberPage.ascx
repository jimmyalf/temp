<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.AdminMemberPage" Codebehind="AdminMemberPage.ascx.cs" %>
<%@ Register Src="WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc1" %>
<link href="common/css/WYSIWYG.css" media="all" rel="Stylesheet" />
<div id="contentlev"> 
<asp:Label ID="lblHeader" runat="server" />
	    
        <uc1:WpcWysiwyg ID="txtBody" runat="server" Width="500" ShowComponent="false" ShowInternalLink="false" />
        
        <div class="formCommands">					    
		    <asp:button ID="btnSave" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Spara sida" SkinId="Big" CssClass="button"/>
		</div>
</div>