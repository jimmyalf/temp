<%@ Page Language="C#" MasterPageFile="~/Settings/SettingsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Settings.Languages.main_languages" Codebehind="main_languages.aspx.cs" %>
<asp:Content ID="Content" ContentPlaceHolderID="cphSettings" Runat="Server">
<div id="dCompSubNavigation">
	<ul title="Navigation">
		<li><a href="main_languages.aspx" title="List all languages"><span>List languages</span></a></li>
		<li id="liAddLanguage" runat="server"><a href="edit_language.aspx" title="Add new language"><span>Add language</span></a></li>
	</ul>
</div>
<div id="dCompMain">
	<div class="fullBox">
	<div class="wrap">
	<h1>List languages</h1>
	<asp:GridView id="gvLanguages" runat="server" OnRowDeleting="delete_language" OnRowEditing="edit_language" AllowSorting="True" DataKeyNames="cId" AutoGenerateColumns="False" SkinID="Striped">
		<Columns>
			<asp:BoundField DataField="cId" HeaderText="LanguageId" />
			<asp:BoundField DataField="cName" HeaderText="Name" />
			<asp:BoundField DataField="cDescription" HeaderText="Description" />
			<asp:BoundField DataField="cResource" HeaderText="Resource" />
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