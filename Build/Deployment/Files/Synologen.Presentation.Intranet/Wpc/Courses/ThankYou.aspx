<%@ Page Language="C#" MasterPageFile="" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.Wpc.Courses.ThankYou" Title="<%$ Resources: PageTitle %>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<h1>Tack f�r din anm�lan!</h1>
<br />
<asp:Button ID="btnApply" runat="server" Text="Anm�l ytterligare en person" OnClick="btnApply_Click" /><br /><br />
<asp:Button ID="btnBack" runat="server" Text="F�rdig" OnClick="btnBack_Click" />
</asp:Content>
