<%@ Register Src="~/Common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="uc1" %>
<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.MenuControl" Codebehind="MenuControl.ascx.cs" %>
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
<div class="Content-MenuControl-ascx fullBox">
<div class="wrap">
<h2>Menu</h2>
<fieldset>
<legend>Edit menu</legend>

<div>
	<asp:Label id="lblErrorMsg" runat="server" visible="False"></asp:Label>&nbsp;
</div>

<div>
	<div id="dPublishTitle" runat="server" class="formItem">
		<asp:Label runat="server" AssociatedControlID="txtTitle" SkinID="Long">Title * <ASP:REQUIREDFIELDVALIDATOR id="rfvTitle" runat="server" errormessage="Title is missing" controltovalidate="txtTitle" SetFocusOnError="True" Display="None" /></asp:Label>
		<asp:TextBox id="txtTitle" onKeyUp="javascript:copychars()" onChange="javascript:copychars()" runat="server"></asp:TextBox>
	</div>
	<div class="formItem">
		<asp:Label ID="Label1" runat="server" AssociatedControlID="txtFileName" SkinID="Long">Filename <ASP:REQUIREDFIELDVALIDATOR id="rfvName" runat="server" controltovalidate="txtFileName" errormessage="Filename is missing" SetFocusOnError="True" Display="None" /><asp:RegularExpressionValidator ID="vldxName" runat="server" ControlToValidate="txtFileName" ErrorMessage="Only characters _ - a-z A-Z 0-9 allowed in the filename!" ValidationExpression="[-a-zA-Z0-9_]*" SetFocusOnError="True" Display="None" /></asp:Label>
		<ASP:TEXTBOX id="txtFileName" runat="server"></ASP:TEXTBOX>
		<asp:CheckBox id="chkName" Checked="false" runat="server" Text="Lock name"  />
	</div>
</div>
<div>
<div class="formItem">
		<asp:Label ID="lblCssClass" runat="server" AssociatedControlID="txtClassName" SkinID="Long">Body class <asp:RegularExpressionValidator ID="vldxClass" runat="server" ControlToValidate="txtClassName" ErrorMessage="Only characters _ - a-z A-Z 0-9 allowed in classname!" ValidationExpression="[-a-zA-Z0-9_]*" SetFocusOnError="True" Display="None" /></asp:Label>
		<asp:TextBox id="txtClassName" runat="server"></asp:TextBox>
</div>
	<div id="dPublishInLocations" runat="server" class="formItem clearLeft" visible="false">
		<asp:Label id="lblLocation" runat="server" AssociatedControlID="chkLocations" SkinID="Long">Publish on location</asp:Label>
		<asp:CheckBoxList id="chkLocations" runat="server" repeatdirection="Horizontal" RepeatLayout="Flow" CssClass="checkBoxItems">
		</asp:CheckBoxList>
	</div>
</div>

<div>
<div id="dPublishHideInMenu" runat="server" class="formItem clearLeft">
	<asp:Label runat="server" AssociatedControlID="rdoHideInMenu" SkinID="Long">Hide in menu</asp:Label>
	<ASP:RADIOBUTTONLIST id="rdoHideInMenu" runat="server" repeatdirection="Horizontal" autopostback="True" RepeatLayout="Flow" CssClass="radioButtonItems">
		<ASP:LISTITEM value="true">Yes</ASP:LISTITEM>
		<ASP:LISTITEM value="false" selected="True">No</ASP:LISTITEM>
	</ASP:RADIOBUTTONLIST>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="rdoExcludeFromSearch" SkinID="Long">Exclude from search</asp:Label>
	<ASP:RADIOBUTTONLIST id="rdoExcludeFromSearch" runat="server" repeatdirection="Horizontal" RepeatLayout="Flow" CssClass="radioButtonItems">
		<ASP:LISTITEM value="true">Yes</ASP:LISTITEM>
		<ASP:LISTITEM value="false" selected="True">No</ASP:LISTITEM>
	</ASP:RADIOBUTTONLIST>
</div>
<div class="formItem clearLeft">
	<ASP:LABEL id="lblAuthenticationPage" runat="server" AssociatedControlID="rdoAuthenticationPage" SkinID="Long">Page needs authentication</ASP:LABEL>
	<ASP:RADIOBUTTONLIST id="rdoAuthenticationPage" runat="server" repeatdirection="Horizontal" autopostback="True" RepeatLayout="Flow" CssClass="radioButtonItems">
		<ASP:LISTITEM value="true">Yes</ASP:LISTITEM>
		<ASP:LISTITEM value="false" selected="True">No</ASP:LISTITEM>
	</ASP:RADIOBUTTONLIST>
</div>
<div class="formItem">
	<asp:Label id="lblPublishPage" runat="server" AssociatedControlID="rblPublishPage" SkinID="Long">Publish</asp:Label>
	<asp:RadioButtonList id="rblPublishPage" runat="server" repeatdirection="Horizontal" autopostback="True" RepeatLayout="Flow" CssClass="radioButtonItems">
		<asp:ListItem value="true" selected="True">Yes</asp:ListItem>
		<asp:ListItem value="false">No</asp:ListItem>
	</asp:RadioButtonList>
</div>
</div>

<div>
<div class="formItem clearLeft" style="display:none">
	<asp:Label runat="server" AssociatedControlID="dtiPublish" SkinID="Long">Publish on date</asp:Label>
	<uc1:DateTimeCalendar ID="dtiPublish" runat="server" />
</div>
<div class="formItem" style="display:none">
	<asp:Label runat="server" AssociatedControlID="dtiUnpublish" SkinID="Long">Unpublish on date</asp:Label>
	<uc1:DateTimeCalendar ID="dtiUnpublish" runat="server" />
</div>
</div>

<div>
<div class="formItem clearLeft">
	<asp:Label runat="server" AssociatedControlID="tddlMaster" SkinID="Long">Choose default template</asp:Label>
	<utility:TemplateDropdownList runat="server" ID="tddlMaster" DisplayType="master" />
	<%--<ASP:DROPDOWNLIST id="drpTemplate" runat="server"></ASP:DROPDOWNLIST>--%>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="tddlStylesheet" SkinID="Long">Choose default stylesheet</asp:Label>
	<utility:TemplateDropdownList runat="server" ID="tddlStylesheet" DisplayType="stylesheet" />
	<%--<ASP:DROPDOWNLIST id="drpStylesheet" runat="server" autopostback="True"></ASP:DROPDOWNLIST>--%>
</div>
</div>

<div>
<div class="formItem clearLeft">
	<asp:Label runat="server" AssociatedControlID="txtNote" SkinID="Long">Note</asp:Label>
	<asp:TextBox id="txtNote" runat="server"></asp:TextBox>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtKeywords" SkinID="Long">Keywords(Meta)</asp:Label>
	<asp:TextBox id="txtKeywords" runat="server" textmode="MultiLine" ></asp:TextBox>
</div>
</div>

<div>
<div class="formItem clearLeft">
	<asp:Label runat="server" AssociatedControlID="txtDescription" SkinID="Long">Description(Meta)</asp:Label>
	<asp:TextBox id="txtDescription" runat="server" textmode="MultiLine" ></asp:TextBox>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtHeader" SkinID="Long">Header</asp:Label>
	<asp:TextBox id="txtHeader" runat="server" textmode="MultiLine" ></asp:TextBox>
</div>
</div>
</fieldset>

</div>
</div>