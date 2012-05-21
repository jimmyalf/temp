<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseSpot.ascx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.Wpc.Courses.CourseSpot" %>
<asp:PlaceHolder ID="plCourseSpotContainer" runat="server">
<div class="course-spot">
<%=GetLocalResourceObject("ComponentHeading").ToString()%>
<asp:Repeater ID="rptCourses" runat="server">
<HeaderTemplate><ul class="course-spot-list"></HeaderTemplate>
<ItemTemplate>
<li>
<a href="/Wpc/Courses/CourseView.aspx?courseid=<%#DataBinder.Eval(Container.DataItem,"cId")%>&location=<%=Location%>&language=<%=Language%>&master=<%=Page.MasterPageFile%>&pageId=<%=PageId%>">
<%# DataBinder.Eval(Container.DataItem, "cCourseStartDate","{0:yyyy-MM-dd}")%>, <%#DataBinder.Eval(Container.DataItem, "cCity")%><br />
<%#DataBinder.Eval(Container.DataItem, "cHeading")%></a>
</li>
</ItemTemplate>
<FooterTemplate></ul></FooterTemplate>
</asp:Repeater>
</div>
</asp:PlaceHolder>