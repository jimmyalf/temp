<%@ Register TagPrefix="WPC" TagName="Base1" Src="~/wpc/Base/LogoutControl.ascx" %>
<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true"  Title="Logout" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="255" Visible="false" runat="server"/>
<WPC:Base1 id="Base1" runat="server"></WPC:Base1>
</asp:Content>