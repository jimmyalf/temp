<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Groups.group_access" Codebehind="group_access.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="group_access.aspx" title="List all groups"><span>List groups</span></a></li>
		<li><a href="group_edit.aspx?add=new" title="Add new group"><span>Add group</span></a></li>
	</ul>
</div>
<div id="dCompMain">
	<div class="fullBox">
	<div class="wrap">
			<h1>Groups</h1>
			<asp:GridView OnRowEditing="editGroup" DataKeyNames="cId" OnRowDeleting="deleteGroup" ID="gvGroups" runat="server" autogeneratecolumns="False" SkinID="Striped">
				<Columns>
					<asp:BoundField datafield="cName" headertext="Name"></asp:BoundField>
					<asp:BoundField datafield="GrpTpeName" headertext="Type"></asp:BoundField>
					<asp:BoundField datafield="cCreatedBy" headertext="Created By"></asp:BoundField>
					<asp:BoundField datafield="cCreatedDate" headertext="CreatedDate"></asp:BoundField>
					<asp:BoundField datafield="cChangedBy" headertext="Changed By"></asp:BoundField>
					<asp:BoundField datafield="cChangedDate" headertext="Changed Date"></asp:BoundField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:Button id="btnEdit" Visible='<%#HasAccess(DataBinder.Eval(Container.DataItem, "GrpTpeId"))%>' runat="server" commandname="Edit" text="Edit" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField>
						<ItemTemplate>
							<asp:Button id="btnDelete" Visible='<%#HasAccess(DataBinder.Eval(Container.DataItem, "GrpTpeId"))%>' runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Delete" />
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
	</div>
	</div>
</div>
</asp:Content>