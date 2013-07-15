<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Components.Content.Add_Component.MenuTitle" Codebehind="MenuTitle.ascx.cs" %>
<div class="Component-Content-AddComponent-MenuTile-ascx fullBox">
    <div class="wrap">
	    <fieldset>
            <legend>Insert menu title</legend>
	        <div class="formItem">
                <asp:Label ID="lblMenuType" runat="server" AssociatedControlID="drpMenuType" SkinId="Long" />
                <asp:DropDownList ID="drpMenuType" runat="server">
                    <asp:ListItem Value="0">Show the page title</asp:ListItem>
                    <asp:ListItem Value="1">Show the parent menu title</asp:ListItem>
                    <asp:ListItem Value="2">Show the first level menu title</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblClassName" runat="server" AssociatedControlID="txtClassName" SkinID="Long" />
                <asp:TextBox ID="txtClassName" runat="server"></asp:TextBox>
            </div>	    	                
	    </fieldset>
    </div>
</div>