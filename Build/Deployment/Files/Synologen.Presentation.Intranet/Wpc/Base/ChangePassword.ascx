<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Site.ChangePassword" %>

<spinit:MessageManager ID="messageManager" runat="server" UniqueClientID="Base-MessageManager-ChangePassword" CaptionElement="h4" />
<asp:ValidationSummary CssClass="negative" ID="vsChangePassword" runat="server" ValidationGroup="ChangePassword" />

<fieldset>
    <legend>�ndra ditt l�senord</legend>
    
    <p class="form-item">
        <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" SkinId="Long" CssClass="long"/>
		<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" SkinID="Wide"/>
        <asp:Label ID="lblNewPassword" runat="server" AssociatedControlID="txtNewPassword" SkinId="Long" CssClass="long"/>
		<asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" SkinID="Wide"/>
        <asp:Label ID="lblConfirmNewPassword" runat="server" AssociatedControlID="txtConfirmNewPassword" SkinId="Long" CssClass="long"/>
		<asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password" SkinID="Wide"/>
		
		<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Nuvarande l�senord �r obligatoriskt" ValidationGroup="ChangePassword" ControlToValidate="txtPassword" CssClass="error" ForeColor="White" Display="None"></asp:RequiredFieldValidator>
		<asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ErrorMessage="Nytt l�senord �r obligatoriskt" ValidationGroup="ChangePassword" ControlToValidate="txtNewPassword" CssClass="error" ForeColor="White" Display="None"></asp:RequiredFieldValidator>
		<asp:RequiredFieldValidator ID="rfvConfirmNewPassword" runat="server" ErrorMessage="Bekr�fta nytt l�senord �r obligatoriskt" ValidationGroup="ChangePassword" ControlToValidate="txtConfirmNewPassword" CssClass="error" ForeColor="White" Display="None"></asp:RequiredFieldValidator>
    </p>            
	<p class="form-actions">					    
        <asp:ImageButton ID="imgBtnChangePassword" runat="server" ValidationGroup="ChangePassword" ImageUrl="/CommonResources/Images/Button-Send-Trans.png" AlternateText="Skicka" OnClick="btnChangePassword_Click"/>
    </p>
    
</fieldset>
