<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewOrder.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ContractSales.ViewOrder" %>
<div id="synologen-view-order" class="synologen-control">
<asp:PlaceHolder ID="plViewOrder" runat="server" Visible="true">
	<fieldset><legend>Fakturauppgifter</legend>
		<p><label>Ordernummer:</label><span><%=Order.Id%></span></p>
		<p><label>Företag:</label><span><asp:Literal ID="ltCompany" runat="server" /></span></p>
		<p><label>Enhet:</label><span><%=Order.CompanyUnit%></span></p>
		<p><label>Kostnadsställe:</label><span><%=Order.RstText %></span></p>
		<p><label>Förnamn:</label><span><%=Order.CustomerFirstName%></span></p>
		<p><label>Efternamn:</label><span><%=Order.CustomerLastName%></span></p>
		<p><label>Personnummer:</label><span><%=Order.PersonalIdNumber%></span></p>
		<p><label>Epost:</label><a href="mailto:<%=Order.Email%>"><%=Order.Email%></a></p>
		<p><label>Telefon:</label><span><%=Order.Phone%></span></p>
		<p><label>Försäljare:</label><span><asp:Literal ID="ltSalesPersonName" runat="server" /></span></p>
		<p><label>Status:</label><span><asp:Literal ID="ltOrderStatus" runat="server" /></span></p>
		<p><label>Skapad:</label><span><%=Order.CreatedDate.ToShortDateString()%></span></p>
		<p><label>Status ändrades:</label><span><%=OrderUpdateDate%></span></p>
		<p><label>Utbetald:</label><span><%=Order.MarkedAsPayedByShop ? "Ja" : "Nej" %></span></p>
	</fieldset>
	<br /><br />	
	<div id="order-items-cart" class="clearLeft">
		<asp:GridView ID="gvOrderItemsCart" 
			runat="server" 
			DataKeyNames="cId" 
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
		<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SalesListPage %>">Tillbaka</a>
	</div>
</asp:PlaceHolder>
<asp:PlaceHolder ID="plNoAccessMessage" runat="server" Visible="false">
<p class="error-message"><strong>!</strong>&nbsp;Du har inte rätt att se vald order.</p><br />
<a href="<%=Spinit.Wpc.Synologen.Presentation.Site.Code.SynologenSessionContext.SalesListPage %>">Tillbaka</a>
</asp:PlaceHolder>
</div>