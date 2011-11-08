<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Include.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Wysiwyg.Include" %>
<%@ Register Src="~/Common/StyleSheetLoader.ascx" TagName="SSLoader" TagPrefix="userControl" %>
<%@ Register Src="~/Common/JavaScriptLoader.ascx" TagName="JSLoader" TagPrefix="userControl" %>
<userControl:SSLoader ID="SSLoader" runat="server" />
<userControl:JSLoader ID="JSLoader" runat="server" />
<link rel="stylesheet" type="text/css" href="/Common/Css/Dialogs.css" media="screen" />
<script src="/Common/Js/WPC-Wysiwyg-Include.js" type="text/javascript"></script>
<div class="Wysiwyg-Dialog">
<h1>Insert include page</h1>

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


<div class="fullBox formCommands">
	<asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
	<input type="button" value="Close" OnClick="javascript:Cancel()" />
	<input type="button" value="Remove" OnClick="javascript:Remove()" />
</div>
</div>