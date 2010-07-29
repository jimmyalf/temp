<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<div style="float:left;margin:15px;">
		<h2>Utbetalning</h2>	
		<WpcSynologen:ViewSettlement ID="ucViewSettlement" runat="server" />
	</div>
</asp:Content>
