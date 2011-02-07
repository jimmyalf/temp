<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ErrorList.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions.ErrorList" %>
<fieldset>
	<legend>Autogiro fel-lista</legend>
	<% if (Model.HasErrors) {%>
	<asp:Repeater ID="rptErrors" OnItemCommand="SetHandled_ItemCommand" runat="server" DataSource='<%#Model.List%>'>
		<HeaderTemplate >
			<table>
				<tr class="synologen-table-headerrow">
						<th>Datum</th><th>Typ av fel</th><th>Hanterad</th><th>Markera som hanterad</th>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
				<tr>
					<td><%# Eval("CreatedDate")%></td>
					<td><%# Eval("TypeName")%></td>
					<td><%# Eval("HandledDate")%></td>
					<td><asp:Button ID="btnSetHandled" CommandName='<%# Eval("ErrorId")%>'  Visible=<%# Eval("IsVisible")%> runat="server" Text="Markera som hanterad" OnClientClick="return confirm('Är du säker på att du vill markera felet som hanterat?');"  /></td>
				</tr>
		</ItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>
	<% } 
		else { %><p>Det finns inga fel att visa.</p><%
	} %>
</fieldset>