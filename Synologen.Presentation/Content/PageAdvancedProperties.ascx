<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Content.Presentation.Content.PageAdvancedProperties" Codebehind="PageAdvancedProperties.ascx.cs" %>
<%@ Register Src="~/Common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="uc1" %>
<div class="Content-PageAdvancedProperties-aspx fullBox">
<div class="wrap">
<h2>Properties</h2>
<fieldset>
<legend>Edit properties</legend>
<div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtMenu" SkinID="Long">Document title</asp:Label>
	<ASP:TEXTBOX id="txtMenu" runat="server"></ASP:TEXTBOX>
</div>
<div id="dPublishInLocations" runat="server" class="formItem clearRight" visible="false">
	<ASP:LABEL id="lblLocation" runat="server" AssociatedControlID="chkLocations" SkinID="Long">Publish on Location</ASP:LABEL>
	<asp:CheckBoxList id="chkLocations" runat="server" repeatdirection="Horizontal" RepeatLayout="Flow" CssClass="checkBoxItems">
	</asp:CheckBoxList>
</div>
</div>

<div>
<div class="formItem clearLeft">
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

<div class="formItem">
	<ASP:LABEL id="lblDefaultPage" runat="server" AssociatedControlID="rdoDefaultPage" SkinID="Long">Menu default page</ASP:LABEL>
	<ASP:RADIOBUTTONLIST id="rdoDefaultPage" runat="server" repeatdirection="Horizontal" autopostback="True" RepeatLayout="Flow" CssClass="radioButtonItems">
		<ASP:LISTITEM value="true">Yes</ASP:LISTITEM>
		<ASP:LISTITEM value="false" selected="True">No</ASP:LISTITEM>
	</ASP:RADIOBUTTONLIST>
</div>

<div class="formItem">
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
	<asp:Label runat="server" AssociatedControlID="tddlMaster" SkinID="Long">Template</asp:Label>
	<utility:TemplateDropdownList runat="server" ID="tddlMaster" DisplayType="master" />
	<%--<ASP:DROPDOWNLIST id="drpTemplate" runat="server"></ASP:DROPDOWNLIST>--%>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="tddlStylesheet" SkinID="Long">Stylesheet</asp:Label>
	<utility:TemplateDropdownList runat="server" ID="tddlStylesheet" DisplayType="stylesheet" />
	<%--<ASP:DROPDOWNLIST id="drpStylesheet" runat="server" autopostback="True"></ASP:DROPDOWNLIST>--%>
</div>
</div>

<div>
<div class="formItem clearLeft" style="display:none">
	<asp:Label runat="server" AssociatedControlID="txtNote" SkinID="Long">Note</asp:Label>
	<ASP:TEXTBOX id="txtNote" runat="server"></ASP:TEXTBOX>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtKeyword" SkinID="Long">Keywords(Meta)</asp:Label>
	<ASP:TEXTBOX id="txtKeyword" runat="server" textmode="MultiLine"></ASP:TEXTBOX>
</div>
</div>

<div>
<div class="formItem clearLeft">
	<asp:Label runat="server" AssociatedControlID="txtDescription" SkinID="Long">Description(Meta)</asp:Label>
	<ASP:TEXTBOX id="txtDescription" runat="server" textmode="MultiLine"></ASP:TEXTBOX>
</div>
<div class="formItem">
	<asp:Label runat="server" AssociatedControlID="txtHeader" SkinID="Long">Header</asp:Label>
	<ASP:TEXTBOX id="txtHeader" runat="server" textmode="MultiLine"></ASP:TEXTBOX>
</div>
</div>

<ASP:VALIDATIONSUMMARY id="vldSummery" runat="server" ShowMessageBox="True" ShowSummary="false"></ASP:VALIDATIONSUMMARY>
</fieldset>

</div>
</div>