<%@ Page Language="C#" MasterPageFile="~/components/Synologen/SynologenMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Files" Title="Untitled Page" Codebehind="Files.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" Runat="Server">
<div id="dCompMain" class="Components-Synologen-Files-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Member</h1>
                
                <fieldset>
	                <legend>Filter and search</legend>		
	                <div class="formItem">
	                    <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpFileCategories" SkinId="Long"/>
	                    <asp:DropDownList runat="server" ID="drpFileCategories"/>&nbsp;
	                    <asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Show"  SkinId="Big" />&nbsp;|&nbsp;
	                    <asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Show All"  SkinId="Big" />
	                </div>
                </fieldset>
    
                <br />
                
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
                        <asp:HyperLinkField HeaderText="Visa" Target="_blank" DataNavigateUrlFields="Link" Text="Visa"  />
                        <asp:TemplateField headertext="Radera" HeaderStyle-CssClass="controlColumn" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center">
	                        <ItemTemplate>
                                <asp:Button id="btnDelete" runat="server" OnDataBinding="AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" />
	                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <fieldset>	
	                <div class="formItem">
						<br />
						<input type="button" name="inputBack" class="btnBig" onclick="javascript:window.history.back();" value="Tillbaka" />
	                </div>
                </fieldset>
    
                <br />
            </div>
        </div>
    </div>
</asp:Content>

