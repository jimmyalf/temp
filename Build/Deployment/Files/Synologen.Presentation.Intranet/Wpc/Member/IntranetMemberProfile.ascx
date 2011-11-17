<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetMemberProfile.ascx.cs" Inherits="Spinit.Wpc.Member.Presentation.Intranet.IntranetMemberProfile" %>
<div id="user-profile">
	<h1><asp:Label ID="lblHeading" runat="server" Text=""></asp:Label></h1>
	<asp:Image ID="imgProfilePicture" CssClass="profile-photo" runat="server" />
	<div id="contact-information">
	    <h2><asp:Label ID="lblContactHeading" runat="server" Text="kontaktuppgifter"></asp:Label></h2>
	    <dl>
	        <dt><strong>Adress</strong></dt>
	        <dd>
	            <asp:Label ID="lblAdress" runat="server" Text=""></asp:Label></dd>

	        <dt><strong>Postnummer</strong></dt>
	        <dd>
	            <asp:Label ID="lblZip" runat="server" Text=""></asp:Label></dd>
	        <dt><strong>Ort</strong></dt>
	        <dd>
	            <asp:Label ID="lblCity" runat="server" Text=""></asp:Label></dd>
	        <dt><strong>Telefon</strong></dt>
	        <dd>
	            <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label></dd>

	        <dt><strong>Mobil</strong></dt>
	        <dd>
	            <asp:Label ID="lblMobile" runat="server" Text=""></asp:Label></dd>
	        <dt><strong>Skype</strong></dt>
	        <dd>
	            <asp:HyperLink ID="hlSkype" runat="server"></asp:HyperLink></dd>
	        <dt><strong>E-post</strong></dt>
	        <dd>
	            <asp:HyperLink ID="hlEmail" runat="server"></asp:HyperLink></dd>

	        <dt><strong>Hemsida</strong></dt>
	        <dd>
	            <asp:HyperLink ID="hlWww" runat="server"></asp:HyperLink></dd>
	    </dl>
	</div>
	<asp:Literal ID="ltBody" runat="server"></asp:Literal>	
</div>