<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForgottenPassword.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Site.ForgottenPassword" %>

<fieldset>
    <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
    <asp:PlaceHolder ID="phMessageManager" runat="server" Visible="false">
	<div id="MessageManager" runat="server">
		<asp:Label ID="lbMessage" runat="server" />
	</div>	   
    </asp:PlaceHolder>
    <p>
        <asp:Label ID="lblUserName" runat="server" AssociatedControlID="txtUserName" SkinId="Long" CssClass="long"/>
		<asp:TextBox ID="txtUserName" runat="server" TextMode="SingleLine" SkinID="Wide"/>
    </p>	
	<p>					    
        <asp:button ID="btnSend" runat="server" CommandName="Send" OnClick="btnSend_Click" SkinId="Big"/>
    </p>
</fieldset>

