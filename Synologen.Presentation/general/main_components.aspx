<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.General.main_components" Codebehind="main_components.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div class="content-area">
	<H1>COMPONENT</H1>
	<HR>
	<ASP:BUTTON id="btnAddComponent" runat="server" text="Add Component" onclick="btnAddComponent_Click"></ASP:BUTTON>
	<HR>
	<TABLE width="520" class="formarea">
		<TR class="tableheader">
			<TD><B>ComponentID</B></TD>
			<TD><B>ComponentName</B></TD>
			<TD><B>Alias</B></TD>
			<TD><B>Path</B></TD>
			<TD><B>Edit</B></TD>
		</TR>
		<TR>
			<TD><B>Internal components</B></TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD>12</TD>
			<TD>News</TD>
			<TD>/news</TD>
			<TD>/extranet</TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD>13</TD>
			<TD>Jobs</TD>
			<TD>/jobs</TD>
			<TD>/extranet</TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD><B>External components</B></TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
		</TR>
		<TR>
			<TD>20</TD>
			<TD>Avvikelsesystem</TD>
			<TD>&nbsp;</TD>
			<TD>&nbsp;</TD>
			<TD><A href="edit_component.aspx"><IMG src="../common/icons/edit.gif" width="13" height="13" border="0"></A></TD>
		</TR>
	</TABLE>
	An internal Component are configured by system administrator and only availible 
	to view.
</div>
</asp:Content>