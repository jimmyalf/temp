<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Site.News" Codebehind="News.ascx.cs" %>
<asp:Label ID="lblCategoryName" runat="server" Visible="false"/>
<asp:Repeater ID="rptNews" runat="server" OnItemDataBound="rptNews_ItemDataBound">
	<HeaderTemplate><div id="news_events"></HeaderTemplate>
	<ItemTemplate>
	<asp:Image ID="imgSpot" runat="server" visible="false"/>
		<p>
			<strong><%# DataBinder.Eval(Container.DataItem, "cHeading") %></strong>
			<br/>
			<%# DataBinder.Eval(Container.DataItem, "cSummary") %>
			<br/>
			<a href='<%=ShowDetailUrl%>?newsId=<%# DataBinder.Eval(Container.DataItem, "cId") %>&Location=<%=Location%>&Language=<%=Language%>'>
				<asp:Literal ID="ltReadMore" runat="server" />
			</a>
		</p>
	</ItemTemplate>
	<FooterTemplate></div></FooterTemplate>
</asp:Repeater>

