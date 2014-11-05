<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryMainList.ascx.cs" Inherits="Spinit.Wpc.Courses.Presentation.Site.Wpc.Courses.CategoryMainList" %>
<asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
    <ItemTemplate>
    <asp:placeholder id="plMainRow" runat="server">
    <div class="course-category">
        <h2><%# DataBinder.Eval(Container.DataItem, "cName") %></h2>
        <asp:Repeater ID="rptMain" runat="server">
        <HeaderTemplate><ul></HeaderTemplate>
        <ItemTemplate>
		<li><a href="/Wpc/Courses/MainView.aspx?mainid=<%#DataBinder.Eval(Container.DataItem,"cId")%>&location=<%=Location%>&language=<%=Language%>&master=<%=Page.MasterPageFile%>&pageId=<%=PageId%>">
			<%# DataBinder.Eval (Container.DataItem, "cName") %>
		</a></li>
        </ItemTemplate>
        <FooterTemplate></ul></FooterTemplate>
        </asp:Repeater>
	</div>        
    </asp:placeholder>
    </ItemTemplate>
</asp:Repeater>