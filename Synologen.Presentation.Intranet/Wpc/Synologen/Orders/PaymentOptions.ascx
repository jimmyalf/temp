<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOptions.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.PaymentOptions" %>

<div id="page" class="step3">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> Emelie Richter</span>
	</header>

    <nav id="tab-navigation">
    	<ul>
    		<li class="completed"><a href="index.html"><span>1</span> Välj Kund</a></li>
    		<li><a href="create_order.html"><span>2</span> Skapa Beställning</a></li>
    		<li class="selected"><span>3</span> Betalningssätt</li>
    		<li><span>4</span> Autogiro Information</li>
    		<li><span>5</span> Bekräfta</li>
    	</ul>
    </nav>
   <div id="tab-container">
      <fieldset>
      <div class="progress">
   			<label>Steg 3 av 5</label>
	 		<div id="progressbar"></div>

   	  </div>
   	  </fieldset>
   	  <fieldset>
      <legend>Leverantörsalternativ</legend>      
      <ul class="radio-list">
          <li>
           <asp:RadioButton id="SupplierOption1" 
               Text="I butik" 
               Checked="False" 
               GroupName="SupplierOptions" 
               runat="server" />
          </li>
          <li>
           <asp:RadioButton id="SupplierOption2" 
               Text="Fakturera" 
               Checked="False" 
               GroupName="SupplierOptions" 
               runat="server" />
          </li>
      </ul>

      </fieldset>
		 <fieldset>  
    <table>
           <caption>Kundens abonnemang</caption>
		    <thead>
			    <tr>
			        <th scope="col"></th>
			        <th scope="col">Referensnr</th>
				    <th scope="col">Saldo</th>
				    <th scope="col">Belopp</th>
				    <th scope="col">Antal dragningar</th>
				    <th scope="col">Status</th>
			    </tr>
		    </thead>
		    <tbody>
		    
			    <tr>
			    <td><input type="checkbox" /></td>
			    <td>3567834</td>
			    <td class="right">+400</td>
			    <td class="right">250</td>
			    <td>3</td>
			    <td>Aktivt</td>
			    </tr>
			    
			    <tr>
			    <td><input type="checkbox" /></td>
			    <td>3567834</td>
			    <td class="right">+400</td>
			    <td class="right">250</td>
			    <td>3</td>
			    <td>Aktivt</td>
			    </tr>
			    
			    <tr>
			    <td><input type="checkbox" /></td>
			    <td>3567834</td>
			    <td class="right">+400</td>
			    <td class="right">250</td>
			    <td>3</td>
			    <td>Aktivt</td>
			    </tr>

		    </tbody>
		    <tfoot>
		    <tr>
		     <td colspan="6"><input type="button" value="Stoppa abonnemang"/> <input type="button" value="Skapa nytt abonnemang"/></td>
		    </tr>
		    </tfoot>
	    </table>
      
      </fieldset>
      
   <fieldset>
    	<div class="next-step"><input type="Submit" value="← Föregående steg"/><input type="Submit" value="Avbryt"/><input type="Submit" value="Nästa steg →"/></div>
    </fieldset>
  </div>
  </div>