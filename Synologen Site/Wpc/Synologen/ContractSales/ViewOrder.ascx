<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ContractSales.ViewOrder" %>
<div id="synologen-view-order" class="synologen-control">
<asp:PlaceHolder ID="plViewOrder" runat="server" Visible="true">
	<fieldset><legend>Fakturauppgifter</legend>
		<label>Ordernummer:</label><span><%=Order.Id%></span><br />
		<label>Företag:</label><span><asp:Literal ID="ltCompany" runat="server" /></span><br />
		<label>Enhet:</label><span><%=Order.CompanyUnit%></span><br />
		<label>Kostnadsställe:</label><span><%=Order.RstText %></span><br />
		<label>Förnamn:</label><span><%=Order.CustomerFirstName%></span><br />
		<label>Efternamn:</label><span><%=Order.CustomerLastName%></span><br />
		<label>Personnummer:</label><span><%=Order.PersonalIdNumber%></span><br />
		<label>Epost:</label><a href="mailto:<%=Order.Email%>"><%=Order.Email%></a><br />
		<label>Telefon:</label><span><%=Order.Phone%></span><br />
		<label>Försäljare:</label><span><asp:Literal ID="ltSalesPersonName" runat="server" /></span><br />
		<label>Status:</label><span><asp:Literal ID="ltOrderStatus" runat="server" /></span><br />
		<label>Skapad:</label><span><%=Order.CreatedDate.ToShortDateString()%></span><br />
		<label>Status ändrades:</label><span><%=OrderUpdateDate%></span><br />
		<label>Utbetald:</label><span><%=Order.MarkedAsPayedByShop ? "Ja" : "Nej" %></span><br />
	</fieldset>
	<br /><br />	
	<div id="order-items-cart" class="clearLeft">
		<asp:GridView ID="gvOrderItemsCart" 
			runat="server" 
			DataKeyNames="cId" 
			SkinID="Striped" 
			AutoGenerateColumns="false" 
			ShowHeader="true"
			AlternatingRowStyle-CssClass="synologen-table-alternative-row"
			RowStyle-CssClass="synologen-table-row"
			HeaderStyle-CssClass="synologen-table-headerrow"
			CssClass="synologen-table">
			<Columns>
				<asp:BoundField headerText="Artikel" DataField="cArticleName"/>
				<asp:BoundField headerText="Artikelnr" DataField="cArticleNumber"/>
				<asp:BoundField headerText="Styckpris" DataField="cSinglePrice"/>
				<asp:BoundField headerText="Antal" DataField="cNumberOfItems"/>
				<asp:BoundField headerText="Noteringar" DataField="cNotes"/>
			</Columns>
		</asp:GridView>  
	</div>
	<div  class="clearLeft"><label>Totalpris exkl moms: </label><span><asp:Literal id="ltTotalPrice" runat="server" /></span></div>
	<br /><br />
	<div class="control-actions clearLeft">
		<%--<asp:Button ID="btnBack" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />--%>
		<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SalesListPage %>">Tillbaka</a>
		<%--<asp:Button ID="btnMarkAsPayed" runat="server" Text="Markera som utbetald" OnClick="btnMarkAsPayed_Click" ValidationGroup="vldSubmit" OnClientClick="return confirm('Är du säker på att du vill markera ordern som utbetald?');" />--%>
	</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoAccessMessage" runat="server" Visible="false">
<p class="error-message"><strong>!</strong>&nbsp;Du har inte rätt att se vald order.</p><br />
<%--<asp:Button ID="btnErrorBack" runat="server" Text="Tillbaka" OnClick="btnBack_Click" />--%>
<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SalesListPage %>">Tillbaka</a>
</asp:PlaceHolder>
</div>