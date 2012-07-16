<%@ Page Language="C#" MasterPageFile="~/components/Document/DocumentMain.master" AutoEventWireup="true" CodeBehind="EditNode.aspx.cs" Inherits="Spinit.Wpc.Document.Presentation.components.Document.EditNode" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phDocument" runat="server">
  <div id="dCompMain" class="Components-Document-EditNode-aspx">
        <div class="fullBox">
        <div class="wrap">
	        <h1>Document folder</h1>
        	
	        <fieldset>
		        <legend><asp:Label ID="lblHeader" runat="server"></asp:Label></legend>
        	
		        <div class="formItem clearLeft">
                        <asp:Label ID="lblName" runat="server" AssociatedControlID="txtName" SkinId="Long"/>
                        <asp:TextBox ID="txtName" runat="server" SkinID="Wide"/>
                        <label id="errorName" runat="server"  style="Color:Red"/>
		        </div>
		        
		        <div class="formItem clearLeft">
		            <asp:Label ID="lblSortType" runat="server" AssociatedControlID="drpSortType" ToolTip="Select how to sort documents in this folder." SkinID="Long">Sort By</asp:Label>
                    <asp:DropDownList ID="drpSortType" runat="server">
                    </asp:DropDownList>
		        </div>
		        
		        <div class="formItem clearLeft" id="dLocations">
                        <asp:Label ID="lblLocations" runat="server" AssociatedControlID="chklLocations" SkinId="Long"></asp:Label>                            
                        <asp:CheckBoxList ID="chklLocations" DataValueField="Id" DataTextField="Name" runat="server" RepeatColumns="2" RepeatLayout="Table"/>
                        <label id="errorLocations" runat="server"  style="Color:Red"/>
		        </div>
		        
	        </fieldset>
	        
	        <asp:PlaceHolder id="phAccess" runat="server">
				<fieldset>
					<legend>Set access to node</legend>
					<div class="formItem clearLeft">
							<asp:Label ID="lblGroup" runat="server" AssociatedControlID="drpGroups" SkinId="Long"></asp:Label>
							<asp:DropDownList ID="drpGroups" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpGroups_SelectedIndexChanged" />
					</div>
					<div class="formItem clearLeft">
							<asp:Label ID="lblUsers" runat="server" AssociatedControlID="drpUsers" SkinId="Long"></asp:Label>
							<asp:DropDownList ID="drpUsers" runat="server" />
					</div>
				</fieldset>
	        </asp:PlaceHolder>
		        <div class="formCommands">					    
		            <asp:button ID="btnSaveAndPublish" runat="server" CommandName="SaveAndPublish" OnClick="btnSave_Click" Text="Save & Publish" SkinId="Big" />
		            <asp:button ID="btnCancel" runat="server" CommandName="Cancel" OnClick="btnCancel_Click" Text="Cancel" SkinId="Big" ValidationGroup="vGroup" />
		        </div>
        </div>
        </div>
    </div>
</asp:Content>
