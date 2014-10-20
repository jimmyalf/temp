<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Languages.edit_language" Codebehind="edit_language.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="main_languages.aspx" title="List all languages"><span>List languages</span></a></li>
		<li id="liAddLanguage" runat="server"><a href="edit_language.aspx" title="Add new language"><span>Add location</span></a></li>
	</ul>
</div>
<div id="dCompMain" class="Settings-EditLanguage-aspx">
	<asp:ValidationSummary ID="validationSummary" runat="server" EnableViewState="False" HeaderText="An error occured" SkinId="Error" />
	
	<div class="fullBox">
	<div class="wrap">
	<h1>Add/Edit language</h1>
	
	<asp:CustomValidator ID="validateError" runat="server" ErrorMessage="An error occured" Display="None" />
	<p><strong>* = Required field</strong></p>

	<fieldset>
	<legend>Language details </legend>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtName" SkinID="Long">Name *<ASP:REQUIREDFIELDVALIDATOR id="RequiredFieldValidator1" runat="server" errormessage="Name is missing" controltovalidate="txtName" SetFocusOnError="True" Display="None" /></asp:Label>
		<ASP:TextBox id="txtName" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="txtDescription" SkinID="Long">Description</asp:Label>
		<ASP:TextBox id="txtDescription" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtResource" SkinID="Long">Resource</asp:Label>
		<ASP:TextBox id="txtResource" runat="server"></ASP:TextBox>
	</div>
	
	</fieldset>

	<div class="formCommands">
		<ASP:BUTTON id="btnSave" runat="server" text="Save" onclick="btnSave_Click" SkinID="Big"></ASP:BUTTON>
		<ASP:BUTTON id="btnCancel" runat="server" causesvalidation="False" text="Cancel" onclick="btnCancel_Click" SkinID="Big"></ASP:BUTTON>
	</div>
	</div>
	</div>
</div>
</asp:Content>