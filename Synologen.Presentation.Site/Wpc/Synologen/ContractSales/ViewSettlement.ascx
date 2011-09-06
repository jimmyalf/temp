<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSettlement.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ContractSales.ViewSettlement" %>
<div class="synologen-control">
	<fieldset><legend>Utbetalningsuppgifter</legend>
		<p><label>Utbetalning:</label>&nbsp;<span><%#Model.SettlementId %></span></p>
		<p><label>Butiknummer:</label>&nbsp;<span><%#Model.ShopNumber %></span></p>
		<p><label>Period:</label>&nbsp;<span><%#Model.Period %></span></p>
		<p><label>Avtalsförsäljningsvärde inkl moms:</label>&nbsp;<span><%#Model.ContractSalesValueIncludingVAT %></span></p>
		<p><label>Linsabonnemangsvärde inkl moms:</label>&nbsp;<span><%#Model.LensSubscriptionsValueIncludingVAT %></span></p>
		<p><label>Antal linsabonnemang-transaktioner:</label>&nbsp;<span><%#Model.LensSubscriptionTransactionsCount %></span></p>
		<p><a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SettlementListPage %>">&laquo;&nbsp;Tillbaka</a></p>
	</fieldset>
	<div class="control-actions">
		<asp:Button ID="btnSwitchView" runat="server" Text="Visa detaljer" OnClick="btnSwitchView_Click" />
		<asp:Button ID="btnMarkAsPayed" runat="server" Text="Markera som utbetalda" OnClick="btnMarkAsPayed_Click" OnClientClick="return confirm('Är du säker på att du vill markera fakturor som utbetalda?');"  Enabled='<%#Model.MarkAsPayedButtonEnabled %>'/>
		<input type="button" onclick="window.print();return false;" value="Skriv ut"/>
	</div>
	<asp:PlaceHolder ID="plSimpleView" runat="server" Visible='<%#Model.DisplaySimpleView%>'>
	<fieldset><legend>Avtalsförsäljning</legend>
	<asp:Repeater ID="rptSettlementOrderItemsSimple" runat="server" DataSource='<%#Model.SimpleContractSales%>'>
	<HeaderTemplate>
		<table>
			<tr class="synologen-table-headerrow">	
				<th>Artikelnummer</th>
				<th>Artikel</th>
				<th>Antal</th>
				<th>Värde</th>
				<th>Momsfri</th>
			</tr>			
	</HeaderTemplate>
	<ItemTemplate>
			<tr>
				<td><%# Eval("ArticleNumber") %></td>
				<td><%# Eval("ArticleName") %></td>
				<td><%# Eval("Quantity") %></td>
				<td><%# Eval("ValueExcludingVAT")%></td>
				<td><%# Eval("IsVATFree")%></td>
			</tr>
	</ItemTemplate>	
	<FooterTemplate>
		</table>
	</FooterTemplate>			
	</asp:Repeater>	
	</fieldset>
	</asp:PlaceHolder>
	<asp:PlaceHolder ID="plDetailedView" runat="server" Visible='<%#Model.DisplayDetailedView%>'>
	<fieldset><legend>Avtalsförsäljning</legend>
	<asp:Repeater ID="rptSettlementOrderItemsDetailed" runat="server" DataSource='<%#Model.DetailedContractSales%>'>
	<HeaderTemplate>
		<table>
			<tr class="synologen-table-headerrow">	
				<th>Order Nr</th>
				<th>Avtalsföretag</th>
				<th>Artikelnummer</th>
				<th>Artikel</th>
				<th>Antal</th>
				<th>Värde</th>
				<th>Momsfri</th>
			</tr>			
	</HeaderTemplate>
	<ItemTemplate>
			<tr>
				<td><%# Eval("ContractSaleId") %></td>
				<td><%# Eval("ContractCompany") %></td>
				<td><%# Eval("ArticleNumber") %></td>
				<td><%# Eval("ArticleName") %></td>
				<td><%# Eval("Quantity") %></td>
				<td><%# Eval("ValueExcludingVAT")%></td>
				<td><%# Eval("IsVATFree") %></td>
			</tr>
	</ItemTemplate>	
	<FooterTemplate>
		</table>
	</FooterTemplate>			
	</asp:Repeater>	
	</fieldset>
	<fieldset><legend>Linsabonnemang transaktioner</legend>
	<asp:Repeater ID="rptSettlementTransactionItemsDetailed" runat="server" DataSource='<%#Model.DetailedSubscriptionTransactions%>'>
	<HeaderTemplate>
		<table>
			<tr class="synologen-table-headerrow">	
				<th>Kund</th>
				<th>Belopp</th>
				<th>Datum</th>
			</tr>			
	</HeaderTemplate>
	<ItemTemplate>
			<tr>
				<%--<td><a href="<%# Eval("SubscriptionLink")%>" title="Abonnemang"><%# Eval("CustomerName")%></a></td>--%>
				<td><%# Eval("CustomerName")%></td>
				<td><%# Eval("Amount") %></td>
				<td><%# Eval("Date") %></td>
			</tr>
	</ItemTemplate>	
	<FooterTemplate>
		</table>
	</FooterTemplate>			
	</asp:Repeater>	
	</fieldset>
	</asp:PlaceHolder>			
</div>