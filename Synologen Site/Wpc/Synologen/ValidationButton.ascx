<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ValidationButton.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.ValidationButton" %>

<asp:Button runat="server" Text='<%#ButtonText%>' OnClick="btn_Validate" />
<%if(Model.DisplayValidationForm){ %>
<div class="user-validation-form-container">
	<div class="user-validation-form">
		<asp:PlaceHolder runat="server" ID="plHeader" />
		<asp:PlaceHolder runat="server" ID="plMessagePlaceHolder" />
		<label>Lösenord</label>
		<asp:TextBox ID="txtPassword" runat="server" Text='<%#Model.PasswordText%>' />
		<%if(Model.DisplayValidationFailureMessage) { %>
		<asp:PlaceHolder runat="server" ID="plErrorMessagePlaceHolder" />
		<% } %>
		<div class="control-actions">
			<asp:Button runat="server" Text='<%#ButtonSubmitText%>' OnClick="btn_Submit" />
			<asp:Button runat="server" OnClick="btn_Close" Text="<%#CloseButtonText %>" />
		</div>
		<asp:PlaceHolder runat="server" ID="plFooter" />
	</div>
</div>
<% } %>

