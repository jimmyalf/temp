<%@ Page Language="C#" MasterPageFile="~/baseMain.master" AutoEventWireup="true" Inherits="Spinit.Wpc.Base.Presentation.NoAccess" Codebehind="NoAccess.aspx.cs" %>
<asp:Content ContentPlaceHolderID="ComponentContent" ID="NoAccessCnt" Runat="Server">
<div id="dCompMain">
	<div class="fullBox">
	<div class="wrap">
		<h1>No access to page</h1>
		<p><asp:Literal id="ltErrorPage" runat="server"></asp:Literal></p>
    </div>
    </div>
</div>
</asp:Content>