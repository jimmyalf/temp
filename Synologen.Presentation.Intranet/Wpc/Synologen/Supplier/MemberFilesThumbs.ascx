<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Supplier.MemberFilesThumbs" Codebehind="MemberFilesThumbs.ascx.cs" %>
<%@ Register TagPrefix="tc" Assembly="pdfthumbnail" Namespace="TallComponents.Web.PDF" %>
<h1>
	<asp:Label ID="lblFileCategory" runat="server"></asp:Label>
</h1>
<asp:DataList ID="dlThumbs" runat="server" OnDataBinding="dlThumbs_DataBinding" OnItemDataBound="dlThumbs_ItemDataBound" RepeatColumns="3">
	<ItemTemplate>
		<div style="border:#d5ded5 1px solid; padding: 10px; margin-right: 10px; margin-bottom: 10px; text-align:center">
		<asp:HyperLink id="hlThumb" runat="server" Target="_blank" ></asp:HyperLink>
		<tc:thumbnail id="Thumbnail1" ThumbnailPage="/Wpc/Synologen/Supplier/PdfThumbnail.aspx" index="1" runat="server"></tc:thumbnail>
		<br /><asp:HyperLink id="hlText" runat="server" Target="_blank" ></asp:HyperLink>
		</div>
	</ItemTemplate>
</asp:DataList>
<a href="/Leverantorer/InfoPage.aspx?memberId=<%=MemberId %>">Tillbaka</a>