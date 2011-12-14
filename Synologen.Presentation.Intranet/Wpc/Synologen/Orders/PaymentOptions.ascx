<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentOptions.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.PaymentOptions" %>

<div id="page" class="step4">
    <header>
		<h1>Linsabonnemang demo</h1>
		<span class="customer-name"><b>Kund:</b> Emelie Richter</span>
	</header>
	<WpcSynologen:OrderMenu runat="server" />
	<div id="tab-container">
      <fieldset>
      <div class="progress">
   			<label>Steg 4 av 6</label>
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