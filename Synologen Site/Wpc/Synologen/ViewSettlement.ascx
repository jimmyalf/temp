<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewSettlement.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ViewSettlement" %>
<div class="synologen-control">
	<fieldset><legend>Utbetalningsuppgifter</legend>
		<label>Utbetalning:</label><span><%=Settlement.Id %></span><br />
		<label>Butiknummer:</label><span><%=MemberShopNumber%></span><br />
		<label>Period:</label><span><%=SettlementPeriodNumber %></span><br />
		<label>Värde inkl moms:</label><span><%=TotalValueIncludingVAT %></span><br />
		<label>Värde exkl moms:</label><span><%=TotalValueExcludingVAT %></span><br />
		<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SettlementListPage %>">&laquo;&nbsp;Tillbaka</a><br /><br />
	</fieldset><br />
	<asp:Button ID="btnSwitchView" runat="server" Text="Visa detaljer" OnClick="btnSwitchView_Click" />
	<asp:Button ID="btnMarkAsPayed" runat="server" Text="Markera som utbetalda" OnClick="btnMarkAsPayed_Click" OnClientClick="return confirm('Är du säker på att du vill markera fakturor som utbetalda?');" />	
	<input type="button" onclick="window.print();return false;" value="Skriv ut"/>
	<br />
	<asp:PlaceHolder ID="plSimpleView" runat="server" Visible="true">
	<asp:Repeater ID="rptSettlementOrderItemsSimple" runat="server">
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
				<td><%# Eval("cArticleNumber") %></td>
				<td><%# Eval("cArticleName") %></td>
				<td><%# Eval("cNumberOfItems") %></td>
				<td><%# Eval("cPriceSummary")%></td>
				<td><%# ((Boolean)Eval("cNoVAT")) ? "Ja" : ""%></td>
			</tr>
	</ItemTemplate>	
	<FooterTemplate>
		</table>
	</FooterTemplate>			
	</asp:Repeater>	
	</asp:PlaceHolder>
	<asp:PlaceHolder ID="plDetailedView" runat="server" Visible="false">
	<asp:Repeater ID="rptSettlementOrderItemsDetailed" runat="server">
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
				<td><%# Eval("cOrderId") %></td>
				<td><%# Eval("cCompany") %></td>
				<td><%# Eval("cArticleNumber") %></td>
				<td><%# Eval("cArticleName") %></td>
				<td><%# Eval("cNumberOfItems") %></td>
				<td><%# Eval("cPriceSummary")%></td>
				<td><%# ((Boolean)Eval("cNoVAT"))? "Ja" : "" %></td>
			</tr>
	</ItemTemplate>	
	<FooterTemplate>
		</table>
	</FooterTemplate>			
	</asp:Repeater>	
	</asp:PlaceHolder>			
</div>