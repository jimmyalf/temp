<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateOrderConfirmation.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.CreateOrderConfirmation" %>

<div id="page" class="step6">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> Emelie Richter</span>
	</header>
	
    <WpcSynologen:OrderMenu runat="server" />
	<div id="tab-container">
      <fieldset>
      <div class="progress">
   			<label>Steg 6 av 6</label>
	 		<div id="progressbar"></div>
   	  </div>

        <p><label>Förnamn</label><asp:TextBox ID="TextBox" runat="server" Enabled="false" Text='<%#Model.FirstName%>' /></p>
    	<p><label>Efternamn</label><asp:TextBox ID="TextBox0" runat="server" Enabled="false" Text='<%#Model.LastName%>' /></p>
    	<p><label>Personnummer</label><asp:TextBox ID="TextBox1" runat="server" Enabled="false" Text='<%#Model.PersonalIdNumber%>' /></p>
    	<p><label>E-post</label><asp:TextBox ID="TextBox2" runat="server" Enabled="false" Text='<%#Model.Email%>' /></p>
    	<p><label>Mobiltelefon</label><asp:TextBox ID="TextBox3" runat="server" Enabled="false" Text='<%#Model.MobilePhone%>' /></p>
    	<p><label>Telefon</label><asp:TextBox ID="TextBox4" runat="server" Enabled="false" Text='<%#Model.Telephone%>' /></p>
    	<p><label>Adress</label><asp:TextBox ID="TextBox5" runat="server" Enabled="false" Text='<%#Model.Address%>' /></p>
    	<p><label>Ort</label><asp:TextBox ID="TextBox6" runat="server" Enabled="false" Text='<%#Model.City%>' /></p>
    	<p><label>Postnummer</label><asp:TextBox ID="TextBox7" runat="server" Enabled="false" Text='<%#Model.PostalCode%>' /></p>
      	<p><label>Artikel/Artiklar</label><asp:TextBox ID="TextBox8" runat="server" Enabled="false" Text='<%#Model.Article%>' /></p>
     	
      </fieldset>

      <fieldset class="right-eye">
      <legend>H</legend>
        <p><label>Styrka</label><asp:TextBox ID="TextBox15" runat="server" Enabled="false" Text='<%#Model.RightPower%>' /></p>
    	<p><label>Addition</label><asp:TextBox ID="TextBox16" runat="server" Enabled="false" Text='<%#Model.RightAddition%>' /></p>
        <p><label>Baskurva</label><asp:TextBox ID="TextBox17" runat="server" Enabled="false" Text='<%#Model.RightBaseCurve%>' /></p>
    	<p><label>Diameter</label><asp:TextBox ID="TextBox18" runat="server" Enabled="false" Text='<%#Model.RightDiameter%>' /></p>
        <p><label>Axel</label><asp:TextBox ID="TextBox19" runat="server" Enabled="false" Text='<%#Model.RightAxis%>' /></p>
        <p><label>Cylinder</label><asp:TextBox ID="TextBox20" runat="server" Enabled="false" Text='<%#Model.RightCylinder%>' /></p>
      </fieldset>

      <fieldset class="left-eye">
      <legend>V</legend>
        <p><label>Styrka</label><asp:TextBox ID="TextBox9" runat="server" Enabled="false" Text='<%#Model.LeftPower%>' /></p>
    	<p><label>Addition</label><asp:TextBox ID="TextBox12" runat="server" Enabled="false" Text='<%#Model.LeftAddition%>' /></p>
        <p><label>Baskurva</label><asp:TextBox ID="TextBox10" runat="server" Enabled="false" Text='<%#Model.LeftBaseCurve%>' /></p>
    	<p><label>Diameter</label><asp:TextBox ID="TextBox11" runat="server" Enabled="false" Text='<%#Model.LeftDiameter%>' /></p>
        <p><label>Axel</label><asp:TextBox ID="TextBox13" runat="server" Enabled="false" Text='<%#Model.LeftAxis%>' /></p>
        <p><label>Cylinder</label><asp:TextBox ID="TextBox14" runat="server" Enabled="false" Text='<%#Model.LeftCylinder%>' /></p>
      </fieldset>
      
      
      
      <fieldset>
    	<p><label>Leveranstyp</label><asp:TextBox ID="TextBox21" runat="server" Enabled="false" Text='<%#Model.DeliveryOption%>' /></p>
    	<p><label>Betalningsätt</label><asp:TextBox ID="TextBox22" runat="server" Enabled="false" Text='Autogiro' /></p>
        <p><label>Momsbelopp</label><asp:TextBox ID="TextBox23" runat="server" Enabled="false" Text='<%#Model.TaxedAmount%>' /></p>
        <p><label>Momsfritt belopp</label><asp:TextBox ID="TextBox25" runat="server" Enabled="false" Text='<%#Model.TaxfreeAmount%>' /></p>
        <p><label>Månadsbelopp</label><asp:TextBox ID="TextBox26" runat="server" Enabled="false" Text='<%#Model.Monthly%>' /></p>
        <p><label>Abonnemangstid</label><asp:TextBox ID="TextBox24" runat="server" Enabled="false" Text='<%#Model.SubscriptionTime%>' /></p>
        <p><label>Totaluttag</label><asp:TextBox ID="TextBox27" runat="server" Enabled="false" Text='<%#Model.TotalWithdrawal%>' /></p>
      </fieldset>
      
   <fieldset>
   		<asp:ValidationSummary runat="server" CssClass="error-list" />
		<div class="next-step">
			<asp:Button ID="btnPreviousStep" runat="server" Text="← Föregående steg" CausesValidation="False" />
			<asp:Button ID="btnCancel" Text="Avbryt" runat="server" CssClass="cancel-button" CausesValidation="False" OnClientClick="return confirm('Detta avbryter beställningen, är du säker på att du vill avbryta beställningen?');" />
			<asp:Button ID="btnNextStep" runat="server" Text="Skicka" />
		</div>
    </fieldset>
  </div>
  </div>