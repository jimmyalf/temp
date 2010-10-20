<%@ Control Language="C#" CodeBehind="LensSubscriptionCustomersList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptionCustomersList" %>
<asp:Repeater ID="rptCustomers" runat="server" DataSource='<%#Model.List%>'>
	<HeaderTemplate >
		<table>
			<tr>
				<th>Förnamn</th><th>Efternamn</th><th>Personnummer</th>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td><%# Eval("FirstName")%></td>
			<td><%# Eval("LastName")%></td>
			<td><%# Eval("PersonalIdNumber")%></td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:Repeater>