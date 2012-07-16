<%@ Page MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/components/Courses/CoursesMain.master" AutoEventWireup="true" CodeBehind="ViewApplication.cs" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.ViewApplication" %>
<%@ Register TagPrefix="WPC" Namespace="Spinit.Wpc.Utility.Business" Assembly="Spinit.Wpc.Utility.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCourses" Runat="Server">
<div id="dCompMain" class="Components-Courses-ViewApplication-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>Application</h1>
	<table class="detailsMode striped">
	<caption>Course Details</caption>
	<tbody>
	<tr>
		<th scope="row">Course</th>
		<td><%=CourseObject.Heading%></td>
	</tr>
	<tr>
		<th scope="row">City</th>
		<td><%=CityObject.CityName%></td>
	</tr>	
	<tr>
		<th scope="row">Dates</th>
		<td><%=CourseObject.CourseStartDate.ToShortDateString()%>
		<%if (CourseObject.CourseEndDate != DateTime.MinValue) {%>
		&nbsp;-&nbsp;<%=CourseObject.CourseEndDate.ToShortDateString()%>
		<%}%>
		</td>
	</tr>		
	</tbody>
	</table>
	<br />
	<table class="detailsMode striped">
	<caption>Application details for <%=ApplicationObject.FirstName + " " + ApplicationObject.LastName%></caption>
	<tbody>
		<tr>
			<th scope="row">Name</th>
			<td><%=ApplicationObject.FirstName + " " + ApplicationObject.LastName%></td>
		</tr>
		<tr>
			<th scope="row">Company</th>
			<td><%=ApplicationObject.Company %></td>
		</tr>
		<tr>
			<th scope="row">Organisation Number</th>
			<td><%=ApplicationObject.PostCode %></td>
		</tr>
		<tr>
			<th scope="row">Address</th>
			<td><%=ApplicationObject.Address %></td>
		</tr>
		<tr>
			<th scope="row">Post Code</th>
			<td><%=ApplicationObject.PostCode %></td>
		</tr>
		<tr>
			<th scope="row">Phone</th>
			<td><%=ApplicationObject.Phone %></td>
		</tr>
		<tr>
			<th scope="row">Mobile</th>
			<td><%=ApplicationObject.Mobile %></td>
		</tr>		
		<tr>
			<th scope="row">Email</th>
			<td><a href="mailto:<%=ApplicationObject.Email %>"><%=ApplicationObject.Email %></a></td>
		</tr>
		<tr>
			<th scope="row">Number of participants</th>
			<td><%=ApplicationObject.NumberOfParticipants %></td>
		</tr>			
		<tr>
			<th scope="row">Application</th>
			<td><%=ApplicationObject.ApplicationText %></td>
		</tr>
	</tbody>	
	</table>
	<div class="formCommands">
		<br /><input class="btnBig" type=button OnClick="javascript:history.go(-1);" value="Back"/>
	</div>
</div>
</div>
</div>
</asp:Content>
