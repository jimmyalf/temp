<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.ContractSales.SalesList" %>
<div class="synologen-control">
<asp:Repeater ID="rptSales" runat="server" OnItemDataBound="rptSales_ItemDataBound" >
<HeaderTemplate>
<table id="synologen-sales-list" >
<tr class="synologen-table-headerrow">
	<th>Order Nr</th>
	<th>Företag</th>
	<th>Kund</th>
	<th>Status</th>
	<th>Registrerad</th>
	<th>Utbetald</th>	
	<th>Visa</th>
</tr>
</HeaderTemplate>
<ItemTemplate>
<tr class="synologen-table-row">
	<td class="center-cell"><%# DataBinder.Eval(Container.DataItem, "cId") %></td>
	<td><%# DataBinder.Eval(Container.DataItem, "cCompanyName") %></td>
	<td><%# DataBinder.Eval(Container.DataItem, "cCustomerName") %></td>
	<td><%# DataBinder.Eval(Container.DataItem, "cStatusName") %></td>
	<td><%# DataBinder.Eval(Container.DataItem, "cCreatedDate","{0:yyyy-MM-dd}") %></td>
	<td class="center-cell"><asp:PlaceHolder ID="plPayed" runat="server"><img src="/Wpc/Synologen/Images/Ok.png" alt="Utbetald" title="Utbetald" /></asp:PlaceHolder></td>	
	<td class="center-cell">
	<asp:PlaceHolder ID="plEditLink" runat="server"><a href="<%=EditOrderPage%>?id=<%# DataBinder.Eval(Container.DataItem, "cId") %>"><img src="/Wpc/Synologen/Images/Edit.png" alt="Redigera" title="Redigera" /></a></asp:PlaceHolder>
	<asp:PlaceHolder ID="plViewLink" runat="server"><a href="<%=ViewOrderPage%>?id=<%# DataBinder.Eval(Container.DataItem, "cId") %>"><img src="/Wpc/Synologen/Images/View.png" alt="Visa" title="Visa" /></a></asp:PlaceHolder>
	</td>	
</tr>
</ItemTemplate>
<AlternatingItemTemplate>
<tr class="synologen-table-alternative-row">
	<td class="center-cell"><%# DataBinder.Eval(Container.DataItem, "cId") %></td>	
	<td><%# DataBinder.Eval(Container.DataItem, "cCompanyName") %></td>
	<td><%# DataBinder.Eval(Container.DataItem, "cCustomerName") %></td>
	<td><%# DataBinder.Eval(Container.DataItem, "cStatusName") %></td>
	<td><%# DataBinder.Eval(Container.DataItem, "cCreatedDate","{0:yyyy-MM-dd}") %></td>
	<td class="center-cell"><asp:PlaceHolder ID="plPayedAlternative" runat="server"><img src="/Wpc/Synologen/Images/Ok.png" alt="Utbetald" title="Utbetald" /></asp:PlaceHolder></td>
	<td class="center-cell">
	<asp:PlaceHolder ID="plEditLinkAlternative" runat="server"><a href="<%=EditOrderPage%>?id=<%# DataBinder.Eval(Container.DataItem, "cId") %>"><img src="/Wpc/Synologen/Images/Edit.png" alt="Redigera" title="Redigera" /></a></asp:PlaceHolder>
	<asp:PlaceHolder ID="plViewLinkAlternative" runat="server"><a href="<%=ViewOrderPage%>?id=<%# DataBinder.Eval(Container.DataItem, "cId") %>"><img src="/Wpc/Synologen/Images/View.png" alt="Visa" title="Visa" /></a></asp:PlaceHolder>
	</td>
</tr>
</AlternatingItemTemplate>
<FooterTemplate></table></FooterTemplate>
</asp:Repeater>
</div>