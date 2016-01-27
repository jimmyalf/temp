<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="FtpProfiles.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.FtpProfiles" Title="Untitled Page" %>
<%@ Register Src="ContractSalesSubMenu.ascx" TagName="SubMenu" TagPrefix="syn" %>
<asp:Content runat="server" ContentPlaceHolderID="SubMenuPlaceHolder">
	<syn:SubMenu runat="server" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h1>Ftp-profiler</h1>
			<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>
            <asp:GridView ID="gvFtpProfiles" 
				OnRowDeleting="gvFtpProfile_Deleting"
				OnRowEditing="gvFtpProfile_Editing" 
                runat="server" 
                DataKeyNames="Id" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField headerText="Namn" DataField="Name" SortExpression="Name"/>
                    <asp:BoundField headerText="Server-url" DataField="ServerUrl" SortExpression="ServerUrl"/>
                    <asp:BoundField headerText="Användarnamn" DataField="Username" SortExpression="Username"/>
                    <asp:BoundField headerText="Lösenord" DataField="Password" SortExpression="Password"/>
                    <asp:BoundField headerText="Protokolltyp" DataField="ProtocolType" SortExpression="ProtocolType"/>
                    <asp:BoundField headerText="Passiv FTP" DataField="PassiveFtp" SortExpression="PassiveFtp"/>
                    <asp:ButtonField Text="Redigera" HeaderText="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn"  />
		            <asp:TemplateField headertext="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn"  >
			            <ItemTemplate>
                            <asp:Button id="btnDelete" runat="server" OnDataBinding="btnDelete_AddConfirmDelete" Commandname="Delete" Text="Radera" CssClass="btnSmall" />
			            </ItemTemplate>
		            </asp:TemplateField>
                </Columns>
            </asp:GridView>          
        </div>
      </div>
    </div>
</asp:Content>
