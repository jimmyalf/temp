<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Campaign.ascx.cs" Inherits="Campaign" %>
<%@ Register Src="CampaignFilesThumb.ascx" TagName="CampaignFilesThumb" TagPrefix="uc1" %>

<h1>
<asp:Label ID="lblHeading" runat="server" Text="Label"></asp:Label>&nbsp;</h1>
<p>
    <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>&nbsp;</p>
<p>
    <asp:Repeater ID="rptFileCategories" runat="server" OnItemDataBound="rptFileCategories_ItemDataBound">
    <ItemTemplate>
        <uc1:CampaignFilesThumb ID="CampaignFilesThumb1" FileCategoryId='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" />
    </ItemTemplate>
    </asp:Repeater>
    
</p>
