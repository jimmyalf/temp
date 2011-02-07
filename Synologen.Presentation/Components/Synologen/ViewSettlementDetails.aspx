<%@ Page Language="C#" MasterPageFile="~/Components/Synologen/SynologenMain.master"	AutoEventWireup="true" Codebehind="ViewSettlementDetails.aspx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Components.Synologen.ViewSettlementDetails" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phSynologen" runat="server">
	<div id="dCompMain" class="Components-Synologen-ViewSettlementDetails-aspx">
		<div class="fullBox">
			<div class="wrap">
				<h2>Utbetalning</h2>
				<fieldset>
					<legend>Utbetalningsdetaljer</legend>
					<div class="formItem clearLeft">
						<label class="labelShort">Id</label><span><%=Settlement.Id %></span><br />
						<label class="labelShort clearLeft">Datum</label><span><%=Settlement.CreatedDate.ToString("yyyy-MM-dd HH:mm") %></span><br />
						<label class="labelShort clearLeft">Period</label><span><%=GetSettlementPeriodNumber(Settlement.CreatedDate)%></span><br />
					</div>
					<div class="formItem">						
						<label class="labelShort clearLeft">Utbetalas inkl moms</label><span><%=TotalValueIncludingVAT.ToString("C2") %></span><br />
						<label class="labelShort clearLeft">Utbetalas exkl moms</label><span><%=TotalValueExcludingVAT.ToString("C2") %></span><br />
					</div>		
					<div class="formCommands hide-on-print">	
						<input class="btnBig" type="button" OnClick="window.location='Settlements.aspx';" value="Tillbaka">
						<input class="btnBig" type="button" OnClick="window.print();return false;" value="Skriv ut">
					</div>										
				</fieldset>		
				<br />
				<asp:gridview id="gvSettlementDetails" 
					runat="server" 
					datakeynames="cId,cPriceIncludingVAT,cPriceExcludingVAT" 
					skinid="Striped">
                <Columns>
					<asp:TemplateField HeaderText="Butik" HeaderStyle-CssClass="wide-column" SortExpression="cShopNumber" >
						<ItemTemplate><%# Eval("cShopNumber")%> - <%# Eval("cShopName")%></ItemTemplate>
					</asp:TemplateField>
					<asp:BoundField headerText="Bankgiro" DataField="cGiroNumber" SortExpression="cIdcGiroNumber"/>
                    <asp:BoundField headerText="Antal fakturor" DataField="cNumberOfOrders" SortExpression="cNumberOfOrders" ItemStyle-CssClass="hide-on-print" HeaderStyle-CssClass="hide-on-print"/>					
					<asp:BoundField headerText="Utbetalas exkl moms" DataFormatString="{0:C2}" DataField="cPriceExcludingVAT" SortExpression="cPriceExcludingVAT" ItemStyle-CssClass="hide-on-print" HeaderStyle-CssClass="hide-on-print"/>
					<asp:BoundField headerText="Utbetalas inkl moms" DataFormatString="{0:C2}" DataField="cPriceIncludingVAT" SortExpression="cPriceIncludingVAT"/>                    
                </Columns>
            </asp:gridview>
			</div>
		</div>
	</div>
</asp:Content>
