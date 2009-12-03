<%@ Page Title="" Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="OpqImprovments.aspx.cs" Inherits="Spinit.Wpc.Synologen.OPQ.Admin.Components.Synologen.OpqImprovments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
    <div id="dCompMain" class="Components-Synologen-OpqImprovments-aspx">
   		<opq:UserMessageManager ID="opqUserMessageManager" ControlId="Opq-UserMessage-Administration" runat="server" />
        <div class="fullBox">
            <div class="wrap">
				<h1>Förbättringsförslag</h1>
				<asp:PlaceHolder ID="phImprovment" runat="server" Visible="false">
				<div id="improvment">
					Butik: <asp:Literal ID="ltShop" runat="server" />
					Avsändare: <asp:HyperLink ID="hlSender" runat="server" />
					Förslag: <asp:Literal ID="ltBody" runat="server" />
				</div>
				</asp:PlaceHolder>
				<asp:GridView ID="gvImprovments" 
                runat="server" 
                OnRowCreated="gvImprovments_RowCreated" 
                DataKeyNames="Id" 
                SkinID="Striped" 
                OnRowEditing="gvImprovments_Editing" 
                OnRowDeleting="gvImprovments_Deleting" >
                <Columns>
		            <asp:TemplateField headertext="Rutin"  ItemStyle-HorizontalAlign="Center" >
			            <ItemTemplate>
							<asp:Literal ID="ltRoutine" runat="server" />
			            </ItemTemplate>
		            </asp:TemplateField>
		            <asp:TemplateField headertext="Butik"  ItemStyle-HorizontalAlign="Center" >
			            <ItemTemplate>
							<asp:Literal ID="ltShopSender" runat="server" />
			            </ItemTemplate>
		            </asp:TemplateField>
  		            <asp:TemplateField headertext="Avsändare"  ItemStyle-HorizontalAlign="Center" >
			            <ItemTemplate>
							<asp:Literal ID="ltEmail" runat="server" />
			            </ItemTemplate>
		            </asp:TemplateField>
		            <asp:TemplateField headertext="Datum"  ItemStyle-HorizontalAlign="Center" >
			            <ItemTemplate>
							<asp:Literal ID="ltDate" runat="server" />
			            </ItemTemplate>
		            </asp:TemplateField>		            
                    <asp:ButtonField Text="Visa" HeaderText="Visa" CommandName="Edit" ButtonType="Button" ControlStyle-CssClass="btnSmall" HeaderStyle-CssClass="controlColumn" ItemStyle-HorizontalAlign="Center"/>
		            <asp:TemplateField headertext="Radera" ControlStyle-CssClass="btnSmall"  ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="controlColumn"  >
			            <ItemTemplate>
                            <asp:Button id="btnDelete" runat="server" OnDataBinding="btnDelete_AddConfirmDelete" commandname="Delete" text="Radera" CssClass="btnSmall" />
			            </ItemTemplate>
		            </asp:TemplateField>
                </Columns>
            </asp:GridView>          

            </div>
        </div>
    </div>
</asp:Content>

