<%@ Page Language="C#" MasterPageFile="~/components/QuickMail/QuickMailMain.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Spinit.Wpc.QuickMail.Presentation.Settings" Title="Untitled Page" %>
<%@ Reference Control="Setting.ascx" %>
<asp:Content ID="cntQuickMailMain" ContentPlaceHolderID="ComponentQuickMail" runat="server">
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-News-EditNews-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Commerce</h1>
	
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
		
		<asp:PlaceHolder ID="phSettings" runat="server" />	
										
		<div class="formCommands">					    
		    <asp:button ID="btnSave" runat="server" CommandName="ChangeStatus" OnClick="btnSave_Click" Text="Change Status" SkinId="Big"/>
		</div>
	</fieldset>
</div>
</div>
</div>
</asp:Content>
