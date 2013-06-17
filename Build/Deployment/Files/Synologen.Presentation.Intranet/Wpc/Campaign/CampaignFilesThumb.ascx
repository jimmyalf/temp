<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CampaignFilesThumb.ascx.cs" Inherits="CampaignFilesThumb" %>
<%@ Register TagPrefix="tc" Assembly="pdfthumbnail" Namespace="TallComponents.Web.PDF" %>
<h4 class="rubrik1"><asp:Label ID="lblFileCategory" runat="server" ></asp:Label></h4><br />
<asp:DataList ID="dlThumbs" runat="server" 
                OnDataBinding="dlThumbs_DataBinding" 
                OnItemDataBound="dlThumbs_ItemDataBound" RepeatColumns="3" ItemStyle-VerticalAlign="Bottom">
            <ItemTemplate>
                <div style="border:#d5ded5 1px solid; padding: 10px; margin-right: 10px; margin-bottom: 10px; text-align:center">
		        <asp:HyperLink id="hlThumb" runat="server" Target="_blank" ></asp:HyperLink>
		        <tc:thumbnail id="Thumbnail1" path="pdfthumbnail.pdf" index="1" runat="server"></tc:thumbnail><br />
                <asp:HyperLink ID="hlText" runat="server" Target="_blank" ></asp:HyperLink><br />
                <%# DataBinder.Eval(Container.DataItem, "Date") %>
                </div>
	        </ItemTemplate> 
</asp:DataList>