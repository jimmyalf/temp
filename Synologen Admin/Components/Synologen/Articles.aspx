<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="Articles.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Articles" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-Category-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h1>Artiklar</h1>
	        <fieldset>
		        <legend><asp:Literal id="ltHeading" runat="server" Text="Lägg till artikel"/></asp:Literal></legend>		
		        <div class="formItem">
		            <label class="labelLong">Artikelnamn</label>
					<asp:RequiredFieldValidator id="rfvArticleName" runat="server" errormessage="Namn saknas" controltovalidate="txtName" Display="Dynamic" ValidationGroup="Save">(Namn saknas!)</asp:RequiredFieldValidator>
		            <asp:TextBox runat="server" ID="txtName"/>
		        </div>
		        <div class="formItem clearLeft">
		            <label class="labelLong">Artikelnummer</label>
					<asp:RequiredFieldValidator id="rfvArticleNumber" runat="server" errormessage="Artikelnummer saknas" controltovalidate="txtArticleNumber" Display="Dynamic" ValidationGroup="Save">(Artikelnummer saknas!)</asp:RequiredFieldValidator>
		            <asp:TextBox runat="server" ID="txtArticleNumber"/>
		        </div>
		        <div class="formItem clearLeft">
		            <label class="labelLong">Beskrivning</label>
		            <asp:TextBox runat="server" ID="txtDescription" cssClass="txtAreaWide" TextMode="MultiLine"/>
		        </div>		        	        
		        <div class="formCommands">
		            <asp:button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Spara" CssClass="btnSmall" ValidationGroup="Save"/>
		        </div>
	        </fieldset>
        
			<br />
            <asp:GridView ID="gvArticles" 
				OnRowDeleting="gvArticles_Deleting"
				OnRowEditing="gvArticles_Editing" 
                runat="server" 
                DataKeyNames="cId" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField headerText="Namn" DataField="cName" SortExpression="cName"/>
                    <asp:BoundField headerText="Artikelnummer" DataField="cArticleNumber" SortExpression="cArticleNumber"/>
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
