<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberInfo" Codebehind="MemberInfo.ascx.cs" %>
<div style="float:right; width:170px; background: #d5ded5; padding: 10px; margin-bottom: 5px; margin-left
: 5px;">

	<b><asp:Literal ID="ltOrganinsation" runat="server" /></b><br />
	<asp:Literal ID="ltAddress" runat="server" /><br />
	<asp:Literal ID="ltZip" runat="server" />&nbsp;<asp:Literal ID="ltCity" runat="server" />
	<br /><br />
Fax: <asp:Literal ID="ltFax" runat="server" /><br />
Mail:<br />
    <div class="kortafter"><div class="kort"><asp:HyperLink ID="hlMail" runat="server">[hlMail]</asp:HyperLink></div></div><br />
Webbplats
	<br />
    <div class="kortafter"><div class="kort"><asp:HyperLink ID="hlWeb" runat="server" CssClass="kort" Target="_blank">[hlWeb]</asp:HyperLink></div></div><br /><br />
	<b>Kontaktperson:</b> <br />
<asp:Literal ID="ltFirstName" runat="server" />&nbsp;<asp:Literal ID="ltLastName" runat="server" /><br /><br />
Mail: <br />
<div class="kortafter"><div class="kort"><asp:HyperLink ID="hlPersonalMail" runat="server" /></div></div><br />

Mobil: <asp:Literal ID="ltMobile" runat="server" /><br />

Tel: <asp:Literal ID="ltPhone" runat="server" /><br />	
</div>
