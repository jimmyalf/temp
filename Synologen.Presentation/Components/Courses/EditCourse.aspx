<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" CodeBehind="EditCourse.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.EditCourse" %>
<%@ Register Src="../../common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="dtc" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="ww" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCourses" Runat="Server">
<div id="dCompMain" class="Components-Courses-EditCourse-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Edit Course</h1>
    <fieldset><legend>Course Info</legend>		
	    <div class="formItem clearLeft">
	        <label for="txtHeading" class="labelLong">Heading *</label>
	        <asp:TextBox class="inputLong" ID="txtHeading" runat="server"/>
	        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtHeading" ErrorMessage="A heading must be supplied"  ValidationGroup="Submit"/>
	    </div>
	    <div class="formItem clearLeft">
	        <label for="txtSummary" class="labelLong">Summary</label>
	        <asp:TextBox class="textareaLong" ID="txtSummary" runat="server" TextMode="MultiLine"/>
	    </div>	    
	    <div class="formItem clearLeft">
	        <label for="drpMain" class="labelLong">Main Course</label>
	        <asp:DropDownList ID="drpMain" DataValueField="cId" DataTextField="cName" runat="server" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="drpMain" ErrorMessage="A main course must be selected" InitialValue="0" ValidationGroup="Submit"/>
            
	    </div>	
	    <div class="formItem">
	        <label for="drpCity" class="labelLong">City</label>
	        <asp:DropDownList ID="drpCity" DataValueField="cId" DataTextField="cCity" runat="server"/>
	        <asp:RequiredFieldValidator runat="server" ControlToValidate="drpCity" ErrorMessage="A city must be selected"  InitialValue="0" ValidationGroup="Submit"/>
	        
	    </div>	
	    <div class="formItem clearLeft">
	        <label for="txtMinNoOfParticipants" class="labelLong">Min Number Of Participants</label>
	        <asp:TextBox ID="txtMinNoOfParticipants" runat="server" MaxLength="4" Width="60px"/>
	        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMinNoOfParticipants" MinimumValue="0" Type="Integer" MaximumValue="1000" ErrorMessage="Value needs to be in range 0-1000" ValidationGroup="Submit"/>
	    </div>	
	    <div class="formItem">
	        <label for="txtSummary" class="labelLong">Max Number Of Participants * </label>
	        <asp:TextBox ID="txtMaxNoOfParticipants" runat="server" MaxLength="4" Width="60px"/>
	        <asp:RangeValidator runat="server" ControlToValidate="txtMaxNoOfParticipants" MinimumValue="0" Type="Integer" MaximumValue="1000" ErrorMessage="Value needs to be in range 0-1000" ValidationGroup="Submit"/>
	        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMaxNoOfParticipants" ErrorMessage="Max number of participants be supplied" ValidationGroup="Submit"/>
	    </div>	
	    <div class="formItem clearLeft">
	        <label for="chkLocations" class="labelLong">Publish Locations *</label>
	        <asp:CheckBoxList ID="chkLocations" runat="server" DataValueField="Id" DataTextField="Name" RepeatLayout="Table" RepeatColumns="2"/>
	        <label id="lblErrorLocations" runat="server"  style="Color:Red"/>
	    </div>			        	    	    	    
    </fieldset>  
    <fieldset><legend>Contact</legend>		
	    <div class="formItem clearLeft">
	        <label for="txtContactName" class="labelLong">Contact Name</label>
	        <asp:TextBox ID="txtContactName" runat="server" />
	    </div>
	    <div class="formItem clearLeft">
	        <label for="txtContactEmail" class="labelLong">Contact Email</label>
	        <asp:TextBox ID="txtContactEmail" runat="server" />
	        <label id="lblErrorContactEmail" runat="server"  style="Color:Red"/>
	    </div>
	    <div class="formItem">
	        <br />
	        <asp:CheckBox ID="chkContactAutoEmail"  Text="Automatically send contact email" runat="server" />
	    </div>	     
	    <div class="formItem clearLeft">
	        <label for="txtContactPhone" class="labelLong">Contact Phone</label>
	        <asp:TextBox ID="txtContactPhone" runat="server" />
	    </div>	    
	    <div class="formItem clearLeft">
	        <label for="txtContactMobile" class="labelLong">Contact Mobile</label>
	        <asp:TextBox ID="txtContactMobile" runat="server" />
	    </div>	     	    	    	    	    
        <div class="formItem clearLeft">
	        <label for="txtAdminEmail" class="labelLong">Administrator Email</label>
	        <asp:TextBox ID="txtAdminEmail" runat="server" />
	        <label id="lblErrorAdminEmail" runat="server"  style="Color:Red"/>
	    </div>	    
	    <div class="formItem">
	        <br />
	        <asp:CheckBox ID="chkAdminAutoEmail" Text="Automatically send admin email" runat="server" />
	    </div>	
    </fieldset>  
    <fieldset><legend>Course Dates</legend>		
	    <div class="formItem clearLeft">
	        <label for="txtHeading" class="labelLong">Last Application Date *</label>
	        <dtc:DateTimeCalendar ID="dtcApplicationDate" runat="server" />
	        <label id="lblErrorLastApplDate" runat="server"  style="Color:Red"/>
	    </div>	    
	    <div class="formItem clearLeft">
	        <label for="txtHeading" class="labelLong">Publish Start Date *</label>
	        <dtc:DateTimeCalendar ID="dtcPublishStartDate" runat="server" />
	        <label id="lblErrorPublishStartDate" runat="server"  style="Color:Red"/>
	    </div>	    
	    <div class="formItem">
	        <label for="txtHeading" class="labelLong">Publish End Date *</label>
	        <dtc:DateTimeCalendar ID="dtcPublishEndDate" runat="server" />
	        <label id="lblErrorPublishEndDate" runat="server"  style="Color:Red"/>
	    </div>	    
	    <div class="formItem clearLeft">
	        <label for="txtHeading" class="labelLong">Course Start Date *</label>
	        <dtc:DateTimeCalendar ID="dtcCourseStartDate" runat="server" />
	        <label id="lblErrorCourseStartDate" runat="server"  style="Color:Red"/>
	    </div>	    
	    <div class="formItem">
	        <label for="txtHeading" class="labelLong">Course End Date</label>
	        <dtc:DateTimeCalendar ID="dtcCourseEndDate" runat="server" />
	    </div>	    	 
	    <div class="formItem clearLeft">
	        <label for="txtDaysBeforeReminder" class="labelLong">Reminder (days before course start) *</label>
	        <asp:TextBox ID="txtDaysBeforeReminder" runat="server" MaxLength="4" Width="60px"/>
	        <label id="lblErrorReminder" runat="server"  style="Color:Red"/>
	    </div>		       	    	    	    
    </fieldset> 
    <fieldset><legend>Course Body</legend>
    	<div id="dWysiwyg" runat="server" class="dWysiwyg clearLeft">
            <WW:WpcWysiwyg  ID="txtBody" runat="server" />
        </div>
        <div ID="divButtons" class="formCommands">
            <br />
            <asp:button ID="btnSaveAndPublish" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Save & Publish" SkinId="Big" ValidationGroup="Submit"/>
            <asp:button ID="btnSaveForLater" runat="server" CommandName="SaveForLater" OnClick="btnSave_Click" Text="Save For Later" SkinId="Big" ValidationGroup="Submit"/>
            <asp:button ID="btnSaveForApproval" runat="server" CommandName="SaveForApproval" OnClick="btnSave_Click" Text="Send For Approval" SkinId="Big" ValidationGroup="Submit"/>
            <asp:button ID="btnCancel" runat="server" CommandName="Cancel" Text="Cancel" SkinId="Big"  ValidationGroup="Submit"/>
        </div>   
    </fieldset>
</div>
</div>
</div>
</asp:Content>

