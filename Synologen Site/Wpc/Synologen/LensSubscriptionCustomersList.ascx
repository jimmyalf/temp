<%@ Control Language="C#" CodeBehind="LensSubscriptionCustomersList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptionCustomersList" %>
<div id="synologen-create-lens-subscription-customer-list" class="synologen-control">
<label for="<%=txtSearch.ClientID%>">Sök</label>
<asp:TextBox ID="txtSearch" runat="server" />
<asp:Button ID="btnSearch" runat="server" Text="Sök" />
<asp:Repeater ID="rptCustomers" runat="server" DataSource='<%#Model.List%>'>
	<HeaderTemplate >
		<table>
			<tr class="synologen-table-headerrow">
				<th>Förnamn</th><th>Efternamn</th><th>Personnummer</th><th>Redigera</th>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
			<tr>
				<td><%# Eval("FirstName")%></td>
				<td><%# Eval("LastName")%></td>
				<td><%# Eval("PersonalIdNumber")%></td>
				<td><a href="<%# Eval("EditPageUrl")%>" >Redigera</a></td>
			</tr>
	</ItemTemplate>
	<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:Repeater>
</div>