<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.WpcWysiwyg" Codebehind="WpcWysiwyg.ascx.cs" %>
<link rel="Stylesheet" media="all" href="<%=Spinit.Wpc.Base.Business.Globals.BaseUrl%>common/css/WYSIWYG.css" />
<div class="edtWpcWysiwyg">
<radE:RadEditor ID="edtWpcWysiwyg" runat="server" Width="390px" height="500px">
</radE:RadEditor>
<% if (m_enabled) { %>
<script type="text/javascript">
        //<![CDATA[
        var theEditor = null;
        var selected = '';
        RadEditorCommandList["InternalLink"] =
                function image(commandName, editor, oTool)
                {                
                   theEditor = editor;
                   var args = editor.GetDialogParameters(commandName);
                    
                   var sel = theEditor.GetSelectionHtml();
                                       
                   if (sel.length == 0) {
                       var tRegExp = new RegExp("intelnal=[\\w]+", "i");
                       var tResult = sel.match (tRegExp);
                       var target = null;
                       if (tResult) {                      
                            sel = theEditor.GetSelection().GetParentElement().outerHTML;
                            selected = sel;
                       }
                   }
                    				    
                   for (var item in args) {
                        if (item == "select") {
                            args[item] = sel;
                        }
                   }
                    				    
                   editor.ShowDialog(
                        "<%=m_root %>InternalLink.aspx"
                        , args //argument
                        , 700
                        , 550
                        , callBackInternalLinkPtr
                        , null
                        , "Internal Links");
                };

           function callBackInternalLinkPtr(returnValue)
            {
                if (returnValue) {
                    if (returnValue == 'Remove text') {
                        theEditor.GetSelection().GetParentElement().outerHTML = "";
                        return;
                    }
                }
               if ((selected.length != 0) && returnValue) {
                    theEditor.GetSelection().GetParentElement().outerHTML = "";
                    select = '';
              }
                if (returnValue) {
                    theEditor.PasteHtml(returnValue);
                }
            };
    
         RadEditorCommandList["RemoveInternalLink"] =
                function image(commandName, editor, oTool)
                {                
                   theEditor = editor;
                   theEditor.GetSelection().GetParentElement().outerHTML = "";
                };
                
            RadEditorCommandList["Component"] =
                function flash(commandName, editor, oTool)
                {
                    theEditor = editor;
                    var args = editor.GetDialogParameters(commandName);
				    
                    var sel = theEditor.GetSelectionHtml();
                                                                              
                    if (sel.length > 0) {
                        var tRegExp = new RegExp("componentid=[\\w]+", "i");
                        var tResult = sel.match (tRegExp);
                        var target = null;
                        if (tResult) {                      
                            sel = theEditor.GetSelection().GetParentElement().parentNode.outerHTML;
                            selected = sel;
                       }
                    }

                    for (var item in args) {
                        if (item == "select") {
                            args[item] = sel;
                        }
                    }
				    
                    editor.ShowDialog(
                        "<%=m_root %>Component.aspx"
                        , args //argument
                        , 700
                        , 420
                        , callBackComponentPtr
                        , null
                        , "Component Manager");
                };

            function callBackComponentPtr(returnValue)
            {
                if (returnValue) {
                    if (returnValue == 'Remove text') {
                        theEditor.GetSelection().GetParentElement().outerHTML = "";
                        return;
                    }
                }
                if ((selected.length != 0) && returnValue) {
                    theEditor.GetSelection().GetParentElement().outerHTML = "";
                    select = '';
                }
                if (returnValue) {
                    theEditor.PasteHtml(returnValue);
                }
            };
            
         RadEditorCommandList["RemoveComponent"] =
                function image(commandName, editor, oTool)
                {                
                   theEditor = editor;
                   theEditor.GetSelection().GetParentElement().outerHTML = "";
                };
      //]]>
 </script>
 <% } %>
</div>