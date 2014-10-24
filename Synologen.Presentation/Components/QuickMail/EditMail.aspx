<%@ Page Language="C#" MasterPageFile="~/components/QuickMail/QuickMailMain.Master" AutoEventWireup="true" CodeBehind="EditMail.aspx.cs" Inherits="Spinit.Wpc.QuickMail.Presentation.EditMail" Title="Untitled Page" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc2" %>
<%@ Register Src="../../common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="uc1" %>
<asp:Content ID="cntQuickMailMain" ContentPlaceHolderID="ComponentQuickMail" runat="server">
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-QuickMail-EditMail-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>QuickMail</h1>
	
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
						
		<div class="formItem clearLeft">
	        <asp:Label ID="lblTypes" runat="server" AssociatedControlID="drpTypes" SkinId="Long"/>
	        <asp:DropDownList runat="server" ID="drpTypes" OnSelectedIndexChanged="drpTypes_SelectedIndexChanged" AutoPostBack="true"  />&nbsp;
		</div>
		<asp:PlaceHolder runat="server" ID="phSendOutDate" Visible="false">
			<div class="formItem clearLeft">
					<asp:Label ID="lblSendOutDate" runat="server" AssociatedControlID="dtcSendOutDate" SkinId="Long"></asp:Label>
					<uc1:DateTimeCalendar ID="dtcSendOutDate" runat="server" />
			</div>
		</asp:PlaceHolder>		
		<div class="formItem clearLeft">
	        <asp:Label ID="lblPriority" runat="server" AssociatedControlID="drpPriority" SkinId="Long"/>
	        <asp:DropDownList runat="server" ID="drpPriority"/>&nbsp;
		</div>

		<div class="formItem clearLeft">
                <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
                <asp:TextBox ID="txtName" runat="server" TextMode="SingleLine" SkinID="Wide"/>
		</div>
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblFrom" runat="server" AssociatedControlID="txtFrom" SkinId="Long"/>
                <asp:TextBox ID="txtFrom" runat="server" TextMode="SingleLine" SkinID="Wide"/>
		</div>

		<div class="formItem clearLeft">
                <asp:Label ID="lblFriendlyFrom" runat="server" AssociatedControlID="txtFriendlyFrom" SkinId="Long"/>
                <asp:TextBox ID="txtFriendlyFrom" runat="server" TextMode="SingleLine" SkinID="Wide"/>
		</div>

		<div class="formItem clearLeft">
                <asp:Label ID="lblSubject" runat="server" AssociatedControlID="txtSubject" SkinId="Long"/>
                <asp:TextBox ID="txtSubject" runat="server" TextMode="SingleLine" SkinID="Wide"/>
		</div>
		
		<div class="formItem clearLeft">
			<asp:Label Id="lblTemplate" runat="server" AssociatedControlID="tddlTemplate" SkinId="Long"/>
			<utility:TemplateDropdownList runat="server" ID="tddlTemplate" DisplayType="snippet" OnSelectedIndexChanged="tddlTemplate_SelectedIndexChanged" AutoPostBack="true" />
		</div>

		<div class="formItem clearLeft">
                <asp:Label ID="lblUseHtml" runat="server" AssociatedControlID="chkUseHtml" SkinId="Long"/>
                <asp:Checkbox ID="chkUseHtml" runat="server" SkinID="Wide" AutoPostBack="True" OnCheckedChanged="chkUseHtml_CheckedChanged"/>
		</div>

		<div id="dWysiwyg" runat="server" class="dWysiwyg clearLeft">
            <asp:Label ID="lblHtmlBody" runat="server" AssociatedControlID="txtHtmlBody" SkinId="Long" Visible="false"/>
			<uc2:WpcWysiwyg ID="txtHtmlBody" Mode="WpcInternalAdvanced" runat="server" />
		</div>
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblUsePlain" runat="server" AssociatedControlID="chkUsePlain" SkinId="Long"/>
                <asp:Checkbox ID="chkUsePlain" runat="server" SkinID="Wide" AutoPostBack="True" OnCheckedChanged="chkUsePlain_CheckedChanged"/>
		</div>

		<div class="formItem clearLeft">
                <asp:Label ID="lblPlainBody" runat="server" AssociatedControlID="txtPlainBody" SkinId="Long"/>
                <asp:TextBox ID="txtPlainBody" runat="server" TextMode="MultiLine" SkinID="Wide"/>
		</div>
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblActive" runat="server" AssociatedControlID="chkActive" SkinId="Long"/>
                <asp:Checkbox ID="chkActive" runat="server" SkinID="Wide" />
		</div>
		
		<div class="formItem clearLeft" id="dLocations">
                <asp:Label ID="lblLocations" runat="server" AssociatedControlID="chklLocations" SkinId="Long"></asp:Label>                            
                <asp:CheckBoxList ID="chklLocations" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="4" RepeatLayout="Table"/>
		</div>

		<div class="formItem clearLeft" id="dComponents">
                <asp:Label ID="lblComponents" runat="server" AssociatedControlID="chklComponents" SkinId="Long"></asp:Label>                            
                <asp:CheckBoxList ID="chklComponents" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="4" RepeatLayout="Table"/>
		</div>
																									
		<div class="formCommands">					    
		    <asp:button ID="btnSaveAndPublish" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Save" SkinId="Big"/>
		    <asp:PlaceHolder ID="phResend" runat="server" Visible="false">
				<asp:button ID="btnSaveAndResend" runat="server" CommandName="SaveAndResend" OnClick="btnSave_Click" Text="Save & ReSend" SkinId="Big"/>
		    </asp:PlaceHolder>
		    <asp:button ID="btnSaveForLater" runat="server" CommandName="SaveForLater" OnClick="btnSave_Click" Text="Save For Later" SkinId="Big" Visible="false"/>
		    <asp:button ID="btnSaveForApproval" runat="server" CommandName="SaveForApproval" OnClick="btnSave_Click" Text="Send For Approval" SkinId="Big" Visible="false"/>
		</div>
	</fieldset>	
</div>
</div>
</asp:Content>
