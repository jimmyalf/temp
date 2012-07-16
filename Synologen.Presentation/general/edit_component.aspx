<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.General.edit_component" Codebehind="edit_component.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div class="content-area">
	<H1>EXTERNAL COMPONENT - EDIT</H1>
	<HR>
	<TABLE id="Table1" cellspacing="1" cellpadding="1" width="600" border="0" class="formarea">
		<TR class="tableheader">
			<TD colspan="3">
				<P class="description">Properties for this component:</P>
			</TD>
		</TR>
		<TR>
			<TD class="formarea">Component ID:</TD>
			<TD class="formarea">
				<ASP:TEXTBOX id="txtComponentId" runat="server" enabled="False"></ASP:TEXTBOX>
			</TD>
			<TD class="formarea">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="formarea">
				<P align="left">Name:</P>
			</TD>
			<TD class="formarea">
				<ASP:TEXTBOX id="txtName" runat="server"></ASP:TEXTBOX>
			</TD>
			<TD class="formarea">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="formarea" valign="top">Description:</TD>
			<TD class="formarea">
				<ASP:TEXTBOX id="txtDescription" runat="server" width="303px"></ASP:TEXTBOX></TD>
			<TD class="formarea"></TD>
		</TR>
		<TR>
			<TD class="formarea" valign="top">
				<P align="left">Admin page:</P>
			</TD>
			<TD class="formarea">
				<P>
					<ASP:TEXTBOX id="txtAdminLink" runat="server" width="250px"></ASP:TEXTBOX>&nbsp;Target:
					<ASP:TEXTBOX id="txtAdminTarget" runat="server"></ASP:TEXTBOX>
				</P>
			</TD>
			<TD class="formarea">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="formarea">Public page:</TD>
			<TD class="formarea">
				<ASP:TEXTBOX id="txtPublicLink" runat="server" width="249px"></ASP:TEXTBOX>&nbsp;Target:
				<ASP:TEXTBOX id="txtPublicTarget" runat="server"></ASP:TEXTBOX>
			</TD>
			<TD class="formarea">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="formarea">
				<P align="left">Username:</P>
			</TD>
			<TD class="formarea">
				<ASP:TEXTBOX id="txtWinLogin" runat="server"></ASP:TEXTBOX>&nbsp;
				<ASP:CHECKBOX id="chkWinCookie" runat="server" text="Send cookie?"></ASP:CHECKBOX></TD>
			<TD class="formarea">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="formarea">Password:</TD>
			<TD class="formarea">
				<ASP:TEXTBOX id="txtWinPassword" runat="server"></ASP:TEXTBOX>
			</TD>
			<TD class="formarea">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="formarea">
				<P align="left">Common Username:</P>
			</TD>
			<TD class="formarea">
				<ASP:TEXTBOX id="txtCommonLogin" runat="server"></ASP:TEXTBOX>&nbsp;
				<ASP:CHECKBOX id="chkCommonCookie" runat="server" text="Send cookie?"></ASP:CHECKBOX>
			</TD>
			<TD class="formarea">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="formarea">
				<P align="left">Common Password:</P>
			</TD>
			<TD class="formarea">
				<ASP:TEXTBOX id="txtCommonPassword" runat="server"></ASP:TEXTBOX>
			</TD>
			<TD class="formarea">&nbsp;</TD>
		</TR>
		<TR>
			<TD class="formarea">Show in admin:</TD>
			<TD class="formarea">
				<ASP:RADIOBUTTONLIST id="rdoFromComponentList" runat="server" repeatdirection="Horizontal">
					<ASP:LISTITEM value="1" selected="True">Yes</ASP:LISTITEM>
					<ASP:LISTITEM value="0">No</ASP:LISTITEM>
				</ASP:RADIOBUTTONLIST></TD>
			<TD class="formarea"></TD>
		</TR>
		<TR>
			<TD class="formarea">Show in Wysiwyg:</TD>
			<TD class="formarea">
				<ASP:RADIOBUTTONLIST id="rdoFromWysiwyg" runat="server" repeatdirection="Horizontal">
					<ASP:LISTITEM value="1" selected="True">Yes</ASP:LISTITEM>
					<ASP:LISTITEM value="0">No</ASP:LISTITEM>
				</ASP:RADIOBUTTONLIST></TD>
			<TD class="formarea"></TD>
		</TR>
		<TR>
			<TD class="formarea" colspan="3">
				<P align="center">
					<ASP:BUTTON id="btnSave" runat="server" text="Save" onclick="btnSave_Click"></ASP:BUTTON>&nbsp;
					<ASP:BUTTON id="btnCancel" runat="server" text="Cancel" onclick="btnCancel_Click"></ASP:BUTTON>
				</P>
			</TD>
		</TR>
	</TABLE>
	<!--Skall man här kunna ange vilka som har har tillgång till denna komponent?-->
</div>
</asp:Content>