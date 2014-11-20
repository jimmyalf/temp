<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Wysiwyg.InternalLinkFirstPage" Codebehind="InternalLinkFirstPage.ascx.cs" %>
<div class="InternalLinkFirstPage-ascx">
<div id="dMainColumn">
	<fieldset>
	<legend>Name</legend>
	    <div class="formItem">
		    <asp:Label ID="lblLinkText" runat="server" AssociatedControlID="txtLinkText" SkinID="Long">Link text</asp:Label>
		    <asp:TextBox ID="txtLinkText" runat="server"></asp:TextBox>
	    </div>

	    <div class="formItem">
		    <asp:Label ID="lblTooltip" runat="server" AssociatedControlID="txtTooltip" SkinID="Long">Title</asp:Label>
		    <asp:TextBox ID="txtTooltip" runat="server"></asp:TextBox>
	    </div>
	    <div class="formItem">
	    	<asp:Label ID="lblTargetPreset" runat="server" AssociatedControlID="drpTarget" SkinID="Long">Target preset</asp:Label>
			<asp:DropDownList ID="drpTarget" runat="server" AutoPostBack="True">
			    <asp:ListItem Value="external-link">New Window</asp:ListItem>
		    </asp:DropDownList>
	    </div>
	</fieldset>
</div>
<div id="dSecondaryColumn">
    <fieldset>
    <legend>Choose page</legend>
	    <div class="formItem">
		    <asp:Label ID="lblLocations" runat="server" AssociatedControlID="drpLocations" SkinID="Long">Choose location</asp:Label>
		    <asp:DropDownList ID="drpLocations" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpLocations_SelectedIndexChanged">
	    </asp:DropDownList>
	    </div>
	    
		<div id="dTreeContainer">
        <asp:TreeView 
            ID="treLinks" 
            runat="server" 
            PathSeparator="#" 
            OnSelectedNodeChanged="treLinks_SelectedNodeChanged"
            OnAdaptedSelectedNodeChanged="treLinks_SelectedNodeChanged">
        </asp:TreeView>	
        </div>
	</fieldset>
</div>
</div>