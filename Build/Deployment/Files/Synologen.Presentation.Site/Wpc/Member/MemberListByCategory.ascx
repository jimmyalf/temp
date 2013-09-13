<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberListByCategory.ascx.cs" Inherits="Spinit.Wpc.Member.Presentation.Site.MemberListByCategory" %>
<%@ Register Src="MemberList.ascx" TagName="MemberList" TagPrefix="uc1" %>
<asp:Repeater ID="rptCategories" runat="server" OnItemDataBound="rptCategories_ItemDataBound">
<ItemTemplate>
<h2><%# DataBinder.Eval(Container.DataItem, "Name") %></h2>
<uc1:MemberList ID="MemberList1"  runat="server" />
</ItemTemplate>
</asp:Repeater>


