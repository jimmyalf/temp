<%@ Page Language="C#" MasterPageFile="~/Window.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Courses.Presentation.Components.Courses.Add_Component.InsertComponent" Codebehind="InsertComponent.aspx.cs" %>
<%@ Register Src="CategoryMainList.ascx" TagName="categoryMainList" TagPrefix="uc" %>
<%@ Register Src="MainCourseList.ascx" TagName="mainCourseList" TagPrefix="uc" %>
<%@ Register Src="CourseSpot.ascx" TagName="courseSpot" TagPrefix="uc" %>
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
	<h1>Courses</h1>
	<asp:PlaceHolder ID="phContentMenu" runat="server" />
	<div class="tabsContentContainer clearAfter">
		<asp:MultiView ID="view" ActiveViewIndex="0" runat="server">
			<asp:View ID="vwCourseList" runat="server">
				<uc:categoryMainList id="ctrlCategoryMainList" runat="server" />
			</asp:View>     				
			<asp:View ID="vwMainCourseList" runat="server">			
				<uc:mainCourseList id="ctrlMainCourseList" runat="server" />
			</asp:View>     				
			<asp:View ID="vwCourseSpot" runat="server">			
				<uc:courseSpot id="ctrlCourseSpot" runat="server" />
			</asp:View>        
		</asp:MultiView>
	</div>
	
	<div class="formCommands">    
		<asp:Button ID="btnSet" runat="server" OnClick="btnSet_Click" Text="Set" SkinId="Big"/>
		<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinId="Big"/>
	</div>
</div>
</asp:Content>
