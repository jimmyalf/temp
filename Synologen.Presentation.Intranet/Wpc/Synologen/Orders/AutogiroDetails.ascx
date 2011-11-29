<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutogiroDetails.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.AutogiroDetails" %>

<div id="page" class="step4">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> Emelie Richter</span>
	</header>

    <nav id="tab-navigation">
    	<ul>
    		<li class="completed"><a href="index.html"><span>1</span> Välj Kund</a></li>
    		<li><a href="create_order.html"><span>2</span> Skapa Beställning</a></li>
    		<li><a href="payment.html"><span>3</span> Betalningssätt</a></li>
    		<li class="selected"><span>4</span> Autogiro Information</li>
    		<li><span>5</span> Bekräfta</li>
    	</ul>
    </nav>
   <div id="tab-container">
   
      <fieldset>
      <div class="progress">
   			<label>Steg 4 av 5</label>
	 		<div id="progressbar"></div>
   	    </div>
    	<p><label>Bankontonummer</label><input type="text" /></p>
    	<p><label>Clearingnummer</label><input type="text" /></p>
    	<p><label>Abonnemangstid</label><input type="text" /></p>
    	<div>
      	<label>Leverantörsalternativ</label>
     	<ul class="radio-list">
      	<li><input type="radio" />3 månader</li>
      	<li><input type="radio" />6 månader</li>
      	<li><input type="radio" />12 månader</li>
      	<li class="inactive"><input type="radio" disabled="disabled" />Löpande</li>
      	<li class="inactive"><input type="radio" disabled="disabled" />Valfritt <input type="text" class="align-right" disabled="disabled" /></li>
      	</ul>
      	</div>
    	
    	<p><label>Momsbelopp</label><input type="email" /></p>
    	<p><label>Momsfritt belopp</label><input type="text"></p>
    	<p><label>Vald artikel</label><input type="text" value="Linear A554" /></p>
    	<p><label>Totaluttag</label><input type="text" /></p>

    	<div class="next-step"><input type="Submit" value="← Föregående steg"/><input type="Submit" value="Avbryt"/><input type="Submit" value="Nästa steg →"/></div>
    </fieldset>
   
    </div>
  </div>