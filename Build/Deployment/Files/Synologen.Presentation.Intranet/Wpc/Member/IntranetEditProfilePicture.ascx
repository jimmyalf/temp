<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetEditProfilePicture.ascx.cs" Inherits="Spinit.Wpc.Member.Presentation.Intranet.Wpc.Member.IntranetEditProfilePicture" %>
<h2>Profilfoto</h2>                                  
    <p>
        <asp:Label ID="lblStatus" runat="server" CssClass="status" Visible="False"></asp:Label>
        <asp:Label ID="lblProfilePhoto" runat="server" Text="Ladda upp ett nytt foto" AssociatedControlID="uplProfilePhoto"></asp:Label><asp:FileUpload ID="uplProfilePhoto" runat="server" />
    </p>
<asp:Button ID="btnSave" runat="server" Text="Ladda upp" OnClick="btnSave_Click" />
<asp:Image ID="imgProfilePhoto"  runat="server" Visible="true" />
