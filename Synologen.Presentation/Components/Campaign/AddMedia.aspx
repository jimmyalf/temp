<%@ Page Language="C#" MasterPageFile="~/components/Campaign/CampaignMain.master" AutoEventWireup="true" CodeFile="AddMedia.aspx.cs" Inherits="components_Campaign_AddMedia" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phCampaign" Runat="Server">
    <div id="dCompMain" class="Components-Campaign-AddMedia-aspx">
    <div class="fullBox">
    <div class="wrap">
        <h1>Campaign</h1>
        
		<fieldset>
			<legend>Add media to Campaign <asp:Label ID="lblName" runat="server" /></legend>
			
			<div class="formItem clearLeft">
                <asp:Label ID="lblFile1" runat="server" AssociatedControlID="uplFile1" SkinId="Long"/>
                <asp:FileUpload ID="uplFile1" runat="server" />
                <asp:Label ID="lblDesc1" runat="server" AssociatedControlID="txtDesc1" SkinId="Long"/>
                <asp:TextBox ID="txtDesc1" runat="server"></asp:TextBox>
                <asp:Label ID="lblCategory1" runat="server" AssociatedControlID="drpCategory1" SkinId="Long"/>
                <asp:DropDownList ID="drpCategory1" runat="server"></asp:DropDownList>

            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblFile2" runat="server" AssociatedControlID="uplFile2" SkinId="Long"/>
                <asp:FileUpload ID="uplFile2" runat="server" />
                <asp:Label ID="lblDesc2" runat="server" AssociatedControlID="txtDesc2" SkinId="Long"/>
                <asp:TextBox ID="txtDesc2" runat="server"></asp:TextBox>
                <asp:Label ID="lblCategory2" runat="server" AssociatedControlID="drpCategory2" SkinId="Long"/>
                <asp:DropDownList ID="drpCategory2" runat="server"></asp:DropDownList>

            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblFile3" runat="server" AssociatedControlID="uplFile3" SkinId="Long"/>
                <asp:FileUpload ID="uplFile3" runat="server" />
                <asp:Label ID="lblDesc3" runat="server" AssociatedControlID="txtDesc3" SkinId="Long"/>
                <asp:TextBox ID="txtDesc3" runat="server"></asp:TextBox>
                <asp:Label ID="lblCategory3" runat="server" AssociatedControlID="drpCategory3" SkinId="Long"/>
                <asp:DropDownList ID="drpCategory3" runat="server"></asp:DropDownList>

            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblFile4" runat="server" AssociatedControlID="uplFile4" SkinId="Long"/>
                <asp:FileUpload ID="uplFile4" runat="server" />
                <asp:Label ID="lblDesc4" runat="server" AssociatedControlID="txtDesc4" SkinId="Long"/>
                <asp:TextBox ID="txtDesc4" runat="server"></asp:TextBox>
                <asp:Label ID="lblCategory4" runat="server" AssociatedControlID="drpCategory4" SkinId="Long"/>
                <asp:DropDownList ID="drpCategory4" runat="server"></asp:DropDownList>

            </div>
            <div class="formItem clearLeft">
                <asp:Label ID="lblFile5" runat="server" AssociatedControlID="uplFile5" SkinId="Long"/>
                <asp:FileUpload ID="uplFile5" runat="server" />
                <asp:Label ID="lblDesc5" runat="server" AssociatedControlID="txtDesc5" SkinId="Long"/>
                <asp:TextBox ID="txtDesc5" runat="server"></asp:TextBox>
                <asp:Label ID="lblCategory5" runat="server" AssociatedControlID="drpCategory5" SkinId="Long"/>
                <asp:DropDownList ID="drpCategory5" runat="server"></asp:DropDownList>

            </div>
            
            <div class="formCommands">					    
			    <asp:button ID="btnAdd" runat="server" CommandName="Add" OnClick="btnSave_Click" Text="Add" SkinId="Big"/>
			</div>
        </fieldset>
        
    </div>
    </div>
    </div>
</asp:Content>

