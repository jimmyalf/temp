<%@ Control Language="C#" CodeBehind="FrameOrderList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.FrameOrderList" %>
<%if(Model.DisplayList){ %>
<asp:Repeater ID="rptFrameOrders" runat="server" DataSource='<%#Model.List%>'>
<HeaderTemplate>
<div class="synologen-control">
	<table>
		<tr class="synologen-table-headerrow">
			<th>Beställningsnr</th>
			<th>Båge</th>
			<th>Skickad</th>
			<th>Visa</th>
		</tr>
</HeaderTemplate>
<ItemTemplate>
<tr>
	<td><%# Eval("Id")%></td>
	<td><%# Eval("FrameName")%></td>
	<td><%# Eval("Sent")%></td>
	<td><a href="<%# Model.ViewPageUrl%>?frameorder=<%# Eval("Id")%>" title="Visa beställning">Visa</a></td>
</tr>
</ItemTemplate>
<FooterTemplate>
	</table>
</div>
</FooterTemplate>
</asp:Repeater>
<%} %>
<%if(Model.ShopDoesNotHaveAccessToFrameOrders){ %>
<p>Rättighet till beställningslista kan inte medges. Var god kontakta systemadministratören.</p>
<%} %>

