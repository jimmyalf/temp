<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Components.Content.Add_Component.Menu" Codebehind="Menu.ascx.cs" %>
<div class="Component-Content-AddComponent-Menu-ascx fullBox">
<div class="wrap">
	<fieldset>
        <legend>Insert menu.</legend>
	    <div class="formItem">
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblShowDefaultPage" AssociatedControlID="rblShowDefaultPage" runat="server" SkinID="Long" />
	        <asp:RadioButtonList ID="rblShowDefaultPage" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
	            <asp:ListItem Value="true">Yes</asp:ListItem>
	            <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
	        </asp:RadioButtonList>
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblShowRootPage" AssociatedControlID="rblShowRootPage" runat="server" SkinID="Long" />
	        <asp:RadioButtonList ID="rblShowRootPage" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
	            <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
	            <asp:ListItem Value="false">No</asp:ListItem>
	        </asp:RadioButtonList>
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblStartLevel" runat="server" AssociatedControlID="txtStartLevel" SkinID="Long" />
	        <asp:TextBox ID="txtStartLevel" runat="server"></asp:TextBox>				
            <asp:RangeValidator ID="vldrStartLevel" Display="Dynamic" runat="server" ControlToValidate="txtStartLevel"
                ErrorMessage="Only numbers allowed!" MaximumValue="1000" MinimumValue="-1" Type="Integer"></asp:RangeValidator>	        
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblStopLevel" runat="server" AssociatedControlID="txtStopLevel" SkinID="Long" />
	        <asp:TextBox ID="txtStopLevel" runat="server"></asp:TextBox>&nbsp;
            <asp:RangeValidator ID="vldrStopLevel" Display="Dynamic" runat="server" ControlToValidate="txtStopLevel"
                ErrorMessage="Only numbers allowed!" MaximumValue="1000" MinimumValue="-1" Type="Integer"></asp:RangeValidator>
        </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblClassName" runat="server" AssociatedControlID="txtClassName" SkinID="Long" />
	        <asp:TextBox ID="txtClassName" runat="server"></asp:TextBox>
	    </div>	    	    
    </fieldset>
</div>
</div>