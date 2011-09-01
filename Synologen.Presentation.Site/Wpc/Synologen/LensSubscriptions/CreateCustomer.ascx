<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreateCustomer.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptions.CreateCustomer" %>
<%if(Model.DisplayForm){%>
<div id="synologen-create-lens-subscription-customer" class="synologen-control">
<fieldset class="synologen-form">
	<legend>Skapa ny kund</legend>
	<p>
		<label for="<%=txtFirstName.ClientID%>">F�rnamn</label>
		<asp:TextBox ID="txtFirstName" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtFirstName" runat="server" ErrorMessage="F�rnamn m�ste anges" ControlToValidate="txtFirstName" Display="Dynamic">*</asp:RequiredFieldValidator>
	</p>
	<p>
		<label for="<%=txtLastName.ClientID%>">Efternamn</label>
		<asp:TextBox ID="txtLastName" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtLastName" runat="server" ErrorMessage="Efternamn m�ste anges" ControlToValidate="txtLastName" Display="Dynamic">*</asp:RequiredFieldValidator>
	</p>
	<p>	
		<label for="<%=txtPersonalIdNumber.ClientID%>">Personnummer</label>
		<asp:TextBox ID="txtPersonalIdNumber" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtPersonalIdNumber" runat="server" ErrorMessage="Personnummer m�ste anges" ControlToValidate="txtPersonalIdNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator ID="regextxtPersonalIdNumber" ValidationExpression="\b(19\d{2}|20\d{2})(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{4}\b" runat="server" ErrorMessage="Personnummer m�ste anges som ����MMDDXXXX" Display="Dynamic" ControlToValidate="txtPersonalIdNumber">*</asp:RegularExpressionValidator>
	</p>
	<p>	
		<label for="<%=txtEmail.ClientID%>">E-post</label>
		<asp:TextBox ID="txtEmail" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtEmail" runat="server" ErrorMessage="E-post m�ste anges" ControlToValidate="txtEmail" Display="Dynamic">*</asp:RequiredFieldValidator>
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
		<asp:RequiredFieldValidator ID="reqtxtAddressLineOne" runat="server" ErrorMessage="Adress 1 m�ste anges" ControlToValidate="txtAddressLineOne" Display="Dynamic">*</asp:RequiredFieldValidator>
	</p>
	<p>	
		<label for="<%=txtAddressLineTwo.ClientID%>">Adress 2</label>
		<asp:TextBox ID="txtAddressLineTwo" runat="server" />
	</p>
	<p>
		<label for="<%=txtCity.ClientID%>">Ort</label>
		<asp:TextBox ID="txtCity" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtCity" runat="server" ErrorMessage="Ort m�ste anges" ControlToValidate="txtCity" Display="Dynamic">*</asp:RequiredFieldValidator>
	</p>
	<p>
		<label for="<%=txtPostalCode.ClientID%>">Postnummer</label>
		<asp:TextBox ID="txtPostalCode" runat="server" />
		<asp:RequiredFieldValidator ID="reqtxtPostalCode" runat="server" ErrorMessage="Postnummer m�ste anges" ControlToValidate="txtPostalCode" Display="Dynamic">*</asp:RequiredFieldValidator>
	</p>
	<p>
		<label for="<%=txtNotes.ClientID%>">Anteckningar</label>
		<asp:TextBox ID="txtNotes" TextMode="MultiLine" runat="server" />
	</p>
	<asp:ValidationSummary ID="vldSummary" runat="server" />	
	
	<div class="control-actions">
		<asp:Button ID="btnSave" runat="server" Text="Spara" />
	</div>

</fieldset>
</div>
<%} %> 
<%if(Model.ShopDoesNotHaveAccessToLensSubscriptions){%>
<p>R�ttighet till linsbest�llning kan inte medges. Var god kontakta systemadministrat�ren.</p>
<%} %>

