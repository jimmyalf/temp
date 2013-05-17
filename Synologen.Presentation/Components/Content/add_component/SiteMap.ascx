<%@ Control Language="C#" AutoEventWireup="true" Codebehind="SiteMap.ascx.cs" Inherits="Spinit.Wpc.Content.Presentation.Components.Content.add_component.SiteMap" %>
<div class="Component-Content-AddComponent-UnfoldingMenu-ascx fullBox">
    <div class="wrap">
        <fieldset>
            <legend>Insert Sitemap</legend>
            <div class="formItem clearLeft">
                <asp:Label ID="lblChooseNode" AssociatedControlID="drpTree" runat="server" SkinID="Long" />
                <asp:DropDownList ID="drpTree" runat="server" />
            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblShowPage" AssociatedControlID="rblShowPages" runat="server" SkinID="Long" />
                <asp:RadioButtonList ID="rblShowPages" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
                    <asp:ListItem Value="true" Selected="True">Yes</asp:ListItem>
                    <asp:ListItem Value="false">No</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblShowDefaultPage" AssociatedControlID="rblShowDefaultPage" runat="server"
                    SkinID="Long" />
                <asp:RadioButtonList ID="rblShowDefaultPage" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
                    <asp:ListItem Value="true">Yes</asp:ListItem>
                    <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblShowHiddenPage" AssociatedControlID="rblShowHiddenPages" runat="server"
                    SkinID="Long" />
                <asp:RadioButtonList ID="rblShowHiddenPages" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" AutoPostBack="false" CssClass="radioButtonItems">
                    <asp:ListItem Value="true">Yes</asp:ListItem>
                    <asp:ListItem Value="false" Selected="True">No</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblClassName" runat="server" AssociatedControlID="txtClassName" SkinID="Long" />
                <asp:TextBox ID="txtClassName" runat="server"></asp:TextBox>
            </div>
        </fieldset>
    </div>
</div>
