<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateOrderConfirmation.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.CreateOrderConfirmation" %>

<div id="page" class="step6">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> <%=Model.CustomerName %></span>
	</header>
	
	
    <WpcSynologen:OrderMenu runat="server" />
	<div id="tab-container">
		<fieldset>
			<div class="progress">
				<label>Steg 6 av 6</label>
				<div id="progressbar" ></div>
			</div>
			<p><label>Förnamn</label><input disabled="disabled" value="<%=Model.FirstName%>" /></p>
			<p><label>Efternamn</label><input disabled="disabled" value="<%=Model.LastName%>" /></p>
			<p><label>Personnummer</label><input disabled="disabled" value="<%=Model.PersonalIdNumber%>" /></p>
			<p><label>E-post</label><input disabled="disabled" value="<%=Model.Email%>" /></p>
			<p><label>Mobiltelefon</label><input disabled="disabled" value="<%=Model.MobilePhone%>" /></p>
			<p><label>Telefon</label><input disabled="disabled" value="<%=Model.Telephone%>" /></p>
			<p><label>Adress</label><input disabled="disabled" value="<%=Model.Address%>" /></p>
			<p><label>Ort</label><input disabled="disabled" value="<%=Model.City%>" /></p>
			<p><label>Postnummer</label><input disabled="disabled" value="<%=Model.PostalCode%>" /></p>
		</fieldset>
		<fieldset class="right-eye">
			<legend>H</legend>
			<p><label>Artikel</label><input disabled="disabled" value="<%=Model.ArticleRight%>" /></p>
			<p><label>Antal</label><input disabled="disabled" value="<%=Model.QuantityRight%>" /></p>
			<p><label>Styrka</label><input disabled="disabled" value="<%=Model.RightPower%>" /></p>
			<p><label>Addition</label><input disabled="disabled" value="<%=Model.RightAddition%>" /></p>
			<p><label>Baskurva</label><input disabled="disabled" value="<%=Model.RightBaseCurve%>" /></p>
			<p><label>Diameter</label><input disabled="disabled" value="<%=Model.RightDiameter%>" /></p>
			<p><label>Cylinder</label><input disabled="disabled" value="<%=Model.RightCylinder%>" /></p>
			<p><label>Axel</label><input disabled="disabled" value="<%=Model.RightAxis%>" /></p>
		</fieldset>
		<fieldset class="left-eye">
			<legend>V</legend>
			<p><label>Artikel</label><input disabled="disabled" value="<%=Model.ArticleLeft%>" /></p>
			<p><label>Antal</label><input disabled="disabled" value="<%=Model.QuantityLeft%>" /></p>
			<p><label>Styrka</label><input disabled="disabled" value="<%=Model.LeftPower%>" /></p>
			<p><label>Addition</label><input disabled="disabled" value="<%=Model.LeftAddition%>" /></p>
			<p><label>Baskurva</label><input disabled="disabled" value="<%=Model.LeftBaseCurve%>" /></p>
			<p><label>Diameter</label><input disabled="disabled" value="<%=Model.LeftDiameter%>" /></p>
			<p><label>Cylinder</label><input disabled="disabled" value="<%=Model.LeftCylinder%>" /></p>
			<p><label>Axel</label><input disabled="disabled" value="<%=Model.LeftAxis%>" /></p>
		</fieldset>
		<fieldset>
			<p><label>Leveranstyp</label><input disabled="disabled" value="<%=Model.DeliveryOption%>"/></p>
			<p><label>Betalningsätt</label><input disabled="disabled" value="Autogiro" /></p>
			<p><label>Produkt</label><input disabled="disabled" value="<%=Model.ProductPrice%>" /></p>
			<p><label>Arvode</label><input disabled="disabled" value="<%=Model.FeePrice%>" /></p>
			<p><label>Totaluttag</label><input disabled="disabled" value="<%=Model.TotalWithdrawal%>" /></p>
			<p><label>Abonnemangstid</label><input disabled="disabled" value="<%=Model.SubscriptionTime%>" /></p>
			<p><label>Månadsbelopp</label><input disabled="disabled" value="<%=Model.Monthly%>" /></p>
			<p title="Förväntat första dragningsdatum"><label>Första dragningsdatum</label><input disabled="disabled" value="<%=Model.ExpectedFirstWithdrawalDate%>" /></p>
		</fieldset>
		<fieldset>
   			<asp:ValidationSummary runat="server" CssClass="error-list" />
			<div class="next-step">
				<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" CausesValidation="False" />
				<asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" CausesValidation="False" OnClientClick="return confirm('Detta avbryter beställningen, är du säker på att du vill avbryta beställningen?');" />
				<asp:Button ID="btnNextStep" runat="server" Text="Bekräfta" />
			</div>
		</fieldset>
	</div>
</div>