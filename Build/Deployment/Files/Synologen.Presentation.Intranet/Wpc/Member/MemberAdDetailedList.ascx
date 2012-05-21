<%@ Control Language="C#" AutoEventWireup="true" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberAdDetailedList" Codebehind="MemberAdDetailedList.ascx.cs" %>
<asp:Repeater ID="rptAds" runat="server" OnItemDataBound="rptAds_ItemDataBound" >
<ItemTemplate>
<div id="aditem">
<a href='<%=InfoPage%>?adId=<%# DataBinder.Eval(Container.DataItem, "Id") %>'><%# DataBinder.Eval(Container.DataItem, "Heading") %></a><br />
<i><%# ((DateTime) DataBinder.Eval(Container.DataItem, "StartDate")).ToShortDateString()%></i><br />
<%# DataBinder.Eval(Container.DataItem, "Description") %><br />
<asp:Label ID="lblMember" runat="server" Visible="true"></asp:Label><br />
Telefon:&nbsp;<asp:Label ID="lblMemberPhone" runat="server" Visible="true"></asp:Label><br />
Fax:&nbsp;<asp:Label ID="lblMemberFax" runat="server" Visible="true"></asp:Label><br />
<asp:HyperLink ID="hlMemberEmail" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "MemberId") %>'></asp:HyperLink><br />
<br /><br />
</div>
</ItemTemplate>
</asp:Repeater>