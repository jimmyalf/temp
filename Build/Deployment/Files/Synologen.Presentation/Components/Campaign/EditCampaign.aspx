<%@ Page Language="C#" MasterPageFile="~/components/Campaign/CampaignMain.master" AutoEventWireup="true" CodeFile="EditCampaign.aspx.cs" Inherits="components_Campaign_EditCampaign" Title="Untitled Page" %>
<%@ Register Src="../../common/DateTimeCalendar.ascx" TagName="DateTimeCalendar" TagPrefix="uc1" %>
<%@ Register Src="spotImage.ascx" TagName="SpotImage" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCampaign" Runat="Server">
    <div id="dCompMain" class="Components-Campaign-EditCampaign-aspx">
    <div class="fullBox">
    <div class="wrap">
        <h1>Campaign</h1>
        
		<fieldset>
			<legend><asp:Label ID="lblHeader" runat="server" /></legend>
			
			<div class="formItem clearLeft">
                <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
                <asp:TextBox ID="txtName" runat="server"/>
			</div>
			
			<div class="formItem clearLeft">
                <asp:Label ID="lblHeading" runat="server" AssociatedControlID="txtHeading" SkinId="Long"/>
                <asp:TextBox ID="txtHeading" runat="server" TextMode="MultiLine" SkinID="Wide"></asp:TextBox>
			</div>
			
			<div class="formItem clearLeft">
                <asp:Label ID="lblDescription" runat="server" AssociatedControlID="txtDescription" SkinId="Long"/>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" SkinID="Wide"></asp:TextBox>
			</div>
			
		    <div class="formItem clearLeft">
		    <asp:Label id="lblSpotImage" runat="server" SkinId="Long" AssociatedControlID="SpotImage1" />
		    <uc3:SpotImage ID="SpotImage1" runat="server" />
		    
		    </div>
			
		    <div class="formItem clearLeft">
                <asp:Label ID="lblSpotHeight" runat="server" AssociatedControlID="txtSpotHeight" SkinId="Long"/>
                <asp:TextBox ID="txtSpotHeight" runat="server" Width="50" />
            </div>
			
		    <div class="formItem clearLeft">
                <asp:Label ID="lblSpotWidth" runat="server" AssociatedControlID="txtSpotWidth" SkinId="Long" />
                <asp:TextBox ID="txtSpotWidth" runat="server" Width="50" />
		    </div>
		    
		    <div class="formItem clearLeft">
                <asp:Label ID="lblStartDate" runat="server" AssociatedControlID="dtcStartDate" SkinId="Long"></asp:Label>
                <uc1:DateTimeCalendar ID="dtcStartDate" runat="server" />
		    </div>
    						
		    <div class="formItem clearLeft">
                    <asp:Label ID="lblEndDate" runat="server" AssociatedControlID="dtcEndDate" SkinId="Long"></asp:Label>
                    <uc1:DateTimeCalendar ID="dtcEndDate" runat="server" />
		    </div>
	    
			
		    <div class="formItem clearLeft" visible="false">
                <asp:Label ID="lblType" runat="server" AssociatedControlID="rblType" SkinId="Long"/>
                <asp:RadioButtonList ID="rblType" 
                                        runat="server" 
                                        RepeatDirection="Horizontal"
                                        RepeatLayout="Flow" 
                                        AutoPostBack="True" 
                                        OnSelectedIndexChanged="rblType_SelectedIndexChanged">
                    <asp:ListItem>Thumbs</asp:ListItem>
                    <asp:ListItem>List</asp:ListItem>
                </asp:RadioButtonList>
		    </div>
			
		    <div class="formItem clearLeft" runat="server" id="divThumbsRows" visible="false">
                <asp:Label ID="lblThumbsRows" runat="server" AssociatedControlID="txtThumbsRows" SkinId="Long"/>
                <asp:TextBox ID="txtThumbsRows" runat="server" />
		    </div>
	    
						
			<div class="formItem clearLeft" runat="server" id="divThumbsColumns">
                <asp:Label ID="lblThumbsColumns" runat="server" AssociatedControlID="txtThumbsColumns" SkinId="Long"/>
                <asp:TextBox ID="txtThumbsColumns" runat="server"  Text="3" />
		    </div>
			
			<div class="formItem clearLeft" runat="server" id="divThumbsHeight">
                <asp:Label ID="lblThumbsHeight" runat="server" AssociatedControlID="txtThumbsHeight" SkinId="Long"/>
                <asp:TextBox ID="txtThumbsHeight" runat="server"  Text="100" />
		    </div>
		    
			<div class="formItem clearLeft" runat="server" id="divThumbsWidth">
                <asp:Label ID="lblThumbsWidth" runat="server" AssociatedControlID="txtThumbsWidth" SkinId="Long"/>
                <asp:TextBox ID="txtThumbsWidth" runat="server"  Text="100"/>
		    </div>
			
		    <div class="formItem clearLeft" runat="server" id="divListRowsPerPage">
                <asp:Label ID="lblListRowsPerPage" runat="server" AssociatedControlID="txtListRowsPerPage" SkinId="Long"/>
                <asp:TextBox ID="txtListRowsPerPage" runat="server" />
		    </div>
			
			<div class="formItem clearLeft" id="dLocations">
                <asp:Label ID="lblLocations" runat="server" AssociatedControlID="chklLocations" SkinId="Long"></asp:Label>                            
                <asp:CheckBoxList ID="chklLocations" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="2" RepeatLayout="Table"/>
			</div>
			
			<div class="formItem clearLeft">
                <asp:Label ID="lblCategories" runat="server" AssociatedControlID="chklCategories" SkinId="Long"></asp:Label>                            
                <asp:CheckBoxList ID="chklCategories" DataValueField="cCategoryId" DataTextField="cName" runat="server" RepeatColumns="2" RepeatLayout="Table"/>
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

