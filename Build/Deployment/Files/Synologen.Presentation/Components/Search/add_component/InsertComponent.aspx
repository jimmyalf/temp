<%@ Page Language="C#" MasterPageFile="~/Window.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Search.Presentation.Components.Search.Add_Component.InsertComponent" Title="Content Component" Codebehind="InsertComponent.aspx.cs" %>
<%@ Register Src="KeyotiSearch.ascx" TagName="Search" TagPrefix="keyoti" %>
<%@ Register Src="KeyotiResults.ascx" TagName="Results" TagPrefix="keyoti" %>
<%@ Register Src="SearchGlobal.ascx" TagName="Global" TagPrefix="search" %>
<%@ Register Src="SearchResult.ascx" TagName="Result" TagPrefix="search" %>

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
        <h1>Search</h1>
        <asp:PlaceHolder ID="phAddComponentIndexSearchMenu" runat="server" />
        <div class="tabsContentContainer clearAfter">
        <asp:MultiView ID="view" ActiveViewIndex="0" runat="server">
            <asp:View ID="vwKSearch" runat="server">
                <keyoti:Search ID="ctrKSearch" runat="server" />
            </asp:View>
            <asp:View ID="vwKResults" runat="server">
                <keyoti:Results ID="ctrKResults" runat="server" />
            </asp:View>
            <asp:View ID="vwSearchGlobal" runat="server">
                <search:Global ID="ctrSearchGlobal" runat="server" />
            </asp:View>
            <asp:View ID="vwSearchResult" runat="server">
                <search:Result ID="ctrSearchResult" runat="server" />
            </asp:View>            
        </asp:MultiView>
        </div>
        <div class="formCommands">
            <asp:Button ID="btnSet" runat="server" Text="Set" OnClick="btnSet_Click" SkinId="Big"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinId="Big"/>
        </div>
    </div>
</asp:Content>

