<%@ Page Language="C#" MasterPageFile="~/Window.master" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Components.News.AddComponent.InsertComponent" Codebehind="InsertComponent.aspx.cs" %>
<%@ Register Src="NewsList.ascx" TagName="NewsList" TagPrefix="news" %>
<%@ Register Src="NewsFull.ascx" TagName="NewsFull" TagPrefix="news" %>
<%@ Register Src="RssLink.ascx" TagName="RssLink" TagPrefix="news" %>
<%@ Register Src="RssHeaderLink.ascx" TagName="RssHeaderLink" TagPrefix="news" %>
<%@ Register Src="RssFeedList.ascx" TagName="RssFeedList" TagPrefix="news" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphWindow" Runat="Server">
	<script language="JavaScript" type="text/javascript">
		<!--
		    function submitForm() {
		        retElement = document.getElementById("returnvalue");
		        parent.Select(retElement.value);
		    }
		    
		    function setHeight() {
                if (parent == window) return;
                else parent.setIframeHeight('cmpFrame');
            }
            window.onload = "javascript:setHeight()";
		//-->
	</script>
	<input type="hidden" id="returnvalue" name="returnvalue" value="<%=m_return %>"/>
	<div id="wrapper" class="wrap">
	    <h1>News</h1>
	    <asp:PlaceHolder ID="phContentMenu" runat="server" />
	    <div class="tabsContentContainer clearAfter">
			<asp:MultiView ID="view" ActiveViewIndex="0" runat="server">
				<asp:View ID="vwNewsList" runat="server">
					<div class="wrap"><news:NewsList id="ctrlNewsList" runat="server"/></div>
				</asp:View>
				<asp:View ID="vwNewsFull" runat="server">
					<div class="wrap"><news:NewsFull id="ctrlNewsFull" runat="server"/></div>
				</asp:View>
				<asp:View ID="wvRssLink" runat="server">
					<div class="wrap"><news:RssLink id="ctrlRssLink" runat="server" /></div>
				</asp:View>
				<asp:View ID="wvRssHeaderLink" runat="server">
					<div class="wrap"><news:RssHeaderLink id="ctrlRssHeaderLink" runat="server" /></div>
				</asp:View>				
				<asp:View ID="wvRssFeedList" runat="server">
					<div class="wrap"><news:RssFeedList id="ctrlRssFeedList" runat="server" /></div>
				</asp:View>						
	        
			</asp:MultiView>
		</div>
		<div class="formCommands">    
		    <asp:Button ID="btnSet" runat="server" OnClick="btnSet_Click" Text="Set" SkinId="Big"/>
		    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinId="Big"/>
		</div>
	</div>
</asp:Content>
