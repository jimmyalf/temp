<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.General.external_components" Codebehind="external_components.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div class="content-area">
	<H1>External Components</H1>
	<HR>
	<ASP:BUTTON id="btnAddExternalComponent" runat="server" text="Add External Component" onclick="btnAddExternalComponent_Click"></ASP:BUTTON>
	<HR>
	<ASP:DATAGRID id="dgrExternalComponents" onitemcreated="dgrExternalComponents_ItemCreate" ondeletecommand="delete_external_component" oneditcommand="edit_external_component" allowsorting="True" width="100%" borderwidth="0" cellspacing="1" cellpadding="5" datakeyfield="cId" autogeneratecolumns="False" runat="server">
		<COLUMNS>
			<ASP:BOUNDCOLUMN itemstyle-width="50" datafield="cId" headertext="ComponentId"></ASP:BOUNDCOLUMN>
			<ASP:BOUNDCOLUMN itemstyle-width="200" datafield="cName" headertext="Name"></ASP:BOUNDCOLUMN>
			<ASP:TEMPLATECOLUMN headertext="Description">
				<ITEMTEMPLATE>
					<%# getShortString ((string) DataBinder.Eval (Container.DataItem, "cDescription")) %>
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
			<ASP:BUTTONCOLUMN headertext="Edit" text="&lt;img border=0 alt='Edit' src=../common/icons/edit.gif&gt;" commandname="Edit">
				<ITEMSTYLE horizontalalign="Center" width="15px"></ITEMSTYLE>
			</ASP:BUTTONCOLUMN>
			<ASP:TEMPLATECOLUMN itemstyle-horizontalalign="Center" itemstyle-width="15" headertext="Delete">
				<ITEMTEMPLATE>
					<ASP:LINKBUTTON id="btnDelete" commandname="Delete" runat="Server" text="<IMG border='0' alt='Delete' src='../common/icons/delete.gif'>" />
				</ITEMTEMPLATE>
			</ASP:TEMPLATECOLUMN>
		</COLUMNS>
	</ASP:DATAGRID>
</div>
</asp:Content>