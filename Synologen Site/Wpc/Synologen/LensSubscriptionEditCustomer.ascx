<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LensSubscriptionEditCustomer.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.LensSubscriptionEditCustomer" %>
<%if(Model.DisplayForm){%>
<fieldset class="synologen-form">
	<legend>Redigera kund</legend>

	<label for="<%=txtFirstName.ClientID%>">Förnamn</label>
	<asp:TextBox ID="txtFirstName" Text='<%#Model.FirstName %>' runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtFirstName" runat="server" ErrorMessage="Förnamn måste anges" ControlToValidate="txtFirstName" Display="Dynamic">*</asp:RequiredFieldValidator>
	
	<label for="<%=txtLastName.ClientID%>">Efternamn</label>
	<asp:TextBox ID="txtLastName" Text='<%#Model.LastName %>' runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtLastName" runat="server" ErrorMessage="Efternamn måste anges" ControlToValidate="txtLastName" Display="Dynamic">*</asp:RequiredFieldValidator>
	
	<label for="<%=txtPersonalIdNumber.ClientID%>">Personnummer</label>
	<asp:TextBox ID="txtPersonalIdNumber" Text='<%#Model.PersonalIdNumber %>' runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtPersonalIdNumber" runat="server" ErrorMessage="Personnummer måste anges" ControlToValidate="txtPersonalIdNumber" Display="Dynamic">*</asp:RequiredFieldValidator>
	<asp:RegularExpressionValidator ID="regextxtPersonalIdNumber" ValidationExpression="^[0-9]{10}$" runat="server" ErrorMessage="Personnummer måste anges som ÅÅMMDDXXXX" Display="Dynamic" ControlToValidate="txtPersonalIdNumber">*</asp:RegularExpressionValidator>
	
	<label for="<%=txtEmail.ClientID%>">E-post</label>
	<asp:TextBox ID="txtEmail" Text='<%#Model.Email %>' runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtEmail" runat="server" ErrorMessage="E-post måste anges" ControlToValidate="txtEmail" Display="Dynamic">*</asp:RequiredFieldValidator>
	<asp:RegularExpressionValidator ID="regextxtEmail" ValidationExpression="^b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$" runat="server" ErrorMessage="Ogiltig e-post-adress" Display="Dynamic" ControlToValidate="txtEmail">*</asp:RegularExpressionValidator>
		
	<label for="<%=txtMobilePhone.ClientID%>">Mobiltelefon</label>
	<asp:TextBox ID="txtMobilePhone" Text='<%#Model.MobilePhone %>' runat="server" />
	
	<label for="<%=txtPhone.ClientID%>">Telefon</label>
	<asp:TextBox ID="txtPhone" Text='<%#Model.Phone %>' runat="server" />
	
	<label for="<%=txtAddressLineOne.ClientID%>">Adress 1</label>
	<asp:TextBox ID="txtAddressLineOne" Text='<%#Model.AddressLineOne %>' runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtAddressLineOne" runat="server" ErrorMessage="Adress 1 måste anges" ControlToValidate="txtAddressLineOne" Display="Dynamic">*</asp:RequiredFieldValidator>
		
	<label for="<%=txtAddressLineTwo.ClientID%>">Adress 2</label>
	<asp:TextBox ID="txtAddressLineTwo" Text='<%#Model.AddressLineTwo %>' runat="server" />
	
	<label for="<%=txtCity.ClientID%>">Ort</label>
	<asp:TextBox ID="txtCity" Text='<%#Model.City %>' runat="server" />
	
	<label for="<%=txtPostalCode.ClientID%>">Postnummer</label>
	<asp:TextBox ID="txtPostalCode" Text='<%#Model.PostalCode %>' runat="server" />
	<asp:RequiredFieldValidator ID="reqtxtPostalCode" runat="server" ErrorMessage="Postnummer måste anges" ControlToValidate="txtPostalCode" Display="Dynamic">*</asp:RequiredFieldValidator>
	
	<label for="<%=drpCountry.ClientID%>">Land</label>
	<asp:DropDownList ID="drpCountry" runat="server" DataSource='<%#Model.List%>' DataValueField="Value" DataTextField="Text" SelectedItemValue=<%#Model.CountryId %> />

	<asp:ValidationSummary ID="vldSummary" runat="server" />	

	<asp:Button ID="btnSave" runat="server" Text="Spara" />
</fieldset>
<fieldset>
		<legend>Kundens abonnemang</legend>
		<asp:Repeater ID="rptSubscriptions" runat="server" DataSource='<%#Model.Subscriptions%>'>
			<HeaderTemplate >
				<table>
					<tr>
						<th>Skapad</th><th>Status</th><th>Redigera</th>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
					<tr>
						<td><%# Eval("CreatedDate")%></td>
						<td><%# Eval("Status")%></td>
						<td><a href="<%# Eval("EditSubscriptionPageUrl")%>" >Redigera</a></td>
					</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</fieldset>
<%} %> 
<%if(Model.ShopDoesNotHaveAccessToLensSubscriptions){%>
<p>Rättighet till linsbeställning kan inte medges. Var god kontakta systemadministratören.</p>
<%} %>
<%if (Model.ShopDoesNotHaveAccessGivenCustomer)
{%>
<p>Rättighet för att hantera given kund saknas. Var god kontakta systemadministratören.</p>
<%} %>
