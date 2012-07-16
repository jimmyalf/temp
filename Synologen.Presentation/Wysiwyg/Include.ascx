<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Include.ascx.cs" Inherits="Spinit.Wpc.Base.Presentation.Wysiwyg.Include" %>
<link rel="stylesheet" type="text/css" href="/Common/Css/Dialogs.css" media="screen" />
<script src="<%=Spinit.Wpc.Utility.Business.Globals.ResourceUrl %>CommonControls/Wysiwyg/Scripts/tiny_mce/tiny_mce.js" type="text/javascript"></script>
<script src="<%=Spinit.Wpc.Utility.Business.Globals.ResourceUrl %>CommonControls/Wysiwyg/Scripts/tiny_mce/tiny_mce_popup.js" type="text/javascript"></script>
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
</div>
</div>