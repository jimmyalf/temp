<%@ Register Src="~/Settings/Groups/GroupObject.ascx" TagName="GroupObject" TagPrefix="cnt" %>
<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Groups.group_edit" Codebehind="group_edit.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="group_access.aspx" title="List all groups"><span>List groups</span></a></li>
		<li><a href="group_edit.aspx?add=new" title="Add new group"><span>Add group</span></a></li>
	</ul>
</div>
<div id="dCompMain" class="Settings-GroupEdit-aspx">
	<asp:ValidationSummary ID="validationSummary" runat="server" EnableViewState="False" HeaderText="An error occured" SkinId="Error" />

	<div class="fullBox">
	<div class="wrap">
	<h1>Add/Edit group</h1>

	<asp:CustomValidator ID="validateError" runat="server" ErrorMessage="An error occured" Display="None" />
	<p><strong>* = Required field</strong></p>
		
	<fieldset>
	<legend>Group details</legend>
		<div class="formItem">
			<asp:Label runat="server" AssociatedControlID="txtName" SkinID="Long">
			    Name * 
			    <ASP:REQUIREDFIELDVALIDATOR id="RequiredFieldValidator1" runat="server" controltovalidate="txtName" errormessage="Name is missing" SetFocusOnError="True" Display="None" />
			</asp:Label>
			<ASP:TEXTBOX id="txtName" runat="server"></ASP:TEXTBOX>
		</div>

		<div class="formItem">
			<asp:Label runat="server" AssociatedControlID="txtDescription" SkinID="Long">Description</asp:Label>
			<ASP:TEXTBOX id="txtDescription" runat="server"></ASP:TEXTBOX>
		</div>

		<div class="formItem clearLeft">
			<asp:Label runat="server" AssociatedControlID="drpGroupType" SkinID="Long">Group type</asp:Label>
			<asp:DropDownList ID="drpGroupType" OnSelectedIndexChanged="drpGroupType_SelectedIndexChanged" AutoPostBack="true" DataValueField="cId" DataTextField="cName" runat="server">
			</asp:DropDownList>
		</div>
	</fieldset>

    <div id="dLocation" runat="server">
	    <fieldset>
	    <legend>Locations & Languages</legend>
		    <div class="formItem">
			    <asp:Label runat="server" AssociatedControlID="chkLocationLanguages" SkinID="Long">Locations & Languages</asp:Label>
			    <asp:CheckBoxList ID="chkLocationLanguages" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
			    </asp:CheckBoxList>
		    </div>
	    </fieldset>
	</div>

    <div id="dObjects" runat="server">
	    <asp:PlaceHolder ID="phGroupComponentsMenu" runat="server"  />
	    <div class="tabsContentContainer clearAfter">
	        <div class="wrap">
		        <asp:MultiView ID="vwComponents" runat="server">
		        </asp:MultiView>
	        </div>
        </div>
    </div>

	<div class="formCommands">
        <ASP:BUTTON id="btnSave" runat="server" SkinID="Big" text="Save" onclick="btnSave_Click" />
        <ASP:BUTTON id="btnCancel" runat="server" SkinID="Big" text="Cancel" causesvalidation="False" onclick="btnCancel_Click" />
	</div>
	</div>
	</div>
</div>
</asp:Content>