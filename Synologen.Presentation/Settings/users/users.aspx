<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Users.Users" Codebehind="Users.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="users.aspx" title="List all users"><span>List users</span></a></li>
		<li><a href="user_edit.aspx?add=new" title="Add new user"><span>Add user</span></a></li>
	</ul>
</div>
<div id="dCompMain">
	<div class="fullBox">
	<div class="wrap">
			<h1>Users</h1>
			
			<asp:GridView id="gvUsers" runat="server" allowsorting="True" autogeneratecolumns="False" OnRowEditing="gvUsers_RowEditing" OnRowDeleting="gvUsers_RowDeleting" DataKeyNames="cId" SkinID="Striped">
				<Columns>
					<asp:BoundField datafield="cId" headertext="UserId"></asp:BoundField>
					<asp:BoundField datafield="cUserName" headertext="User Name"></asp:BoundField>
					<asp:BoundField datafield="cEmail" headertext="E-mail"></asp:BoundField>
					<asp:TemplateField headertext="Active">
						<ItemStyle CssClass="center" />
						<ItemTemplate>
							<asp:Image id="imgActive" runat="server" />
						</ItemTemplate>
					</asp:TemplateField>
					<asp:ButtonField CommandName="Edit" ButtonType="Button" Text="Edit" ControlStyle-CssClass="btnSmall" />
					<asp:TemplateField>
						<ItemTemplate>
							<asp:Button id="btnDelete" OnDataBinding="AddConfirmDelete" commandname="Delete" runat="Server" text="Delete" />
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
	</div>
	</div>
</div>
</asp:Content>