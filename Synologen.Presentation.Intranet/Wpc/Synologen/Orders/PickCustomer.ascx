<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PickCustomer.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Orders.PickCustomer" %>
	
<div id="page">
    <div>
		<h1>Linsabonnemang demo</h1>
	</div>

    <nav id="tab-navigation">
    	<ul>
    		<li class="selected"><a href="index.html"><span>1</span> Välj Kund</a></li>
    		<li><span>2</span> Skapa Beställning</li>
    		<li><span>3</span> Betalningssätt</li>
    		<li><span>4</span> Autogiro Information</li>
    		<li><span>5</span> Bekräfta</li>
    	</ul>
    </nav>
   <div id="tab-container">

   	<fieldset>
   		<div class="progress">
   	    	<label>Steg 1 av 5</label>
					<div id="progressbar"></div>
   	    </div>
    	<p>
            <label for="<%=txtFirstName.ClientID%>">Förnamn</label>
		    <asp:TextBox ID="txtFirstName" runat="server" />
		    <asp:RequiredFieldValidator ID="reqtxtFirstName" runat="server" ErrorMessage="Förnamn måste anges" ControlToValidate="txtFirstName" Display="Dynamic">*</asp:RequiredFieldValidator>
        </p>
    	<p>
            <label for="<%=txtLastName.ClientID%>">Efternamn</label>
		    <asp:TextBox ID="txtLastName" runat="server" />
		    <asp:RequiredFieldValidator ID="reqtxtLastName" runat="server" ErrorMessage="Efternamn måste anges" ControlToValidate="txtLastName" Display="Dynamic">*</asp:RequiredFieldValidator>
        </p>
    	<p>
        
            <label for="<%=txtPersonalIdNumber.ClientID%>">Personnummer</label>
            <span>
		    <asp:TextBox ID="txtPersonalIdNumber" runat="server" />
		    <asp:RequiredFieldValidator ID="reqtxtPersonalIdNumber" runat="server" ErrorMessage="Personnummer måste anges" ControlToValidate="txtPersonalIdNumber" Display="Dynamic" ValidationGroup="PersonalIdNumberValidationGroup">*</asp:RequiredFieldValidator>
		    <asp:RegularExpressionValidator ID="regextxtPersonalIdNumber" ValidationExpression="\b(19\d{2}|20\d{2})(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{4}\b" runat="server" ErrorMessage="Personnummer måste anges som ÅÅÅÅMMDDXXXX" Display="Dynamic" ControlToValidate="txtPersonalIdNumber" ValidationGroup="PersonalIdNumberValidationGroup">*</asp:RegularExpressionValidator>
            <asp:Button ID="btnFetchByPersonalIdNumber" runat="server" Text="Hämta →" />
            </span>
	    
        </p>
    	<p>
            <label for="<%=txtEmail.ClientID%>">E-post</label>
		    <asp:TextBox ID="txtEmail" runat="server" />
		    <asp:RequiredFieldValidator ID="reqtxtEmail" runat="server" ErrorMessage="E-post måste anges" ControlToValidate="txtEmail" Display="Dynamic">*</asp:RequiredFieldValidator>
		    <asp:RegularExpressionValidator ID="regextxtEmail" ValidationExpression="^[A-Za-z0-9._%-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$" runat="server" ErrorMessage="Ogiltig e-post-adress" Display="Dynamic" ControlToValidate="txtEmail">*</asp:RegularExpressionValidator>	
        </p>
    	<p>
            <label for="<%=txtMobilePhone.ClientID%>">Mobiltelefon</label>
		    <asp:TextBox ID="txtMobilePhone" runat="server" />
        </p>
    	<p>
            <label for="<%=txtPhone.ClientID%>">Telefon</label>
		    <asp:TextBox ID="txtPhone" runat="server" />
        </p>
    	<p>
            <label for="<%=txtAddressLineOne.ClientID%>">Adress 1</label>
		    <asp:TextBox ID="txtAddressLineOne" runat="server" />
		    <asp:RequiredFieldValidator ID="reqtxtAddressLineOne" runat="server" ErrorMessage="Adress 1 måste anges" ControlToValidate="txtAddressLineOne" Display="Dynamic">*</asp:RequiredFieldValidator>
        </p>
    	<p>
            <label for="<%=txtAddressLineTwo.ClientID%>">Adress 2</label>
		    <asp:TextBox ID="txtAddressLineTwo" runat="server" />
        </p>
    	<p>
            <label for="<%=txtCity.ClientID%>">Ort</label>
		    <asp:TextBox ID="txtCity" runat="server" />
		    <asp:RequiredFieldValidator ID="reqtxtCity" runat="server" ErrorMessage="Ort måste anges" ControlToValidate="txtCity" Display="Dynamic">*</asp:RequiredFieldValidator>
        </p>
    	<p>
            <label for="<%=txtPostalCode.ClientID%>">Postnummer</label>
		    <asp:TextBox ID="txtPostalCode" runat="server" />
		    <asp:RequiredFieldValidator ID="reqtxtPostalCode" runat="server" ErrorMessage="Postnummer måste anges" ControlToValidate="txtPostalCode" Display="Dynamic">*</asp:RequiredFieldValidator>
        </p>
    	<p>
            <label for="<%=txtNotes.ClientID%>">Anteckningar</label>
		    <asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" />
        </p>

        <asp:ValidationSummary ID="vldSummary" runat="server" />

    	<div class="next-step">
            <div class="control-actions">
                <asp:Button ID="btnCancel" value="Avbryt" CssClass="cancel-button" />
		        <asp:Button ID="btnNextStep" runat="server" Text="Nästa steg →" />
                
	        </div>
        </div>
    </fieldset>

  </div>
  </div>


