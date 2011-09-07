<%@ Page Language="C#"  MasterPageFile="~/Default.master" AutoEventWireup="true" Title="Intranät" %>
<asp:Content ID="cnt1" ContentPlaceHolderID="Content" Runat="Server">
<WpcSynologen:YammerFeed ID="testYammerFeed" runat="server" NumberOfMessages="10" ExcludeJoins="true" Threaded="true" NewerThan="111864279" />
</asp:Content>
