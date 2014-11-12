<%@ Page Language="C#" MasterPageFile="~/components/Document/DocumentMain.master" AutoEventWireup="true" CodeBehind="Documents.aspx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.Documents" Title="Untitled Page" %>

<%@ Register Src="MoveManager.ascx" TagName="MoveManager" TagPrefix="uc4" %>

<%@ Register Src="PropertiesManager.ascx" TagName="PropertiesManager" TagPrefix="uc3" %>
<%@ Register Src="NewFolder.ascx" TagName="NewFolder" TagPrefix="uc1" %>
<%@ Register Src="Upload.ascx" TagName="Upload" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="phDocument" runat="server">
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
                         if (type == "file") {
                            savTO = setTimeout("properties()", dcTime);
                         }
                         if (type == "directory") {
                            savTO = setTimeout("directory()", dcTime);
                         }
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

	        // Fires when a picture is clicked.
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
	        
			// Fires when a directory is clicked.
	        function directory()
	        {
                // preempt if DC occurred after original click.
                if (savEvtTime - dcAt <= 0) {
                    return false;
                }
	            document.getElementById("commandtype").value = "directory";
	            document.getElementById("commandparameter").value = id;
	            document.forms [0].submit();
	        }

//-->
</script>
<div>
	<input type="hidden" name="commandtype" id="commandtype" value="" />
	<input type="hidden" name="commandparameter" id="commandparameter" value="" />
</div>
<div id="dCompMain" class="Components-Document-Documents-aspx">
	<div class="fullBox">
	<div class="wrap">
		<h1>Documents</h1>

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
			<div class="floatLeft">
				<asp:LinkButton ID="btnLnkParentFolder" runat="server" Enabled="false" Text="Parent folder" ToolTip="Go to the parent folder" CausesValidation="False" OnClick="btnLnkParentFolder_Click" />
			</div>
			
			<div class="floatRight">
				<asp:Label ID="Label2" runat="server" AssociatedControlID="lblUrl" Visible="false">Document url:</asp:Label>
				<asp:Label ID="lblUrl" runat="server" Visible="false"></asp:Label>
			</div>
			</td>
		</tr>
		<tr>
			<th scope="col"></th>
			<th scope="col"></th>
			<th scope="col">Name</th>
			<th scope="col">Modified</th>
			<th scope="col">Description</th>
			<th scope="col"></th>
			<th scope="col"></th>
			<th scope="col"></th>
		</tr>
		</thead>
		<tbody>
		<asp:Repeater ID="rptFiles" runat="server" OnItemDataBound="rptFiles_ItemDataBound" DataMember="tblFiles" OnItemCommand="rptFiles_ItemCommand" OnItemCreated="rptFiles_ItemCreated">
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
			<td><asp:Button ID="btnView" Visible="false" runat="server" Text="View" ToolTip="View document" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
                <asp:Button ID="btnOpen" Visible="false" runat="server" Text="Open" ToolTip="Open folder" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> /></td>
			<td><asp:Button ID="btnEditDocument" Visible="false" runat="server" Text="Edit" ToolTip="Edit document properties" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
			    <asp:Button ID="btnEditNode" Visible="false" runat="server" Text="Edit" ToolTip="Edit folder properties" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> /></td>	
			<td>
			    <asp:Button ID="btnMoveUp" Visible="false" runat="server" Text="Up" ToolTip="Move up" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
			    <asp:Button ID="btnMoveDown" Visible="false" runat="server" Text="Down" ToolTip="Move down" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
				<asp:Button ID="btnMove" Visible="false" runat="server" Text="Move" ToolTip="Move" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
				<asp:Button ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete document" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
				<asp:Button ID="btnDeleteNode" runat="server" Text="Delete" ToolTip="Delete folder" CausesValidation="False" CommandArgument=<%# DataBinder.Eval(Container.DataItem, "Id") %> />
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
			<div class="sidebarClose"><asp:LinkButton ID="btnCloseSidebar" runat="server" Text="Close" ToolTip="Close sidebar" OnClick="btnCloseSidebar_Click" /></div>
			<uc1:NewFolder id="creManager" Visible="false" runat="server">
            </uc1:NewFolder>
            <uc2:Upload id="uplManager" Visible="false" runat="server">
            </uc2:Upload>
            <uc3:PropertiesManager id="prpManager" runat="server">
            </uc3:PropertiesManager>
            <uc4:MoveManager id="MoveManager1" runat="server">
            </uc4:MoveManager>
                  
		</div>
		</div>
		</div>
	</div>
	</div>
	</div>
	</div>
</div>

</asp:Content>


