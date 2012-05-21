<%@ Page Language="C#" MasterPageFile="" AutoEventWireup="true" CodeBehind="CourseView.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.Wpc.Courses.CourseView" Title="<%$ Resources: PageTitle %>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <h3><%=Course.Heading %></h3>
    <div id="course-body"><%=Course.Body %></div>
    <br/>
    <label for="last-application-date"><%=GetLocalResourceObject("lblLastApplicationDate.Text")%></label>
	<span id="last-application-date"><%=Course.LastApplicationDate.ToShortDateString() %></span>
	<br />
	<label for="max-participants"><%=GetLocalResourceObject("lblMaxNoOfParticipants.Text")%></label>
	<span id="max-participants"><%=Course.MaxParticipants %></span>
	<br />
	<label for="available-positions"><%=GetLocalResourceObject("lblAvailable.Text")%></label>
	<span id="available-positions"><%=Course.MaxParticipants - Course.Participants %></span>
	<br /><br />
    <%if (!String.IsNullOrEmpty(Course.ContactName)){ %>
		<label><%=GetLocalResourceObject("lblContact.Text")%></label>
		<br />
		<label for="contact-name"><%=GetLocalResourceObject("lblName.Text")%></label>
		<span id="contact-name"><%=Course.ContactName %></span>
		<br />
	<%} %>    
	<%if (!String.IsNullOrEmpty(Course.ContactEmail)){ %>
		<label for="contact-email"><%=GetLocalResourceObject("lblEmail.Text")%></label>
		<a id="contact-email" href="mailto:<%=Course.ContactEmail %>"><%=Course.ContactEmail %></a>
		<br />
	<%} %>
	<%if(!String.IsNullOrEmpty(Course.ContactPhone)){ %>
		<label for="contact-phone"><%=GetLocalResourceObject("lblPhone.Text")%></label>
		<span id="contact-phone"><%=Course.ContactPhone %></span>
		<br />
	<%} %>
	<%if(!String.IsNullOrEmpty(Course.ContactMobile)){ %>
		<label for="contact-cellphone"><%=GetLocalResourceObject("lblCellphone.Text")%></label>
		<span id="contact-cellphone"><%=Course.ContactMobile %></span>
		<br />
	<%} %>
    <br />
    <div class="control-bar">
		<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" meta:resourcekey="btnBack" />
	        <input type="button" <%if (ApplyDisabled) Response.Write("disabled"); %> value="<%=GetLocalResourceObject("btnApply.Text")%>" onclick="window.location='/Wpc/Courses/ApplicationAlternative.aspx?courseid=<%=CourseId %>&location=<%=Location%>&language=<%=Language%>&master=<%=Page.MasterPageFile%>&pageId=<%=PageId %>'"/>
    </div>
<asp:PlaceHolder ID="plApplicationList" runat="server" Visible="false">
	<asp:Repeater id="rptApplicationList" runat="server">
	<HeaderTemplate><br /><br />
	<h1>Anmälda</h1>
	<ul id="application-list">
	</HeaderTemplate>
	<ItemTemplate><li><%#DataBinder.Eval(Container.DataItem, "cFirstName")%> <%#DataBinder.Eval(Container.DataItem, "cLastName")%>, <%#DataBinder.Eval(Container.DataItem, "cCompany")%></li></ItemTemplate>
	<FooterTemplate></ul></FooterTemplate>
	</asp:Repeater>
</asp:PlaceHolder>
</asp:Content>
