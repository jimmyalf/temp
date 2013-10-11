<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetMemberSearch.ascx.cs" Inherits="Spinit.Wpc.Member.Presentation.Intranet.Wpc.Member.IntranetMemberSearch" %>
<div id="local-search">
    <fieldset>      
        <asp:Label ID="lblSearch" AssociatedControlID="txtSearch" runat="server" Text="Sök bland elever"></asp:Label>
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Label ID="lblGroup" AssociatedControlID="drpGroup" runat="server" Text="Årskurs"></asp:Label>
        <asp:DropDownList ID="drpGroup" runat="server">
        </asp:DropDownList>
        <asp:Button ID="btnSearch" runat="server" Text="Sök" OnClick="btnSearch_Click" />
    </fieldset>
</div>