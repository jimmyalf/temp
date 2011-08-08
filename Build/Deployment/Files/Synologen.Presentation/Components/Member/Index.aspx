<%@ Page Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Index" MasterPageFile="~/components/Member/MemberMain.master" Codebehind="Index.aspx.cs" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phMember" Runat="Server">
    <div id="dCompMain" class="Components-Member-Index-aspx">
        <div class="fullBox">
            <div class="wrap">
                <h1>Member</h1>
                
                <fieldset>
	                <legend>Filter and search</legend>		
	                <div class="formItem">
	                    <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpCategories" SkinId="Long"/>
	                    <asp:DropDownList runat="server" ID="drpCategories"/>&nbsp;
	                    <asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Show"/>&nbsp;|&nbsp;
	                    <asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Show All" />
	                </div>
	                <div class="formItem clearLeft">
	                    <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" SkinId="Long"/>
	                    <asp:TextBox runat="server" ID="txtSearch"/>&nbsp;
	                    <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="Search"/>
	                </div>
                </fieldset>
    
                <div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>
                
                <asp:GridView ID="gvMembers" 
                                runat="server" 
                                OnRowCreated="gvMembers_RowCreated" 
                                DataKeyNames="cMemberId" 
                                OnSorting="gvMembers_Sorting" 
                                OnPageIndexChanging="gvMembers_PageIndexChanging" 
                                SkinID="Striped" 
                                OnRowEditing="gvMembers_Editing" 
                                OnRowDeleting="gvMembers_Deleting" 
                                OnRowCommand="gvMembers_RowCommand" 
                                OnRowDataBound="gvMembers_RowDataBound" 
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
                        <asp:BoundField headerText="Id" DataField="cMemberId" SortExpression="cMemberId" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField headerText="Organisation" DataField="cOrgName" SortExpression="cOrgName"/>
                        <asp:BoundField headerText="FirstName" DataField="cContactFirst" SortExpression="cFirstName"/>
                        <asp:BoundField headerText="SurName" DataField="cContactLast" SortExpression="cLastName"/>
                        <asp:BoundField headerText="Email" DataField="cEmail" SortExpression="cEmail"/>
						<asp:TemplateField headertext="Active" SortExpression="cActive">
							<ItemStyle CssClass="center" />
							<ItemTemplate>
								<asp:Image id="imgActive" runat="server" />
							</ItemTemplate>
						</asp:TemplateField>
                        <asp:ButtonField Text="Files" CommandName="Files" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
                        <asp:ButtonField Text="AddFiles" CommandName="AddFiles" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
                        <asp:ButtonField Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
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
