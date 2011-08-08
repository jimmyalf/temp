<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetEditProfilePage.ascx.cs" Inherits="Spinit.Wpc.Member.Presentation.Intranet.Wpc.Member.IntranetEditProfilePage" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc1" %>

<asp:Label ID="lblStatus" runat="server" CssClass="status" Visible="False" ></asp:Label><uc1:WpcWysiwyg id="WpcWysiwyg1" runat="server">
</uc1:WpcWysiwyg>
<asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" />
