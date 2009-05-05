<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SettlementList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.SettlementList" %>
<asp:Repeater ID="rptSettlements" runat="server">
<HeaderTemplate>
<div class="synologen-control">
	<table>
		<tr class="synologen-table-headerrow"><th>Butik</th><th>Utbetalning</th></tr>
</HeaderTemplate>
<ItemTemplate>
<tr><td><%=ShopNumber %></td><td><a href="<%=Spinit.Wpc.Synologen.Business.Globals.ViewSettlementPage%>?settlementId=<%# DataBinder.Eval(Container.DataItem, "cId") %>" title="2009-02-25">Period <%# GetSettlementPeriodNumber((System.Data.DataRowView)Container.DataItem)%></a></td></tr>
</ItemTemplate>
<FooterTemplate>
	</table>
</div>	
</FooterTemplate>
</asp:Repeater>