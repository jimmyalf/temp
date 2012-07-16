<%@ Page Language="C#" MasterPageFile="~/Window.master" AutoEventWireup="true" Inherits="Spinit.Wpc.QuickForm.Presentation.Components.QuickForm.AddComponent.InsertComponent" Title="Untitled Page" Codebehind="InsertComponent.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphWindow" Runat="Server">
		<script language="JavaScript">
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
	    <input type="hidden" id="returnvalue" name="returnvalue" value="<%=_retVal %>"/>
	    <div class="wrap">
			<fieldset>
				<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
				
				<div class="formItem">
				    <asp:Label ID="lblFrm" runat="server" AssociatedControlID="drpFrm" SkinId="Long"/>
				    <asp:DropDownList ID="drpFrm" runat="server"/>
				</div>
				<div class="formItem">
				    <asp:Label ID="lblViewName" runat="server" AssociatedControlID="txtViewName" SkinId="Long"/>
				    <asp:TextBox ID="txtViewName" runat="server" />
				</div>
				<div class="formCommands">    
				    <asp:Button ID="btnSet" runat="server" OnClick="btnSet_Click" Text="Set" SkinId="Big"/>
				    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinId="Big"/>
				</div>
			</fieldset>
		</div>
</asp:Content>

