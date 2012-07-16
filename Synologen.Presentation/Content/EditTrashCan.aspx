<%@ Page Language="C#" MasterPageFile="~/content/ContentMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.EditTrashCan" MaintainScrollPositionOnPostback="true" Codebehind="EditTrashCan.aspx.cs" %>
<asp:Content ID="Archive" ContentPlaceHolderID="ComponentPage" Runat="Server">
<div class="fullBox">
<div class="wrap">
<h1>Content</h1>

<p>Do you whish to empty the trash can?</p>

<asp:Button ID="btnEmptyTrashCan" runat="server" OnClick="btnEmptyTrashCan_Click" Text="Empty trash can" SkinID="Big" />

</div>
</div>
</asp:Content>
