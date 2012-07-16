<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/components/News/NewsMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.News.Presentation.Components.News.EditNews" Title="Untitled Page" Codebehind="EditNews.aspx.cs" %>
<%@ Register Src="~/CommonResources/CommonControls/Wysiwyg/WpcWysiwyg.ascx" TagName="WpcWysiwyg" TagPrefix="uc2" %>
<%@ Register Src="../../common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="uc1" %>
<%@ Register Src="spotImage.ascx" TagName="SpotImage" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phNews" Runat="Server">
<style>.bodytext { color:red; }</style>
<div id="dCompMain" class="Components-News-EditNews-aspx">
<div class="fullBox">
<div class="wrap">
	<h1>News</h1>
	
	<fieldset>
		<legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
		
		<asp:PlaceHolder ID="phNewsType" runat="server">
		<div class="formItem">
		    <asp:Label ID="lblNewsType" runat="server" AssociatedControlID="drpNewsType" SkinId="Long"/>
		    <asp:DropDownList ID="drpNewsType" runat="server" OnSelectedIndexChanged="drpNewsType_SelectedIndexChanged" AutoPostBack="true"/>
		</div>
		</asp:PlaceHolder>
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblHeading" runat="server" AssociatedControlID="txtHeading" SkinId="Long"/>
                <asp:TextBox ID="txtHeading" runat="server" SkinId="Long" />
		</div>
		
		<div class="formItem clearLeft">
                <asp:Label ID="lblSummary" runat="server" AssociatedControlID="txtSummary" SkinId="Long"/>
                <asp:TextBox ID="txtSummary" runat="server" TextMode="MultiLine" SkinID="Wide"></asp:TextBox>
		</div>
		
		<div id="dSpotImage" runat="server">
		<fieldset class="clearBoth">
			<legend>Image</legend>
			<div class="formItem clearLeft">
				<asp:Label id="lblSpotImage" runat="server" SkinId="Long" AssociatedControlID="SpotImage1" />
				<uc3:SpotImage ID="SpotImage1" runat="server" />
			</div>
    		<div class="formItem clearLeft">
				<asp:Label ID="lblSpotWidth" runat="server" AssociatedControlID="txtSpotWidth" SkinId="Long"/>
				<asp:TextBox ID="txtSpotWidth" runat="server" SkinID="Tiny" /> pixels
			</div>
			<div class="formItem clearLeft">
				<asp:Label ID="lblSpotHeight" runat="server" AssociatedControlID="txtSpotHeight" SkinId="Long"/>
				<asp:TextBox ID="txtSpotHeight" runat="server" SkinID="Tiny" /> pixels
			</div>
			<asp:PlaceHolder ID="phSpotAlign" runat="server">
			<div class="formItem clearLeft">
				<asp:Label ID="lblSpotAlign" runat="server" AssociatedControlID="drpSpotAlign" SkinId="Long"/>
				<asp:DropDownList runat="server" ID="drpSpotAlign" />
			</div>
			</asp:PlaceHolder>
		</fieldset>
		</div>
		
		<fieldset class="clearBoth">
			<legend>Publish dates</legend>
			<div class="formItem clearLeft">
				<asp:Label ID="lblStartDate" runat="server" AssociatedControlID="dtcStartDate" SkinId="Long"></asp:Label>
				<uc1:DateTimeCalendar ID="dtcStartDate" runat="server" />
				<label id="lblErrorStartDate" runat="server" style="color:Red" />
			</div>
							
			<div class="formItem clearLeft">
				<asp:Label ID="lblEndDate" runat="server" AssociatedControlID="dtcEndDate" SkinId="Long"></asp:Label>
				<uc1:DateTimeCalendar ID="dtcEndDate" runat="server" />
			</div>
		</fieldset>
		
		<fieldset class="clearBoth">
			<legend>Connections</legend>
			<div class="formItem clearLeft" id="dLocations">
					<asp:Label ID="lblLocations" runat="server" AssociatedControlID="chklLocations" SkinId="Long"></asp:Label>                            
					<asp:CheckBoxList ID="chklLocations" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="2" RepeatLayout="Table"/>
			</div>
			
			<asp:PlaceHolder ID="phCategories" runat="server">
			<div class="formItem clearLeft">
					<asp:Label ID="lblCategories" runat="server" AssociatedControlID="chklCategories" SkinId="Long"></asp:Label>                            
					<asp:CheckBoxList ID="chklCategories" DataValueField="cCategoryId" DataTextField="cName" runat="server" RepeatColumns="2" RepeatLayout="Table"/>
			</div>
			</asp:PlaceHolder>
			
			<asp:PlaceHolder ID="phGroups" runat="server">
			<div class="formItem clearLeft">
					<asp:Label ID="lblGroups" runat="server" AssociatedControlID="chklGroups" SkinId="Long"></asp:Label>                            
					<asp:CheckBoxList ID="chklGroups" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="2" RepeatLayout="Table"/>
			</div>
			</asp:PlaceHolder>
		</fieldset>
		
		<div id="dExternalLink" runat="server" class="formItem clearLeft">
		    <asp:Label ID="lblExternalLink" runat="server" AssociatedControlID="dtcEndDate" SkinId="Long"></asp:Label>
		    <asp:TextBox runat="server" ID="txtExternalLink"/>
		</div>
		
		<div id="dWysiwyg" runat="server" class="dWysiwyg clearLeft">
			<uc2:WpcWysiwyg ID="txtBody" Mode="WpcInternalAdvanced" runat="server" />
		</div>
																	
		<div class="formCommands">					    
		    <asp:button ID="btnSaveAndPublish" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Save & Publish" SkinId="Big"/>
		    <asp:button ID="btnSaveForLater" runat="server" CommandName="SaveForLater" OnClick="btnSave_Click" Text="Save For Later" SkinId="Big"/>
		    <asp:button ID="btnSaveForApproval" runat="server" CommandName="SaveForApproval" OnClick="btnSave_Click" Text="Send For Approval" SkinId="Big"/>
		</div>
	</fieldset>
</div>
</div>
</div>
</asp:Content>