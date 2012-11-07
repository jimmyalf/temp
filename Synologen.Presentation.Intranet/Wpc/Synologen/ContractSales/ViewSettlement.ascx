<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSettlement.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.ContractSales.ViewSettlement" %>
<%@ Import Namespace="Spinit.Wpc.Synologen.Presentation.Intranet.Code" %>
<div class="synologen-control">
	<fieldset><legend>Utbetalningsuppgifter</legend>
		<h3>Utbetalning</h3>
		<p><label>Id:</label>&nbsp;<span><%#Model.SettlementId %></span></p>
		<p><label>Butiknummer:</label>&nbsp;<span><%#Model.ShopNumber %></span></p>
		<p><label>Period:</label>&nbsp;<span><%#Model.Period %></span></p>
		<h3>Avtalsförsäljning</h3>
		<p><label>Värde inkl moms:</label>&nbsp;<span><%#Model.ContractSalesValueIncludingVAT %></span></p>
		<h3>Gamla linsabonnemang</h3>
		<p><label>Antal transaktioner:</label>&nbsp;<span><%#Model.OldTransactionsCount %></span></p>
		<p><label>Värde inkl moms:</label>&nbsp;<span><%#Model.OldTransactionsValueIncludingVAT %></span></p>
		<h3>Nya linsabonnemang</h3>
		<p><label>Antal transaktioner:</label>&nbsp;<span><%#Model.NewTransactionCount %></span></p>
		<p><label>Totalt värde:</label>&nbsp;<span><%#Model.NewTransactionsValueIncludingVAT %></span></p>
		<p><label>Momsbelagt värde</label>&nbsp;<span><%#Model.NewTransactionTaxedValue %></span></p>
		<p><label>Momsfritt värde</label>&nbsp;<span><%#Model.NewTransactionTaxFreeValue %></span></p>
		<p><a href="<%=SynologenSessionContext.SettlementListPage %>">&laquo;&nbsp;Tillbaka</a></p>
	</fieldset>
	<div class="control-actions">
		<asp:Button ID="btnSwitchView" runat="server" Text='<%#Model.SwitchViewButtonText%>' OnClick="btnSwitchView_Click" />
		<input type="button" onclick="window.print();return false;" value="Skriv ut"/>
	</div>
	<asp:PlaceHolder ID="plSimpleView" runat="server" Visible='<%#Model.DisplaySimpleView%>'>
	<fieldset><legend>Avtalsförsäljning</legend>
	<asp:Repeater ID="rptSettlementOrderItemsSimple" runat="server" DataSource='<%#Model.SimpleContractSales%>'>
	<HeaderTemplate>
		<table>
			<thead>
				<tr class="synologen-table-headerrow">	
					<th>Artikelnummer</th>
					<th>Artikel</th>
					<th>Antal</th>
					<th>Värde</th>
					<th>Momsfri</th>
				</tr>
			</thead>
			<tbody>
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
			</tbody>
		</table>
	</FooterTemplate>			
	</asp:Repeater>
	<%if(Model.MarkAsPayedButtonEnabled){ %>
		<asp:Button ID="btnMarkAsPayed" runat="server" Text="Markera som utbetalda" OnClick="btnMarkAsPayed_Click" OnClientClick="return confirm('Är du säker på att du vill markera fakturor som utbetalda?');" />
	<%} %>
	</fieldset>
	</asp:PlaceHolder>
	<asp:PlaceHolder ID="plDetailedView" runat="server" Visible='<%#Model.DisplayDetailedView%>'>
	<fieldset><legend>Avtalsförsäljning</legend>
	<asp:Repeater ID="rptSettlementOrderItemsDetailed" runat="server" DataSource='<%#Model.DetailedContractSales%>'>
	<HeaderTemplate>
		<table>
			<thead>
				<tr class="synologen-table-headerrow">
					<th>Order Nr</th>
					<th>Avtalsföretag</th>
					<th>Artikelnummer</th>
					<th>Artikel</th>
					<th>Antal</th>
					<th>Värde</th>
					<th>Momsfri</th>
				</tr>
			</thead>
			<tbody>
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
			</tbody>
		</table>
	</FooterTemplate>			
	</asp:Repeater>	
	</fieldset>
	</asp:PlaceHolder>			
	<fieldset><legend>Gamla linsabonnemang transaktioner</legend>
	<asp:Repeater ID="rptSettlementTransactionItemsDetailed" runat="server" DataSource='<%#Model.OldTransactions%>'>
	<HeaderTemplate>
		<table>
			<thead>
				<tr class="synologen-table-headerrow">	
					<th>Kund</th>
					<th>Belopp</th>
					<th>Datum</th>
				</tr>
			</thead>
			<tbody>
	</HeaderTemplate>
	<ItemTemplate>
				<tr>
					<td><%# Eval("CustomerName")%></td>
					<td><%# Eval("Amount") %></td>
					<td><%# Eval("Date") %></td>
				</tr>
	</ItemTemplate>	
	<FooterTemplate>
			</tbody>		
		</table>
	</FooterTemplate>			
	</asp:Repeater>	
	</fieldset>
	<fieldset><legend>Nya linsabonnemang transaktioner</legend>
	<asp:Repeater runat="server" DataSource='<%#Model.NewTransactions%>'>
	<HeaderTemplate>
		<table>
			<thead>
				<tr class="synologen-table-headerrow">	
					<th>Kund</th>
					<th>Belopp</th>
					<th>Momsbelagt</th>
					<th>Momsfritt</th>
					<th>Datum</th>
				</tr>			
			</thead>
			<tbody>
	</HeaderTemplate>
	<ItemTemplate>
				<tr>
					<td><%# Eval("CustomerName")%></td>
					<td><%# Eval("Amount") %></td>
					<td><%# Eval("TaxedAmount") %></td>
					<td><%# Eval("TaxFreeAmount") %></td>
					<td><%# Eval("Date") %></td>
				</tr>
	</ItemTemplate>	
	<FooterTemplate>
			</tbody>
		</table>
	</FooterTemplate>			
	</asp:Repeater>	
	</fieldset>
</div>