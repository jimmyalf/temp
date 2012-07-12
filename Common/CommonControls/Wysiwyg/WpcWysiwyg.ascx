<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Wysiwyg.WpcWysiwyg" %>
<%@ Import Namespace="Spinit.Wpc.Wysiwyg.Code" %>
<asp:PlaceHolder ID="phScriptManager" runat="server" />
<div id="div-edtWpcWysiwyg" class="edtWpcWysiwyg">

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
<script type="text/javascript" language="javascript">
$(document).ready(function() {

	tinyMCE.init({
	    mode: "specific_textareas",
	    editor_selector: 'mceEditor',
	    theme: "advanced",
	    plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,wordcount,advlist,autosave,inlinepopups,filemanager,imagemanager,editinternallink,editcomponent,editinclude",
	    theme_advanced_buttons1: <%= TinyMceButtonConfiguration.GetRow("FirstRow") %>,
	    theme_advanced_buttons2: <%= TinyMceButtonConfiguration.GetRow("SecondRow") %>,
	    theme_advanced_buttons3: <%= TinyMceButtonConfiguration.GetRow("ThirdRow") %>,
	    theme_advanced_buttons4: <%= TinyMceButtonConfiguration.GetRow("FourthRow") %>,
	    theme_advanced_toolbar_location: "top",
	    theme_advanced_toolbar_align: "left",
	    theme_advanced_statusbar_location: "bottom",
	    theme_advanced_resizing: true,
	    content_css: <%=GetCss() %>
	    template_templates : <%=GetTemplates()%>
	    height: '400',
	    relative_urls : false, 
	    remove_script_host : true,
	    doctype: '<!DOCTYPE html>',
	    extended_valid_elements: 'section,article'
	    //valid_children : '+body[style],p[strong|a|#text]'
	});
});

</script>

<textarea runat="server" id="wysiwyg" class="mceEditor"></textarea>

<asp:PlaceHolder ID="phScript" runat="server" />
</div>
