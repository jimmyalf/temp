<%@ Page Language="C#" MasterPageFile="~/components/QuickForm/QuickFormMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.QuickForm.Presentation.Components.QuickForm.Index" Title="Untitled Page" Codebehind="Index.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phQuickForm" Runat="Server">
<div id="dCompMain" class="Components-QuickForm-Index-aspx">
<div class="fullBox">
<div class="wrap">
<h1>QuickForm</h1>

<asp:GridView ID="gvQuickForm" runat="server" DataKeyNames="cId" SkinID="Striped" 
OnRowEditing="gvQuickForm_Editing" OnRowCommand="gvQuickForm_RowCommand" 
OnRowDeleting="gvQuickForm_Deleting" AllowSorting="false" OnRowCreated="gvQuickForm_RowCreated"
autogeneratecolumns="false">
    <Columns>
         <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelectHeader" runat="server" OnCheckedChanged="chkSelectHeader_CheckedChanged" AutoPostBack="true" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField headerText="Id" DataField="cId" SortExpression="cId" ItemStyle-HorizontalAlign="Center"/>
        <asp:BoundField headerText="Name" DataField="cName" SortExpression="cName"/>
        <asp:ButtonField Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
        <asp:ButtonField Text="Inbox" CommandName="Inbox" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
		<asp:TemplateField headertext="Delete">
			<ItemTemplate>
                <asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Delete" CssClass="btnSmall" />
			</ItemTemplate>
		</asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
</div>
</div>
</asp:Content>
