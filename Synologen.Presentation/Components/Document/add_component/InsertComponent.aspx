<%@ Page Language="C#" MasterPageFile="~/Window.master" AutoEventWireup="true" CodeBehind="InsertComponent.aspx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.add_component.InsertComponent" Title="Untitled Page" %>

<%@ Register Src="LatestDocuments.ascx" TagName="LatestDocuments" TagPrefix="uc2" %>
<%@ Register Src="DocumentRootNode.ascx" TagName="DocumentRootNode" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphWindow" runat="server">
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
                window.onload = "javascript:setHeight()";
			//-->
		</script>
		<input type="hidden" id="returnvalue" name="returnvalue" value="<%=m_return %>"/>
	    <div id="wrapper" class="wrap">
	    <h1>Document</h1>
	    <asp:PlaceHolder ID="phContentMenu" runat="server" />
	    <div class="tabsContentContainer clearAfter">
	    <asp:MultiView ID="view" ActiveViewIndex="0" runat="server">
            <asp:View ID="vwDocument" runat="server">
                <uc1:DocumentRootNode id="ctrlDocumentRootNode" runat="server">
                </uc1:DocumentRootNode>
            </asp:View>
            <asp:View ID="vwLatestDocuments" runat="server">
                <uc2:LatestDocuments id="ctrlLatestDocuments" runat="server"></uc2:LatestDocuments>
            </asp:View>
        
        </asp:MultiView>
	    
		</div>
		<div class="formCommands">    
				    <asp:Button ID="btnSet" runat="server" OnClick="btnSet_Click" Text="Set" SkinId="Big"/>
				    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinId="Big"/>
				</div>
		
		</div>
</asp:Content>
