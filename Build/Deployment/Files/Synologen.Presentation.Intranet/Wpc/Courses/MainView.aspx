<%@ Page Language="C#"  MasterPageFile="" AutoEventWireup="true" CodeBehind="MainView.aspx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.Wpc.Courses.MainView" Title="<%$ Resources: PageTitle %>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="main-course">
    <h3><%=MainHeading %></h3>
    <p><%=MainDescription %></p>
    <br/>
    <div><%=MainBody %></div>
    <br/>
    <label class="main-course-heading"><%=GetLocalResourceObject("Heading").ToString()%></label>
    <hr/>
    <asp:Repeater ID="rptCourses" runat="server" OnItemDataBound="rptCourses_ItemDataBound">
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
    <hr/>
    <br />
    <div class="control-bar">
        <input type="button" value="<%=GetLocalResourceObject("btnBack.Text")%>" onclick="javascript:history.back();return false;" />
    </div>
</div>    
</asp:Content>