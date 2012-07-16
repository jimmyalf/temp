<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Locations.main_locations" Codebehind="main_locations.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="main_locations.aspx" title="List all locations"><span>List locations</span></a></li>
		<li id="liAddLocation" runat="server"><a href="edit_location.aspx" title="Add new location"><span>Add location</span></a></li>
		<%if (Spinit.Wpc.Utility.Core.CxUser.Current.IsInGroupType(Spinit.Wpc.Base.Data.GroupTypeRow.TYPE.SUPER_ADMIN)) { %>
		    <li id="liUpgradeFiles" runat="server"><a href="main_locations.aspx?runupgrad=true" title="Upgrade locations"><span>Upgrade locations</span></a></li>
		<%} %>
	</ul>
</div>
<div id="dCompMain">
	<div class="fullBox">
	<div class="wrap">
	<h1>List locations</h1>
	<asp:GridView id="gvLocations" runat="server" OnRowDeleting="delete_location" OnRowEditing="edit_location" AllowSorting="True" DataKeyNames="cId" AutoGenerateColumns="False" SkinID="Striped">
		<Columns>
			<asp:BoundField DataField="cId" HeaderText="LocationId" />
			<asp:BoundField DataField="cName" HeaderText="Name" />
			<asp:BoundField DataField="cPublishPath" HeaderText="Path" />
			<asp:ButtonField Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
			<asp:TemplateField>
				<ItemTemplate>
					<asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Delete" />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	</div>
	</div>
</div>
</asp:Content>