<%@ Register Src="Delete.ascx" TagName="Delete" TagPrefix="uc6" %>
<%@ Register Src="NewFolder.ascx" TagName="NewFolder" TagPrefix="uc5" %>
<%@ Register Src="CopyMove.ascx" TagName="CopyMove" TagPrefix="uc2" %>
<%@ Register Src="~/files/Upload.ascx" TagName="Upload" TagPrefix="uc1" %>
<%@ Register Src="~/files/PropertiesManager.ascx" TagName="PropertiesManager" TagPrefix="uc4" %>
<%@ Register Src="~/files/ImageManager.ascx" TagName="ImageManager" TagPrefix="uc3" %>
<%@ Page Language="C#" MasterPageFile="~/BaseMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.Files.FileMain" MaintainScrollPositionOnPostback="True" Codebehind="FileMain.aspx.cs" %>
<asp:Content ID="MainComponent" ContentPlaceHolderID="ComponentContent" Runat="Server">
<script type="text/javascript">
<!--
            var dcTime=250;    // doubleclick time
            var dcDelay=100;   // no clicks after doubleclick
            var dcAt=0;        // time of doubleclick
            var savEvent=null; // save Event for handling doClick().
            var savEvtTime=0;  // save time of click event.
            var savTO=null;    // handle of click setTimeOut
            var id=null;
	       
	        // Handle events
	        function handleEvent(evn, input, type)
	        {
	            id = input;
	            
                switch (evn) {
                    case "click": 
                       // If we've just had a doubleclick then ignore it
                        if (hadDoubleClick()) {
                            return false;
                        }
                     
                         // Otherwise set timer to act.  It may be preempted by a doubleclick.
                         savEvent = evn;
                         d = new Date();
                         savEvtTime = d.getTime();
                         savTO = setTimeout("properties()", dcTime);
                         break;
                    case "dblclick":
                       if (type == "directory") {
                            changeDirectory();
                       }
                       break;
                    default:
                }
	        }
	        
	        // Check to see if double-click
            function hadDoubleClick() 
            {
                var d = new Date();
                var now = d.getTime();
                if((now - dcAt) < dcDelay) {
                    return true;
                }
                return false;
            }
   
	        // Fires whe a directory is double-clicked.
	        function changeDirectory()
	        {
               var d = new Date();
                dcAt = d.getTime();
                if (savTO != null) {
                    clearTimeout(savTO);          // Clear pending Click  
                    savTO = null;
                }
	            document.getElementById("commandtype").value = "changedirectory";
	            document.getElementById("commandparameter").value = id;
	            document.forms [0].submit();
	        }

	        // Fires whe a picture is double-clicked.
	        function properties()
	        {
                // preempt if DC occurred after original click.
                if (savEvtTime - dcAt <= 0) {
                    return false;
                }
	            document.getElementById("commandtype").value = "properties";
	            document.getElementById("commandparameter").value = id;
	            document.forms [0].submit();
	        }
//-->
</script>
<div>
	<input type="hidden" name="commandtype" id="commandtype" value="" />
	<input type="hidden" name="commandparameter" id="commandparameter" value="" />
</div>
<div id="dCompNavigation">
	<asp:PlaceHolder ID="phFilesMenu" runat="server" />
</div>
<div id="dCompMain" class="Files-FilesMain-aspx">

	<asp:ValidationSummary ID="validationSummary" runat="server" EnableViewState="False" HeaderText="An error occured" SkinId="Error" />

	<div class="fullBox">
	<div class="wrap">
		<h1>Files</h1>

		<fieldset id="fsFilesLocation" runat="server">
		<legend>Locations</legend>
		<div class="formItem">
			<asp:Label runat="server" AssociatedControlID="drpLocation" SkinID="Long">Show folders and files from location</asp:Label>
			<asp:DropDownList ID="drpLocation" runat="server" OnSelectedIndexChanged="drpLocation_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
		</div>
		</fieldset>
	</div>
	</div>
	
	<div class="tableContainer clearLeft">
	<div class="displayAsTable"><div class="tableBody"><div class="tableRow">
		<div class="tableCell">
		<div class="wrap">
		<h2>Folders/Files</h2>
		<table class="striped fixedPadding" summary="List of files and folders">
		<caption>Folders and files</caption>
		<thead>
		<tr>
			<td colspan="6">
			<div>
				<asp:Label ID="Label1" runat="server" AssociatedControlID="lblUrl">Folder url:</asp:Label>
				<asp:Label ID="lblUrl" runat="server"></asp:Label>
			</div>
			<div class="clearLeft">
				<asp:LinkButton ID="btnLnkParentFolder" runat="server" Text="Parent folder" ToolTip="Go to the parent folder" CausesValidation="False" OnClick="btnLnkParentFolder_Click" />
			</div>
			
			</td>
		</tr>
		<tr>
			<th scope="col" id="colSelect">Select</th>
			<th scope="col" id="colThumbnails">Thumbnails</th>
			<th scope="col">Name</th>
			<th scope="col">Modified</th>
			<th scope="col">Description</th>
			<th scope="col" id="colActions">Actions</th>
		</tr>
		</thead>
		<tbody>
		<asp:Repeater ID="rptFiles" runat="server" DataMember="tblFiles" OnItemCommand="rptFiles_ItemCommand" OnItemCreated="rptFiles_ItemCreated">
		<itemtemplate>
		<tr>
			<td><asp:CheckBox ID="chkSelect" runat="server" /></td>
			<td><%# DataBinder.Eval(Container.DataItem, "Img") %></td>
			<td>
				<%# DataBinder.Eval(Container.DataItem, "Name") %><br />
				<%# DataBinder.Eval(Container.DataItem, "Size") %>
			</td>
			<td><%# DataBinder.Eval(Container.DataItem, "Date") %></td>
			<td><%# DataBinder.Eval(Container.DataItem, "Description") %></td>
			<td>
				<asp:LinkButton ID="btnMove" runat="server" Text="Move" ToolTip="Move" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> /><br />
				<asp:LinkButton ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
			</td>
		</tr>
		</itemtemplate>
		</asp:Repeater>
		</tbody>
		</table>
		</div>
		</div>
	   
		<div id="dFilesSidebar" runat="server" visible="false" class="tableCell filesSiderbar">
		<div class="margin">
		<div class="wrap">
			<div class="sidebarClose"><asp:LinkButton ID="btnCloseSidebar" runat="server" Text="Close" ToolTip="Close sidebar" OnClick="btnCloseSidebar_Click" CausesValidation="False" /></div>
			<uc4:PropertiesManager id="prpManager" Visible="false" runat="server" />
			<uc1:Upload id="uplManager" Visible="false" runat="server" />
			<uc2:CopyMove Id="cpyManager" Visible="false" runat="server" />
			<uc5:NewFolder id="creManager" Visible="false" runat="server" />
			<uc6:Delete ID="dleManager" Visible="false" runat="server" />
			<uc3:ImageManager id="imgManager" Visible="false" runat="server" />
		</div>
		</div>
		</div>
	</div></div></div>
	</div>
</div>
</asp:Content>
