<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Locations.edit_location" Codebehind="edit_location.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="main_locations.aspx" title="List all locations"><span>List locations</span></a></li>
		<li id="liAddLocation" runat="server"><a href="edit_location.aspx" title="Add new location"><span>Add location</span></a></li>
	</ul>
</div>
<div id="dCompMain" class="Settings-EditLocation-aspx">
	<asp:ValidationSummary ID="validationSummary" runat="server" EnableViewState="False" HeaderText="An error occured" SkinId="Error" />
	
	<div class="fullBox">
	<div class="wrap">
	<h1>Add/Edit location</h1>

	<asp:CustomValidator ID="validateError" runat="server" ErrorMessage="An error occured" Display="None" />
	<p><strong>* = Required field</strong></p>

	<fieldset>
	<legend>Location details (Web site)</legend>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox1" SkinID="Long">Name *<ASP:REQUIREDFIELDVALIDATOR id="RequiredFieldValidator1" runat="server" errormessage="Name is missing" controltovalidate="TextBox1" SetFocusOnError="True" Display="None" /></asp:Label>
		<ASP:TextBox id="TextBox1" runat="server"></ASP:TextBox>
	</div>
	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="drpFrontType" SkinID="Long">Choose webfront</asp:Label>
		<asp:DropdownList id="drpFrontType" runat="server">
			<asp:ListItem Text="Mvc" Value="1" />
			<asp:ListItem Text="WebForms" Value="2" Selected="True" />
		</asp:DropdownList>
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="TextBox2" SkinID="Long">Alias 1</asp:Label>
		<ASP:TextBox id="TextBox2" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox3" SkinID="Long">Alias 2</asp:Label>
		<ASP:TextBox id="TextBox3" runat="server"></ASP:TextBox>
	</div>
	
	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox4" SkinID="Long">Alias 3</asp:Label>
		<ASP:TextBox id="TextBox4" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem clearLeft">
		<ASP:CheckBox id="CheckBox2" runat="server" Text="Allow cross publishing to this location"></ASP:CheckBox>
	</div>

	<div class="formItem">
		<ASP:CheckBox id="extranetCheckBox" runat="server" Text="Extranet"></ASP:CheckBox>
	</div>

	</fieldset>

	<fieldset>
	<legend>Location languages</legend>
	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="chkLanguages" SkinID="Long">Languages</asp:Label>
		<ASP:CheckBoxLIST id="chkLanguages" runat="server" datatextfield="cName" datavaluefield="cId" autopostback="True" onselectedindexchanged="chkLanguages_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow"></ASP:CheckBoxLIST>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="drpLanguages" SkinID="Long">Default language</asp:Label>
		<ASP:DROPDOWNLIST id="drpLanguages" runat="server"></ASP:DROPDOWNLIST>
	</div>
	</fieldset>

	<fieldset>
	<legend>Publish details</legend>
	<div class="formItem clearLeft">
		<asp:CheckBox id="CheckBox3" runat="server" checked="true" Text="Publish is active"></asp:CheckBox>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox5" SkinID="Long">Path to publish directory</asp:Label>
		<asp:TextBox id="TextBox5" runat="server"></asp:TextBox>
	</div>
	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="txtRootPath" SkinID="Long">Relative path to root application</asp:Label>
		<ASP:TextBox id="txtRootPath" runat="server"></ASP:TextBox>
	</div>
	
	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtSitePath" SkinID="Long">Path to site root</asp:Label>
		<ASP:TextBox id="txtSitePath" runat="server"></ASP:TextBox>
	</div>
	<div runat="server" visible="false">
		<div class="formItem clearLeft">
			<ASP:CheckBox id="CheckBox4" runat="server" Text="Ftp publish"></ASP:CheckBox>
		</div>

		<div class="formItem">
			<ASP:CheckBox id="CheckBox1" runat="server" Text="Passive Ftp"></ASP:CheckBox>
		</div>

		<div class="formItem clearLeft">
			<asp:Label runat="server" AssociatedControlID="TextBox6" SkinID="Long">Username</asp:Label>
			<ASP:TextBox id="TextBox6" runat="server"></ASP:TextBox>
		</div>

		<div class="formItem">
			<asp:Label runat="server" AssociatedControlID="TextBox7" SkinID="Long">Password</asp:Label>
			<ASP:TextBox id="TextBox7" runat="server"></ASP:TextBox>
		</div>
	</div>
	</fieldset>

	<fieldset>
	<legend>Document type (<abbr title="Document Type Definition">DTD</abbr>)</legend>
	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="drpDocType" SkinID="Long">Document type</asp:Label>
		<asp:dropdownlist id="drpDocType" runat="server">
			<asp:ListItem Value="0" Selected="True">None</asp:ListItem>
			<asp:ListItem Value="1">HTML 4.01</asp:ListItem>
			<asp:ListItem Value="2">XHTML 1.0</asp:ListItem>
		</asp:dropdownlist>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="drpDocSubType" SkinID="Long">Rendering mode</asp:Label>
		<asp:dropdownlist id="drpDocSubType" runat="server">
			<asp:ListItem Value="0" Selected="True">None</asp:ListItem>
			<asp:ListItem Value="1">Strict</asp:ListItem>
			<asp:ListItem Value="2">Loose</asp:ListItem>
			<asp:ListItem Value="3">Frames</asp:ListItem>
		</asp:dropdownlist>
	</div>
	</fieldset>

	<fieldset>
	<legend>Information about this Location</legend>
	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox8" SkinID="Long">Name</asp:Label>
		<ASP:TextBox id="TextBox8" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="TextBox9" SkinID="Long">Address</asp:Label>
		<ASP:TextBox id="TextBox9" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox10" SkinID="Long">Visit address</asp:Label>
		<ASP:TextBox id="TextBox10" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="TextBox11" SkinID="Long">Postal no.</asp:Label>
		<ASP:TextBox id="TextBox11" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox12" SkinID="Long">City</asp:Label>
		<ASP:TextBox id="TextBox12" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="TextBox13" SkinID="Long">Phone</asp:Label>
		<ASP:TextBox id="TextBox13" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox14" SkinID="Long">Fax</asp:Label>
		<ASP:TextBox id="TextBox14" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox15" SkinID="Long">E-mail</asp:Label>
		<ASP:TextBox id="TextBox15" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="TextBox16" SkinID="Long">Copyright info</asp:Label>
		<ASP:TextBox id="TextBox16" runat="server"></ASP:TextBox>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="TextBox17" SkinID="Long">Webmaster</asp:Label>
		<ASP:TextBox id="TextBox17" runat="server"></ASP:TextBox>
	</div>
	</fieldset>

	<fieldset>
	<legend>Choose components to bind to this Location</legend>

	<div>
		<ASP:CheckBoxLIST datatextfield="cName" datavaluefield="cId" id="chkComponents" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" CssClass="checkBoxItems displayAsNonTable componentsList" EnableTheming="False"></ASP:CheckBoxLIST>
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