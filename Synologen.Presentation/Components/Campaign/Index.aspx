<%@ Page Language="C#" MasterPageFile="~/components/Campaign/CampaignMain.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="components_Campaign_Index" Title="Untitled Page" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCampaign" Runat="Server">
    <div id="dCompMain" class="Components-Campaign-Index-aspx">
    <div class="fullBox">
    <div class="wrap">
        <h1>Campaign</h1>
        
	    <fieldset>
		    <legend>Filter and search</legend>		
		    <div class="formItem">
		        <asp:Label ID="lblShow" runat="server" AssociatedControlID="drpCategories" SkinId="labelLong"/>
		        <asp:DropDownList runat="server" ID="drpCategories"/>&nbsp;
		        <asp:Button runat="server" id="btnSetFilter" OnClick="btnSetFilter_Click" text="Show"/>&nbsp;|&nbsp;
		        <asp:Button runat="server" id="btnShowAll" OnClick="btnShowAll_Click" text="Show All" />
		    </div>
		    <div class="formItem clearLeft">
		        <asp:Label ID="lblSearch" runat="server" AssociatedControlID="txtSearch" SkinId="labelLong"/>
		        <asp:TextBox runat="server" ID="txtSearch"/>&nbsp;
		        <asp:Button runat="server" id="btnSearch" OnClick="btnSearch_Click" text="Search"/>
		    </div>
	    </fieldset>
    
    
    <div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>
    
    <asp:GridView ID="gvCampaigns" 
                    runat="server" 
                    OnRowCreated="gvCampaigns_RowCreated" 
                    DataKeyNames="cId" 
                    OnSorting="gvCampaigns_Sorting" 
                    OnPageIndexChanging="gvCampaigns_PageIndexChanging" 
                    SkinID="Striped" 
                    OnRowEditing="gvCampaigns_Editing" 
                    OnRowDeleting="gvCampaigns_Deleting" 
                    OnRowCommand="gvCampaigns_RowCommand" 
                    OnRowDataBound="gvCampaigns_RowDataBound" 
                    AllowSorting="true"
                    AutoGenerateColumns="False">
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
            <asp:TemplateField headertext="Status">
			    <ItemTemplate>
                    <asp:Label id="lblStatus" runat="server"/>
			    </ItemTemplate>
		    </asp:TemplateField>
            <asp:BoundField headerText="Name" DataField="cName" SortExpression="cName"/>
            <asp:BoundField headerText="Heading" DataField="cHeading" SortExpression="cHeading"/>
            <asp:BoundField headerText="Start date" DataField="cStartDate" DataFormatString="{0:yyyy-MM-dd}"  />
            <asp:ButtonField headerText="Files" Text="Files" CommandName="Files"  ButtonType="Button" ControlStyle-CssClass="btnSmall" />
            <asp:ButtonField headerText="Add Files" Text="Add Files" CommandName="AddFiles"  ButtonType="Button" ControlStyle-CssClass="btnSmall" />
            <asp:ButtonField headerText="Edit" Text="Edit" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" />
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

