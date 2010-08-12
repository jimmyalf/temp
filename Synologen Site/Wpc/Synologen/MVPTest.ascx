<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MVPTest.ascx.cs" Inherits="Spinit.Wpc.Synologen.Presentation.Site.Wpc.Synologen.MVPTest" %>
<h1><%# Model.Message %></h1>

<asp:DropDownList 
	ID="drpFrameListABC" 
	OnSelectedIndexChanged="drpFrameList_OnSelectedIndexChanged" 
	runat="server" 
	DataSource="<%#Model.List %>">
</asp:DropDownList>