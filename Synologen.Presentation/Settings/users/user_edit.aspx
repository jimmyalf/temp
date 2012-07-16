<%@ Register Src="~/Common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="uc1" %>
<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Users.user_edit" Codebehind="user_edit.aspx.cs" MaintainScrollPositionOnPostback="True" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="users.aspx" title="List all users"><span>List users</span></a></li>
		<li><a href="user_edit.aspx?add=new" title="Add new user"><span>Add user</span></a></li>
	</ul>
</div>
<div id="dCompMain" class="Settings-UserEdit-aspx">
	<asp:ValidationSummary ID="validationSummary" runat="server" EnableViewState="False" HeaderText="An error occured" SkinId="Error" />

	<div class="fullBox">
	<div class="wrap">
	
	<h1>Add/Edit user</h1>

	<asp:CustomValidator ID="validateError" runat="server" ErrorMessage="An error occured" Display="None" />
	<p><strong>* = Required field</strong></p>

	<fieldset>
	<legend>User preferences</legend>

	<div class="formItem clearLeft">
		<ASP:CHECKBOX id="chkActive" runat="server" Text="Active account"></ASP:CHECKBOX>
	</div>

	<div class="formItem">
		<asp:Label ID="Label1" runat="server" AssociatedControlID="drpLocation" SkinID="Long">Default location * <ASP:REQUIREDFIELDVALIDATOR id="rfvLocation" runat="server" errormessage="Default location is missing" controltovalidate="drpLocation" Display="None"></ASP:REQUIREDFIELDVALIDATOR></asp:Label>
		<asp:DropDownList ID="drpLocation" DataValueField="cId" DataTextField="cName" runat="server"></asp:DropDownList>
	</div>

	<div class="box clearLeft">
		<asp:Label ID="Label2" runat="server" AssociatedControlID="listNonMemberOf" SkinID="Long">Not a member of</asp:Label>
		<ASP:LISTBOX id="listNonMemberOf" runat="server" datatextfield="cName" datavaluefield="cId" selectionmode="Multiple"></ASP:LISTBOX>
	</div>
	<div class="box center">
		<input id="btnRemoveRole" type="submit" value="<" title="Move to: not a member of" runat="server" onserverclick="btnRemoveRole_ServerClick" causesvalidation="false" class="btnSmall" />
		<input id="btnAddRole" type="submit" value=">" title="Move to: member of" runat="server" onserverclick="btnAddRole_ServerClick" causesvalidation="false" class="btnSmall" />

	</div>
	<div class="box">
		<asp:Label ID="Label3" runat="server" AssociatedControlID="listMemberOf" SkinID="Long">Member of</asp:Label>
		<ASP:LISTBOX id="listMemberOf" runat="server" datatextfield="cName" datavaluefield="cId" selectionmode="Multiple"></ASP:LISTBOX>
	</div>

	</fieldset>

	<fieldset>
	<legend>User details</legend>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtUserName" SkinID="Long">Username * <ASP:REQUIREDFIELDVALIDATOR id="rfvUserName" runat="server" ErrorMessage="Username is missing" controltovalidate="txtUserName" SetFocusOnError="True" Display="None" /></asp:Label>
		<ASP:TEXTBOX id="txtUserName" runat="server"></ASP:TEXTBOX>
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="txtPassword" SkinID="Long">Password * <ASP:REQUIREDFIELDVALIDATOR id="rfvPassword" runat="server" errormessage="Password is missing" controltovalidate="txtPassword" SetFocusOnError="True" Display="None" /></asp:Label>
		<ASP:TEXTBOX id="txtPassword"  TextMode="Password" runat="server"></ASP:TEXTBOX>
	</div>
	
	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtRepeat" SkinID="Long">Confirm password * <ASP:REQUIREDFIELDVALIDATOR id="rfvConfirmPassword" runat="server" errormessage="Confirmation of password is missing" controltovalidate="txtRepeat" SetFocusOnError="True" Display="None" /><ASP:COMPAREVALIDATOR id="vldCmpPassword" runat="server" errormessage="Confirmation of password is incorrect!" controltovalidate="txtRepeat" controltocompare="txtPassword" enableclientscript="true" type="String" operator="equal" display="None"></ASP:COMPAREVALIDATOR></asp:Label>
		<ASP:TEXTBOX id="txtRepeat" TextMode="Password" runat="server"></ASP:TEXTBOX><br />
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="txtFirstName" SkinID="Long">First name * <ASP:REQUIREDFIELDVALIDATOR id="rfvFirstName" runat="server" errormessage="First name is missing" controltovalidate="txtFirstName" SetFocusOnError="True" Display="None" /></asp:Label>
		<ASP:TEXTBOX id="txtFirstName" runat="server"></ASP:TEXTBOX>
	</div>

	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtLastName" SkinID="Long">Last name * <ASP:REQUIREDFIELDVALIDATOR id="rfvLastName" runat="server" errormessage="Last name is missing" controltovalidate="txtLastName" SetFocusOnError="True" Display="None" /></asp:Label>
		<ASP:TEXTBOX id="txtLastName" runat="server"></ASP:TEXTBOX>
	</div>

	<div class="formItem clearLeft">
		<asp:Label runat="server" AssociatedControlID="txtEmail" SkinID="Long">E-mail * <ASP:REQUIREDFIELDVALIDATOR id="rfvEmail" runat="server" errormessage="E-mail is missing" controltovalidate="txtEmail" SetFocusOnError="True" Display="None" /></asp:Label>
		<ASP:TEXTBOX id="txtEmail" runat="server"></ASP:TEXTBOX>
	</div>

	</fieldset>

	<div class="formCommands">
		<ASP:BUTTON id="btnSave" runat="server" text="Save" onclick="btnSave_Click" ToolTip="Save user details." SkinID="Big" />
		<ASP:BUTTON id="btnSaveAndSendUserDetails" runat="server" text="Save and send email" onclick="btnSaveAndSendUserDetails_Click" ToolTip="Save user details and, if new password, send details to user." SkinID="Big" />
		<ASP:BUTTON id="btnCancel" runat="server" text="Cancel" causesvalidation="False" onclick="btnCancel_Click" SkinID="Big" />
	</div>
	</div>
	</div>
</div>
</asp:Content>