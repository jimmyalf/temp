<%@ Page Language="C#" MasterPageFile="~/components/QuickForm/QuickFormMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.QuickForm.Presentation.Components.QuickForm.Inbox" Title="Untitled Page" Codebehind="Inbox.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phQuickForm" Runat="Server">
<div id="dCompMain" class="Components-QuickForm-Inbox-aspx">
<div class="fullBox">
<div class="wrap">
<h1>QuickForm</h1>
<div id="dInboxItem" runat="server" visible="false">
    <fieldset>
	    <legend>Item Details</legend>		
	    <div class="formItem">
	        <asp:PlaceHolder id="phContent" runat="server"/>
	        <asp:Label ID="lblContent" runat="server" SkinId="labelLong"/>
	    </div>
    </fieldset>
</div>

<asp:GridView ID="gvInbox" runat="server" DataKeyNames="cId" SkinID="Striped" 
OnRowEditing="gvInbox_Editing" OnRowDeleting="gvInbox_Deleting" 
AllowSorting="false" autogeneratecolumns="false">
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
		<asp:TemplateField headertext="Read Flag">
    		<ItemStyle CssClass="center" />
        	<ItemTemplate>
		        <asp:Image id="imgRead" runat="server" />
	        </ItemTemplate>
	    </asp:TemplateField>
        <asp:BoundField headerText="Date" DataField="cSubmitDate" SortExpression="cSubmitDate"/>
        <asp:BoundField headerText="From" DataField="cFrom" SortExpression="cFrom"/>
        <asp:ButtonField Text="View" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
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
