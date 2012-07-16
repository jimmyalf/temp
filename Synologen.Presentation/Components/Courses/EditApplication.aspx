<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" CodeBehind="EditApplication.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.EditApplication" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCourses" Runat="Server">
<div id="dCompMain" class="Components-Courses-EditApplication-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Edit Application</h1>

    <fieldset>
	    <legend>Contact Info</legend>		
	    <div class="formItem clearLeft">
	        <label for="txtName" class="labelLong">Name</label>
	        <asp:TextBox ID="txtName" runat="server" Enabled="false"/>
	    </div>
	    <div class="formItem clearLeft">
	        <label for="txtCompany" class="labelLong">Company</label>
	        <asp:TextBox ID="txtCompany" runat="server" Enabled="false"/>
	    </div>
	    <div class="formItem">
	        <label for="txtOrgNr" class="labelLong">Organisation Number</label>
	        <asp:TextBox ID="txtOrgNr" runat="server" Enabled="false"/>
	    </div>
	    <div class="formItem clearLeft">
	        <label for="txtAddress" class="labelLong">Address</label>
	        <asp:TextBox ID="txtAddress" runat="server" Enabled="false"/>
	    </div>	    
	    <div class="formItem">
	        <label for="txtPostCode" class="labelLong">Post Code</label>
	        <asp:TextBox ID="txtPostCode" runat="server" Enabled="false"/>
	    </div>	 	 	    
	    <div class="formItem clearLeft">
	        <label for="txtCity" class="labelLong">City</label>
	        <asp:TextBox ID="txtCity" runat="server" Enabled="false"/>
	    </div>   
	    <div class="formItem clearLeft">
	        <label for="txtPhone" class="labelLong">Phone</label>
	        <asp:TextBox ID="txtPhone" runat="server" Enabled="false"/>
	    </div> 	 
	    <div class="formItem">
	        <label for="txtMobile" class="labelLong">Mobile</label>
	        <asp:TextBox ID="txtMobile" runat="server" Enabled="false"/>
	    </div> 
	    <div class="formItem clearLeft">
	        <label for="txtEmail" class="labelLong">Email</label>
	        <asp:TextBox ID="txtEmail" runat="server" Enabled="false"/>
	    </div> 	
	    <div class="formItem clearLeft">
	        <label for="txtApplication" class="labelLong">Application</label>
	        <asp:TextBox ID="txtApplication" runat="server" TextMode="MultiLine" Enabled="false"/>
	    </div>
	    <div class="formItem clearLeft">
	        <label for="txtNrOfParticipants" class="labelLong">Number of participants</label>
	        <asp:TextBox ID="txtNrOfParticipants" runat="server" Enabled="false"/>
	    </div> 	
	    <div>
	        <br />
	        <label ID="lblCreatedDate" runat="server" class="labelLong" />
	    </div> 		    	    	        	        	    	    	    
    </fieldset>
</div>
</div>
</div>
</asp:Content>
