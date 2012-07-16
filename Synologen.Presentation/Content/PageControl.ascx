<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc1" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.PageControl" Codebehind="PageControl.ascx.cs" %>
<script type="text/javascript" src="Js/UrlUtility.js"></script>
<script language="javascript" type="text/javascript">
<!--
    // Function for copy legal characters from title -> name.
    function copychars ()
    {
        var title = document.forms[0].<%=txtTitle.UniqueID%>.value;
        var checked = document.forms[0].<%=chkName.UniqueID%>.checked;

        if (!checked) {
            document.forms[0].<%=txtFileName.UniqueID %>.value = getUrlFriendlyName(title)
        }
    }
//-->
</script>
<%--<link href="common/css/WYSIWYG.css" media="all" rel="Stylesheet" />--%>
<div class="Content-PageControl-ascx fullBox">
<div class="wrap">
	<h2>Page</h2>

	<fieldset>
	<legend>Edit page</legend>
	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtTitle" SkinID="Long">Title <ASP:REQUIREDFIELDVALIDATOR id="rfvTitlte" runat="server" controltovalidate="txtTitle" errormessage="Title must be given!">(Title must be given!)</ASP:REQUIREDFIELDVALIDATOR></asp:Label>
		<ASP:TEXTBOX id="txtTitle" onKeyUp="javascript:copychars()" onChange="javascript:copychars()" runat="server"></ASP:TEXTBOX>
	</div>
	<div class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtFileName" SkinID="Long">Filename <ASP:REQUIREDFIELDVALIDATOR id="rfvName" runat="server" controltovalidate="txtFileName" errormessage="Name must be given!">(Name must be given!)</ASP:REQUIREDFIELDVALIDATOR><asp:RegularExpressionValidator ID="vldxName" runat="server" ControlToValidate="txtFileName" ErrorMessage="Only characters _ - a-z A-Z 0-9 allowed!" Display="Dynamic" ValidationExpression="[-a-zA-Z0-9_]*">(Only characters _ - a-z A-Z 0-9 allowed!)</asp:RegularExpressionValidator></asp:Label>
		<ASP:TEXTBOX id="txtFileName" runat="server"></ASP:TEXTBOX>
		<asp:CheckBox id="chkName" Checked="false" runat="server" Text="Lock name" />
    </div>

	<div id="dWysiwyg" runat="server">
		<asp:Label runat="server" AssociatedControlID="edtContent" SkinID="Long">Page content</asp:Label>
		<div class="clearLeft">
		<uc1:WpcWysiwyg ID="edtContent" runat="server" Mode="WpcInternalAdvanced" />
		</div>
	</div>
	
	<div id="dPreView" runat="server">
		<div class="clearLeft">
            <iframe src="<%=Spinit.Wpc.Base.Business.Globals.BaseUrl%>content/PageViewControl.aspx?TreeId=<%=_treeId %>" width="100%" height="500"></iframe>
		</div>
	</div>
	</fieldset>
</div>
</div>