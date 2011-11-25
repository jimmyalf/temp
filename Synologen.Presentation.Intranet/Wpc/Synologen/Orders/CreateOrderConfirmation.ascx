<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateOrderConfirmation.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.CreateOrderConfirmation" %>

<div id="page" class="step5">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> Emelie Richter</span>
	</header>

    <nav id="tab-navigation">
    	<ul>
    		<li class="completed"><a href="index.html"><span>1</span> Välj Kund</a></li>
    		<li><a href="create_order.html"><span>2</span> Skapa Beställning</a></li>
    		<li><a href="payment.html"><span>3</span> Betalningssätt</a></li>
    		<li><a href="account_information.html"><span>4</span> Autogiro Information</a></li>
    		<li class="selected"><span>5</span> Bekräfta</li>
    	</ul>
    </nav>
   <div id="tab-container">
      <fieldset>
      <div class="progress">
   			<label>Steg 5 av 5</label>
	 		<div id="progressbar"></div>
   	  </div>

        <p><label>Förnamn</label><input type="text" value="Emelie" disabled="disabled" /></p>
    	<p><label>Efternamn</label><input type="text" value="Richter" disabled="disabled" /></p>
    	<p><label>Personnummer</label><input type="text" value="8907091234" disabled="disabled" /></p>
    	<p><label>E-post</label><input type="text" value="emelie@richter.se" disabled="disabled" /></p>
    	<p><label>Mobiltelefon</label><input type="text" value="0703-456 123" disabled="disabled" /></p>
    	<p><label>Telefon</label><input type="text" value="031-456 123" disabled="disabled"/></p>
    	<p><label>Adress</label><input type="text" value="Lindholsmgatan 3" disabled="disabled"/></p>
    	<p><label>Ort</label><input type="text" value="Göteborg" disabled="disabled"/></p>
    	<p><label>Postnummer</label><input type="text" value="415 18" disabled="disabled"/></p>
    	<div>
      	<label>Artiklel/ artiklar</label>
     	<ul class="radio-list">
      	<li>Synundersökning</li>
      	<li>Linser</li>
      	<li>Linsvätska</li>
      	</ul>
      	</div>
      </fieldset>
      <fieldset class="left-eye">
      <legend>V</legend>
        <p><label>Styrka</label><input type="text" value="-7,5" disabled="disabled"/></p>
    	<p><label>Baskurva</label><input type="text" value="8,5" disabled="disabled"/></p>
    	<p><label>Diometer</label><input type="text" value="-14" disabled="disabled"/></p>
      </fieldset>
      
      <fieldset class="right-eye">
      <legend>H</legend>
        <p><label>Styrka</label><input type="text" value="-7,5" disabled="disabled"/></p>
    	<p><label>Baskurva</label><input type="text" value="8,5" disabled="disabled"/></p>
    	<p><label>Diometer</label><input type="text" value="-14" disabled="disabled"/></p>
      </fieldset>
      
      <fieldset>
    	<p><label>Leveranstyp</label><input type="text" value="Till butik" disabled="disabled"/></p>
    	<p><label>Betalningsätt</label><input type="text" value="Abonnemang" disabled="disabled"/></p>
        <p><label>Belopp</label><input type="text" value="3000 kr" disabled="disabled"/></p>
        <p><label>Abonnemangstid</label><input type="text" value="6 månader" disabled="disabled"/></p>
      </fieldset>
      
   <fieldset>
    	<div class="next-step"><input type="Submit" value="← Föregående steg"/><input class="cancel-button" type="Submit" value="Avbryt"/><input type="Submit" class="confirm-button" value="Bekräfta"/></div>
    </fieldset>
  </div>
  </div>