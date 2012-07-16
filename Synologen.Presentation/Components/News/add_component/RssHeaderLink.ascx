<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.components.News.add_component.RssHeaderLink" Codebehind="RssHeaderLink.ascx.cs" %>
<div class="Component-News-AddComponent-RssHeaderLink-ascx fullBox">
<div class="wrap">
	<fieldset>
		<legend>Rss Header Link</legend>
		<div class="formItem">
		    <label class="labelLong" for="<%=drpCategories.ClientID%>">Filter on category</label>
		    <asp:DropDownList ID="drpCategories" runat="server"/>
		</div>
		<div class="formItem">
		    <label class="labelLong" for="<%=drpGroups.ClientID%>">Filter on groups</label>
            <asp:DropDownList ID="drpGroups" runat="server"/>
		</div>			
		<div class="formItem clearLeft">
		    <label class="labelLong" for="<%=drpAuthors.ClientID%>">Filter on author</label>
            <asp:DropDownList ID="drpAuthors" runat="server"/>
		</div>
		<div class="formItem">
		    <label class="labelLong" for="<%=txtMax.ClientID%>">Max number of news items to show</label>		    
            <asp:TextBox ID="txtMax" runat="server" />
		</div>		
		<div class="formItem clearLeft">
		    <label class="labelLong" for="<%=txtLinkText.ClientID%>">Link text</label>
			<asp:TextBox ID="txtLinkText" runat="server" />
		</div>			
		
	</fieldset>
</div>
</div>