<%@ Page Language="C#" MasterPageFile="~/components/Member/MemberMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Files" Title="Untitled Page" Codebehind="Files.aspx.cs" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phMember" Runat="Server">
<div id="dCompMain" class="Components-Member-Files-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Member</h1>
                
                <fieldset>
	                <legend>Filter and search</legend>		
	                <div class="formItem">
	                    <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpFileCategories" SkinId="Long"/>
	                    <asp:DropDownList runat="server" ID="drpFileCategories"/>&nbsp;
	                    <asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Show"/>&nbsp;|&nbsp;
	                    <asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Show All" />
	                </div>
                </fieldset>
    
                
                
                <asp:GridView ID="gvFiles" 
                                runat="server" 
                                OnRowCreated="gvFiles_RowCreated" 
                                DataKeyNames="Id" 
                                OnSorting="gvFiles_Sorting" 
                                OnPageIndexChanging="gvFiles_PageIndexChanging" 
                                SkinID="Striped" 
                                OnRowEditing="gvFiles_Editing" 
                                OnRowDeleting="gvFiles_Deleting" 
                                OnRowCommand="gvFiles_RowCommand" 
                                OnRowDataBound="gvFiles_RowDataBound" 
                                AllowSorting="true">
                    <Columns>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectHeader" runat="server" OnCheckedChanged="chkSelectHeader_CheckedChanged" AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField headerText="Id" DataField="Id" SortExpression="Id" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField headerText="Name" DataField="Name" SortExpression="Name" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField headerText="Description" DataField="Description" SortExpression="Description" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField headerText="Category" DataField="Application" SortExpression="Application" ItemStyle-HorizontalAlign="Center"/>
                        <asp:HyperLinkField HeaderText="Visa" Target="_blank" DataNavigateUrlFields="Link" Text="Visa" />
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

