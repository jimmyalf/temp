<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Site.Show" Codebehind="Show.ascx.cs" %>
<!-- use newsRow to get all info you want i.e. <%=newsRow.Summary%>-->
<%=newsRow.StartDate.ToShortDateString()%>&nbsp;<%=newsRow.Summary%><br />
<%=newsRow.Body%>

