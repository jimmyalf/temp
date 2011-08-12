<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="Articles.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Articles" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h1>Artiklar</h1>
			<br />
			<div class="wpcPager"><WPC:PAGER id="pager" runat="server" /></div>
            <asp:GridView ID="gvArticles" 
				OnRowDeleting="gvArticles_Deleting"
				OnRowEditing="gvArticles_Editing" 
                runat="server" 
                DataKeyNames="Id" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField headerText="Namn" DataField="Name" SortExpression="Name"/>
                    <asp:BoundField headerText="Artikelnummer" DataField="Number" SortExpression="Number"/>
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
