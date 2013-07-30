<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Components.News.AddComponent.NewsList" Codebehind="NewsList.ascx.cs" %>
<div class="Component-News-AddComponent-NewsList-ascx fullBox">
<div class="wrap">
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
		<div class="formItem">
		    <asp:Label ID="lblNewsCat" runat="server" AssociatedControlID="drpNewsCat" SkinId="Long"/>
		    <asp:DropDownList ID="drpNewsCat" runat="server"/>
		</div>
		<div class="formItem">
		    <asp:Label ID="lblMax" runat="server" AssociatedControlID="txtMax" SkinId="Long"/>
            <asp:TextBox ID="txtMax" runat="server"></asp:TextBox>
		</div>
		
	</fieldset>
</div>
</div>