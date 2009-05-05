<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master" AutoEventWireup="true" CodeBehind="Settlements.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.Settlements" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
<div id="dCompMain" class="Components-Synologen-Settlements-aspx">
        <div class="fullBox">
            <div class="wrap">
            <h2>Butiksutbetalningar</h2>
	        <fieldset>	 
		        <div class="formItem">
		            <label class="labelLong">Antal ordrar redo för utbetalning: <%=NumberOfOrdersReadyForSettlement %></label>
		        </div>	               	        
		        <div class="formCommands">
		            <asp:button ID="btnCreateSettlement" runat="server" OnClick="btnCreateSettlement_Click" Text="Skapa utbetalning" CssClass="btnBig" ValidationGroup="Error"/>
		        </div>
	        </fieldset>
        
			<br />
            <asp:GridView ID="gvSettlements" 
                runat="server" 
                DataKeyNames="cId" 
                SkinID="Striped" >
                <Columns>
                    <asp:BoundField headerText="Id" DataField="cId" SortExpression="cId"/>
                    <asp:BoundField headerText="Skapad" DataField="cCreatedDate" SortExpression="cCreatedDate" DataFormatString="{0:yyyy-MM-dd HH:mm}"/>
                    <asp:BoundField headerText="Antal fakturor" DataField="cNumberOfOrders" SortExpression="cNumberOfOrders"/>
					<asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn" HeaderText="Fakturor" >
						<ItemTemplate>
							<a href="Orders.aspx?settlementId=<%# DataBinder.Eval(Container.DataItem, "cId")%>">Visa fakturor</a>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField ItemStyle-HorizontalAlign="Center"  HeaderStyle-CssClass="controlColumn" HeaderText="Detaljer" >
						<ItemTemplate>
							<a href="ViewSettlementDetails.aspx?settlementId=<%# DataBinder.Eval(Container.DataItem, "cId")%>">Visa detaljer</a>
						</ItemTemplate>
					</asp:TemplateField>					
                </Columns>
            </asp:GridView>          
        </div>
      </div>
    </div>
</asp:Content>
