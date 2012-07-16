<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Components.Content.Add_Component.BreadCrumb" Codebehind="BreadCrumb.ascx.cs" %>
<div class="Component-Content-AddComponent-BreadCrumb-ascx fullBox">
<div class="wrap">
	<fieldset>
        <legend>Insert breadcrumb</legend>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblShowDefaultPage" AssociatedControlID="rblShowDefaultPage" runat="server" SkinID="Long" />
	        <asp:RadioButtonList ID="rblShowDefaultPage" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
	            <asp:ListItem Value="true">Yes</asp:ListItem>
	            <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
	        </asp:RadioButtonList>
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblShowPage" AssociatedControlID="rblShowPage" runat="server" SkinID="Long" />
	        <asp:RadioButtonList ID="rblShowPage" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
	            <asp:ListItem Value="true">Yes</asp:ListItem>
	            <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
	        </asp:RadioButtonList>
	    </div>	    
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblShowLanguageLevel" AssociatedControlID="rblShowLanguageLevel" runat="server" SkinID="Long" />
	        <asp:RadioButtonList ID="rblShowLanguageLevel" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
	            <asp:ListItem Value="true">Yes</asp:ListItem>
	            <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
	        </asp:RadioButtonList>
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblLinkPage" AssociatedControlID="rblLinkPage" runat="server" SkinID="Long" />
	        <asp:RadioButtonList ID="rblLinkPage" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
	            <asp:ListItem Value="true">Yes</asp:ListItem>
	            <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
	        </asp:RadioButtonList>
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblRootName" runat="server" AssociatedControlID="txtRootName" SkinID="Long" />
	        <asp:TextBox ID="txtRootName" runat="server" Text="Home"></asp:TextBox>
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblSeparator" runat="server" AssociatedControlID="txtSeparator" SkinID="Long" />
	        <asp:TextBox ID="txtSeparator" runat="server" Text="&nbsp;/&nbsp;"></asp:TextBox>
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblClassName" runat="server" AssociatedControlID="txtClassName" SkinID="Long" />
	        <asp:TextBox ID="txtClassName" runat="server"></asp:TextBox>
	    </div>
	    <div class="formItem clearLeft">
	        <asp:Label ID="lblMaxLength" runat="server" AssociatedControlID="txtMaxLength" SkinID="Long" />
	        <asp:TextBox ID="txtMaxLength" runat="server" Text="-1"></asp:TextBox>
	    </div>   	    
    </fieldset>
</div>
</div>
