<%@ Page Language="C#" MasterPageFile="" AutoEventWireup="true" CodeBehind="ApplicationAlternative.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.Wpc.Courses.ApplicationAlternative" Title="<%$ Resources: PageTitle %>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">		
    <div class="formItem clearLeft">
        <asp:Label ID="lblFirstName"  CssClass="labelLong" runat="server" meta:resourcekey="lblFirstName"/><br />
        <asp:TextBox ID="txtFirstName" runat="server"/>
        <asp:RequiredFieldValidator ID="reqFirstName" runat="server"  ValidationGroup="vldSubmit"  ControlToValidate="txtFirstName" meta:resourcekey="reqFirstName" />
    </div>
    <div class="formItem clearLeft">
        <asp:Label ID="lblLastName" CssClass="labelLong" runat="server" meta:resourcekey="lblLastName"/><br />
        <asp:TextBox ID="txtLastName" runat="server"/>
        <asp:RequiredFieldValidator ID="reqLastName" runat="server" ValidationGroup="vldSubmit"  ControlToValidate="txtLastName" meta:resourcekey="reqLastName" />
    </div>    
    <div class="formItem clearLeft">
        <asp:Label ID="lblCompany" CssClass="labelLong" runat="server" meta:resourcekey="lblCompany"/><br />
        <asp:TextBox ID="txtCompany" runat="server"/>
        <asp:RequiredFieldValidator ID="reqCompany" runat="server" ValidationGroup="vldSubmit"  ControlToValidate="txtCompany" meta:resourcekey="reqCompany"/>
    </div>
    <div class="formItem clearLeft">
        <asp:Label ID="lblEmail" CssClass="labelLong" runat="server" meta:resourcekey="lblEmail"/><br />
        <asp:TextBox ID="txtEmail" runat="server"/>
        <asp:RequiredFieldValidator ID="reqEmail" runat="server" ValidationGroup="vldSubmit"  ControlToValidate="txtEmail" meta:resourcekey="reqEmail"/>
    </div>
    <div class="formItem clearLeft">
        <asp:Label ID="lblPhone" CssClass="labelLong" runat="server" meta:resourcekey="lblPhone"/><br />
        <asp:TextBox ID="txtPhone" runat="server"/>
    </div>    
    <div class="formItem clearLeft">
        <asp:Label ID="lblApplication" CssClass="labelLong" runat="server" meta:resourcekey="lblApplication"/><br />
        <asp:TextBox ID="txtApplication" runat="server" TextMode="MultiLine" Rows="5"/>
    </div>
    <div>
		<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" meta:resourcekey="btnBack" />
        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" ValidationGroup="vldSubmit" meta:resourcekey="btnSubmit"/>&nbsp;
	</div>
</asp:Content>
