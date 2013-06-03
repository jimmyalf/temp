<%@ Control Language="C#" %>
<%@ Import Namespace="Spinit.Wpc.Forum.Components" %>
<%@ Register TagPrefix="Forums" Namespace="Spinit.Wpc.Forum.Controls" Assembly="Spinit.Wpc.Forum.Controls" %>
<%@ Register TagPrefix="ftb" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<!-- ********* View-CreateEditPost.ascx:Start ************* //-->	
<script language="JavaScript">
function InsertText(textToInsert) {
  //if (window.navigator.appName.toLowerCase().indexOf("netscape") > -1) {
  if (!FTB_GetIFrame) {
    document.PostForm.<%=PostBody.UniqueID.Replace(":","_") %>.value += textToInsert +" ";
    document.PostForm.<%=PostBody.UniqueID.Replace(":","_") %>.focus();
  } else {
  	FTB_InsertText("<%=PostBodyRichText.ClientID %>",textToInsert);
    <%=PostBodyRichText.ClientID + "_Editor" %>.focus();
  }

/*
  if (window.navigator.appName.toLowerCase().indexOf("netscape") > -1) {
    document.PostForm.<%=PostBody.UniqueID.Replace(":","_") %>.value += textToInsert +" ";
    document.PostForm.<%=PostBody.UniqueID.Replace(":","_") %>.focus();
  } else {
    <%=PostBodyRichText.UniqueID.Replace(":","_") + "_editor" %>.focus();
    sel = <%=PostBodyRichText.UniqueID.Replace(":","_") + "_editor" %>.document.selection.createRange();
    html = textToInsert;
    sel.pasteHTML(html);
  }
 */
}


function AddTo() {

  var EmailAddres = new Object();
  var _rv
  EmailAddres.to = document.PostForm.<%=To.UniqueID.Replace(":","_") %>.value;

  _rv = OpenWindow('EditFavorites','530','475','','','','no','no','no','no','/',EmailAddres);

  document.PostForm.<%=To.UniqueID.Replace(":","_") %>.value = _rv.to;

}

function OpenWindow (strName,iW,iH,TOP,LEFT,R,S,SC,T,TB,URL,dArg) {

    var sF=""
    var _rv
    sF+=T?'unadorned:'+T+';':'';
    sF+=TB?'help:'+TB+';':'';
    sF+=S?'status:'+S+';':'';
    sF+=SC?'scroll:'+SC+';':'';
    sF+=R?'resizable:'+R+';':'';
    sF+=iW?'dialogWidth:'+iW+'px;':'';
    sF+=iH?'dialogHeight:'+iH+'px;':'';
    sF+=TOP?'dialogTop:'+TOP+'px;':'';
    sF+=LEFT?'dialogLeft:'+LEFT+'px;':'';

    var _R = window.showModalDialog("/", sF);

    return _R;

}

</script>

<!-- 
<table cellpadding="" cellspacing="" align="center" width="75%">
    <tr>
        <td align="left" valign="middle" class="txt4">
			<br />
			&nbsp;<Forums:BreadCrumb ShowHome="true" runat="server" ID="Breadcrumb1"/>
			<br /><br /><br />
		</td>        
    </tr>
</table>
//-->	
<table cellpadding="" cellspacing="" align="center" width="80%">    
	<tr>
		<td>
			<table class="tableBorder" cellSpacing="1" cellPadding="3" align="center" width="100%">
			
    <tr>
        <td class="column" align="left" height="25">
            &nbsp;<asp:label id="PostTitle" runat="server"></asp:label>
        </td>
    </tr>
	<span id="ReplyTo" runat="server" visible="false">
    <tr>
        <td class="f" align="left">
            <table cellSpacing="1" cellPadding="3">
                <tr>
                    <td colSpan="2" class="txt3"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_Inline1") %></td>
                </tr>
                <tr>
                    <td vAlign="top" align="left" class="txt3"><b><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_Inline2") %></b><br />
                    <b><asp:hyperlink Target="_blank" id="ReplyPostedBy" Runat="server" /></b><asp:label id="ReplyPostedByDate" Runat="server" ></asp:label></td>
                </tr>
                <tr>
                    <td vAlign="top" align="left" class="txt3"><b><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_Inline3") %></b><br />
                    <b><asp:hyperlink Target="_blank" id="ReplySubject" runat="server" /></b></td>
                </tr>
                <tr>
                    <td vAlign="top" align="left" class="quoteTable"><b><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_Inline4") %></b>
                    <asp:label CssClass="txt4" id="ReplyBody" runat="server"></asp:label></td>
                </tr>
            </table>
        </td>
    </tr>

	</span>
	<span id="Preview" runat="server" visible="false">

        <tr>
            <td class="f">
                <table cellSpacing="1" cellPadding="3" width="100%" class="tableBorder" border="0">
                    <tr>
                        <td class="f" vAlign="top" align="left">
                            <b><asp:label id="PreviewSubject" runat="server"></asp:label></b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="left" class="fh3">
                            <asp:label id="PreviewBody" runat="server"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="f" vAlign="top" align="right">
                            <asp:button id="BackButton" Runat="server"></asp:button>&nbsp;
                            <asp:button id="PreviewPostButton" Runat="server"></asp:button></td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
   
	</span>
	<span id="Post" runat="server" visible="true">

        <tr>
            <td class="f">
                <table cellSpacing="1" cellPadding="3">
                    <tr>                        
                        <td vAlign="top" align="left" colSpan="2" class="txt3"><span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_Inline5") %></span><asp:label id="PostAuthor" runat="server" /></td>
                    </tr>
                    <tr id="MessageTo" runat="server" visible="false">
                      <td vAlign="top" align="left" colSpan="2">To:<br /><asp:textbox enabled="false" autocomplete="off" id="To" runat="server" columns="83"></asp:textbox></td>
                    </tr>
                    <tr id="Edit" runat="server" visible="false">
                        <td vAlign="top" align="left" colSpan="2" class="txt3"><span class="txt3Bold"><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_Inline6") %></span><br />
                        <asp:label id="PostEditor" runat="server" /></td>
                    </tr>
                    <tr>
                        <td class="txt3Bold"><asp:textbox autocomplete="off" id="PostSubject" runat="server" columns="83"></asp:textbox><br />
                        <asp:requiredfieldvalidator id="postSubjectValidator" runat="server" CssClass="validationWarning" ControlToValidate="PostSubject">*</asp:requiredfieldvalidator></td>
                    </tr>
                    <tr>
                        <span id="PostBodyTextBox" Visible="false" runat="server">
                          <td vAlign="top" class="txt3Bold"><asp:textbox id="PostBody" runat="server" columns="83" TextMode="MultiLine" rows="20"></asp:textbox><br />
                          <asp:requiredfieldvalidator id="postBodyValidator" runat="server" CssClass="validationWarning" ControlToValidate="PostBody" EnableClientScript="False">*</asp:requiredfieldvalidator></td>
                        </span>

                        <span id="PostBodyRichTextBox" runat="server">
                          <td vAlign="top" align="left">
                          <FTB:FreeTextBox id="PostBodyRichText" StripAllScripting="true" SupportFolder="~/FreeTextBox/" runat="server" Height="400px" EnableHtmlMode="false" AutoGenerateToolbarsFromString=false>
							<Toolbars>
								<FTB:Toolbar runat="server">
									<FTB:Bold runat="server" />
									<FTB:Italic runat="server" />		
									<FTB:Underline runat="server" />
									<FTB:StrikeThrough runat="server" />
									<FTB:SuperScript runat="server" />
									<FTB:SubScript runat="server" />
									
									<FTB:ToolbarSeparator runat="server"  />	
									<FTB:JustifyLeft runat="server" />
									<FTB:JustifyCenter runat="server" />
									<FTB:JustifyRight runat="server" />
									<FTB:JustifyFull runat="server" />
									
									<FTB:ToolbarSeparator runat="server"  />
									<FTB:Indent runat="server" />
									<FTB:Outdent runat="server" />	
									<FTB:BulletedList runat="server" />
									<FTB:NumberedList runat="server" />	
									
									<FTB:ToolbarSeparator runat="server"  />
									<FTB:Cut runat="server" />
									<FTB:Copy runat="server" />	
									<FTB:Paste runat="server" />								
									
									<FTB:ToolbarSeparator runat="server"  />
									<FTB:CreateLink runat="server" />
									<FTB:UnLink runat="server" />	
									<FTB:RemoveFormat runat="server" />
									<FTB:Undo runat="server" />	
									<FTB:Redo runat="server" />
								</FTB:Toolbar>
                          	</Toolbars>
                          </FTB:FreeTextBox>
                          <br /><asp:requiredfieldvalidator id="postBodyRichTextValidator" runat="server" CssClass="validationWarning" ControlToValidate="PostBodyRichText" EnableClientScript="False" >*</asp:requiredfieldvalidator>
                        </span>
                    </tr>
                    
                    <tr id="EditNotes" runat="server" visible="false">
                        <td vAlign="top" align="left" class="txt3"><b><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_EditNotes") %></b><br />
                        <asp:textbox id="EditNotesBody" runat="server" columns="90" TextMode="MultiLine" rows="5"></asp:textbox><asp:requiredfieldvalidator id="editNotesValidator" runat="server" CssClass="validationWarning" ControlToValidate="EditNotesBody" EnableClientScript="False"/></td>
                    </tr>
                    <tr id="CurrentEditNotes" runat="server" visible="false">
                        <td vAlign="top" align="left" colSpan="2" class="txt3"><b><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_CurrentEditNotes") %></b><br />
                        <asp:textbox ReadOnly="true" id="CurrentEditNotesBody" runat="server" columns="90" TextMode="MultiLine" rows="5"></asp:textbox></td>
                    </tr>
                    <tr id="Attachements" runat="server" visible="false">
                        <td vAlign="top" align="left" colSpan="2"><b><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_Inline12") %></b>&nbsp;<input id="FileToUpload" type="file" runat="server" NAME="FileToUpload"></td>
                    </tr>
                    <tr id="AllowPinnedPosts" runat="server" visible="false">
                        <td vAlign="top" align="left" class="txt3"><b><% = Spinit.Wpc.Forum.Components.ResourceManager.GetString("CreateEditPost_Inline13") %></b><br />
                        <asp:dropdownlist id="PinnedPost" runat="server"></asp:dropdownlist></td>
                    </tr>
                    <tr>
                        <td vAlign="top" align="left" class="txt3"><asp:checkbox id="IsLocked" runat="server" /></td>
                    </tr>
                    <tr>
                        <td vAlign="top" align="left" class="txt3"><asp:checkbox id="SubscribeToThread" Checked="true" runat="server"/></td>
                    </tr>
                    <tr>
                        <td vAlign="top" align="left" colSpan="2" class="txt3"><asp:button id="PostButton" Runat="server" />&nbsp;<asp:button CausesValidation="false" id="Cancel" Runat="server" />&nbsp;
                            </td>
                    </tr>
                    <tr>
                        <td vAlign="top" align="left" colSpan="2" class="txt3">
                            <asp:button id="PreviewButton" Runat="server"/>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
</span>      
			</table>
		</td>
	</tr>
</table>
<!-- ********* View-CreateEditPost.ascx:End ************* //-->	