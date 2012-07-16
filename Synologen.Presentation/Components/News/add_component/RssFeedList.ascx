<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.components.News.add_component.RssFeedList" Codebehind="RssFeedList.ascx.cs" %>
<div class="Component-News-AddComponent-RssFeedList-ascx fullBox">
<div class="wrap">
	<fieldset>
		<legend>Rss Feed List</legend>
		<div class="formItem clearLeft">
		    <label class="labelLong" for="<%=txtMax.ClientID%>">Max number of news items to show</label>		    
            <asp:TextBox ID="txtMax" runat="server" />
		</div>		
		<div class="formItem clearLeft">
		    <label class="labelLong" for="<%=txtFeedUrl.ClientID%>">Feed url</label>
			<asp:TextBox ID="txtFeedUrl" runat="server" />
		</div>			
		
	</fieldset>
</div>
</div>