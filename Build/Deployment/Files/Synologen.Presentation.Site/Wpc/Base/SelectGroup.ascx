<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectGroup.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Site.Wpc.Base.SelectGroup" %>
<asp:PlaceHolder ID="phSelectGroup" runat="server" Visible="false">
Välj Klass: <asp:DropDownList runat="server" ID="ddlSelectGroup" OnSelectedIndexChanged="ddlSelectGroup_SelectedIndexChanged" AutoPostBack="true" />
</asp:PlaceHolder>