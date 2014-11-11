<%@ Register Src="MenuTitle.ascx" TagName="MenuTitle" TagPrefix="uc2" %>
<%@ Register Src="Menu.ascx" TagName="Menu" TagPrefix="uc1" %>
<%@ Page Language="C#" MasterPageFile="~/Window.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Components.Content.Add_Component.InsertComponent" Title="Content Component" Codebehind="InsertComponent.aspx.cs" %>

<%@ Register Src="UnfoldingMenu.ascx" TagName="UnfoldingMenu" TagPrefix="uc3" %>
<%@ Register Src="BreadCrumb.ascx" TagName="BreadCrumb" TagPrefix="uc4" %>
<%@ Register Src="SiteMap.ascx" TagName="SiteMap" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphWindow" Runat="Server">
	<script language="JavaScript" type="text/javascript">
		<!--
		    function submitForm()
		    {
		        retElement = document.getElementById("returnvalue");
		        parent.Select(retElement.value);
		    }
		    
		    function setHeight() {
                if (parent == window) return;
                else parent.setIframeHeight('cmpFrame');
            }
		//-->
	</script>
    <div id="wrapper" class="wrap">
        <input type="hidden" id="returnvalue" name="returnvalue" value="<%=m_return %>"/>
        <h1>Content</h1>
        <asp:PlaceHolder ID="phContentMenu" runat="server" />
        <div class="tabsContentContainer clearAfter">
        <asp:MultiView ID="view" ActiveViewIndex="0" runat="server">
            <asp:View ID="vwMenu" runat="server">
                <uc1:Menu ID="ctrMenu" runat="server" />
            </asp:View>
            <asp:View ID="vwUnfoldingMenu" runat="server">
                <uc3:UnfoldingMenu ID="ctrUnfoldingMenu" runat="server" />
            
            </asp:View>
            <asp:View ID="vwBreadCrumb" runat="server">
                <uc4:BreadCrumb ID="ctrBreadCrumb" runat="server" />
            
            </asp:View>
            <asp:View ID="vwMenuTile" runat="server">
                <uc2:MenuTitle id="ctrMenuTitle" runat="server" />
            </asp:View>
            <asp:View ID="vwSiteMap" runat="server">
                <uc5:SiteMap id="ctrSiteMap" runat="server" />
            </asp:View>
        </asp:MultiView>
        </div>
        <div class="formCommands">
            <asp:Button ID="btnSet" runat="server" Text="Set" OnClick="btnSet_Click" SkinId="Big"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinId="Big"/>
        </div>
    </div>
</asp:Content>

