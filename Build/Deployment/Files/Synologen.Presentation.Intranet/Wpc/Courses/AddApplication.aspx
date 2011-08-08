<%@ Page Language="C#" MasterPageFile="" AutoEventWireup="true" CodeBehind="AddApplication.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.Wpc.Courses.AddApplication" Title="<%$ Resources: PageTitle %>" %>
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
    </div>
    <div class="formItem">
        <asp:Label ID="lblOrgName" CssClass="labelLong" runat="server" meta:resourcekey="lblOrgName"/><br />
        <asp:TextBox ID="txtOrgNr" runat="server"/>
    </div>
    <div class="formItem clearLeft">
        <asp:Label ID="lblAddress" CssClass="labelLong" runat="server" meta:resourcekey="lblAddress"/><br />
        <asp:TextBox ID="txtAddress" runat="server"/>
    </div>	    
    <div class="formItem">
        <asp:Label ID="lblPostCode" CssClass="labelLong" runat="server" meta:resourcekey="lblPostCode"/><br />
        <asp:TextBox ID="txtPostCode" runat="server"/>
    </div>	 	 	    
    <div class="formItem clearLeft">
        <asp:Label ID="lblCity" CssClass="labelLong" runat="server" meta:resourcekey="lblCity"/><br />
        <asp:TextBox ID="txtCity" runat="server"/>
    </div>   
    <div class="formItem clearLeft">
        <asp:Label  ID="lblCountry" CssClass="labelLong" runat="server" meta:resourcekey="lblCountry"/><br />
        <asp:TextBox ID="txtCountry" runat="server"/>
    </div>       
    <div class="formItem clearLeft">
        <asp:Label ID="lblPhone" CssClass="labelLong" runat="server" meta:resourcekey="lblPhone"/><br />
        <asp:TextBox ID="txtPhone" runat="server"/>
    </div> 	 
    <div class="formItem">
        <asp:Label ID="lblMobile" CssClass="labelLong" runat="server" meta:resourcekey="lblMobile"/><br />
        <asp:TextBox ID="txtMobile" runat="server"/>
    </div> 
    <div class="formItem clearLeft">
        <asp:Label ID="lblEmail" CssClass="labelLong" runat="server" meta:resourcekey="lblEmail"/><br />
        <asp:TextBox ID="txtEmail" runat="server"/>
        <asp:RequiredFieldValidator ID="reqEmail" runat="server" ValidationGroup="vldSubmit"  ControlToValidate="txtEmail" meta:resourcekey="reqEmail"/>
    </div> 	
    <div class="formItem clearLeft">
        <asp:Label ID="lblApplication" CssClass="labelLong" runat="server" meta:resourcekey="lblApplication"/><br />
        <asp:TextBox ID="txtApplication" runat="server" TextMode="MultiLine" Rows="5"/>
    </div>
    <div class="formItem clearLeft">
        <asp:Label ID="lblNoOfParticipants" CssClass="labelLong" runat="server" meta:resourcekey="lblNoOfParticipants"/><br />
        <asp:TextBox ID="txtNrOfParticipants" runat="server"/>
        <asp:RangeValidator ID="rangeNrOfParticipants" runat="server" ValidationGroup="vldSubmit" ControlToValidate="txtNrOfParticipants"  MinimumValue="1" MaximumValue="1000"  meta:resourcekey="rangeNrOfParticipants" Type="Integer"/>
        <asp:RequiredFieldValidator ID="reqNrOfParticipants" runat="server" ValidationGroup="vldSubmit" ControlToValidate="txtNrOfParticipants"  meta:resourcekey="reqNrOfParticipants" />
    </div> 	
    <div>
		<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" meta:resourcekey="btnBack" />
        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" ValidationGroup="vldSubmit" meta:resourcekey="btnSubmit"/>&nbsp;
        <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server"  ValidationGroup="vldClear" meta:resourcekey="btnClear"/>&nbsp;<br />
        <%--<asp:LinkButton ID="btnBack" runat="server" meta:resourcekey="btnBack"/>--%>
        
	</div>
    <div id="divDisplayError" visible="false" style="color:Red" runat="server" >
        <asp:Label ID="lblError" runat="server" meta:resourcekey="lblError"/>
    </div>
    <div id="divThankYou" runat="server" visible="false">
        <asp:Label ID="lblThankYou" runat="server" meta:resourcekey="lblThankYou"/>
    </div>
</asp:Content>
