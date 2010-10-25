<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LensSubscriptionCreateCustomer.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptionCreateCustomer" %>
<%if(Model.DisplayForm){%>
<fieldset class="synologen-form">
	<legend>Skapa ny kund</legend>

	<label for="<%=txtFirstName.ClientID%>">Förnamn</label>
	<asp:TextBox ID="txtFirstName" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtFirstName" runat="server" ErrorMessage="Förnamn måste anges" ControlToValidate="txtFirstName" Display="Dynamic">*</asp:RequiredFieldValidator>
	
	<label for="<%=txtLastName.ClientID%>">Efternamn</label>
	<asp:TextBox ID="txtLastName" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtLastName" runat="server" ErrorMessage="Efternamn måste anges" ControlToValidate="txtLastName" Display="Dynamic">*</asp:RequiredFieldValidator>
	
	<label for="<%=txtPersonalIdNumber.ClientID%>">Personnummer</label>
	<asp:TextBox ID="txtPersonalIdNumber" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtPersonalIdNumber" runat="server" ErrorMessage="Personnummer måste anges" ControlToValidate="txtPersonalIdNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
	<asp:RegularExpressionValidator ID="regextxtPersonalIdNumber" ValidationExpression="^[0-9]{10}$" runat="server" ErrorMessage="Personnummer måste anges som ÅÅMMDDXXXX" Display="Dynamic" ControlToValidate="txtPersonalIdNumber">*</asp:RegularExpressionValidator>
	
	<label for="<%=txtEmail.ClientID%>">E-post</label>
	<asp:TextBox ID="txtEmail" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtEmail" runat="server" ErrorMessage="E-post måste anges" ControlToValidate="txtEmail" Display="Dynamic">*</asp:RequiredFieldValidator>
		
	<label for="<%=txtMobilePhone.ClientID%>">Mobiltelefon</label>
	<asp:TextBox ID="txtMobilePhone" runat="server" />
	
	<label for="<%=txtPhone.ClientID%>">Telefon</label>
	<asp:TextBox ID="txtPhone" runat="server" />
	
	<label for="<%=txtAddressLineOne.ClientID%>">Adress 1</label>
	<asp:TextBox ID="txtAddressLineOne" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtAddressLineOne" runat="server" ErrorMessage="Adress 1 måste anges" ControlToValidate="txtAddressLineOne" Display="Dynamic">*</asp:RequiredFieldValidator>
		
	<label for="<%=txtAddressLineTwo.ClientID%>">Adress 2</label>
	<asp:TextBox ID="txtAddressLineTwo" runat="server" />
	
	<label for="<%=txtCity.ClientID%>">Ort</label>
	<asp:TextBox ID="txtCity" runat="server" />
	
	<label for="<%=txtPostalCode.ClientID%>">Postnummer</label>
	<asp:TextBox ID="txtPostalCode" runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtPostalCode" runat="server" ErrorMessage="Postnummer måste anges" ControlToValidate="txtPostalCode" Display="Dynamic">*</asp:RequiredFieldValidator>
	
	<label for="<%=drpCountry.ClientID%>">Land</label>
	<asp:DropDownList ID="drpCountry" runat="server" DataSource='<%#Model.List%>' DataValueField="Value" DataTextField="Text" />

	<asp:ValidationSummary ID="vldSummary" runat="server" />	

	<asp:Button ID="btnSave" runat="server" Text="Spara" />

</fieldset>
<%} %> 
<%if(Model.ShopDoesNotHaveAccessToLensSubscriptions){%>
<p>Rättighet till linsbeställning kan inte medges. Var god kontakta systemadministratören.</p>
<%} %>

