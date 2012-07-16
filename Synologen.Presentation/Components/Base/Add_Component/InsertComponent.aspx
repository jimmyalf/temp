<%@ Page Language="C#" MasterPageFile="~/Window.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Components.Base.Add_Component.InsertComponent" Title="Untitled Page" Codebehind="InsertComponent.aspx.cs" %>
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
				
				<div class="formItem clearLeft">
				    <asp:Label ID="lblControl" runat="server" AssociatedControlID="drpControl" SkinId="Long"/>
				    <asp:DropDownList ID="drpControl" runat="server" AutoPostBack="True" 
						onselectedindexchanged="drpControl_SelectedIndexChanged"/>
				</div>
				<asp:PlaceHolder ID="phRedirectControl" runat="server" Visible="false">
					<div class="formItem clearLeft">
						<asp:Label ID="lblRedirectPage" runat="server" AssociatedControlID="txtRedirectPage" SkinID="Long" />
						<asp:TextBox ID="txtRedirectPage" runat="server"></asp:TextBox>
					</div>	    	    
				</asp:PlaceHolder>
				<div class="formCommands">    
				    <asp:Button ID="btnSet" runat="server" OnClick="btnSet_Click" Text="Set" SkinId="Big"/>
				    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinId="Big"/>
				</div>
			</fieldset>
		</div>

</asp:Content>

