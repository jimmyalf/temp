<div id="center_content"><%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsFull.ascx.cs" Inherits="NewsFull" %>
<%if (_newsId > 0) {%>
<div style="border: 1px solid rgb(187, 193, 198); padding: 15px; background-color: rgb(234, 239, 244); margin-bottom:15px"><b><%=newsRow.StartDate.ToShortDateString()%></b><br /><h2><%=newsRow.Heading%></h2><%=newsRow.Summary%><br />
<%=newsRow.Body%></div>
<%} %>
<asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound">
<HeaderTemplate><div id="newsContainer"></HeaderTemplate>
<ItemTemplate>
    <b><%# ((DateTime) DataBinder.Eval(Container.DataItem, "cStartDate")).ToShortDateString()%></b><br />
    <h2><%# DataBinder.Eval(Container.DataItem, "cHeading")%></h2>
	<%# DataBinder.Eval(Container.DataItem, "cSummary")%><br />
    <%# DataBinder.Eval(Container.DataItem, "cBody")%><br /><br />
</ItemTemplate>
<FooterTemplate></div></FooterTemplate>
</asp:Repeater></div><div id="right_content"></div>
