<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IntranetEditProfile.ascx.cs" Inherits="Spinit.Wpc.Member.Presentation.Intranet.IntranetEditProfile" %>
<div id="edit-contact-card">
    <fieldset>
        <legend>Redigera kontaktuppgifter</legend>
        <asp:Label ID="lblStatus" runat="server" CssClass="status" Visible="False"></asp:Label>
        <h3>
        <asp:Label ID="lblName" runat="server"></asp:Label></h3>
		<p>  
            <asp:Label ID="lblAddress" runat="server" Text="Adress" AssociatedControlID="txtAddress"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></p>
        <p>
            <asp:Label ID="lblZip" runat="server" Text="Postnummer" AssociatedControlID="txtZip"></asp:Label>
            <asp:TextBox ID="txtZip" runat="server"></asp:TextBox></p>
        <p>
            <asp:Label ID="lblCity" runat="server" Text="Ort" AssociatedControlID="txtCity"></asp:Label>
            <asp:TextBox ID="txtCity" runat="server"></asp:TextBox></p>
        <p>
            <asp:Label ID="lblPhone" runat="server" Text="Telefon" AssociatedControlID="txtPhone"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></p>
        <p>

            <asp:Label ID="lblMobile" runat="server" Text="Mobil" AssociatedControlID="txtMobile"></asp:Label>
            <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></p>
        <p>
            <asp:Label ID="lblSkype" runat="server" Text="Skype" AssociatedControlID="txtSkype"></asp:Label>
            <asp:TextBox ID="txtSkype" runat="server"></asp:TextBox></p>
        <p>
            <asp:Label ID="lblEmail" runat="server" Text="E-post" AssociatedControlID="txtEmail"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="vldxEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Felaktig e-postadress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></p>
        <p>
            <asp:Label ID="lblWeb" runat="server" Text="Hemsida" AssociatedControlID="txtWeb"></asp:Label>
            <asp:TextBox ID="txtWeb" runat="server"></asp:TextBox></p>
        <p>
            <asp:Button ID="btnSave" runat="server" Text="Spara" OnClick="btnSave_Click" /></p>
    </fieldset>
</div>
                    

