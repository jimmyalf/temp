<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Site.NewsFullAlternative" Codebehind="NewsFullAlternative.ascx.cs" %>
<div id="center_content">
<%if (NewsId > 0) {%>
	<div>
	<b><%=newsRow.StartDate.ToShortDateString()%></b><br />
	<h2><%=newsRow.Heading%></h2>
	<%=newsRow.Summary%><br />
	<%=newsRow.FormatedBody%></div>
<%}%>
<asp:Repeater ID="rptNews" runat="server">
<HeaderTemplate><div id="newsContainer"></HeaderTemplate>
<ItemTemplate>
    <b><%# ((DateTime) DataBinder.Eval(Container.DataItem, "cStartDate")).ToShortDateString()%></b><br />
    <h2><%# DataBinder.Eval(Container.DataItem, "cHeading")%></h2>
	<%# DataBinder.Eval(Container.DataItem, "cSummary")%><br />
	<%if (ShowBody) {%>
    <%# DataBinder.Eval(Container.DataItem, "cFormatedBody")%><br />
    <%} %>    
	<a href='<%=ShowDetailUrl%>?newsId=<%# DataBinder.Eval(Container.DataItem, "cId") %>&Location=<%=Location%>&Language=<%=Language%>'>
		<asp:Localize ID="locReadMore" runat="server" meta:resourcekey="locReadMore">Läs mer&nbsp;»</asp:Localize>
	</a><br /><br />
</ItemTemplate>
<FooterTemplate></div></FooterTemplate>
</asp:Repeater>
</div>
