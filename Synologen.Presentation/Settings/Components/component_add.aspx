<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Components.component_add" Codebehind="component_add.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="#" title="List all groups"><span>List</span></a></li>
		<li><a href="#" title="Add new group"><span>Add</span></a></li>
	</ul>
</div>
<div id="dCompMain">
	<asp:ValidationSummary ID="validationSummary" runat="server" EnableViewState="False" HeaderText="An error occured" SkinId="Error" />
	<asp:ValidationSummary ID="actionSummary" runat="server" EnableViewState="False" HeaderText="Success" SkinID="Success" ValidationGroup="Message" />
	
	<div class="fullBox">
	<div class="wrap">
	<h1>Components</h1>
	
	<asp:CustomValidator ID="validateError" runat="server" ErrorMessage="An error occured" Display="None" />
	<asp:CustomValidator ID="actionMessage" runat="server" ErrorMessage="" Display="None" ValidationGroup="Message" />
	<p><strong>* = Required field</strong></p>
	
	<fieldset>
	<legend>Component Installation</legend>

    <div id="divFileUpload">
        <div class="formItem clearLeft">
			<asp:Label runat="server" AssociatedControlID="fileUpload1" SkinID="Long">Select Installation file *<asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ErrorMessage="File is missing" ControlToValidate="fileUpload1" SetFocusOnError="True" Display="None" /></asp:Label>
            <asp:FileUpload ID="fileUpload1" runat="server"  />
        </div>
        <div class="formItem clearLeft">
            <asp:Button ID="btnInstallComp" runat="server" Text="Install component" OnClick="btn_Install" />
        </div>
    </div>
	</fieldset>
	</div>
	</div>
</div>
</asp:Content>