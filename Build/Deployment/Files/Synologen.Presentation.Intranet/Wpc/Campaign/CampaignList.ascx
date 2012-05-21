<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignList.ascx.cs" Inherits="CampaignList" %>
<asp:Repeater ID="rptCampaign" runat="server" OnItemDataBound="rptCampaign_ItemDataBound">
<ItemTemplate>

        <h5 class="rubrik1"><asp:Label ID="lblHeading" runat="server" ><%# DataBinder.Eval(Container.DataItem, "cHeading")%></asp:Label></h5><div style="border-right:#d5ded5 1px solid; border-left:#d5ded5 1px solid; border-bottom:#d5ded5 1px solid;  padding: 10px; margin-top: 0; text-align:left">
<asp:Image ID="imgSpot" runat="server" /><br />

        <asp:Label ID="lblDescription" runat="server" ><%# DataBinder.Eval(Container.DataItem, "cDescription")%></asp:Label><br />
       <a href='<%=CampaignPage %>?campaignid=<%# DataBinder.Eval(Container.DataItem, "cId")%>'>Visa &gt;</a>
       <br /><br /></div><br />

</ItemTemplate>
</asp:Repeater>
