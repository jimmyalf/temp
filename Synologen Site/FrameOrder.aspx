<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<%@ Register TagPrefix="Synologen" TagName="MVPTest" Src="~/Wpc/Synologen/FrameOrder.ascx" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<asp:Literal ID="ltPageId" Text="190" Visible="false" runat="server"/>
<Synologen:MVPTest ID="synologenMvpTestControl" runat="server" />
</asp:Content>