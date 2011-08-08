<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainCourseList.ascx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.Wpc.Courses.MainCourseList" %>
<asp:Repeater ID="rptMain" runat="server" OnItemDataBound="rptMain_ItemDataBound">
    <ItemTemplate>
    <asp:PlaceHolder ID="plCourseRow" runat="server">
    <div class="main-course">
        <h2><%# DataBinder.Eval (Container.DataItem, "cName") %></h2>
        <asp:Repeater ID="rptCourse" runat="server" OnItemDataBound="rptCourse_ItemDataBound">
        <HeaderTemplate><ul></HeaderTemplate>
        <ItemTemplate>
		<li><a href="/Wpc/Courses/CourseView.aspx?courseid=<%#DataBinder.Eval(Container.DataItem,"cId")%>&location=<%=Location%>&language=<%=Language%>&master=<%=Page.MasterPageFile%>&pageId=<%=PageId%>">
			<asp:PlaceHolder ID="plSingleDate" runat="server" Visible="true">
			<span><%# DataBinder.Eval(Container.DataItem, "cCourseStartDate","{0:yyyy-MM-dd}")%>
			</asp:PlaceHolder>
			<asp:PlaceHolder ID="plDateInterval" runat="server" Visible="false">
			<span><%# DataBinder.Eval(Container.DataItem, "cCourseStartDate","{0:yyyy-MM-dd}")%>&nbsp;-&nbsp;<%# DataBinder.Eval(Container.DataItem, "cCourseEndDate","{0:yyyy-MM-dd}")%>
			</asp:PlaceHolder>
			:&nbsp;<%#DataBinder.Eval(Container.DataItem, "cHeading")%> (<%#DataBinder.Eval(Container.DataItem, "cCity")%>)</span>
		</a></li>
        </ItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
	</div>  
	</asp:PlaceHolder>	      
    </ItemTemplate>
</asp:Repeater>