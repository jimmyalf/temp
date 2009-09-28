<%@ Page Language="C#" MasterPageFile="~/components/Synologen/SynologenMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.FileCategories" Title="Untitled Page" Codebehind="FileCategories.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" Runat="Server">
<div id="dCompMain" class="Components-Member-Synologen-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Filkategorier - <%=CategoryType %></h2>
	        <fieldset>
		        <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>		
		        <div class="formItem">
		            <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
		            <asp:TextBox runat="server" ID="txtName"/>
		        </div>
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server"  Text="Spara" OnClick="btnSave_Click"  SkinId="Big"/>
		        </div>
	        </fieldset>
	        <br />
	        <asp:GridView ID="gvFileCategory" 
                runat="server" 
                DataKeyNames="cId" 
                SkinID="Striped" 
                OnRowEditing="gvFileCategory_Editing" 
                OnRowDeleting="gvFileCategory_Deleting" 
                AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="Namn" DataField="cDescription" SortExpression="cDescription"/>
                    <asp:ButtonField HeaderText="Redigera" Text="Redigera" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall"  HeaderStyle-CssClass="controlColumn"  ItemStyle-HorizontalAlign="Center" />
		            <asp:TemplateField HeaderText="Radera"  ControlStyle-CssClass="btnSmall" HeaderStyle-CssClass="controlColumn"  ItemStyle-HorizontalAlign="Center" >
			            <ItemTemplate>
                            <asp:Button id="btnDelete" runat="server" OnDataBinding="btnDelete_AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" />
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
        </div>
      </div>
    </div>
</asp:Content>

